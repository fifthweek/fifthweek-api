namespace Fifthweek.Api.Identity.OAuth.Commands
{
    using System;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;

    [AutoEqualityMembers, AutoConstructor]
    public partial class UpdateLastAccessTokenDateCommand
    {
        public enum AccessTokenCreationType
        {
            SignIn,
            RefreshToken,
        }

        public UserId UserId { get; private set; }

        public DateTime Timestamp { get; private set; }

        public AccessTokenCreationType CreationType { get; private set; }
    }
}