using System;

namespace Fifthweek.Api.Identity.Membership.Queries
{
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence.Identity;

    [AutoEqualityMembers]
    public partial class GetUserQuery : IQuery<FifthweekUser>
    {
        public GetUserQuery(NormalizedUsername username, Password password)
        {
            if (username == null)
            {
                throw new ArgumentNullException("username");
            }

            if (password == null)
            {
                throw new ArgumentNullException("password");
            }

            this.Username = username;
            this.Password = password;
        }

        public NormalizedUsername Username { get; private set; }

        public Password Password { get; private set; }
    }
}