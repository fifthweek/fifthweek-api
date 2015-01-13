using Fifthweek.Api.Identity.Membership;

namespace Fifthweek.Api.Identity.OAuth
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.OAuth.Commands;
    using Fifthweek.Api.Identity.OAuth.Queries;
    using Fifthweek.Api.Persistence;

    using Microsoft.Owin.Security.Infrastructure;

    public class FifthweekRefreshTokenHandler : IFifthweekRefreshTokenHandler
    {
        private readonly ICommandHandler<AddRefreshTokenCommand> addRefreshToken;
        private readonly ICommandHandler<RemoveRefreshTokenCommand> removeRefreshToken;
        private readonly IQueryHandler<GetRefreshTokenQuery, RefreshToken> getRefreshToken;

        public FifthweekRefreshTokenHandler(
            ICommandHandler<AddRefreshTokenCommand> addRefreshToken,
            ICommandHandler<RemoveRefreshTokenCommand> removeRefreshToken,
            IQueryHandler<GetRefreshTokenQuery, RefreshToken> getRefreshToken)
        {
            if (addRefreshToken == null)
            {
                throw new ArgumentNullException("addRefreshToken");
            }

            if (removeRefreshToken == null)
            {
                throw new ArgumentNullException("removeRefreshToken");
            }

            if (getRefreshToken == null)
            {
                throw new ArgumentNullException("getRefreshToken");
            }

            this.addRefreshToken = addRefreshToken;
            this.removeRefreshToken = removeRefreshToken;
            this.getRefreshToken = getRefreshToken;
        }

        public async Task CreateAsync(AuthenticationTokenCreateContext context)
        {
            var refreshTokenId = RefreshTokenId.Create();

            var clientid = context.Ticket.Properties.Dictionary[Constants.TokenClientIdKey];

            if (string.IsNullOrEmpty(clientid))
            {
                return;
            }

            var refreshTokenLifeTime = context.OwinContext.Get<string>(Constants.TokenRefreshTokenLifeTimeKey);

            var username = ValidatedUsername.Parse(context.Ticket.Identity.Name);

            var token = new RefreshToken()
            {
                HashedId = Helper.GetHash(refreshTokenId.Value),
                ClientId = clientid,
                Username = username.Value,
                IssuedUtc = DateTime.UtcNow,
                ExpiresUtc = DateTime.UtcNow.AddMinutes(Convert.ToDouble((string)refreshTokenLifeTime))
            };

            context.Ticket.Properties.IssuedUtc = token.IssuedUtc;
            context.Ticket.Properties.ExpiresUtc = token.ExpiresUtc;

            token.ProtectedTicket = context.SerializeTicket();

            await this.addRefreshToken.HandleAsync(new AddRefreshTokenCommand(token));

            context.SetToken(refreshTokenId.Value);
        }

        public async Task ReceiveAsync(AuthenticationTokenReceiveContext context)
        {
            var allowedOrigin = context.OwinContext.Get<string>(Constants.TokenAllowedOriginKey);
            Helper.SetAccessControlAllowOrigin(context.OwinContext, allowedOrigin);

            var hashedRefreshTokenId = HashedRefreshTokenId.FromRefreshToken(context.Token);
            var refreshToken = await this.getRefreshToken.HandleAsync(new GetRefreshTokenQuery(hashedRefreshTokenId));

            if (refreshToken != null)
            {
                // Get protectedTicket from refreshToken class
                context.DeserializeTicket(refreshToken.ProtectedTicket);

                // We can remove the current refresh token because a new one
                // is created in the CreateAsync method when a new auth token
                // is requested using the refresh token.
                await this.removeRefreshToken.HandleAsync(new RemoveRefreshTokenCommand(hashedRefreshTokenId));
            }
        }
    }
}