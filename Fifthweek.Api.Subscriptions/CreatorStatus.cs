using Fifthweek.Api.Core;

namespace Fifthweek.Api.Subscriptions
{
    [AutoConstructor, AutoEqualityMembers]
    public partial class CreatorStatus
    {
        [Optional]
        public SubscriptionId SubscriptionId { get; private set; }

        public bool MustWriteFirstPost { get; private set; }
    }
}