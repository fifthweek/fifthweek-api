namespace Fifthweek.GarbageCollection
{
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers]
    public partial class OrphanedFileData
    {
        public FileId FileId { get; private set; }

        [Optional]
        public ChannelId ChannelId { get; private set; }

        public string Purpose { get; private set; }
    }
}