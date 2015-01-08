using System;
using Fifthweek.Api.Core;

namespace Fifthweek.Api.Subscriptions
{
    [AutoEqualityMembers, AutoConstructor]
    public partial class SubscriptionId
    {
        public Guid Value { get; private set; }
    }
}