namespace Fifthweek.Api.Subscriptions
{
    using System;

    using Fifthweek.Api.Core;
    using Fifthweek.Shared;

    [AutoEqualityMembers, AutoConstructor]
    public partial class ChannelId
    {
        public Guid Value { get; private set; }
    }
}