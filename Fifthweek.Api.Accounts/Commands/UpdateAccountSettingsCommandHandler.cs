namespace Fifthweek.Api.Accounts.Commands
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    [AutoConstructor]
    public partial class UpdateAccountSettingsCommandHandler : ICommandHandler<UpdateAccountSettingsCommand>
    {
        private readonly IAccountRepository accountRepository;

        private readonly IFileSecurity fileSecurity;

        public async Task HandleAsync(UpdateAccountSettingsCommand command)
        {
            command.AssertNotNull("command");
            command.AuthenticatedUserId.AssertAuthorizedFor(command.RequestedUserId);
            
            if (command.NewProfileImageId != null)
            {
                await this.fileSecurity.AssertFileBelongsToUserAsync(command.AuthenticatedUserId, command.NewProfileImageId);
            }

            var result = await this.accountRepository.UpdateAccountSettingsAsync(
                    command.RequestedUserId,
                    command.NewUsername,
                    command.NewEmail,
                    command.NewPassword,
                    command.NewProfileImageId);

            if (result.EmailChanged)
            {

            }
        }
    }
}