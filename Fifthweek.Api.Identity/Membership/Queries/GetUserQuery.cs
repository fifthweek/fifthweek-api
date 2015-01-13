namespace Fifthweek.Api.Identity.Membership.Queries
{
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence.Identity;

    [AutoEqualityMembers, AutoConstructor]
    public partial class GetUserQuery : IQuery<FifthweekUser>
    {
        public ValidUsername Username { get; private set; }

        public ValidPassword Password { get; private set; }
    }
}