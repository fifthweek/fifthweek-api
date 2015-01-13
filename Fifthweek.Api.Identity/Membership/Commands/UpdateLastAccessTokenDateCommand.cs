using System;
using Fifthweek.Api.Core;

namespace Fifthweek.Api.Identity.Membership.Commands
{
    [AutoEqualityMembers, AutoConstructor]
    public partial class UpdateLastAccessTokenDateCommand
    {
        public enum AccessTokenCreationType
        {
            SignIn,
            RefreshToken,
        }

        public ValidUsername Username { get; private set; }

        public DateTime Timestamp { get; private set; }

        public AccessTokenCreationType CreationType { get; private set; }
    }
}