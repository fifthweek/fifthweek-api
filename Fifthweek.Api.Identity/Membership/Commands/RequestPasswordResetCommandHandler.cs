using System.Web;
using Fifthweek.Api.Persistence;

namespace Fifthweek.Api.Identity.Membership.Commands
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;

    public class RequestPasswordResetCommandHandler : ICommandHandler<RequestPasswordResetCommand>
    {
        private readonly IUserManager userManager;

        public RequestPasswordResetCommandHandler(IUserManager userManager)
        {
            if (userManager == null)
            {
                throw new ArgumentNullException("userManager");
            }

            this.userManager = userManager;
        }

        public async Task HandleAsync(RequestPasswordResetCommand command)
        {
            ApplicationUser user = null;

            if (command.Username != null)
            {
                user = await this.userManager.FindByNameAsync(command.Username.Value);  
            }

            if (command.Email != null && user == null)
            {
                user = await this.userManager.FindByEmailAsync(command.Email.Value);
            }

            if (user == null) // || !(await this.userManager.IsEmailConfirmedAsync(user.Id))
            {
                // Don't reveal that the user does not exist.
                return;
            }

            var token = await this.userManager.GeneratePasswordResetTokenAsync(user.Id);

            var callbackUrl = string.Format("https://www.fifthweek.com/#/resetPassword?userId={0}&token={1}", user.Id, NonEscapedUrlEncoder.Encode(token));
            const string emailBodyTemplate = @"
Hey, we heard you lost your Fifthweek password. Don't worry!

Use the following link to reset your password:

<strong><a href=""{0}"">Reset Password</a></strong>

Thanks,
The Fifthweek Team";

            var emailBody = string.Format(emailBodyTemplate.Trim(), callbackUrl).Replace("\n", "<br />");
            await this.userManager.SendEmailAsync(user.Id, "Reset Password", emailBody);
        }
    }
}