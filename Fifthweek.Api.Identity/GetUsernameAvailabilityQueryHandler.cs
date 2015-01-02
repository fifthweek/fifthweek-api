namespace Fifthweek.Api.Identity
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;

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