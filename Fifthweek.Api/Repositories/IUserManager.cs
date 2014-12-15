using System;
using System.Threading.Tasks;
using Fifthweek.Api.Entities;
using Microsoft.AspNet.Identity;

namespace Fifthweek.Api.Repositories
{
    public interface IUserManager : IDisposable
    {
        Task<IdentityResult> CreateAsync(ApplicationUser user, string password);

        Task<ApplicationUser> FindAsync(string userName, string password);

        Task<ApplicationUser> FindByEmailAsync(string email);

        Task<ApplicationUser> FindByNameAsync(string userName);

        Task<IdentityResult> UpdateAsync(ApplicationUser user);
    }

    public class UserManagerImpl : IUserManager
    {
        private readonly UserManager<ApplicationUser> userManager;

        public UserManagerImpl(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        } 

        public void Dispose()
        {
            this.userManager.Dispose();
        }

        public Task<IdentityResult> CreateAsync(ApplicationUser user, string password)
        {
            return this.userManager.CreateAsync(user, password);
        }

        public Task<ApplicationUser> FindAsync(string userName, string password)
        {
            return this.userManager.FindAsync(userName, password);
        }

        public Task<ApplicationUser> FindByEmailAsync(string email)
        {
            return this.userManager.FindByEmailAsync(email);
        }

        public Task<ApplicationUser> FindByNameAsync(string userName)
        {
            return this.userManager.FindByNameAsync(userName);
        }

        public Task<IdentityResult> UpdateAsync(ApplicationUser user)
        {
            return this.userManager.UpdateAsync(user);
        }
    }
}