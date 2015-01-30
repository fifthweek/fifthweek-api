namespace Fifthweek.Api.Identity.OAuth
{
    using System.Collections.Generic;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers]
    public partial class UsernameAndRoles
    {
        public Username Username { get; private set; }

        public IReadOnlyList<string> Roles { get; private set; }
    }
}