namespace Fifthweek.Api.Identity.OAuth.Queries
{
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoEqualityMembers, AutoConstructor]
    public partial class GetRefreshTokenQuery : IQuery<RefreshToken>
    {
        public HashedRefreshTokenId HashedRefreshTokenId { get; private set; }
    }
}