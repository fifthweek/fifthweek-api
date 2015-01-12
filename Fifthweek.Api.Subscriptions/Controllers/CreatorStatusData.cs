using System;
using Fifthweek.Api.Core;

namespace Fifthweek.Api.Subscriptions.Controllers
{
    [AutoConstructor, AutoEqualityMembers]
    public partial class CreatorStatusData
    {
        public Guid? SubscriptionId { get; set; }

        public bool MustWriteFirstPost { get; set; }
    }
}