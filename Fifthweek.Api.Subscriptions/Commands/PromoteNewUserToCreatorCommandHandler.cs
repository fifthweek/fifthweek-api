namespace Fifthweek.Api.Subscriptions.Commands
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;

    [AutoConstructor]
    public partial class PromoteNewUserToCreatorCommandHandler : ICommandHandler<PromoteNewUserToCreatorCommand>
    {
        private readonly IUserManager userManager;

        public Task HandleAsync(PromoteNewUserToCreatorCommand command)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }

            return this.userManager.AddToRoleAsync(command.NewUserId.Value, FifthweekRole.Creator);
        }
    }
}