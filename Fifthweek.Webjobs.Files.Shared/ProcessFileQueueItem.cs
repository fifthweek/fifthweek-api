namespace Fifthweek.Webjobs.Files.Shared
{
    public class ProcessFileQueueItem
    {
        public string Purpose { get; set; }

        public string BlobLocation { get; set; }

        public bool Overwrite { get; set; }
    }
}