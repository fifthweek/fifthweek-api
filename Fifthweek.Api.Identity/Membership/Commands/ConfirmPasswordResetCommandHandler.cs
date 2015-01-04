using System.Linq;

namespace Fifthweek.Api.Identity.Membership.Commands
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;

    public class ConfirmPasswordResetCommandHandler : ICommandHandler<ConfirmPasswordResetCommand>
    {
        private readonly IUserManager userManager;

        public ConfirmPasswordResetCommandHandler(IUserManager userManager)
        {
            if (userManager == null)
            {
                throw new ArgumentNullException("userManager");
            }

            this.userManager = userManager;
        }

        public async Task HandleAsync(ConfirmPasswordResetCommand command)
        {
            var user = await this.userManager.FindByIdAsync(command.UserId.Value.ToString());
            if (user == null)
            {
                // This is unexpected behaviour which the user cannot recover from, so we don't report it to them.
                throw new Exception("User not found");    
            }

            var tokenValid = await this.userManager.UserTokenProvider.ValidateAsync(
                UserManagerDataProtectorPurposes.ResetPassword,
                command.Token,
                this.userManager.Base,
                user);

            if (!tokenValid)
            {
                // Tokens are provided to the user through links. The user understands links, so we make the exception 
                // message refer to the link to help the user understand.
                throw new RecoverableException("This link has expired.");
            }

            var result = await this.userManager.ResetPasswordAsync(command.UserId.Value.ToString(), command.Token, command.NewPassword.Value);
            if (!result.Succeeded)
            {
                throw new AggregateException("Failed to reset password", result.Errors.Select(e => new Exception(e)));
            }
        }
    }
}