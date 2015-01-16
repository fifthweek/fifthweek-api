namespace Fifthweek.Api.Subscriptions.Controllers
{
    using Fifthweek.Api.Core;
    using Fifthweek.Shared;

    [AutoConstructor, AutoEqualityMembers]
    public partial class CreatorStatusData
    {
        [Optional]
        public string SubscriptionId { get; set; }

        public bool MustWriteFirstPost { get; set; }
    }
}