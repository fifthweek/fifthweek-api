namespace Fifthweek.Api.Providers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using System.Web;

    using Autofac.Core.Lifetime;

    using Fifthweek.Api.Entities;
    using Fifthweek.Api.Models;
    using Fifthweek.Api.Repositories;

    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.Owin;
    using Microsoft.Owin.Security;
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
            var handler =
                (IFifthweekAuthorizationServerHandler)
                Helper.GetOwinRequestLifetimeScope().GetService(typeof(IFifthweekAuthorizationServerHandler));
            return handler;
        }
    }
}