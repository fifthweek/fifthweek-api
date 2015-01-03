namespace Fifthweek.Api.Identity.Membership.Queries
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence;

    public class IsPasswordResetTokenValidQueryHandler : IQueryHandler<IsPasswordResetTokenValidQuery, bool>
    {
        private readonly IUserManager userManager;

        public IsPasswordResetTokenValidQueryHandler(IUserManager userManager)
        {
            if (userManager == null)
            {
                throw new ArgumentNullException("userManager");
            }

            this.userManager = userManager;
        }

        public async Task<bool> HandleAsync(IsPasswordResetTokenValidQuery query)
        {
            var user = await this.userManager.FindByIdAsync(query.UserId.Value);
            if (user == null)
            {
                return false;
            }

            return await this.userManager.ValidatePasswordResetTokenAsync(user, query.Token);
        }
    }
}