namespace Fifthweek.Api.Subscriptions.Shared
{
    using System;

    using Fifthweek.CodeGeneration;

    [AutoPrimitive]
    public partial class SubscriptionId
    {
        public Guid Value { get; private set; }
    }
}