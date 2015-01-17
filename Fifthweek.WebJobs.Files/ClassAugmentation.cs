namespace Fifthweek.WebJobs.Files
{
    using System;

    public partial class FileProcessor 
    {
        public FileProcessor(
            IFilePurposeTasks filePurposeTasks, 
            ICloudQueueResolver cloudQueueResolver)
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



