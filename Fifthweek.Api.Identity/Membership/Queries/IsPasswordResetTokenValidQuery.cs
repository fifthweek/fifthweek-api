namespace Fifthweek.Api.Identity.Membership.Queries
{
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoEqualityMembers, AutoConstructor]
    public partial class IsPasswordResetTokenValidQuery : IQuery<bool>
    {
        public UserId UserId { get; private set; }

        public string Token { get; private set; }
    }
}