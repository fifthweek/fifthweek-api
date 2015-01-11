namespace Fifthweek.Api.Persistence.Identity
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNet.Identity;

    public class FifthweekUserManager : UserManager<FifthweekUser, Guid>, IUserManager
    {
        // Configure the application user manager
        public FifthweekUserManager(IUserStore<FifthweekUser, Guid> store)
            : base(store)
        {
        }

        public Task<bool> ValidatePasswordResetTokenAsync(FifthweekUser user, string token)
        {
            // This is internal detail from ASP.NET UserManager.cs, available on GitHub:
            // https://github.com/aspnet/Identity/tree/dev/src/Microsoft.AspNet.Identity/UserManager.cs
            // Issue raised here:
            // https://github.com/aspnet/Identity/issues/307#issuecomment-68791565
            return this.UserTokenProvider.ValidateAsync(
                "ResetPassword",
                token,
                this,
                user);
        }
    }
}