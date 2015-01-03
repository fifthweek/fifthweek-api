namespace Fifthweek.Api.Identity.Membership.Queries
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;

    public class IsUsernameAvailableQueryHandler : IQueryHandler<IsUsernameAvailableQuery, bool>
    {
        private readonly IUserManager userManager;

        public IsUsernameAvailableQueryHandler(IUserManager userManager)
        {
            if (userManager == null)
            {
                throw new ArgumentNullException("userManager");
            }

            this.userManager = userManager;
        }

        public async Task<bool> HandleAsync(IsUsernameAvailableQuery query)
        {
            var user = await this.userManager.FindByNameAsync(query.Username.Value);
            return user == null;
        }
    }
}