namespace Fifthweek.Api.Identity.OAuth
{
    using System.Collections.Generic;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers]
    public partial class UserIdAndRoles
    {
        public UserId UserId { get; private set; }

        public IReadOnlyList<string> Roles { get; private set; }
    }
}