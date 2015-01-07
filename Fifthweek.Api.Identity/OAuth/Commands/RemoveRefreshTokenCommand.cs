using Fifthweek.Api.Core;

namespace Fifthweek.Api.Identity.OAuth.Commands
{
    [AutoConstructor]
    [AutoEqualityMembers]
    public partial class RemoveRefreshTokenCommand
    {
        public HashedRefreshTokenId HashedRefreshTokenId { get; private set; }
    }
}