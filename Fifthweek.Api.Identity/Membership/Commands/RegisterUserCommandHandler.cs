namespace Fifthweek.Api.Identity.Membership.Commands
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership.Events;
    using Fifthweek.CodeGeneration;

    [AutoConstructor]
    public partial class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand>
    {
        private readonly IEventHandler<UserRegisteredEvent> userRegistered;
        private readonly IRegisterUserDbStatement registerUser;
        private readonly IReservedUsernameService reservedUsernames;

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
                command.Password,
                DateTime.UtcNow);

            await this.userRegistered.HandleAsync(new UserRegisteredEvent(command.UserId));
        }
    }
}