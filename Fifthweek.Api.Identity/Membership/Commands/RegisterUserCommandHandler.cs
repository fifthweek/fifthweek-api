namespace Fifthweek.Api.Identity.Membership.Commands
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Identity.Shared.Membership.Events;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.CodeGeneration;

    [AutoConstructor]
    public partial class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand>
    {
        private readonly IRegisterUserDbStatement registerUser;
        private readonly IReservedUsernameService reservedUsernames;
        private readonly IUserManager userManager;

        public async Task HandleAsync(RegisterUserCommand command)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }

            this.reservedUsernames.AssertNotReserved(command.Username);

            await this.registerUser.ExecuteAsync(
                command.UserId,
                command.Username,
                command.Email,
                command.ExampleWork,
                command.CreatorName,
                command.Password,
                DateTime.UtcNow);

            if (command.RegiserAsCreator)
            {
                await this.userManager.AddToRoleAsync(command.UserId.Value, FifthweekRole.Creator);
            }

            if (this.RegisterAsTestUser(command.Email))
            {
                await this.userManager.AddToRoleAsync(command.UserId.Value, FifthweekRole.Test);
            }
        }

        internal bool RegisterAsTestUser(Email email)
        {
            return email.Value.EndsWith(Constants.TestDomain);
        }
    }
}