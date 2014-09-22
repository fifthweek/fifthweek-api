namespace Dexter.Api.Providers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using System.Web;

    using Autofac.Core.Lifetime;

    using Dexter.Api.Entities;
    using Dexter.Api.Models;
    using Dexter.Api.Repositories;

    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.Owin;
    using Microsoft.Owin.Security;
    using Microsoft.Owin.Security.OAuth;

    public class DexterAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public DexterAuthorizationServerProvider()
        {
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            return GetAuthorizationServerHandler().ValidateClientAuthenticationAsync(context);
        }

        public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            return GetAuthorizationServerHandler().GrantResourceOwnerCredentialsAsync(context);
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            return GetAuthorizationServerHandler().TokenEndpointAsync(context);
        }

        public override Task GrantRefreshToken(OAuthGrantRefreshTokenContext context)
        {
            return GetAuthorizationServerHandler().GrantRefreshTokenAsync(context);
        }

        private static IDexterAuthorizationServerHandler GetAuthorizationServerHandler()
        {
            var handler =
                (IDexterAuthorizationServerHandler)
                Helper.GetOwinRequestLifetimeScope().GetService(typeof(IDexterAuthorizationServerHandler));
            return handler;
        }
    }
}