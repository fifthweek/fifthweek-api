namespace Fifthweek.Api.Identity.Membership.Queries
{
    using Fifthweek.Api.Core;

    [AutoEqualityMembers, AutoConstructor]
    public partial class IsPasswordResetTokenValidQuery : IQuery<bool>
    {
        public UserId UserId { get; private set; }

        public string Token { get; private set; }
    }
}