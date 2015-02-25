namespace Fifthweek.Api.Subscriptions.Queries
{
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Subscriptions.Shared;
    using Fifthweek.CodeGeneration;

    [AutoEqualityMembers, AutoConstructor]
    public partial class GetSubscriptionQuery : IQuery<GetSubscriptionResult>
    {
        public SubscriptionId NewSubscriptionId { get; private set; }
    }
}