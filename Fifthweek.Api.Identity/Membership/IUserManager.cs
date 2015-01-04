namespace Fifthweek.Api.Identity.Membership
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Persistence;

    using Microsoft.AspNet.Identity;

    public interface IUserManager : IDisposable
    {
        Task<IdentityResult> CreateAsync(ApplicationUser user, string password);

        Task<ApplicationUser> FindAsync(string userName, string password);

        Task<ApplicationUser> FindByIdAsync(string userId);

        Task<ApplicationUser> FindByEmailAsync(string email);

        Task<ApplicationUser> FindByNameAsync(string userName);

        Task<IdentityResult> UpdateAsync(ApplicationUser user);

        Task<string> GeneratePasswordResetTokenAsync(string userId);
        
        Task<IdentityResult> ResetPasswordAsync(string userId, string token, string newPassword);
        
        Task SendEmailAsync(string userId, string subject, string body);

        Task<bool> ValidatePasswordResetTokenAsync(ApplicationUser user, string token);
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

        public IUserTokenProvider<ApplicationUser, string> UserTokenProvider
        {
            get { return this.userManager.UserTokenProvider; }
        }

        public Task<IdentityResult> CreateAsync(ApplicationUser user, string password)
        {
            return this.userManager.CreateAsync(user, password);
        }

        public Task<ApplicationUser> FindAsync(string userName, string password)
        {
            return this.userManager.FindAsync(userName, password);
        }

        public Task<ApplicationUser> FindByIdAsync(string userId)
        {
            return this.userManager.FindByIdAsync(userId);
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

        public Task<string> GeneratePasswordResetTokenAsync(string userId)
        {
            return this.userManager.GeneratePasswordResetTokenAsync(userId);
        }

        public Task<IdentityResult> ResetPasswordAsync(string userId, string token, string newPassword)
        {
            return this.userManager.ResetPasswordAsync(userId, token, newPassword);
        }

        public Task SendEmailAsync(string userId, string subject, string body)
        {
            return this.userManager.SendEmailAsync(userId, subject, body);
        }

        public Task<bool> ValidatePasswordResetTokenAsync(ApplicationUser user, string token)
        {
            // This is internal detail from ASP.NET UserManager.cs, available on GitHub:
            // https://github.com/aspnet/Identity/tree/dev/src/Microsoft.AspNet.Identity/UserManager.cs
            return this.userManager.UserTokenProvider.ValidateAsync(
                "ResetPassword",
                token,
                this.userManager,
                user);
        }
    }
}