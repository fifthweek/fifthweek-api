namespace Fifthweek.Api.Subscriptions
{
    using System;

    using Fifthweek.Api.Core;

    [AutoEqualityMembers, AutoConstructor]
    public partial class ChannelId
    {
        public Guid Value { get; private set; }
    }
}