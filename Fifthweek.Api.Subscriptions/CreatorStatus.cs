using Fifthweek.Api.Core;

namespace Fifthweek.Api.Subscriptions
{
    [AutoConstructor, AutoEqualityMembers]
    public partial class CreatorStatus
    {
        public static readonly CreatorStatus NoSubscriptions = new CreatorStatus(null, false);

        [Optional]
        public SubscriptionId SubscriptionId { get; private set; }

        public bool MustWriteFirstPost { get; private set; }
    }
}