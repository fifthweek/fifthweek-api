namespace Fifthweek.Api.Identity.OAuth.Queries
{
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.CodeGeneration;

    [AutoEqualityMembers, AutoConstructor]
    public partial class GetUserClaimsIdentityQuery : IQuery<UserClaimsIdentity>
    {
        [Optional]
        public UserId UserId { get; private set; }

        [Optional]
        public Username Username { get; private set; }

        [Optional]
        public Password Password { get; private set; }

        public string AuthenticationType { get; private set; }
    }
}