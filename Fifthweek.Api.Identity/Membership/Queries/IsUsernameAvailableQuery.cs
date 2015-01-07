using System;

namespace Fifthweek.Api.Identity.Membership.Queries
{
    using Fifthweek.Api.Core;

    [AutoEqualityMembers]
    public partial class IsUsernameAvailableQuery : IQuery<bool>
    {
        public IsUsernameAvailableQuery(NormalizedUsername username)
        {
            if (username == null)
            {
                throw new ArgumentNullException("username");
            }

            this.Username = username;
        }

        public NormalizedUsername Username { get; private set; }
    }
}