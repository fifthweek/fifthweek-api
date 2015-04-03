namespace Fifthweek.Api.Identity.Membership.Commands
{
    using System.Threading.Tasks;
    using System.Web;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class RequestPasswordResetCommandHandler : ICommandHandler<RequestPasswordResetCommand>
    {
        private const string EmailBodyTemplate = @"
            <p>Hey, we heard you lost your Fifthweek password. Don't worry!</p>

            <p>Use the following link to reset your password:</p>

            <p><strong><a id=""reset-password-link"" href=""{0}"">Reset Password</a></strong></p>

            <p>Oh, and incase you forgot, your username is <strong id=""username"">{1}</strong>.</p>

            <p>Thanks,<br />
            The Fifthweek Team</p>";

        private readonly IUserManager userManager;
        private readonly IHtmlLinter htmlLinter;

        public async Task HandleAsync(RequestPasswordResetCommand command)
        {
            FifthweekUser user = null;

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

            var callbackUrl = string.Format("{0}/#/sign-in/reset?userId={1}&token={2}", Core.Constants.FifthweekWebsiteBaseUrl, user.Id.EncodeGuid(), HttpUtility.UrlEncode(token));

            // Some email clients render whitespace.
            var lintedTemplate = this.htmlLinter.RemoveWhitespaceForHtmlEmail(EmailBodyTemplate);

            var emailBody = string.Format(lintedTemplate, callbackUrl, user.UserName).Replace("\n", "<br />");
            await this.userManager.SendEmailAsync(user.Id, "Reset Password", emailBody);
        }
    }
}