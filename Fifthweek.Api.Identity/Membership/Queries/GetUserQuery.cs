namespace Fifthweek.Api.Identity.Membership.Queries
{
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence;

    public class GetUserQuery : IQuery<ApplicationUser>
    {
        public GetUserQuery(NormalizedUsername username, string password)
        {
            this.Username = username;
            this.Password = password;
        }

        public NormalizedUsername Username { get; private set; }

        public string Password { get; private set; }
    }
}