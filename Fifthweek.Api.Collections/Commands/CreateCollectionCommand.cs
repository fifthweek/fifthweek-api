namespace Fifthweek.Api.Collections.Commands
{
    using Fifthweek.Api.Channels;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.CodeGeneration;

    using ChannelId = Fifthweek.Api.Channels.Shared.ChannelId;

    [AutoConstructor, AutoEqualityMembers]
    public partial class CreateCollectionCommand
    {
        public Requester Requester { get; private set; }

        public CollectionId NewCollectionId { get; private set; }

        public ChannelId ChannelId { get; private set; }

        public ValidCollectionName Name { get; private set; }
    }
}