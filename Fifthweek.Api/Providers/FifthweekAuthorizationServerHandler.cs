namespace Fifthweek.Api.Providers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Security.Claims;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    using Fifthweek.Api.Entities;
    using Fifthweek.Api.Models;
    using Fifthweek.Api.Queries;
    using Fifthweek.Api.QueryHandlers;
    using Microsoft.Owin.Security;
    using Microsoft.Owin.Security.OAuth;

    public class FifthweekAuthorizationServerHandler : IFifthweekAuthorizationServerHandler
    {
        private readonly IQueryHandler<GetClientQuery, Client> getClient;

        private readonly IQueryHandler<GetUserQuery, ApplicationUser> getUser;

        public FifthweekAuthorizationServerHandler(
            IQueryHandler<GetClientQuery, Client> getClient,
            IQueryHandler<GetUserQuery, ApplicationUser> getUser)
        {
            this.getClient = getClient;
            this.getUser = getUser;
        }

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

            var allowedOrigin = this.GetAllowedOrigin(context, client);

            Helper.SetAccessControlAllowOrigin(context.OwinContext, allowedOrigin);

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

            context.OwinContext.Set<string>(Constants.TokenAllowedOriginKey, allowedOrigin);
            context.OwinContext.Set<string>(Constants.TokenRefreshTokenLifeTimeKey, client.RefreshTokenLifeTimeMinutes.ToString());

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

        private string GetAllowedOrigin(OAuthValidateClientAuthenticationContext context, Client client)
        {
            var allowedOrigin = client.DefaultAllowedOrigin;
            try
            {
                string origin = this.GetOriginFromHeader(context);
                if (!string.IsNullOrWhiteSpace(origin))
                {
                    if (Regex.IsMatch(origin, client.AllowedOriginRegex))
                    {
                        allowedOrigin = origin;
                    }
                    else
                    {
                        throw new Exception("Unexpected origin: " + origin);
                    }
                }
                else
                {
                    throw new Exception("Origin header not found.");
                }
            }
            catch (Exception t)
            {
                ExceptionHandlerUtilities.ReportExceptionAsync(t);
            }

            return allowedOrigin;
        }

        private string GetOriginFromHeader(OAuthValidateClientAuthenticationContext context)
        {
            string[] origins;
            if (context.Request.Headers.TryGetValue("Origin", out origins))
            {
                return origins.FirstOrDefault();
            }

            return null;
        }
    }
}