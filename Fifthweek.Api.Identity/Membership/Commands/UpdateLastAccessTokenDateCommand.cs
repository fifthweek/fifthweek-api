namespace Fifthweek.Api.Identity.Membership.Commands
{
    using System;

    public class UpdateLastAccessTokenDateCommand
    {
        public UpdateLastAccessTokenDateCommand(
            string username, 
            DateTime timestamp,
            AccessTokenCreationType creationType)
        {
            this.Username = username;
            this.Timestamp = timestamp;
            this.CreationType = creationType;
        }

        public enum AccessTokenCreationType
        {
            SignIn,
            RefreshToken,
        }

        public string Username { get; private set; }

        public DateTime Timestamp { get; private set; }

        public AccessTokenCreationType CreationType { get; private set; }
    }
}