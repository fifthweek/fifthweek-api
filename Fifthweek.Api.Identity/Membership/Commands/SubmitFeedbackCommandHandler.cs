namespace Fifthweek.Api.Identity.Membership.Commands
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class SubmitFeedbackCommandHandler : ICommandHandler<SubmitFeedbackCommand>
    {
        private readonly IFifthweekActivityReporter activityReporter;
        private readonly IExceptionHandler exceptionHandler;

        public async Task HandleAsync(SubmitFeedbackCommand command)
        {
            command.AssertNotNull("command");

            try
            {
                if (command.Email != null && command.Email.Value.EndsWith(Core.Constants.TestEmailDomain))
                {
                    return;
                }

                await this.activityReporter.ReportActivityAsync(
                    string.Format("Feedback: {0}, {1}", command.Email == null ? "Anonymous" : command.Email.Value, command.Message.Value));
            }
            catch (Exception t)
            {
                this.exceptionHandler.ReportExceptionAsync(new Exception("Failed to submit feedback: " + command, t));
            }
        }
    }
}