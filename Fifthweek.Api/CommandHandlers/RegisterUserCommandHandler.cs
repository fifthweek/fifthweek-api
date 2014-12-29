using System;
using System.Linq;
using Fifthweek.Api.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Fifthweek.Api.CommandHandlers
{
    using System.Data.SqlTypes;
    using System.Threading.Tasks;

    using Fifthweek.Api.Commands;
    using Fifthweek.Api.Repositories;

    public class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand>
    {
        private readonly IUserManager userManager;

        public RegisterUserCommandHandler(IUserManager userManager)
        {
            this.userManager = userManager;
        }

        public async Task HandleAsync(RegisterUserCommand command)
        {
            var userByEmail = await this.userManager.FindByEmailAsync(command.Email);
            if (userByEmail != null)
            {
                throw new RecoverableException("The email address '" + command.Email + "' is already taken.");
            }

            var userByUsername = await this.userManager.FindByNameAsync(command.Username);
            if (userByUsername != null)
            {
                throw new RecoverableException("The username '" + command.Username + "' is already taken.");
            }

            var user = new ApplicationUser
            {
                UserName = command.Username,
                Email = command.Email,
                ExampleWork = command.ExampleWork,
                RegistrationDate = DateTime.UtcNow,
                LastSignInDate = SqlDateTime.MinValue.Value,
                LastAccessTokenDate = SqlDateTime.MinValue.Value,
            };

            var result = await this.userManager.CreateAsync(user, command.Password);

            if (!result.Succeeded)
            {
                throw new AggregateException("Failed to create user: " + user.UserName, result.Errors.Select(v => new Exception(v)));
            }
        }
    }
}