namespace Fifthweek.Api.Identity.OAuth.Queries
{
    using System.Security.Claims;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers]
    public partial class UserClaimsIdentity
    {
        public UserId UserId { get; private set; }

        public Username Username { get; private set; }

        public ClaimsIdentity ClaimsIdentity { get; private set; }
    }
}