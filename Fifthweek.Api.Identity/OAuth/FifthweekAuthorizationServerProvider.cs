namespace Fifthweek.Api.Identity.OAuth
{
    using System.Threading.Tasks;

    using Microsoft.Owin.Security.OAuth;

    public class FifthweekAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public FifthweekAuthorizationServerProvider()
        {
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            return ProviderErrorHandler.CallAndHandleError(
                () => GetAuthorizationServerHandler().ValidateClientAuthenticationAsync(context), 
                context);
        }

        public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            return ProviderErrorHandler.CallAndHandleError(
                () => GetAuthorizationServerHandler().GrantResourceOwnerCredentialsAsync(context), 
                context);
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            return ProviderErrorHandler.CallAndHandleError(
               () => GetAuthorizationServerHandler().TokenEndpointAsync(context));
        }

        public override Task GrantRefreshToken(OAuthGrantRefreshTokenContext context)
        {
            return ProviderErrorHandler.CallAndHandleError(
                () => GetAuthorizationServerHandler().GrantRefreshTokenAsync(context),
                context);
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

        private static IFifthweekAuthorizationServerHandler GetAuthorizationServerHandler()
        {
            return Helper.GetOwinScopeService<IFifthweekAuthorizationServerHandler>();
        }
    }
}