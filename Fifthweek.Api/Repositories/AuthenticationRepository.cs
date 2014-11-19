namespace Fifthweek.Api.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Fifthweek.Api.Entities;
    using Fifthweek.Api.Models;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class AuthenticationRepository : IDisposable, IAuthenticationRepository
    {
        private readonly UserManager<IdentityUser> userManager;

        public AuthenticationRepository(IFifthweekDbContext fifthweekDbContext)
        {
            this.userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>((FifthweekDbContext)fifthweekDbContext));
        }

        public async Task AddInternalUserAsync(string username, string password)
        {
            var user = new IdentityUser
            {
                UserName = username
            };

            var result = await this.userManager.CreateAsync(user, password);
            
            ThrowIfFailed(result, "Failed to create internal user: " + user);
        }

        public async Task AddExternalUserAsync(string username, string provider, string providerKey)
        {
            var user = new IdentityUser() { UserName = username };
            
            var createResult = await this.userManager.CreateAsync(user);
            ThrowIfFailed(createResult, "Failed to create external user: " + user);

            var loginInfo = new UserLoginInfo(provider, providerKey);
            var updateResult = await this.userManager.AddLoginAsync(user.Id, loginInfo);
            ThrowIfFailed(updateResult, "Failed to add external login data for " + user);
        }

        public async Task<IdentityUser> FindInternalUserAsync(string username, string password)
        {
            return await this.userManager.FindAsync(username, password);
        }

        public async Task<IdentityUser> FindExternalUserAsync(string provider, string providerKey)
        {
            return await this.userManager.FindAsync(new UserLoginInfo(provider, providerKey));
        }

        public void Dispose()
        {
            this.userManager.Dispose();
        }

        private static void ThrowIfFailed(IdentityResult result, string errorMessage)
        {
            if (!result.Succeeded)
            {
                if (result.Errors == null)
                {
                    throw new Exception(errorMessage);
                }

                throw new AggregateException(errorMessage, result.Errors.Select(v => new Exception(v)));
            }
        }
    }
}