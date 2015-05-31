namespace Fifthweek.Api.FileManagement.Queries
{
    using Fifthweek.Api.Azure;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;

    [AutoConstructor]
    [AutoEqualityMembers]
    public partial class GenerateWritableBlobUriQuery : IQuery<BlobSharedAccessInformation>
    {
        public Requester Requester { get; private set; }

        [Optional]
        public ChannelId ChannelId { get; private set; }

        public FileId FileId { get; private set; }

        public string Purpose { get; private set; }
    }
}