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
    using Fifthweek.Api.Identity.OAuth.Commands;
    using Fifthweek.Api.Identity.OAuth.Queries;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    using Microsoft.Owin.Security;
    using Microsoft.Owin.Security.OAuth;

    [AutoConstructor]
    public partial class FifthweekAuthorizationServerHandler : IFifthweekAuthorizationServerHandler
    {
        private readonly IQueryHandler<GetValidatedClientQuery, Client> getValidatedClient;
        private readonly IQueryHandler<GetUserClaimsIdentityQuery, UserClaimsIdentity> getUserClaimsIdentity;
        private readonly ICommandHandler<UpdateLastAccessTokenDateCommand> updateLastAccessTokenDate;
        private readonly IOwinExceptionHandler exceptionHandler;

        /// <summary>
        /// Called to validate that the origin of the request is a registered "client_id", and that the 
        /// correct credentials for that client are present on the request. If the web application accepts
        /// Basic authentication credentials, context.TryGetBasicCredentials(out clientId, out clientSecret) 
        /// may be called to acquire those values if present in the request header. If the web application 
        /// accepts "client_id" and "client_secret" as form encoded POST parameters, 
        /// context.TryGetFormCredentials(out clientId, out clientSecret) may be called to acquire those 
        /// values if present in the request body. If context.Validated is not called the request will 
        /// not proceed further.
        /// </summary>
        public async Task ValidateClientAuthenticationAsync(OAuthValidateClientAuthenticationContext context)
        {
            context.AssertNotNull("context");

            Helper.SetAccessControlAllowOrigin(context.OwinContext);

            string clientId;
            string clientSecret;

            if (!context.TryGetBasicCredentials(out clientId, out clientSecret))
            {
                context.TryGetFormCredentials(out clientId, out clientSecret);
            }

            if (context.ClientId == null)
            {
                context.SetError("invalid_clientId", "Client ID should be sent.");
                return;
            }

            Client client;
            try
            {
                client = await this.getValidatedClient.HandleAsync(new GetValidatedClientQuery(new ClientId(context.ClientId), clientSecret));
            }
            catch (ClientRequestException t)
            {
                context.SetError("invalid_clientId", t.Message);
                return;
            }

            var allowedOrigin = this.GetAllowedOrigin(context, client);

            Helper.SetAccessControlAllowOrigin(context.OwinContext, allowedOrigin);

            context.OwinContext.Set<string>(Constants.TokenAllowedOriginKey, allowedOrigin);
            context.OwinContext.Set<int>(Constants.TokenRefreshTokenLifeTimeKey, client.RefreshTokenLifeTimeMinutes);

            context.Validated();
        }

        /// <summary>
        /// Called when a request to the Token endpoint arrives with a "grant_type" of "password". This 
        /// occurs when the user has provided name and password credentials directly into the client 
        /// application's user interface, and the client application is using those to acquire an "access_token"
        /// and optional "refresh_token". If the web application supports the resource owner credentials 
        /// grant type it must validate the context.Username and context.Password as appropriate. To issue an 
        /// access token the context.Validated must be called with a new ticket containing the claims about the
        /// resource owner which should be associated with the access token. The application should take 
        /// appropriate measures to ensure that the endpoint isn’t abused by malicious callers. The default 
        /// behavior is to reject this grant type. See also http://tools.ietf.org/html/rfc6749#section-4.3.2
        /// </summary>
        public async Task GrantResourceOwnerCredentialsAsync(OAuthGrantResourceOwnerCredentialsContext context)
        {
            context.AssertNotNull("context");

            var allowedOrigin = context.OwinContext.Get<string>(Constants.TokenAllowedOriginKey); // ?? Constants.DefaultAllowedOrigin;

            if (string.IsNullOrEmpty(allowedOrigin))
            {
                throw new UnauthorizedException("The allowed origin was not inferred from the client ID");
            }

            Helper.SetAccessControlAllowOrigin(context.OwinContext, allowedOrigin);

            var username = new Username(context.UserName);
            var password = new Password(context.Password);

            UserClaimsIdentity userClaimsIdentity;
            try
            {
                userClaimsIdentity = await this.getUserClaimsIdentity.HandleAsync(
                    new GetUserClaimsIdentityQuery(null, username, password, context.Options.AuthenticationType));
            }
            catch (BadRequestException t)
            {
                context.SetError("invalid_grant", t.Message);
                return;
            }

            await this.updateLastAccessTokenDate.HandleAsync(new UpdateLastAccessTokenDateCommand(
                    userClaimsIdentity.UserId, 
                    DateTime.UtcNow, 
                    UpdateLastAccessTokenDateCommand.AccessTokenCreationType.SignIn));

            var props = this.CreateAuthenticationProperties(context.ClientId, userClaimsIdentity);

            var ticket = new AuthenticationTicket(userClaimsIdentity.ClaimsIdentity, props);
            context.Validated(ticket);
        }

        /// <summary>
        /// Called at the final stage of a successful Token endpoint request. An application may implement 
        /// this call in order to do any final modification of the claims being used to issue access or 
        /// refresh tokens. This call may also be used in order to add additional response parameters to 
        /// the Token endpoint's json response body.
        /// </summary>
        public Task TokenEndpointAsync(OAuthTokenEndpointContext context)
        {
            context.AssertNotNull("context");

            foreach (var property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }
            
            return Task.FromResult<object>(null);
        }

        /// <summary>
        /// Called when a request to the Token endpoint arrives with a "grant_type" of "refresh_token". 
        /// This occurs if your application has issued a "refresh_token" along with the "access_token", 
        /// and the client is attempting to use the "refresh_token" to acquire a new "access_token", 
        /// and possibly a new "refresh_token". To issue a refresh token the an Options.RefreshTokenProvider 
        /// must be assigned to create the value which is returned. The claims and properties associated 
        /// with the refresh token are present in the context.Ticket. The application must call 
        /// context.Validated to instruct the Authorization Server middleware to issue an access token 
        /// based on those claims and properties. The call to context.Validated may be given a different 
        /// AuthenticationTicket or ClaimsIdentity in order to control which information flows from the 
        /// refresh token to the access token. The default behavior when using the OAuthAuthorizationServerProvider 
        /// is to flow information from the refresh token to the access token unmodified. 
        /// See also http://tools.ietf.org/html/rfc6749#section-6
        /// </summary>
        public async Task GrantRefreshTokenAsync(OAuthGrantRefreshTokenContext context)
        {
            context.AssertNotNull("context");

            var originalClient = context.Ticket.Properties.Dictionary[Constants.TokenClientIdKey];
            var currentClient = context.ClientId;

            if (originalClient != currentClient)
            {
                context.SetError("invalid_clientId", "Refresh token is issued to a different clientId.");
                return;
            }

            var nameIdentifierClaim = context.Ticket.Identity.FindFirst(ClaimTypes.NameIdentifier);
            var userId = new UserId(nameIdentifierClaim.Value.DecodeGuid());

            UserClaimsIdentity userClaimsIdentity;
            try
            {
                userClaimsIdentity = await this.getUserClaimsIdentity.HandleAsync(
                    new GetUserClaimsIdentityQuery(userId, null, null, context.Options.AuthenticationType));
            }
            catch (BadRequestException t)
            {
                context.SetError("invalid_grant", t.Message);
                return;
            }

            await this.updateLastAccessTokenDate.HandleAsync(
                new UpdateLastAccessTokenDateCommand(userId, DateTime.UtcNow, UpdateLastAccessTokenDateCommand.AccessTokenCreationType.RefreshToken));

            var props = this.CreateAuthenticationProperties(context.ClientId, userClaimsIdentity);

            var ticket = new AuthenticationTicket(userClaimsIdentity.ClaimsIdentity, props);
            context.Validated(ticket);
        }

        private AuthenticationProperties CreateAuthenticationProperties(string clientId, UserClaimsIdentity userClaimsIdentity)
        {
            var props = new AuthenticationProperties(new Dictionary<string, string>
            {
                {
                    Constants.TokenClientIdKey, clientId
                },
                { 
                    "username", userClaimsIdentity.Username.Value 
                },
                { 
                    "user_id", userClaimsIdentity.UserId.Value.EncodeGuid() 
                },
                {
                    "roles", string.Join(",", userClaimsIdentity.ClaimsIdentity.FindAll(ClaimTypes.Role).Select(v => v.Value))
                },
            });

            return props;
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
                this.exceptionHandler.ReportExceptionAsync(context.Request, t);
                allowedOrigin = Constants.DefaultAllowedOrigin;  // Remove this line to restrict origins to those defined by the client.
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
