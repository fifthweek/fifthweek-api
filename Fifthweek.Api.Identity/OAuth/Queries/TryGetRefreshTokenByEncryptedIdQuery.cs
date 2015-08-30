namespace Fifthweek.Api.Identity.OAuth.Queries
{
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;

    [AutoEqualityMembers, AutoConstructor]
    public partial class TryGetRefreshTokenByEncryptedIdQuery : IQuery<RefreshToken>
    {
        public EncryptedRefreshTokenId EncryptedId { get; private set; }
    }
}