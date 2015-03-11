namespace Fifthweek.Api.Collections.Commands
{
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers]
    public partial class CreateCollectionCommand
    {
        public Requester Requester { get; private set; }

        public CollectionId NewCollectionId { get; private set; }

        public ChannelId ChannelId { get; private set; }

        public ValidCollectionName Name { get; private set; }

        public HourOfWeek InitialWeeklyReleaseTime { get; private set; }
    }
}