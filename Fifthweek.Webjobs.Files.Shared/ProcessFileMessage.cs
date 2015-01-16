namespace Fifthweek.Webjobs.Files.Shared
{
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    [AutoEqualityMembers]
    public partial class ProcessFileMessage
    {
        public ProcessFileMessage()
        {
        }

        public string ContainerName { get; set; }
        
        public string BlobName { get; set; }

        public string Purpose { get; set; }

        public bool Overwrite { get; set; }
    }
}