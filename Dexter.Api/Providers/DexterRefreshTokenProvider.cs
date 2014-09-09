namespace Dexter.Api.Providers
{
    using System;
    using System.Threading.Tasks;

    using Dexter.Api.Entities;
    using Dexter.Api.Repositories;

    using Microsoft.Owin.Security.Infrastructure;

    public class DexterRefreshTokenProvider : IAuthenticationTokenProvider
    {
        private readonly DexterRefreshTokenHandler.Factory handlerFactory;

        public DexterRefreshTokenProvider(DexterRefreshTokenHandler.Factory handlerFactory)
        {
            this.handlerFactory = handlerFactory;
        }

        public void Create(AuthenticationTokenCreateContext context)
        {
            throw new NotImplementedException();
        }

        public Task CreateAsync(AuthenticationTokenCreateContext context)
        {
            return this.handlerFactory().CreateAsync(context);
        }

        public void Receive(AuthenticationTokenReceiveContext context)
        {
            throw new NotImplementedException();
        }

        public Task ReceiveAsync(AuthenticationTokenReceiveContext context)
        {
            return this.handlerFactory().ReceiveAsync(context);
        }
    }
}