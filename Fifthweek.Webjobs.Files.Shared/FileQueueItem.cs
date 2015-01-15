namespace Fifthweek.Webjobs.Files.Shared
{
    public class FileQueueItem
    {
        public string FileExtension { get; set; }

        public string Purpose { get; set; }

        public string BlobLocation { get; set; }
    }
}