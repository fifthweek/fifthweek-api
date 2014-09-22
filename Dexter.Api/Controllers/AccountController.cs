using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Dexter.Api.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Dexter.Api.CommandHandlers;
    using Dexter.Api.Commands;
    using Dexter.Api.Models;
    using Dexter.Api.Queries;
    using Dexter.Api.QueryHandlers;
    using Dexter.Api.Repositories;
    using Dexter.Api.Results;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin.Security;
    using Microsoft.Owin.Security.OAuth;

    using Newtonsoft.Json.Linq;

    [RoutePrefix("api/Account")]
    public class AccountController : ApiController
    {
        private readonly IAuthenticationRepository authenticationRepository;

        private readonly IClientRepository clientRepository;

        private readonly ICommandHandler<RegisterInternalUserCommand> registerInternalUser;

        private readonly ICommandHandler<RegisterExternalUserCommand> registerExternalUser;

        private readonly IQueryHandler<GetUsernameFromExternalAccessTokenQuery, string> getUsernameFromExternalAccessToken;

        public AccountController(
            IAuthenticationRepository authenticationRepository,
            IClientRepository clientRepository,
            ICommandHandler<RegisterInternalUserCommand> registerInternalUser,
            ICommandHandler<RegisterExternalUserCommand> registerExternalUser,
            IQueryHandler<GetUsernameFromExternalAccessTokenQuery, string> getUsernameFromExternalAccessToken)
        {
            this.authenticationRepository = authenticationRepository;
            this.clientRepository = clientRepository;
            this.registerInternalUser = registerInternalUser;
            this.registerExternalUser = registerExternalUser;
            this.getUsernameFromExternalAccessToken = getUsernameFromExternalAccessToken;
        }

        // POST api/Account/RegisterInternalUser
        [RequireHttps]
        [ConvertExceptionsToResponses]
        [ValidateModel]
        [AllowAnonymous]
        [Route("RegisterInternalUser")]
        public async Task<IHttpActionResult> RegisterInternalUser(InternalRegistrationData registrationData)
        {
            await this.registerInternalUser.HandleAsync(new RegisterInternalUserCommand(registrationData));
            return this.Ok();
        }

        // POST api/Account/RegisterExternalUser
        [RequireHttps]
        [ConvertExceptionsToResponses]
        [ValidateModel]
        [AllowAnonymous]
        [Route("RegisterExternalUser")]
        public async Task<IHttpActionResult> RegisterExternalUser(ExternalRegistrationData registrationData)
        {
            await this.registerExternalUser.HandleAsync(new RegisterExternalUserCommand(registrationData));
            var accessTokenResponse = this.GenerateLocalAccessTokenResponse(registrationData.Username);
            return this.Ok(accessTokenResponse);
        }

        [RequireHttps]
        [ConvertExceptionsToResponses]
        [AllowAnonymous]
        [HttpGet]
        [Route("ObtainAccessTokenForExternalUser")]
        public async Task<IHttpActionResult> ObtainAccessTokenForExternalUser(string provider, string externalAccessToken)
        {
            var username = await this.getUsernameFromExternalAccessToken.HandleAsync(new GetUsernameFromExternalAccessTokenQuery(provider, externalAccessToken));
            var accessTokenResponse = this.GenerateLocalAccessTokenResponse(username);
            return this.Ok(accessTokenResponse);
        }

        // GET api/Account/InitiateExternalSignIn
        [RequireHttps]
        [ConvertExceptionsToResponses]
        [OverrideAuthentication]
        [HostAuthentication(DefaultAuthenticationTypes.ExternalCookie)]
        [AllowAnonymous]
        [Route("InitiateExternalSignIn", Name = "InitiateExternalSignIn")]
        public async Task<IHttpActionResult> InitiateExternalSignIn(string provider, string error = null)
        {
            string redirectUri = string.Empty;

            if (error != null)
            {
                return this.BadRequest(Uri.EscapeDataString(error));
            }

            if (!this.User.Identity.IsAuthenticated)
            {
                return new ChallengeResult(provider, this.Request);
            }

            try
            {
                redirectUri = await this.ValidateClientAndRedirectionUriAsync();
            }
            catch (BadRequestException t)
            {
                return this.BadRequest(t.Message);
            }

            var externalSignInData = ExternalSignInData.FromIdentity(this.User.Identity as ClaimsIdentity);

            if (externalSignInData == null)
            {
                return this.InternalServerError();
            }

            if (externalSignInData.Provider != provider)
            {
                this.Request.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);
                return new ChallengeResult(provider, this.Request);
            }

            var user = await this.authenticationRepository.FindExternalUserAsync(externalSignInData.Provider, externalSignInData.ProviderKey);

            bool hasRegistered = user != null;

            redirectUri = string.Format(
                "{0}#externalAccessToken={1}&provider={2}&hasLocalAccount={3}&externalUsername={4}",
                redirectUri,
                externalSignInData.ExternalAccessToken,
                externalSignInData.Provider,
                hasRegistered.ToString(),
                externalSignInData.Username);

            return this.Redirect(redirectUri);
        }
        
        private async Task<string> ValidateClientAndRedirectionUriAsync()
        {
            Uri redirectUri;

            var redirectUriString = this.GetQueryString(this.Request, "redirect_uri");

            if (string.IsNullOrWhiteSpace(redirectUriString))
            {
                throw new BadRequestException("redirect_uri is required");
            }

            bool validUri = Uri.TryCreate(redirectUriString, UriKind.Absolute, out redirectUri);

            if (!validUri)
            {
                throw new BadRequestException("redirect_uri is invalid");
            }

            var clientId = this.GetQueryString(this.Request, "client_id");

            if (string.IsNullOrWhiteSpace(clientId))
            {
                throw new BadRequestException("client_Id is required");
            }

            var client = await this.clientRepository.TryGetClientAsync(clientId);

            if (client == null)
            {
                throw new BadRequestException(string.Format("Client_id '{0}' is not registered in the system.", clientId));
            }

            if (!string.Equals(client.AllowedOrigin, redirectUri.GetLeftPart(UriPartial.Authority), StringComparison.OrdinalIgnoreCase))
            {
                throw new BadRequestException(string.Format("The given URL is not allowed by Client_id '{0}' configuration.", clientId));
            }

            return redirectUri.AbsoluteUri;
        }

        private string GetQueryString(HttpRequestMessage request, string key)
        {
            var queryStrings = request.GetQueryNameValuePairs();

            if (queryStrings == null)
            {
                return null;
            }

            var match = queryStrings.FirstOrDefault(keyValue => string.Compare(keyValue.Key, key, true) == 0);

            if (string.IsNullOrEmpty(match.Value))
            {
                return null;
            }

            return match.Value;
        }


        private JObject GenerateLocalAccessTokenResponse(string username)
        {
            var tokenExpiration = TimeSpan.FromDays(1);

            var identity = new ClaimsIdentity(OAuthDefaults.AuthenticationType);

            identity.AddClaim(new Claim(ClaimTypes.Name, username));
            identity.AddClaim(new Claim("role", "user"));

            var props = new AuthenticationProperties()
            {
                IssuedUtc = DateTime.UtcNow,
                ExpiresUtc = DateTime.UtcNow.Add(tokenExpiration),
            };

            var ticket = new AuthenticationTicket(identity, props);

            var accessToken = OAuthConfig.OAuthBearerOptions.AccessTokenFormat.Protect(ticket);

            var tokenResponse = new JObject(
                new JProperty("username", username),
                new JProperty("access_token", accessToken),
                new JProperty("token_type", "bearer"),
                new JProperty("expires_in", tokenExpiration.TotalSeconds.ToString()),
                new JProperty(".issued", ticket.Properties.IssuedUtc.ToString()),
                new JProperty(".expires", ticket.Properties.ExpiresUtc.ToString()));

            return tokenResponse;
        }

        private class ExternalSignInData
        {
            public string Provider { get; set; }

            public string ProviderKey { get; set; }

            public string Username { get; set; }

            public string ExternalAccessToken { get; set; }

            public static ExternalSignInData FromIdentity(ClaimsIdentity identity)
            {
                if (identity == null)
                {
                    return null;
                }

                Claim providerKeyClaim = identity.FindFirst(ClaimTypes.NameIdentifier);

                if (providerKeyClaim == null
                    || string.IsNullOrEmpty(providerKeyClaim.Issuer)
                    || string.IsNullOrEmpty(providerKeyClaim.Value))
                {
                    return null;
                }

                if (providerKeyClaim.Issuer == ClaimsIdentity.DefaultIssuer)
                {
                    return null;
                }

                return new ExternalSignInData
                {
                    Provider = providerKeyClaim.Issuer,
                    ProviderKey = providerKeyClaim.Value,
                    Username = identity.FindFirstValue(ClaimTypes.Name),
                    ExternalAccessToken = identity.FindFirstValue("ExternalAccessToken"),
                };
            }
        }
    }
}
