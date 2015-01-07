namespace Fifthweek.Api.Identity.Membership.Queries
{
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence.Identity;

    [AutoEqualityMembers, AutoConstructor]
    public partial class GetUserQuery : IQuery<FifthweekUser>
    {
        public NormalizedUsername Username { get; private set; }

        public Password Password { get; private set; }
    }
}