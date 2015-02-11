namespace Fifthweek.Api
{
    using System;
    using System.Web.Http;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity;
    using Fifthweek.Api.Identity.OAuth;

    using Microsoft.AspNet.Identity;
    using Microsoft.Owin;
    using Microsoft.Owin.Security.Facebook;
    using Microsoft.Owin.Security.OAuth;

    using Owin;

    public static class OAuthConfig
    {
        public static readonly string FacebookAppId = "561913473930746";

        public static OAuthBearerAuthenticationOptions OAuthBearerOptions { get; private set; }

        public static FacebookAuthenticationOptions FacebookAuthenticationOptions { get; private set; }

        public static void Register(HttpConfiguration httpConfiguration, IAppBuilder app)
        {
            OAuthBearerOptions = new OAuthBearerAuthenticationOptions();
            app.UseOAuthBearerAuthentication(OAuthBearerOptions);

            var authorizationServerProvider = httpConfiguration.DependencyResolver.GetService<FifthweekAuthorizationServerProvider>();
            var refreshTokenProvider = httpConfiguration.DependencyResolver.GetService<FifthweekRefreshTokenProvider>();

            var serverOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = false,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = Constants.AccessTokenExpiryTime,
                Provider = authorizationServerProvider,
                RefreshTokenProvider = refreshTokenProvider,
            };

            app.UseOAuthAuthorizationServer(serverOptions);
        }
    }
}