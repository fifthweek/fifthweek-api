namespace Fifthweek.Api.Identity.Membership.Commands
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class UpdateAccountSettingsCommandHandler : ICommandHandler<UpdateAccountSettingsCommand>
    {
        private readonly IUpdateAccountSettingsDbStatement updateAccountSettings;
        private readonly IRequesterSecurity requesterSecurity;
        private readonly IFileSecurity fileSecurity;
        private readonly IReservedUsernameService reservedUsernames;

        public async Task HandleAsync(UpdateAccountSettingsCommand command)
        {
            command.AssertNotNull("command");

            var userId = await this.requesterSecurity.AuthenticateAsAsync(command.Requester, command.RequestedUserId);

            this.reservedUsernames.AssertNotReserved(command.NewUsername);

            if (command.NewProfileImageId != null)
            {
                await this.fileSecurity.AssertReferenceAllowedAsync(userId, command.NewProfileImageId);
            }

            var result = await this.updateAccountSettings.ExecuteAsync(
                    command.RequestedUserId,
                    command.NewUsername,
                    command.NewEmail,
                    command.NewPassword,
                    command.NewProfileImageId,
                    command.NewSecurityStamp);

            if (!result.EmailConfirmed)
            {
                // Send activation email.
            }
        }
    }
}