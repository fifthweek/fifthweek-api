using Fifthweek.Api.Entities;

namespace Fifthweek.Api.QueryHandlers
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Queries;
    using Fifthweek.Api.Repositories;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class GetUserQueryHandler : IQueryHandler<GetUserQuery, ApplicationUser>
    {
        private readonly IUserManager userManager;

        public GetUserQueryHandler(IUserManager userManager)
        {
            this.userManager = userManager;
        }

        public Task<ApplicationUser> HandleAsync(GetUserQuery query)
        {
            return this.userManager.FindAsync(query.Username, query.Password);
        }
    }
}