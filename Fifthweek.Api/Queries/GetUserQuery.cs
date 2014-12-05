using Fifthweek.Api.Entities;

namespace Fifthweek.Api.Queries
{
    using Microsoft.AspNet.Identity.EntityFramework;

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