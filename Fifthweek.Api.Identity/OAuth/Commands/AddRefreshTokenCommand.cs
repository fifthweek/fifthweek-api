using Fifthweek.Api.Core;

namespace Fifthweek.Api.Identity.OAuth.Commands
{
    using Fifthweek.Api.Persistence;

    [AutoEqualityMembers, AutoConstructor]
    public partial class AddRefreshTokenCommand
    {
        public RefreshToken RefreshToken { get; private set; }
    }
}