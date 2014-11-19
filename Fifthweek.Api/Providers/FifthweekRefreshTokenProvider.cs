namespace Fifthweek.Api.Providers
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Entities;
    using Fifthweek.Api.Repositories;

    using Microsoft.Owin.Security.Infrastructure;

    public class FifthweekRefreshTokenProvider : IAuthenticationTokenProvider
    {
        public FifthweekRefreshTokenProvider()
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

        private static IFifthweekRefreshTokenHandler GetAuthorizationServerHandler()
        {
            var handler = (IFifthweekRefreshTokenHandler)
                Helper.GetOwinRequestLifetimeScope().GetService(typeof(IFifthweekRefreshTokenHandler));
            return handler;
        }
    }
}