namespace Fifthweek.Api.Identity.OAuth.Queries
{
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence;

    [AutoConstructor]
    [AutoEqualityMembers]
    public partial class GetRefreshTokenQuery : IQuery<RefreshToken>
    {
        public HashedRefreshTokenId HashedRefreshTokenId { get; private set; }
    }
}