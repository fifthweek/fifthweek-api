namespace Fifthweek.Api.Identity.Membership.Queries
{
    using Fifthweek.Api.Core;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoEqualityMembers, AutoConstructor]
    public partial class IsUsernameAvailableQuery : IQuery<bool>
    {
        public ValidUsername Username { get; private set; }
    }
}