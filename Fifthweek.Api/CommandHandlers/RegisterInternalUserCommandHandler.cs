namespace Fifthweek.Api.CommandHandlers
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Commands;
    using Fifthweek.Api.Repositories;

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
                command.InternalRegistrationData.Email,
                command.InternalRegistrationData.Username,
                command.InternalRegistrationData.Password);
        }
    }
}