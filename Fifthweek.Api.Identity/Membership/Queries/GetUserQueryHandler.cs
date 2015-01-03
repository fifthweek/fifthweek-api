namespace Fifthweek.Api.Identity.Membership.Queries
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;

    public class GetUserQueryHandler : IQueryHandler<GetUserQuery, FifthweekUser>
    {
        private readonly IUserManager userManager;

        public GetUserQueryHandler(IUserManager userManager)
        {
            this.userManager = userManager;
        }

        public Task<FifthweekUser> HandleAsync(GetUserQuery query)
        {
            return this.userManager.FindAsync(query.Username.Value, query.Password.Value);
        }
    }
}