namespace Dexter.Api.Queries
{
    using Microsoft.AspNet.Identity.EntityFramework;

    public class GetUserQuery : IQuery<IdentityUser>
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