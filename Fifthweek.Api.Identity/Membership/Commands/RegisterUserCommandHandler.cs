namespace Fifthweek.Api.Identity.Membership.Commands
{
    using System;
    using System.Data.SqlTypes;
    using System.Linq;
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;

    public class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand>
    {
        private readonly IUserManager userManager;

        public RegisterUserCommandHandler(IUserManager userManager)
        {
            this.userManager = userManager;
        }

        public async Task HandleAsync(RegisterUserCommand command)
        {
            var userByEmail = await this.userManager.FindByEmailAsync(command.Email.Value);
            if (userByEmail != null)
            {
                throw new RecoverableException("The email address '" + command.Email + "' is already taken.");
            }

            var userByUsername = await this.userManager.FindByNameAsync(command.Username.Value);
            if (userByUsername != null)
            {
                throw new RecoverableException("The username '" + command.Username + "' is already taken.");
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
        }
    }
}