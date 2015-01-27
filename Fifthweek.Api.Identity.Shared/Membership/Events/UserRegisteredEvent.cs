namespace Fifthweek.Api.Identity.Shared.Membership.Events
{
    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers]
    public partial class UserRegisteredEvent
    {
        public UserId UserId { get; private set; }
    }
}