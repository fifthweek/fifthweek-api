namespace Fifthweek.Api.Identity
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;

    public class UpdateLastAccessTokenDateCommandHandler : ICommandHandler<UpdateLastAccessTokenDateCommand>
    {
        private readonly IUserRepository userRepository;

        public UpdateLastAccessTokenDateCommandHandler(
            IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task HandleAsync(UpdateLastAccessTokenDateCommand command)
        {
            if (command.CreationType == UpdateLastAccessTokenDateCommand.AccessTokenCreationType.SignIn)
            {
                await this.userRepository.UpdateLastSignInDateAndAccessTokenDateAsync(command.Username, command.Timestamp);
            }
            else
            {
                await this.userRepository.UpdateLastAccessTokenDateAsync(command.Username, command.Timestamp);
            }
        }
    }
}