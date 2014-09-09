namespace Dexter.Api.CommandHandlers
{
    using System.Threading.Tasks;

    using Dexter.Api.Commands;
    using Dexter.Api.Models;
    using Dexter.Api.Queries;
    using Dexter.Api.QueryHandlers;
    using Dexter.Api.Repositories;

    using Microsoft.AspNet.Identity.EntityFramework;

    public class RegisterExternalUserCommandHandler : ICommandHandler<RegisterExternalUserCommand>
    {
        private readonly IAuthenticationRepository authenticationRepository;

        private readonly IQueryHandler<GetVerifiedAccessTokenQuery, ParsedExternalAccessToken> verifyAccessToken;

        public RegisterExternalUserCommandHandler(
            IAuthenticationRepository authenticationRepository,
            IQueryHandler<GetVerifiedAccessTokenQuery, ParsedExternalAccessToken> verifyAccessToken)
        {
            this.authenticationRepository = authenticationRepository;
            this.verifyAccessToken = verifyAccessToken;
        }

        public async Task HandleAsync(RegisterExternalUserCommand command)
        {
            var registrationData = command.ExternalRegistrationData;

            var verifiedAccessToken = await this.verifyAccessToken.HandleAsync(new GetVerifiedAccessTokenQuery(registrationData.Provider, registrationData.ExternalAccessToken));
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