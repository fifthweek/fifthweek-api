namespace Fifthweek.WebJobs.Files
{
    using System;

    public partial class FileProcessor 
    {
        public FileProcessor(
            IFilePurposeToTasksMappings filePurposeToTasksMappings, 
            ICloudQueueResolver cloudQueueResolver)
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



