namespace Dexter.Api.QueryHandlers
{
    using System.Threading.Tasks;

    using Dexter.Api.Commands;
    using Dexter.Api.Models;
    using Dexter.Api.Queries;
    using Dexter.Api.Repositories;

    using Microsoft.AspNet.Identity.EntityFramework;

    public class GetUsernameFromExternalAccessTokenQueryHandler : IQueryHandler<GetUsernameFromExternalAccessTokenQuery, string>
    {
        private readonly IAuthenticationRepository authenticationRepository;

        private readonly IQueryHandler<GetVerifiedAccessTokenQuery, ParsedExternalAccessToken> getVerifiedAccessToken;

        public GetUsernameFromExternalAccessTokenQueryHandler(
            IAuthenticationRepository authenticationRepository,
            IQueryHandler<GetVerifiedAccessTokenQuery, ParsedExternalAccessToken> getVerifiedAccessToken)
        {
            this.authenticationRepository = authenticationRepository;
            this.getVerifiedAccessToken = getVerifiedAccessToken;
        }

        public async Task<string> HandleAsync(GetUsernameFromExternalAccessTokenQuery query)
        {
            var provider = query.Provider;
            var externalAccessToken = query.ExternalAccessToken;

            if (string.IsNullOrWhiteSpace(provider) || string.IsNullOrWhiteSpace(externalAccessToken))
            {
                throw new BadRequestException("Provider or external access token is not sent");
            }

            var verifiedAccessToken = await this.getVerifiedAccessToken.HandleAsync(new GetVerifiedAccessTokenQuery(provider, externalAccessToken));
            if (verifiedAccessToken == null)
            {
                throw new BadRequestException("Invalid Provider or External Access Token");
            }

            IdentityUser user = await this.authenticationRepository.FindExternalUserAsync(provider, verifiedAccessToken.UserId);

            if (user == null)
            {
                throw new BadRequestException("External user is not registered");
            }

            return user.UserName;
        }
    }
}