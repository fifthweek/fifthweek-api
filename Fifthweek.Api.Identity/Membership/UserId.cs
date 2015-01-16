using System;
using Fifthweek.Api.Core;

namespace Fifthweek.Api.Identity.Membership
{
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoEqualityMembers, AutoConstructor]
    public partial class UserId
    {
        public Guid Value { get; private set; }
    }
}