namespace Dexter.Api.Providers
{
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Dexter.Api.Entities;
    using Dexter.Api.Models;
    using Dexter.Api.Queries;
    using Dexter.Api.QueryHandlers;
    using Dexter.Api.Repositories;

    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.Owin.Security;
    using Microsoft.Owin.Security.OAuth;

    public class DexterAuthorizationServerHandler : IDexterAuthorizationServerHandler
    {
        private readonly IQueryHandler<GetClientQuery, Client> getClient;

        private readonly IQueryHandler<GetUserQuery, IdentityUser> getUser;

        public DexterAuthorizationServerHandler(
            IQueryHandler<GetClientQuery, Client> getClient,
            IQueryHandler<GetUserQuery, IdentityUser> getUser)
        {
            this.getClient = getClient;
            this.getUser = getUser;
        }

        public delegate IDexterAuthorizationServerHandler Factory();

        public async Task ValidateClientAuthenticationAsync(OAuthValidateClientAuthenticationContext context)
        {
            Helper.SetAccessControlAllowOrigin(context.OwinContext);

            string clientId;
            string clientSecret;

            if (!context.TryGetBasicCredentials(out clientId, out clientSecret))
            {
                context.TryGetFormCredentials(out clientId, out clientSecret);
            }

            if (context.ClientId == null)
            {
                context.SetError("invalid_clientId", "ClientId should be sent.");
                return;
            }

            var client = await this.getClient.HandleAsync(new GetClientQuery(new ClientId(context.ClientId)));

            if (client == null)
            {
                context.SetError("invalid_clientId", string.Format("Client '{0}' is not registered in the system.", context.ClientId));
                return;
            }

            Helper.SetAccessControlAllowOrigin(context.OwinContext, client.AllowedOrigin);

            if (client.ApplicationType == ApplicationType.NativeConfidential)
            {
                if (string.IsNullOrWhiteSpace(clientSecret))
                {
                    context.SetError("invalid_clientId", "Client secret should be sent.");
                    return;
                }
                else
                {
                    if (client.Secret != Helper.GetHash(clientSecret))
                    {
                        context.SetError("invalid_clientId", "Client secret is invalid.");
                        return;
                    }
                }
            }

            if (!client.Active)
            {
                context.SetError("invalid_clientId", "Client is inactive.");
                return;
            }

            context.OwinContext.Set<string>(Constants.TokenAllowedOriginKey, client.AllowedOrigin);
            context.OwinContext.Set<string>(Constants.TokenRefreshTokenLifeTimeKey, client.RefreshTokenLifeTime.ToString());

            context.Validated();
        }


        public async Task GrantResourceOwnerCredentialsAsync(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var allowedOrigin = context.OwinContext.Get<string>(Constants.TokenAllowedOriginKey) ?? "*";
            Helper.SetAccessControlAllowOrigin(context.OwinContext, allowedOrigin);

            var user = await this.getUser.HandleAsync(new GetUserQuery(context.UserName, context.Password));

            if (user == null)
            {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
                return;
            }

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
            identity.AddClaim(new Claim("sub", context.UserName));
            identity.AddClaim(new Claim("role", "user"));

            var props = new AuthenticationProperties(new Dictionary<string, string>
                {
                    { 
                        Constants.TokenClientIdKey, context.ClientId ?? string.Empty
                    },
                    { 
                        "username", context.UserName
                    }
                });

            var ticket = new AuthenticationTicket(identity, props);
            context.Validated(ticket);
        }

        public Task TokenEndpointAsync(OAuthTokenEndpointContext context)
        {
            foreach (var property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }

        public Task GrantRefreshTokenAsync(OAuthGrantRefreshTokenContext context)
        {
            var originalClient = context.Ticket.Properties.Dictionary[Constants.TokenClientIdKey];
            var currentClient = context.ClientId;

            if (originalClient != currentClient)
            {
                context.SetError("invalid_clientId", "Refresh token is issued to a different clientId.");
                return Task.FromResult<object>(null);
            }

            var newIdentity = new ClaimsIdentity(context.Ticket.Identity);
            ////newIdentity.AddClaim(new Claim("newClaim", "newValue"));

            var newTicket = new AuthenticationTicket(newIdentity, context.Ticket.Properties);
            context.Validated(newTicket);

            return Task.FromResult<object>(null);
        }
    }
}