namespace Fifthweek.Api.Subscriptions
{
    using System;

    using Fifthweek.CodeGeneration;

    [AutoPrimitive]
    public partial class SubscriptionId
    {
        public Guid Value { get; private set; }
    }
}