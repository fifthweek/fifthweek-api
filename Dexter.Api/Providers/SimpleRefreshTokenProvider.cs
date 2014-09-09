namespace Dexter.Api.Providers
{
    using System;
    using System.Threading.Tasks;

    using Dexter.Api.Entities;

    using Microsoft.Owin.Security.Infrastructure;

    public class SimpleRefreshTokenProvider : IAuthenticationTokenProvider
    {
        public void Create(AuthenticationTokenCreateContext context)
        {
            throw new NotImplementedException();
        }

        public async Task CreateAsync(AuthenticationTokenCreateContext context)
        {
            var clientid = context.Ticket.Properties.Dictionary[Constants.TokenClientIdKey];

            if (string.IsNullOrEmpty(clientid))
            {
                return;
            }

            var refreshTokenId = Guid.NewGuid().ToString("n");

            using (var repository = new AuthenticationRepository())
            {
                var refreshTokenLifeTime = context.OwinContext.Get<string>(Constants.TokenRefreshTokenLifeTimeKey);

                var token = new RefreshToken()
                {
                    Id = Helper.GetHash(refreshTokenId),
                    ClientId = clientid,
                    UserName = context.Ticket.Identity.Name,
                    IssuedUtc = DateTime.UtcNow,
                    ExpiresUtc = DateTime.UtcNow.AddMinutes(Convert.ToDouble(refreshTokenLifeTime))
                };

                context.Ticket.Properties.IssuedUtc = token.IssuedUtc;
                context.Ticket.Properties.ExpiresUtc = token.ExpiresUtc;

                token.ProtectedTicket = context.SerializeTicket();

                var result = await repository.AddRefreshToken(token);

                if (result)
                {
                    context.SetToken(refreshTokenId);
                }

            }
        }

        public void Receive(AuthenticationTokenReceiveContext context)
        {
            throw new NotImplementedException();
        }

        public async Task ReceiveAsync(AuthenticationTokenReceiveContext context)
        {
            var allowedOrigin = context.OwinContext.Get<string>(Constants.TokenAllowedOriginKey);
            Helper.SetAccessControlAllowOrigin(context.OwinContext, allowedOrigin);

            string hashedTokenId = Helper.GetHash(context.Token);

            using (var repository = new AuthenticationRepository())
            {
                var refreshToken = await repository.FindRefreshToken(hashedTokenId);

                if (refreshToken != null)
                {
                    //Get protectedTicket from refreshToken class
                    context.DeserializeTicket(refreshToken.ProtectedTicket);

                    // We can remove the current refresh token because a new one
                    // is created in the CreateAsync method when a new auth token
                    // is requested using the refresh token.
                    await repository.RemoveRefreshToken(hashedTokenId);
                }
            }
        }
    }
}