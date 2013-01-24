using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace PoorMansPingdomWorkerRole
{
    public class PingResult : TableEntity
    {
        /// <summary>
        /// Gets or sets the URL pinged.
        /// </summary>
        public string Url
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the HTTP Status code.
        /// </summary>
        public string StatusCode
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the time taken to process the ping in milliseconds.
        /// </summary>
        public double TimeTaken
        {
            get;
            set;
        }

        public long ContentLength
        {
            get;
            set;
        }
    }
}
