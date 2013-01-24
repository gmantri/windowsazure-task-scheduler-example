using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace PoorMansPingdomWorkerRole
{
    public class PingItem : TableEntity
    {
        public PingItem()
        {
            PartitionKey = "PingItem";
            RowKey = Guid.NewGuid().ToString();
        }

        /// <summary>
        /// Gets or sets the URL to be pinged.
        /// </summary>
        public string Url
        {
            get;
            set;
        }

        public override string ToString()
        {
            return this.RowKey + "|" + Url;
        }

        public static PingItem ParseFromString(string s)
        {
            string[] splitter = {"|"};
            string[] rowKeyAndUrl = s.Split(splitter, StringSplitOptions.RemoveEmptyEntries);
            return new PingItem()
            {
                PartitionKey = "PingItem",
                RowKey = rowKeyAndUrl[0],
                Url = rowKeyAndUrl[1],
            };
        }
    }
}
