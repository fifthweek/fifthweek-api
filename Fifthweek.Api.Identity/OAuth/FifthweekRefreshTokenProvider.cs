namespace Fifthweek.Api.Identity.OAuth
{
    using System;
    using System.Threading.Tasks;

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
            return ProviderErrorHandler.CallAndHandleError(
                () => Helper.GetOwinScopeService<IFifthweekRefreshTokenHandler>().CreateAsync(context));
        }

        public void Receive(AuthenticationTokenReceiveContext context)
        {
            throw new NotImplementedException();
        }

        public Task ReceiveAsync(AuthenticationTokenReceiveContext context)
        {
            return ProviderErrorHandler.CallAndHandleError(
                () => Helper.GetOwinScopeService<IFifthweekRefreshTokenHandler>().ReceiveAsync(context));
        }
    }
}