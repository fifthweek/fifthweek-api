namespace Fifthweek.Api.Channels.Commands
{
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Subscriptions;
    using Fifthweek.Api.Subscriptions.Shared;
    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers]
    public partial class CreateChannelCommand
    {
        public Requester Requester { get; private set; }

        public ChannelId NewChannelId { get; private set; }

        public SubscriptionId SubscriptionId { get; private set; }

        public ValidChannelName Name { get; private set; }
    }
}