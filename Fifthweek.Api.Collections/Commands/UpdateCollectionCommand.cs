namespace Fifthweek.Api.Collections.Commands
{
    using System.Collections.Generic;

    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers]
    public partial class UpdateCollectionCommand
    {
        public Requester Requester { get; private set; }

        public CollectionId CollectionId { get; private set; }

        public ChannelId ChannelId { get; private set; }

        public ValidCollectionName Name { get; private set; }

        public IReadOnlyList<HourOfWeek> WeeklyReleaseTimes { get; private set; } 
    }
}