namespace Fifthweek.Api.Identity.Membership.Commands
{
    using System;
    using System.Data.SqlTypes;
    using System.Linq;
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership.Events;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.CodeGeneration;

    [AutoConstructor]
    public partial class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand>
    {
        private readonly IUserManager userManager;
        private readonly IEventHandler<UserRegisteredEvent> userRegistered;

        public async Task HandleAsync(RegisterUserCommand command)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }

            var userByEmail = await this.userManager.FindByEmailAsync(command.Email.Value);
            if (userByEmail != null)
            {
                throw new RecoverableException("The email address '" + command.Email.Value + "' is already taken.");
            }

            var userByUsername = await this.userManager.FindByNameAsync(command.Username.Value);
            if (userByUsername != null)
            {
                throw new RecoverableException("The username '" + command.Username.Value + "' is already taken.");
            }

            var user = new FifthweekUser
            {
                Id = command.UserId.Value,
                UserName = command.Username.Value,
                Email = command.Email.Value,
                ExampleWork = command.ExampleWork,
                RegistrationDate = DateTime.UtcNow,
                LastSignInDate = SqlDateTime.MinValue.Value,
                LastAccessTokenDate = SqlDateTime.MinValue.Value,
            };

            var result = await this.userManager.CreateAsync(user, command.Password.Value);

            if (!result.Succeeded)
            {
                throw new AggregateException("Failed to create user: " + user.UserName, result.Errors.Select(v => new Exception(v)));
            }

            await this.userRegistered.HandleAsync(new UserRegisteredEvent(command.UserId));
        }
    }
}