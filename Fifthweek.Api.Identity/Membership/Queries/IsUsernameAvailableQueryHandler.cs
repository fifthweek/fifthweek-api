namespace Fifthweek.Api.Identity.Membership.Queries
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.CodeGeneration;

    [AutoConstructor]
    public partial class IsUsernameAvailableQueryHandler : IQueryHandler<IsUsernameAvailableQuery, bool>
    {
        private readonly IUserManager userManager;
        private readonly IReservedUsernameService reservedUsernames;

        public async Task<bool> HandleAsync(IsUsernameAvailableQuery query)
        {
            if (this.reservedUsernames.IsReserved(query.Username))
            {
                return false;
            }

            var user = await this.userManager.FindByNameAsync(query.Username.Value);
            return user == null;
        }
    }
}