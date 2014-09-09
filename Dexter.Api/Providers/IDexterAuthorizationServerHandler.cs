namespace Dexter.Api.Providers
{
    using System.Threading.Tasks;

    using Microsoft.Owin.Security.OAuth;

    public interface IDexterAuthorizationServerHandler
    {
        Task ValidateClientAuthenticationAsync(OAuthValidateClientAuthenticationContext context);

        Task GrantResourceOwnerCredentialsAsync(OAuthGrantResourceOwnerCredentialsContext context);

        Task TokenEndpointAsync(OAuthTokenEndpointContext context);

        Task GrantRefreshTokenAsync(OAuthGrantRefreshTokenContext context);
    }
}