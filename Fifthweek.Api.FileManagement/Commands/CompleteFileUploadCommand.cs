namespace Fifthweek.Api.FileManagement.Commands
{
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;

    [AutoConstructor]
    [AutoEqualityMembers]
    public partial class CompleteFileUploadCommand
    {
        public Requester Requester { get; private set; }

        [Optional]
        public ChannelId ChannelId { get; private set; }

        public FileId FileId { get; private set; }
    }
}