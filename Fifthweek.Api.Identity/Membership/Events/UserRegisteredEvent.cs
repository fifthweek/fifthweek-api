using Fifthweek.Api.Core;

namespace Fifthweek.Api.Identity.Membership.Events
{
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor, AutoEqualityMembers]
    public partial class UserRegisteredEvent
    {
        public UserId UserId { get; private set; }
    }
}