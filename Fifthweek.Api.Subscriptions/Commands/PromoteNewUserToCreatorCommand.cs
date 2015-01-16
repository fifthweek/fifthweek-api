using Fifthweek.Api.Core;
using Fifthweek.Api.Identity.Membership;

namespace Fifthweek.Api.Subscriptions.Commands
{
    using Fifthweek.Shared;

    [AutoConstructor, AutoEqualityMembers]
    public partial class PromoteNewUserToCreatorCommand
    {
        public UserId NewUserId { get; private set; }
    }
}