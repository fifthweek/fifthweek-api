namespace Fifthweek.Api.Identity
{
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence;

    public class GetUserQuery : IQuery<ApplicationUser>
    {
        public GetUserQuery(string username, string password)
        {
            this.Username = username;
            this.Password = password;
        }

        public string Username { get; private set; }

        public string Password { get; private set; }
    }
}