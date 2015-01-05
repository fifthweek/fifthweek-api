namespace Fifthweek.Api.Identity.Membership.Queries
{
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;

    public class GetUserQuery : IQuery<FifthweekUser>
    {
        public GetUserQuery(NormalizedUsername username, Password password)
        {
            this.Username = username;
            this.Password = password;
        }

        public NormalizedUsername Username { get; private set; }

        public Password Password { get; private set; }
    }
}