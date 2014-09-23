namespace Dexter.Api
{
    using System;
    using System.Web.Http;

    using Dexter.Api.Providers;

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
            // Use a cookie to temporarily store information about 
            // a user signing in with a third party provider
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            OAuthBearerOptions = new OAuthBearerAuthenticationOptions();
            app.UseOAuthBearerAuthentication(OAuthBearerOptions);

            FacebookAuthenticationOptions = new FacebookAuthenticationOptions()
            {
                AppId = FacebookAppId,
                AppSecret = "269c38dc36e569a435e4f0abd3f72b78",
                Provider = new DexterFacebookAuthenticationProvider(),
            };
            app.UseFacebookAuthentication(FacebookAuthenticationOptions);

            var authorizationServerProvider = httpConfiguration.DependencyResolver.GetService<DexterAuthorizationServerProvider>();
            var refreshTokenProvider = httpConfiguration.DependencyResolver.GetService<DexterRefreshTokenProvider>();

            var serverOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = false,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(30),
                Provider = authorizationServerProvider,
                RefreshTokenProvider = refreshTokenProvider,
            };

            app.UseOAuthAuthorizationServer(serverOptions);
        }
    }
}