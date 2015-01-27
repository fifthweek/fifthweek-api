namespace Fifthweek.Api.Identity.Membership.Commands
{
    using System;
    using System.Data.SqlTypes;
    using System.Linq;
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership.Events;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.CodeGeneration;

    [AutoConstructor]
    public partial class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand>
    {
        private readonly IEventHandler<UserRegisteredEvent> userRegistered;
        private readonly IRegisterUserDbStatement registerUser;

        public async Task HandleAsync(RegisterUserCommand command)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }

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