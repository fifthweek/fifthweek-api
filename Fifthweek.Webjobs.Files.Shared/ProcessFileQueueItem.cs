namespace Fifthweek.Webjobs.Files.Shared
{
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    [AutoEqualityMembers]
    public partial class ProcessFileQueueItem
    {
        public string Purpose { get; set; }

        public string BlobLocation { get; set; }

        public bool Overwrite { get; set; }
    }
}