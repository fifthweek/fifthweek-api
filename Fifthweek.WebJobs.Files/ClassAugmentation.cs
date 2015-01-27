using System;
using System.Linq;




namespace Fifthweek.WebJobs.Files
{
    using System;
    using System.Linq;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;
    using Fifthweek.Azure;
    using Fifthweek.CodeGeneration;
    using Fifthweek.WebJobs.Files.Shared;
    using Fifthweek.WebJobs.Shared;
    using Microsoft.Azure.WebJobs;
    public partial class FileProcessor 
    {
        public FileProcessor(
            Fifthweek.WebJobs.Files.IFilePurposeTasks filePurposeTasks, 
            Fifthweek.WebJobs.Files.ICloudQueueResolver cloudQueueResolver)
        {
            if (filePurposeTasks == null)
            {
                throw new ArgumentNullException("filePurposeTasks");
            }

            if (cloudQueueResolver == null)
            {
                throw new ArgumentNullException("cloudQueueResolver");
            }

            this.filePurposeTasks = filePurposeTasks;
            this.cloudQueueResolver = cloudQueueResolver;
        }
    }

}


