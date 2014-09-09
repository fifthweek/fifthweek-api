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

    using Dexter.Api.Models;
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
        private readonly AuthenticationRepository repository = null;

        public AccountController()
        {
            this.repository = new AuthenticationRepository();
        }

        // POST api/Account/RegisterInternalUser
        [RequireHttps]
        [AllowAnonymous]
        [Route("RegisterInternalUser")]
        public async Task<IHttpActionResult> RegisterInternalUser(InternalRegistrationData userModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var result = await this.repository.RegisterUser(userModel);

            var errorResult = this.GetErrorResult(result);

            if (errorResult != null)
            {
                return errorResult;
            }

            return this.Ok();
        }

        // POST api/Account/RegisterExternalUser
        [RequireHttps]
        [AllowAnonymous]
        [Route("RegisterExternalUser")]
        public async Task<IHttpActionResult> RegisterExternalUser(ExternalRegistrationData model)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var verifiedAccessToken = await this.VerifyExternalAccessToken(model.Provider, model.ExternalAccessToken);
            if (verifiedAccessToken == null)
            {
                return this.BadRequest("Invalid Provider or External Access Token");
            }

            var user = await this.repository.FindAsync(new SignInData(model.Provider, verifiedAccessToken.UserId));

            bool hasRegistered = user != null;

            if (hasRegistered)
            {
                return this.BadRequest("External user is already registered");
            }

            user = new IdentityUser() { UserName = model.Username };

            var result = await this.repository.CreateAsync(user);
            if (!result.Succeeded)
            {
                return this.GetErrorResult(result);
            }

            var signInData = new SignInData(model.Provider, verifiedAccessToken.UserId);

            result = await this.repository.AddUserSignInDataAsync(user.Id, signInData);
            if (!result.Succeeded)
            {
                return this.GetErrorResult(result);
            }

            var accessTokenResponse = this.GenerateLocalAccessTokenResponse(model.Username);

            return this.Ok(accessTokenResponse);
        }

        // GET api/Account/InitiateExternalSignIn
        [RequireHttps]
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

            var redirectUriValidationResult = this.ValidateClientAndRedirectUri(ref redirectUri);

            if (!string.IsNullOrWhiteSpace(redirectUriValidationResult))
            {
                return this.BadRequest(redirectUriValidationResult);
            }

            var externalSignInData = ExternalSignInData.FromIdentity(this.User.Identity as ClaimsIdentity);

            if (externalSignInData == null)
            {
                return this.InternalServerError();
            }

            if (externalSignInData.Provider != provider)
            {
                this.GetAuthenticationManager().SignOut(DefaultAuthenticationTypes.ExternalCookie);
                return new ChallengeResult(provider, this.Request);
            }

            var user = await this.repository.FindAsync(new SignInData(externalSignInData.Provider, externalSignInData.ProviderKey));

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

        [RequireHttps]
        [AllowAnonymous]
        [HttpGet]
        [Route("ObtainAccessTokenForExternalUser")]
        public async Task<IHttpActionResult> ObtainAccessTokenForExternalUser(string provider, string externalAccessToken)
        {
            if (string.IsNullOrWhiteSpace(provider) || string.IsNullOrWhiteSpace(externalAccessToken))
            {
                return this.BadRequest("Provider or external access token is not sent");
            }

            var verifiedAccessToken = await this.VerifyExternalAccessToken(provider, externalAccessToken);
            if (verifiedAccessToken == null)
            {
                return this.BadRequest("Invalid Provider or External Access Token");
            }

            IdentityUser user = await this.repository.FindAsync(new SignInData(provider, verifiedAccessToken.UserId));

            bool hasRegistered = user != null;

            if (!hasRegistered)
            {
                return this.BadRequest("External user is not registered");
            }

            var accessTokenResponse = this.GenerateLocalAccessTokenResponse(user.UserName);

            return this.Ok(accessTokenResponse);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.repository.Dispose();
            }

            base.Dispose(disposing);
        }

        private IAuthenticationManager GetAuthenticationManager()
        {
            return Request.GetOwinContext().Authentication;
        }   

        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return this.InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        this.ModelState.AddModelError(string.Empty, error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return this.BadRequest();
                }

                return this.BadRequest(this.ModelState);
            }

            return null;
        }

        private string ValidateClientAndRedirectUri(ref string redirectUriOutput)
        {
            Uri redirectUri;

            var redirectUriString = this.GetQueryString(this.Request, "redirect_uri");

            if (string.IsNullOrWhiteSpace(redirectUriString))
            {
                return "redirect_uri is required";
            }

            bool validUri = Uri.TryCreate(redirectUriString, UriKind.Absolute, out redirectUri);

            if (!validUri)
            {
                return "redirect_uri is invalid";
            }

            var clientId = this.GetQueryString(this.Request, "client_id");

            if (string.IsNullOrWhiteSpace(clientId))
            {
                return "client_Id is required";
            }

            var client = this.repository.FindClient(clientId);

            if (client == null)
            {
                return string.Format("Client_id '{0}' is not registered in the system.", clientId);
            }

            if (!string.Equals(client.AllowedOrigin, redirectUri.GetLeftPart(UriPartial.Authority), StringComparison.OrdinalIgnoreCase))
            {
                return string.Format("The given URL is not allowed by Client_id '{0}' configuration.", clientId);
            }

            redirectUriOutput = redirectUri.AbsoluteUri;

            return string.Empty;
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

        private async Task<ParsedExternalAccessToken> VerifyExternalAccessToken(string provider, string accessToken)
        {
            ParsedExternalAccessToken parsedToken = null;

            string verifyTokenEndPoint;

            if (provider == "Facebook")
            {
                // You can get it from here: https://developers.facebook.com/tools/accesstoken/
                // More about debug_tokn here: http://stackoverflow.com/questions/16641083/how-does-one-get-the-app-access-token-for-debug-token-inspection-on-facebook
                const string AppToken = "561913473930746|O1q3gr_5LfSgVyJy4aR1eWH0nEk";
                verifyTokenEndPoint = string.Format(@"https://graph.facebook.com/debug_token?input_token={0}&access_token={1}", accessToken, AppToken);
            }
            else
            {
                return null;
            }

            var client = new HttpClient();
            var uri = new Uri(verifyTokenEndPoint);
            var response = await client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                dynamic jsonObject = Newtonsoft.Json.JsonConvert.DeserializeObject(content);

                parsedToken = new ParsedExternalAccessToken();

                if (provider == "Facebook")
                {
                    parsedToken.UserId = jsonObject["data"]["user_id"];
                    parsedToken.ApplicationId = jsonObject["data"]["app_id"];

                    if (!string.Equals(Startup.FacebookAuthenticationOptions.AppId, parsedToken.ApplicationId, StringComparison.OrdinalIgnoreCase))
                    {
                        return null;
                    }
                }
            }

            return parsedToken;
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

            var accessToken = Startup.OAuthBearerOptions.AccessTokenFormat.Protect(ticket);

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
