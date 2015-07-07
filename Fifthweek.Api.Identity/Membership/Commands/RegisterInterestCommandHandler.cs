namespace Fifthweek.Api.Identity.Membership.Commands
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class RegisterInterestCommandHandler : ICommandHandler<RegisterInterestCommand>
    {
        private readonly IFifthweekActivityReporter activityReporter;
        private readonly IExceptionHandler exceptionHandler;

        public async Task HandleAsync(RegisterInterestCommand command)
        {
            command.AssertNotNull("command");

            try
            {
                if (command.Email.Value.EndsWith(Core.Constants.TestEmailDomain))
                {
                    return;
                }

                await this.activityReporter.ReportActivityAsync(
                        string.Format("Registered Interest: {0}, {1}", command.Name, command.Email.Value));
            }
            catch (Exception t)
            {
                this.exceptionHandler.ReportExceptionAsync(new Exception("Failed to register interest: " + command, t));
            }
        }
    }
}