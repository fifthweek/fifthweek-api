using System;
using System.Linq;
using Fifthweek.Api.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Fifthweek.Api.CommandHandlers
{
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
            var userByEmail = await this.userManager.FindByEmailAsync(command.RegistrationData.Email);
            if (userByEmail != null)
            {
                throw new RecoverableException("The email address '" + command.RegistrationData.Email + "' is already taken.");
            }

            var userByUsername = await this.userManager.FindByNameAsync(command.RegistrationData.Username);
            if (userByUsername != null)
            {
                throw new RecoverableException("The username '" + command.RegistrationData.Username + "' is already taken.");
            }

            var user = new ApplicationUser
            {
                UserName = command.RegistrationData.Username,
                Email = command.RegistrationData.Email,
                ExampleWork = command.RegistrationData.ExampleWork
            };

            var result = await this.userManager.CreateAsync(user, command.RegistrationData.Password);

            if (!result.Succeeded)
            {
                var errorMessage = "Failed to create user: " + user;
                if (result.Errors == null)
                {
                    throw new Exception(errorMessage);
                }

                throw new AggregateException(errorMessage, result.Errors.Select(v => new Exception(v)));
            }
        }
    }
}