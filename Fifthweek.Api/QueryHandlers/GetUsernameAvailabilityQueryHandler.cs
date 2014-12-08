using System;
using System.Threading.Tasks;
using Fifthweek.Api.Queries;
using Fifthweek.Api.Repositories;

namespace Fifthweek.Api.QueryHandlers
{
    public class GetUsernameAvailabilityQueryHandler : IQueryHandler<GetUsernameAvailabilityQuery, bool>
    {
        private readonly IUserManager userManager;

        public GetUsernameAvailabilityQueryHandler(IUserManager userManager)
        {
            if (userManager == null)
            {
                throw new ArgumentNullException("userManager");
            }

            this.userManager = userManager;
        }

        public async Task<bool> HandleAsync(GetUsernameAvailabilityQuery query)
        {
            var user = await this.userManager.FindByNameAsync(query.Username);
            return user == null;
        }
    }
}