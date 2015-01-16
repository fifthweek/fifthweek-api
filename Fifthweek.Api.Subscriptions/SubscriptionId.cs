namespace Fifthweek.Api.Subscriptions
{
    using System;

    using Fifthweek.Api.Core;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoEqualityMembers, AutoConstructor]
    public partial class SubscriptionId
    {
        public Guid Value { get; private set; }
    }
}