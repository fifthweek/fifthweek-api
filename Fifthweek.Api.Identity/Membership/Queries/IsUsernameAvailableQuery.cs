namespace Fifthweek.Api.Identity.Membership.Queries
{
    using Fifthweek.Api.Core;

    [AutoEqualityMembers, AutoConstructor]
    public partial class IsUsernameAvailableQuery : IQuery<bool>
    {
        public NormalizedUsername Username { get; private set; }
    }
}