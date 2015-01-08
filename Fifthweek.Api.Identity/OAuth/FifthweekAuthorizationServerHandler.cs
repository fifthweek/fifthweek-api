using Fifthweek.Api.Identity.Membership;

namespace Fifthweek.Api.Identity.OAuth
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership.Commands;
    using Fifthweek.Api.Identity.Membership.Queries;
    using Fifthweek.Api.Identity.OAuth.Queries;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;

    using Microsoft.Owin.Security;
    using Microsoft.Owin.Security.OAuth;

    [AutoConstructor]
    public partial class FifthweekAuthorizationServerHandler : IFifthweekAuthorizationServerHandler
    {
        private readonly IQueryHandler<GetClientQuery, Client> getClient;
        private readonly IQueryHandler<GetUserQuery, FifthweekUser> getUser;
        private readonly ICommandHandler<UpdateLastAccessTokenDateCommand> updateLastAccessTokenDate;
        private readonly IExceptionHandler exceptionHandler;

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
                context.SetError("invalid_clientId", string.Format((string)"Client '{0}' is not registered in the system.", (object)context.ClientId));
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

            var username = Username.Parse(context.UserName);
            var normalizedUsername = NormalizedUsername.Normalize(username);
            var password = Password.Parse(context.Password);

            var user = await this.getUser.HandleAsync(new GetUserQuery(normalizedUsername, password));

            if (user == null)
            {
                context.SetError("invalid_grant", "Invalid username or password.");
                return;
            }

            await this.updateLastAccessTokenDate.HandleAsync(
                new UpdateLastAccessTokenDateCommand(normalizedUsername, DateTime.UtcNow, UpdateLastAccessTokenDateCommand.AccessTokenCreationType.SignIn));

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim(ClaimTypes.Name, normalizedUsername.Value));
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));

            foreach (var role in user.Roles)
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, role.RoleId.ToString()));
            }

            var props = new AuthenticationProperties(new Dictionary<string, string>
                {
                    { 
                        Constants.TokenClientIdKey, context.ClientId ?? string.Empty
                    },
                    { 
                        "username", normalizedUsername.Value
                    },
                    {
                        "user_id", user.Id.ToString()
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

        public async Task GrantRefreshTokenAsync(OAuthGrantRefreshTokenContext context)
        {
            var originalClient = context.Ticket.Properties.Dictionary[Constants.TokenClientIdKey];
            var currentClient = context.ClientId;

            if (originalClient != currentClient)
            {
                context.SetError("invalid_clientId", "Refresh token is issued to a different clientId.");
                return;
            }

            var username = Username.Parse(context.Ticket.Identity.Name);
            var normalizedUsername = NormalizedUsername.Normalize(username);

            await this.updateLastAccessTokenDate.HandleAsync(
                new UpdateLastAccessTokenDateCommand(normalizedUsername, DateTime.UtcNow, UpdateLastAccessTokenDateCommand.AccessTokenCreationType.RefreshToken));

            var newIdentity = new ClaimsIdentity(context.Ticket.Identity);

            var newTicket = new AuthenticationTicket(newIdentity, context.Ticket.Properties);
            context.Validated(newTicket);
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
                this.exceptionHandler.ReportExceptionAsync(t);
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
