using Fifthweek.Api.Core;

namespace Fifthweek.Api.Identity.OAuth.Commands
{
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoEqualityMembers, AutoConstructor]
    public partial class RemoveRefreshTokenCommand
    {
        public HashedRefreshTokenId HashedRefreshTokenId { get; private set; }
    }
}