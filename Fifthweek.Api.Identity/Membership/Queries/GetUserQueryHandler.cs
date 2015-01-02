namespace Fifthweek.Api.Identity.Membership.Queries
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence;

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