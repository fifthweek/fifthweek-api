namespace Fifthweek.Api.Providers
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.CommandHandlers;
    using Fifthweek.Api.Commands;
    using Fifthweek.Api.Entities;
    using Fifthweek.Api.Models;
    using Fifthweek.Api.Queries;
    using Fifthweek.Api.QueryHandlers;
    using Fifthweek.Api.Repositories;

    using Microsoft.Owin.Security.Infrastructure;

    public class FifthweekRefreshTokenHandler : IFifthweekRefreshTokenHandler
    {
        private readonly ICommandHandler<AddRefreshTokenCommand> addRefreshToken;

        private readonly ICommandHandler<RemoveRefreshTokenCommand> removeRefreshToken;

        private readonly IQueryHandler<GetRefreshTokenQuery, RefreshToken> getRefreshToken;

        private readonly IUserInputNormalization userInputNormalization;

        public FifthweekRefreshTokenHandler(
            ICommandHandler<AddRefreshTokenCommand> addRefreshToken,
            ICommandHandler<RemoveRefreshTokenCommand> removeRefreshToken,
            IQueryHandler<GetRefreshTokenQuery, RefreshToken> getRefreshToken,
            IUserInputNormalization userInputNormalization)
        {
            this.addRefreshToken = addRefreshToken;
            this.removeRefreshToken = removeRefreshToken;
            this.getRefreshToken = getRefreshToken;
            this.userInputNormalization = userInputNormalization;
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

            var normalizedUserName = this.userInputNormalization.NormalizeUsername(context.Ticket.Identity.Name);

            var token = new RefreshToken()
            {
                HashedId = Helper.GetHash(refreshTokenId.Value),
                ClientId = clientid,
                Username = normalizedUserName,
                IssuedUtc = DateTime.UtcNow,
                ExpiresUtc = DateTime.UtcNow.AddMinutes(Convert.ToDouble(refreshTokenLifeTime))
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