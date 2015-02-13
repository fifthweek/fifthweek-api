namespace Fifthweek.Api.Identity.OAuth
{
    using System.Threading.Tasks;
    using System.Web;

    using Fifthweek.Api.Core;

    using Microsoft.Owin;
    using Microsoft.Owin.Security.OAuth;

    public class FifthweekAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            var exceptionHandler = Helper.GetOwinScopeService<IExceptionHandler>(context.OwinContext);
            var authorizationServerHandler = Helper.GetOwinScopeService<IFifthweekAuthorizationServerHandler>(context.OwinContext);
            return ProviderErrorHandler.CallAndHandleError(
                () => authorizationServerHandler.ValidateClientAuthenticationAsync(context), 
                context,
                exceptionHandler);
        }

        public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var exceptionHandler = Helper.GetOwinScopeService<IExceptionHandler>(context.OwinContext);
            var authorizationServerHandler = Helper.GetOwinScopeService<IFifthweekAuthorizationServerHandler>(context.OwinContext);
            return ProviderErrorHandler.CallAndHandleError(
                () => authorizationServerHandler.GrantResourceOwnerCredentialsAsync(context),
                context,
                exceptionHandler);
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            var exceptionHandler = Helper.GetOwinScopeService<IExceptionHandler>(context.OwinContext);
            var authorizationServerHandler = Helper.GetOwinScopeService<IFifthweekAuthorizationServerHandler>(context.OwinContext);
            return ProviderErrorHandler.CallAndHandleError(
                () => authorizationServerHandler.TokenEndpointAsync(context),
                exceptionHandler);
        }

        public override Task GrantRefreshToken(OAuthGrantRefreshTokenContext context)
        {
            var exceptionHandler = Helper.GetOwinScopeService<IExceptionHandler>(context.OwinContext);
            var authorizationServerHandler = Helper.GetOwinScopeService<IFifthweekAuthorizationServerHandler>(context.OwinContext);
            return ProviderErrorHandler.CallAndHandleError(
                () => authorizationServerHandler.GrantRefreshTokenAsync(context),
                context,
                exceptionHandler);
        }

        public override Task MatchEndpoint(OAuthMatchEndpointContext context)
        {
            // Handle the pre-flight options. Taken from http://stackoverflow.com/a/27083151/37725.
            if (context.OwinContext.Request.Method == "OPTIONS" && context.IsTokenEndpoint)
            {
                context.OwinContext.Response.Headers.Add(
                    "Access-Control-Allow-Methods", new[] { "POST" });

                context.OwinContext.Response.Headers.Add(
                    "Access-Control-Allow-Headers", new[] { "accept", "authorization", "content-type", Core.Constants.DeveloperNameRequestHeaderKey });
                
                context.OwinContext.Response.Headers.Add(
                    "Access-Control-Allow-Origin", new[] { Core.Constants.DefaultAllowedOrigin });
                
                context.OwinContext.Response.StatusCode = 200;
                context.RequestCompleted();
                return Task.FromResult<object>(null);
            }

            return base.MatchEndpoint(context);
        }
    }
}