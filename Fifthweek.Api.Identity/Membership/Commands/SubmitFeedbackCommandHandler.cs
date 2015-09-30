namespace Fifthweek.Api.Identity.Membership.Commands
{
    using System;
    using System.Net.Mail;
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class SubmitFeedbackCommandHandler : ICommandHandler<SubmitFeedbackCommand>
    {
        private readonly IRequesterSecurity requesterSecurity;
        private readonly IGetAccountSettingsDbStatement getAccountSettings;
        private readonly IFifthweekActivityReporter activityReporter;
        private readonly IMarkdownRenderer markdownRenderer;
        private readonly ISendEmailService sendEmailService;
        private readonly IExceptionHandler exceptionHandler;

        public async Task HandleAsync(SubmitFeedbackCommand command)
        {
            command.AssertNotNull("command");
            var userId = await this.requesterSecurity.AuthenticateAsync(command.Requester);

            try
            {
                var accountSettings = await this.getAccountSettings.ExecuteAsync(userId);

                if (accountSettings.Email.Value.EndsWith(Core.Constants.TestEmailDomain))
                {
                    return;
                }

                await this.activityReporter.ReportActivityAsync(
                    string.Format("Feedback from {0}, {1}: {2}", accountSettings.Username.Value, accountSettings.Email.Value, command.Message.Value));

                var htmlMessage = this.markdownRenderer.GetHtml(command.Message.Value);

                await this.sendEmailService.SendEmailAsync(
                    new MailAddress(accountSettings.Email.Value, accountSettings.Username.Value),
                    Fifthweek.Shared.Constants.FifthweekEmailAddress.Address,
                    "Feedback from " + accountSettings.Username.Value,
                    htmlMessage);

            }
            catch (Exception t)
            {
                this.exceptionHandler.ReportExceptionAsync(new Exception("Failed to submit feedback: " + command, t));
            }
        }
    }
}