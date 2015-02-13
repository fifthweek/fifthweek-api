namespace Fifthweek.Api.Identity.OAuth
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;

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
            var exceptionHandler = Helper.GetOwinScopeService<IExceptionHandler>(context.OwinContext);
            var refreshTokenHandler = Helper.GetOwinScopeService<IFifthweekRefreshTokenHandler>(context.OwinContext);
            return ProviderErrorHandler.CallAndHandleError(
                () => refreshTokenHandler.CreateAsync(context),
                exceptionHandler);
        }

        public void Receive(AuthenticationTokenReceiveContext context)
        {
            throw new NotImplementedException();
        }

        public Task ReceiveAsync(AuthenticationTokenReceiveContext context)
        {
            var exceptionHandler = Helper.GetOwinScopeService<IExceptionHandler>(context.OwinContext);
            var refreshTokenHandler = Helper.GetOwinScopeService<IFifthweekRefreshTokenHandler>(context.OwinContext);
            return ProviderErrorHandler.CallAndHandleError(
                () => refreshTokenHandler.ReceiveAsync(context),
                exceptionHandler);
        }
    }
}