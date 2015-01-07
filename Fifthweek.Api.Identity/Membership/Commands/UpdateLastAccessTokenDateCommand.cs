namespace Fifthweek.Api.Identity.Membership.Commands
{
    using System;

    [AutoConstructor]
    [AutoEqualityMembers]
    public partial class UpdateLastAccessTokenDateCommand
    {
        public enum AccessTokenCreationType
        {
            SignIn,
            RefreshToken,
        }

        public NormalizedUsername Username { get; private set; }

        public DateTime Timestamp { get; private set; }

        public AccessTokenCreationType CreationType { get; private set; }
    }
}