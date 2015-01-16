
namespace Fifthweek.Api.Subscriptions
{
    using System;

    using Fifthweek.Api.Core;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoEqualityMembers, AutoConstructor]
    public partial class PostId
    {
        public Guid Value { get; private set; }
    }
}