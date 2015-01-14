namespace Fifthweek.Api.Subscriptions.Commands
{
    using System;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;

    [AutoConstructor, AutoEqualityMembers]
    public partial class CreateNoteCommand
    {
        public UserId Requester { get; private set; }

        public SubscriptionId SubscriptionId { get; private set; }

        [Optional]
        public ChannelId ChannelId { get; private set; }

        public ValidNote Note { get; private set; }

        [Optional]
        public DateTime? ScheduledPostDate { get; private set; }
    }
}