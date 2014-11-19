namespace Fifthweek.Api.CommandHandlers
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Commands;
    using Fifthweek.Api.Models;
    using Fifthweek.Api.Queries;
    using Fifthweek.Api.QueryHandlers;
    using Fifthweek.Api.Repositories;

    public class RegisterExternalUserCommandHandler : ICommandHandler<RegisterExternalUserCommand>
    {
        private readonly IAuthenticationRepository authenticationRepository;

        private readonly IQueryHandler<GetVerifiedAccessTokenQuery, ParsedExternalAccessToken> getVerifiedAccessToken;

        public RegisterExternalUserCommandHandler(
            IAuthenticationRepository authenticationRepository,
            IQueryHandler<GetVerifiedAccessTokenQuery, ParsedExternalAccessToken> getVerifiedAccessToken)
        {
            this.authenticationRepository = authenticationRepository;
            this.getVerifiedAccessToken = getVerifiedAccessToken;
        }

        public async Task HandleAsync(RegisterExternalUserCommand command)
        {
            var registrationData = command.ExternalRegistrationData;

            var verifiedAccessToken = await this.getVerifiedAccessToken.HandleAsync(new GetVerifiedAccessTokenQuery(registrationData.Provider, registrationData.ExternalAccessToken));
            if (verifiedAccessToken == null)
            {
                throw new BadRequestException("Invalid Provider or External Access Token");
            }

            var user = await this.authenticationRepository.FindExternalUserAsync(registrationData.Provider, verifiedAccessToken.UserId);

            if (user != null)
            {
                throw new BadRequestException("External user is already registered");
            }

            await this.authenticationRepository.AddExternalUserAsync(
                registrationData.Username,
                registrationData.Provider,
                verifiedAccessToken.UserId);
        }
    }
}