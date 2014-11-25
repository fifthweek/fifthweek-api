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

        public async Task AddInternalUserAsync(string email, string username, string password)
        {
            var user = new IdentityUser
            {
                UserName = username,
                Email = email,
            };

            var result = await this.userManager.CreateAsync(user, password);
            
            ThrowIfFailed(result, "Failed to create internal user: " + user);
        }

        public async Task<IdentityUser> FindInternalUserAsync(string username, string password)
        {
            return await this.userManager.FindAsync(username, password);
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