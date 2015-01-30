using Fifthweek.Api.Identity.Membership;

namespace Fifthweek.Api.Identity.OAuth
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.OAuth.Commands;
    using Fifthweek.Api.Identity.OAuth.Queries;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;

    using Microsoft.Owin.Security.Infrastructure;

    [AutoConstructor]
    public partial class FifthweekRefreshTokenHandler : IFifthweekRefreshTokenHandler
    {
        private readonly ICommandHandler<CreateRefreshTokenCommand> createRefreshToken;
        private readonly ICommandHandler<RemoveRefreshTokenCommand> removeRefreshToken;
        private readonly IQueryHandler<TryGetRefreshTokenQuery, RefreshToken> tryGetRefreshToken;

        public async Task CreateAsync(AuthenticationTokenCreateContext context)
        {
            context.AssertNotNull("context");

            var refreshTokenId = RefreshTokenId.Create();
            var clientId = new ClientId(context.Ticket.Properties.Dictionary[Constants.TokenClientIdKey]);
            var username = new Username(context.Ticket.Identity.Name);

            var refreshTokenLifeTime = context.OwinContext.Get<int>(Constants.TokenRefreshTokenLifeTimeKey);
            if (refreshTokenLifeTime == default(int))
            {
                throw new InvalidOperationException("Refresh token lifetime not found.");
            }

            var issuedDate = DateTime.UtcNow;
            var expiresDate = issuedDate.AddMinutes(refreshTokenLifeTime);

            context.Ticket.Properties.IssuedUtc = issuedDate;
            context.Ticket.Properties.ExpiresUtc = expiresDate;

            await this.createRefreshToken.HandleAsync(new CreateRefreshTokenCommand(
                refreshTokenId,
                clientId,
                username,
                context.SerializeTicket(),
                issuedDate,
                expiresDate));

            context.SetToken(refreshTokenId.Value);
        }

        public async Task ReceiveAsync(AuthenticationTokenReceiveContext context)
        {
            context.AssertNotNull("context");

            var allowedOrigin = context.OwinContext.Get<string>(Constants.TokenAllowedOriginKey);

            if (allowedOrigin == null)
            {
                throw new InvalidOperationException("Allowed origin not found.");
            }

            Helper.SetAccessControlAllowOrigin(context.OwinContext, allowedOrigin);

            var hashedRefreshTokenId = HashedRefreshTokenId.FromRefreshToken(context.Token);
            var refreshToken = await this.tryGetRefreshToken.HandleAsync(new TryGetRefreshTokenQuery(hashedRefreshTokenId));

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