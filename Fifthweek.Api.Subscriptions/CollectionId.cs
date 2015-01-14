namespace Fifthweek.Api.Subscriptions
{
    using System;

    using Fifthweek.Api.Core;

    [AutoEqualityMembers, AutoConstructor]
    public partial class CollectionId
    {
        public Guid Value { get; private set; }
    }
}