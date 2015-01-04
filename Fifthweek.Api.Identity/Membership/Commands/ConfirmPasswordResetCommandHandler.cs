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
            var result = await this.userManager.ResetPasswordAsync(command.UserId.Value.ToString(), command.Token, command.NewPassword.Value);
            if (!result.Succeeded)
            {
                throw new AggregateException("Failed to reset password", result.Errors.Select(e => new Exception(e)));
            }
        }
    }
}