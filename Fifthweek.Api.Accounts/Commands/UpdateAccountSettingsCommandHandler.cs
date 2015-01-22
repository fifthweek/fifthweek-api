namespace Fifthweek.Api.Accounts.Commands
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.CodeGeneration;

    [AutoConstructor]
    public partial class UpdateAccountSettingsCommandHandler : ICommandHandler<UpdateAccountSettingsCommand>
    {
        private readonly IAccountRepository accountRepository;
        private readonly IRequesterSecurity requesterSecurity;
        private readonly IFileSecurity fileSecurity;

        public async Task HandleAsync(UpdateAccountSettingsCommand command)
        {
            command.AssertNotNull("command");

            var userId = await this.requesterSecurity.AuthenticateAsAsync(command.Requester, command.RequestedUserId);
            
            if (command.NewProfileImageId != null)
            {
                await this.fileSecurity.AssertUsageAllowedAsync(userId, command.NewProfileImageId);
            }

            var result = await this.accountRepository.UpdateAccountSettingsAsync(
                    command.RequestedUserId,
                    command.NewUsername,
                    command.NewEmail,
                    command.NewPassword,
                    command.NewProfileImageId);

            if (result.EmailChanged)
            {
                // Send activation email.
            }
        }
    }
}