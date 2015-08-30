namespace Fifthweek.Api.Identity.OAuth.Queries
{
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoEqualityMembers, AutoConstructor]
    public partial class TryGetRefreshTokenQuery : IQuery<RefreshToken>
    {
        public ClientId ClientId { get; private set; }

        public Username Username { get; private set; }
    }
}