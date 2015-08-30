using Fifthweek.Api.Identity.Membership;

namespace Fifthweek.Api.Identity.OAuth
{
    using System;
    using System.Diagnostics;
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.OAuth.Commands;
    using Fifthweek.Api.Identity.OAuth.Queries;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    using Microsoft.Owin.Security.Infrastructure;

    [AutoConstructor]
    public partial class FifthweekRefreshTokenHandler : IFifthweekRefreshTokenHandler
    {
        private readonly ICommandHandler<SetRefreshTokenCommand> createRefreshToken;
        private readonly IQueryHandler<TryGetRefreshTokenQuery, RefreshToken> tryGetRefreshToken;
        private readonly IQueryHandler<TryGetRefreshTokenByEncryptedIdQuery, RefreshToken> tryGetRefreshTokenByEncryptedId;
        private readonly IRefreshTokenIdEncryptionService encryptionService;
        private readonly ITimestampCreator timestampCreator;

        public async Task CreateAsync(AuthenticationTokenCreateContext context)
        {
            context.AssertNotNull("context");

            var clientId = new ClientId(context.Ticket.Properties.Dictionary[Core.Constants.TokenClientIdKey]);
            var username = new Username(context.Ticket.Identity.Name);

            var refreshTokenLifeTime = context.OwinContext.Get<int>(Core.Constants.TokenRefreshTokenLifeTimeKey);
            if (refreshTokenLifeTime == default(int))
            {
                throw new InvalidOperationException("Refresh token lifetime not found.");
            }

            var refreshToken = await this.tryGetRefreshToken.HandleAsync(
                new TryGetRefreshTokenQuery(clientId, username));

            var now = this.timestampCreator.Now();

            RefreshTokenId refreshTokenId;
            if (refreshToken != null && refreshToken.ExpiresDate > now)
            {
                refreshTokenId = this.encryptionService.DecryptRefreshTokenId(
                    new EncryptedRefreshTokenId(refreshToken.EncryptedId));
            }
            else
            {
                refreshTokenId = RefreshTokenId.Create();

                var issuedDate = now;
                var expiresDate = issuedDate.AddMinutes(refreshTokenLifeTime);

                context.Ticket.Properties.IssuedUtc = issuedDate;
                context.Ticket.Properties.ExpiresUtc = expiresDate;

                await this.createRefreshToken.HandleAsync(
                    new SetRefreshTokenCommand(
                        refreshTokenId,
                        clientId,
                        username,
                        context.SerializeTicket(),
                        issuedDate,
                        expiresDate));
            }

            context.SetToken(refreshTokenId.Value);
        }

        public async Task ReceiveAsync(AuthenticationTokenReceiveContext context)
        {
            context.AssertNotNull("context");

            var allowedOrigin = context.OwinContext.Get<string>(Core.Constants.TokenAllowedOriginKey);

            if (allowedOrigin == null)
            {
                throw new InvalidOperationException("Allowed origin not found.");
            }

            Helper.SetAccessControlAllowOrigin(context.OwinContext, allowedOrigin);

            var suppliedRefreshTokenId = context.Token;
            var encryptedrefreshTokenId = this.encryptionService.EncryptRefreshTokenId(new RefreshTokenId(suppliedRefreshTokenId));

            var refreshToken = await this.tryGetRefreshTokenByEncryptedId.HandleAsync(new TryGetRefreshTokenByEncryptedIdQuery(encryptedrefreshTokenId));

            if (refreshToken != null)
            {
                // Get protectedTicket from refreshToken class
                context.DeserializeTicket(refreshToken.ProtectedTicket);
            }
            else
            {
                Trace.TraceWarning("Refresh token not found: " + encryptedrefreshTokenId);
            }
        }
    }
}