namespace Fifthweek.Api.Identity
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

        private static IFifthweekAuthorizationServerHandler GetAuthorizationServerHandler()
        {
            return Helper.GetOwinScopeService<IFifthweekAuthorizationServerHandler>();
        }
    }
}