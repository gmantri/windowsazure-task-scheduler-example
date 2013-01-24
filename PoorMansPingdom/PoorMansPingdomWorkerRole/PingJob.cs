using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using Microsoft.WindowsAzure.Diagnostics;
using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.WindowsAzure.Storage.Queue;
using Quartz;
using System.Threading.Tasks;

namespace PoorMansPingdomWorkerRole
{
    public class PingJob : IInterruptableJob
    {
        CloudStorageAccount storageAccount;
        CloudBlobContainer blobContainer;
        CloudBlockBlob blob;

        public void Execute(IJobExecutionContext context)
        {
            // Introduce a random delay between 100 and 200 ms to to avoid race condition.
            Thread.Sleep((new Random()).Next(100, 200));
            Trace.WriteLine(string.Format("[{0}] - Executing ping job. Role instance: {1}.", DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"), RoleEnvironment.CurrentRoleInstance.Id));
            Init();
            // Try and acquire the lease.
            if (AcquireLease())
            {
                Trace.WriteLine(string.Format("[{0}] - Lease acquired. Role instance: {1}.", DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"), RoleEnvironment.CurrentRoleInstance.Id));
                // If successful then read the data.
                var itemsToBeProcessed = ReadMasterData();
                //Now save this data as messages in process queue.
                SaveMessages(itemsToBeProcessed);
            }
            else
            {
                Trace.WriteLine(string.Format("[{0}] - Failed to acquire lease. Role instance: {1}.", DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"), RoleEnvironment.CurrentRoleInstance.Id));
                // Else just sleep for 15 seconds.
                Thread.Sleep(15 * 1000);
            }
            // Now we'll fetch 5 messages from top of queue
            var itemsToBeProcessedByThisInstance = FetchMessages(5);
            if (itemsToBeProcessedByThisInstance.Count > 0)
            {
                int numTasks = itemsToBeProcessedByThisInstance.Count;
                List<Task> tasks = new List<Task>();
                for (int i = 0; i < numTasks; i++)
                {
                    var pingItem = itemsToBeProcessedByThisInstance[i];
                    var task = Task.Factory.StartNew(() =>
                        {
                            var pingResult = FetchUrl(pingItem);
                            SaveResult(pingResult);
                        });
                    tasks.Add(task);
                }
                Task.WaitAll(tasks.ToArray());
            }
        }

        public void Interrupt()
        {
            throw new NotImplementedException();
        }

        private void Init()
        {
            storageAccount = CloudStorageAccount.Parse(RoleEnvironment.GetConfigurationSettingValue("StorageAccount"));
            string blobContainerName = RoleEnvironment.GetConfigurationSettingValue("BlobContainer");
            blobContainer = storageAccount.CreateCloudBlobClient().GetContainerReference(blobContainerName);
            string blobName = RoleEnvironment.GetConfigurationSettingValue("BlobToBeLeased");
            blob = blobContainer.GetBlockBlobReference(blobName);
        }

        private bool AcquireLease()
        {
            try
            {
                blob.AcquireLease(TimeSpan.FromSeconds(45), null);
                return true;
            }
            catch (Exception exception)
            {
                return false;
            }
        }

        private List<PingItem> ReadMasterData()
        {
            string pingItemTableName = RoleEnvironment.GetConfigurationSettingValue("PingItemsTableName");
            CloudTable table = storageAccount.CreateCloudTableClient().GetTableReference(pingItemTableName);
            TableQuery<PingItem> query = new TableQuery<PingItem>();
            var queryResult = table.ExecuteQuery<PingItem>(query);
            return queryResult.ToList();
        }

        private void SaveMessages(List<PingItem> pingItems)
        {
            string queueName = RoleEnvironment.GetConfigurationSettingValue("ProcessQueueName");
            CloudQueue queue = storageAccount.CreateCloudQueueClient().GetQueueReference(queueName);
            foreach (var pingItem in pingItems)
            {
                CloudQueueMessage msg = new CloudQueueMessage(pingItem.ToString());
                queue.AddMessage(msg, TimeSpan.FromSeconds(45));
            }
        }

        private List<PingItem> FetchMessages(int maximumMessagesToFetch)
        {
            string queueName = RoleEnvironment.GetConfigurationSettingValue("ProcessQueueName");
            CloudQueue queue = storageAccount.CreateCloudQueueClient().GetQueueReference(queueName);
            var messages = queue.GetMessages(maximumMessagesToFetch);
            List<PingItem> itemsToBeProcessed = new List<PingItem>();
            foreach (var message in messages)
            {
                itemsToBeProcessed.Add(PingItem.ParseFromString(message.AsString));
                queue.DeleteMessage(message);
            }
            return itemsToBeProcessed;
        }

        private PingResult FetchUrl(PingItem item)
        {
            DateTime startDateTime = DateTime.UtcNow;
            TimeSpan elapsedTime = TimeSpan.FromSeconds(0);
            string statusCode = "";
            long contentLength = 0;
            try
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(item.Url);
                req.Timeout = 30 * 1000;//Let's timeout the request in 30 seconds.
                req.Method = "GET";
                using (HttpWebResponse resp = (HttpWebResponse)req.GetResponse())
                {
                    DateTime endDateTime = DateTime.UtcNow;
                    elapsedTime = new TimeSpan(endDateTime.Ticks - startDateTime.Ticks);
                    statusCode = resp.StatusCode.ToString();
                    contentLength = resp.ContentLength;
                }
            }
            catch (WebException webEx)
            {
                DateTime endDateTime = DateTime.UtcNow;
                elapsedTime = new TimeSpan(endDateTime.Ticks - startDateTime.Ticks);
                statusCode = webEx.Status.ToString();
            }
            return new PingResult()
            {
                PartitionKey = DateTime.UtcNow.Ticks.ToString("d19"),
                RowKey = item.RowKey,
                Url = item.Url,
                StatusCode = statusCode,
                ContentLength = contentLength,
                TimeTaken = elapsedTime.TotalMilliseconds,
            };
        }

        private void SaveResult(PingResult result)
        {
            string tableName = RoleEnvironment.GetConfigurationSettingValue("PingResultsTableName");
            CloudTable table = storageAccount.CreateCloudTableClient().GetTableReference(tableName);
            TableOperation addOperation = TableOperation.Insert(result);
            table.Execute(addOperation);
        }
    }
}
