using Fifthweek.Api.Core;

namespace Fifthweek.Api.Identity.Membership.Events
{
    [AutoConstructor, AutoEqualityMembers]
    public partial class UserRegisteredEvent
    {
        public UserId UserId { get; private set; }
    }
}