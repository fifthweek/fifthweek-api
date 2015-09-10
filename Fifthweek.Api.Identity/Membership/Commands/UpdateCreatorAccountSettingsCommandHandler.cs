namespace Fifthweek.Api.Identity.Membership.Commands
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class UpdateCreatorAccountSettingsCommandHandler : ICommandHandler<UpdateCreatorAccountSettingsCommand>
    {
        private readonly IRequesterSecurity requesterSecurity;
        private readonly IUserManager userManager;

        public async Task HandleAsync(UpdateCreatorAccountSettingsCommand command)
        {
            command.AssertNotNull("command");

            var userId = await this.requesterSecurity.AuthenticateAsAsync(command.Requester, command.RequestedUserId);
            var isCreator = await this.requesterSecurity.IsInRoleAsync(command.Requester, FifthweekRole.Creator);

            if (!isCreator)
            {
                await this.userManager.AddToRoleAsync(userId.Value, FifthweekRole.Creator);
            }
        }
    }
}