namespace Dexter.Api.CommandHandlers
{
    using System.Threading.Tasks;

    using Dexter.Api.Commands;
    using Dexter.Api.Repositories;

    public class RegisterInternalUserCommandHandler : ICommandHandler<RegisterInternalUserCommand>
    {
        private readonly IAuthenticationRepository authenticationRepository;

        public RegisterInternalUserCommandHandler(IAuthenticationRepository authenticationRepository)
        {
            this.authenticationRepository = authenticationRepository;
        }

        public Task HandleAsync(RegisterInternalUserCommand command)
        {
            return this.authenticationRepository.AddInternalUserAsync(
                command.InternalRegistrationData.Username,
                command.InternalRegistrationData.Password);
        }
    }
}