using System;
using System.Linq;



namespace Fifthweek.Webjobs.Files
{
    using System;
    using System.Linq;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Webjobs.Files.Shared;
    using Fifthweek.Webjobs.Thumbnails.Shared;
    using Microsoft.Azure.WebJobs;
    using Fifthweek.Azure;
    using Microsoft.WindowsAzure.Storage.Queue;
    public partial class FileProcessor 
    {
        public FileProcessor(
            Fifthweek.Webjobs.Files.IFilePurposeToTasksMappings filePurposeToTasksMappings, 
            Fifthweek.Webjobs.Files.ICloudQueueResolver cloudQueueResolver)
        {
            if (filePurposeToTasksMappings == null)
            {
                throw new ArgumentNullException("filePurposeToTasksMappings");
            }

            if (cloudQueueResolver == null)
            {
                throw new ArgumentNullException("cloudQueueResolver");
            }

            this.filePurposeToTasksMappings = filePurposeToTasksMappings;
            this.cloudQueueResolver = cloudQueueResolver;
        }
    }

}



