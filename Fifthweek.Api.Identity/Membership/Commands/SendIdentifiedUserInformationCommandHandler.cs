namespace Fifthweek.Api.Identity.Membership.Commands
{
    using System;
    using System.Text;
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class SendIdentifiedUserInformationCommandHandler : ICommandHandler<SendIdentifiedUserInformationCommand>
    {
        private readonly IFifthweekActivityReporter activityReporter;
        private readonly IExceptionHandler exceptionHandler;

        public async Task HandleAsync(SendIdentifiedUserInformationCommand command)
        {
            command.AssertNotNull("command");

            try
            {
                if (command.Email.Value.EndsWith(Core.Constants.TestDomain))
                {
                    return;
                }

                var sb = new StringBuilder();

                if (command.IsUpdate)
                {
                    sb.Append("Updated Identified User: ");
                }
                else
                {
                    sb.Append("Identified User: ");
                }

                if (!string.IsNullOrWhiteSpace(command.Name))
                {
                    sb.Append(command.Name).Append(", ");
                }

                if (command.Username != null)
                {
                    sb.Append(command.Username.Value).Append(", ");
                }

                sb.Append(command.Email.Value);

                await this.activityReporter.ReportActivityAsync(sb.ToString());
            }
            catch (Exception t)
            {
                this.exceptionHandler.ReportExceptionAsync(new Exception("Failed to report identified user: " + command, t));
            }
        }
    }
}