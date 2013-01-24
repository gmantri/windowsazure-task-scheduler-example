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
using Quartz.Impl;

namespace PoorMansPingdomWorkerRole
{
    public class WorkerRole : RoleEntryPoint
    {
        public override void Run()
        {
            // This is a sample worker implementation. Replace with your logic.
            Trace.WriteLine("PoorMansPingdomWorkerRole entry point called", "Information");
            // Call the initialization routine.
            Init();
            // Call the job scheduling routine.
            ScheduleJob();
            while (true)
            {
                Thread.Sleep(10000);
                //Trace.WriteLine("Working", "Information");
            }
        }

        public override bool OnStart()
        {
            // Set the maximum number of concurrent connections 
            ServicePointManager.DefaultConnectionLimit = 12;

            // For information on handling configuration changes
            // see the MSDN topic at http://go.microsoft.com/fwlink/?LinkId=166357.

            return base.OnStart();
        }

        private void Init()
        {
            // Get the cloud storage account.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(RoleEnvironment.GetConfigurationSettingValue("StorageAccount"));
            // Get the name of the blob container
            string blobContainerName = RoleEnvironment.GetConfigurationSettingValue("BlobContainer");
            CloudBlobContainer blobContainer = storageAccount.CreateCloudBlobClient().GetContainerReference(blobContainerName);
            // Create the blob container.
            blobContainer.CreateIfNotExists();
            // Get the blob name
            string blobName = RoleEnvironment.GetConfigurationSettingValue("BlobToBeLeased");
            CloudBlockBlob blob = blobContainer.GetBlockBlobReference(blobName);
            // Write some dummy data in the blob.
            string blobContent = "This is dummy data";
            // Upload blob
            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(blobContent)))
            {
                blob.UploadFromStream(ms);
            }
            // Get the table name for storing results.
            string tableName = RoleEnvironment.GetConfigurationSettingValue("PingResultsTableName");
            // Create the table.
            CloudTable table = storageAccount.CreateCloudTableClient().GetTableReference(tableName);
            table.CreateIfNotExists();
            // Get the queue name where ping items will be stored.
            string queueName = RoleEnvironment.GetConfigurationSettingValue("ProcessQueueName");
            // Create the queue.
            CloudQueue queue = storageAccount.CreateCloudQueueClient().GetQueueReference(queueName);
            queue.CreateIfNotExists();
        }

        private void ScheduleJob()
        {
            DateTimeOffset runTime = DateBuilder.EvenMinuteDate(DateTime.Now);

            // construct a scheduler factory
            ISchedulerFactory schedFact = new StdSchedulerFactory();

            // get a scheduler
            IScheduler sched = schedFact.GetScheduler();
            sched.Start();

            JobDataMap jobDataMap = new JobDataMap();

            IJobDetail websitePingJobDetail = JobBuilder.Create<PingJob>()
                    .WithIdentity("WebsitePingJob", "group1")
                    .WithDescription("Website Ping Job")
                    .UsingJobData(jobDataMap)
                    .Build();

            ITrigger websitePingJobTrigger = TriggerBuilder.Create()
                .WithIdentity("WebsitePingJob", "group1")
                .StartAt(runTime)
                .WithCronSchedule(RoleEnvironment.GetConfigurationSettingValue("PingJobCronSchedule"))
                .StartNow()
                .Build();

            sched.ScheduleJob(websitePingJobDetail, websitePingJobTrigger);
        }
    }
}
