namespace Fifthweek.Api.Accounts.Commands
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;

    public class UpdateAccountSettingsCommandHandler : ICommandHandler<UpdateAccountSettingsCommand>
    {
        public async Task HandleAsync(UpdateAccountSettingsCommand command)
        {
            command.AssertNotNull("command");
            command.Requester.AssertAuthorizedFor(command.RequestedUserId);

        }
    }
}