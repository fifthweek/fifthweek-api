namespace Dexter.Api.Providers
{
    using System;
    using System.Threading.Tasks;

    using Dexter.Api.Entities;
    using Dexter.Api.Repositories;

    using Microsoft.Owin.Security.Infrastructure;

    public class DexterRefreshTokenProvider : IAuthenticationTokenProvider
    {
        public DexterRefreshTokenProvider()
        {
        }

        public void Create(AuthenticationTokenCreateContext context)
        {
            throw new NotImplementedException();
        }

        public Task CreateAsync(AuthenticationTokenCreateContext context)
        {
            return GetAuthorizationServerHandler().CreateAsync(context);
        }

        public void Receive(AuthenticationTokenReceiveContext context)
        {
            throw new NotImplementedException();
        }

        public Task ReceiveAsync(AuthenticationTokenReceiveContext context)
        {
            return GetAuthorizationServerHandler().ReceiveAsync(context);
        }

        private static IDexterRefreshTokenHandler GetAuthorizationServerHandler()
        {
            var handler = (IDexterRefreshTokenHandler)
                Helper.GetOwinRequestLifetimeScope().GetService(typeof(IDexterRefreshTokenHandler));
            return handler;
        }
    }
}