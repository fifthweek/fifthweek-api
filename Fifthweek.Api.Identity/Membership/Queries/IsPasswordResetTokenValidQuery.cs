using System;

namespace Fifthweek.Api.Identity.Membership.Queries
{
    using Fifthweek.Api.Core;

    [AutoEqualityMembers]
    public partial class IsPasswordResetTokenValidQuery : IQuery<bool>
    {
        public IsPasswordResetTokenValidQuery(UserId userId, string token)
        {
            if (userId == null)
            {
                throw new ArgumentNullException("userId");
            }

            if (token == null)
            {
                throw new ArgumentNullException("token");
            }

            this.UserId = userId;
            this.Token = token;
        }

        public UserId UserId { get; private set; }

        public string Token { get; private set; }
    }
}