namespace Dexter.Api.Providers
{
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Dexter.Api.Entities;
    using Dexter.Api.Models;
    using Dexter.Api.Repositories;

    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.Owin;
    using Microsoft.Owin.Security;
    using Microsoft.Owin.Security.OAuth;

    public class DexterAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        private readonly DexterAuthorizationServerHandler.Factory handlerFactory;

        public DexterAuthorizationServerProvider(DexterAuthorizationServerHandler.Factory handlerFactory)
        {
            this.handlerFactory = handlerFactory;
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            return this.handlerFactory().ValidateClientAuthenticationAsync(context);
        }

        public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            return this.handlerFactory().GrantResourceOwnerCredentialsAsync(context);
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            return this.handlerFactory().TokenEndpointAsync(context);
        }

        public override Task GrantRefreshToken(OAuthGrantRefreshTokenContext context)
        {
            return this.handlerFactory().GrantRefreshTokenAsync(context);
        }
    }
}