[assembly: Microsoft.Owin.OwinStartup(typeof(Dexter.Api.Startup))]
namespace Dexter.Api
{
    using System;
    using System.Data.Entity;
    using System.Reflection;
    using System.Web.Http;

    using Autofac;
    using Autofac.Integration.WebApi;

    using Dexter.Api.Providers;

    using Microsoft.Owin;
    using Microsoft.Owin.Logging;
    using Microsoft.Owin.Security.Facebook;
    using Microsoft.Owin.Security.OAuth;

    using Owin;

    public class Startup
    {
        public static OAuthBearerAuthenticationOptions OAuthBearerOptions { get; private set; }
        
        public static FacebookAuthenticationOptions FacebookAuthenticationOptions { get; private set; }
 
        public void Configuration(IAppBuilder app)
        {
            this.ConfigureAutofac();
            this.ConfigureOAuth(app);
            this.ConfigureDatabase();

            var config = new HttpConfiguration();
            WebApiConfig.Register(config);
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseWebApi(config);
        }

        private void ConfigureDatabase()
        {
            Database.SetInitializer(new DatabaseInitializer());
        }

        private void ConfigureAutofac()
        {
            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // Register other dependencies.
            ////builder.Register(c => new Logger()).As<ILogger>().InstancePerApiRequest();

            var container = builder.Build();

            var resolver = new AutofacWebApiDependencyResolver(container);
            GlobalConfiguration.Configuration.DependencyResolver = resolver;
        }

        public void ConfigureOAuth(IAppBuilder app)
        {
            // Use a cookie to temporarily store information about 
            // a user signing in with a third party provider
            app.UseExternalSignInCookie(Microsoft.AspNet.Identity.DefaultAuthenticationTypes.ExternalCookie);
            
            OAuthBearerOptions = new OAuthBearerAuthenticationOptions();
            app.UseOAuthBearerAuthentication(OAuthBearerOptions);

            FacebookAuthenticationOptions = new FacebookAuthenticationOptions()
            {
                AppId = "561913473930746",
                AppSecret = "269c38dc36e569a435e4f0abd3f72b78",
                Provider = new DexterFacebookAuthenticationProvider(),
            };
            app.UseFacebookAuthentication(FacebookAuthenticationOptions);

            var serverOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = false,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(30),
                Provider = new SimpleAuthorizationServerProvider(),
                RefreshTokenProvider = new SimpleRefreshTokenProvider(),
            };

            app.UseOAuthAuthorizationServer(serverOptions);
        }
    }
}