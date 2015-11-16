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
        private readonly IGetFeedbackUserDataDbStatement getUserData;
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
                var userData = await this.getUserData.ExecuteAsync(userId);

                if (userData.Email.Value.EndsWith(Core.Constants.TestEmailDomain))
                {
                    return;
                }

                await this.activityReporter.ReportActivityAsync(
                    string.Format("Feedback from {0}, {1}: {2}", userData.Username.Value, userData.Email.Value, command.Message.Value));

                var htmlMessage = this.markdownRenderer.GetHtml(command.Message.Value);

                await this.sendEmailService.SendEmailAsync(
                    new MailAddress(userData.Email.Value, userData.Username.Value),
                    Fifthweek.Shared.Constants.FifthweekEmailAddress.Address,
                    "Feedback from " + userData.Username.Value,
                    htmlMessage);

            }
            catch (Exception t)
            {
                this.exceptionHandler.ReportExceptionAsync(new Exception("Failed to submit feedback: " + command, t));
            }
        }
    }
}