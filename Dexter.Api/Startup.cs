[assembly: Microsoft.Owin.OwinStartup(typeof(Dexter.Api.Startup))]
namespace Dexter.Api
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Reflection;
    using System.Web.Http;
    using System.Web.Http.Dependencies;

    using Autofac;
    using Autofac.Integration.WebApi;

    using Dexter.Api.Controllers;
    using Dexter.Api.Providers;
    using Dexter.Api.Repositories;

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
            var httpConfiguration = new HttpConfiguration();
            this.ConfigureAutofac(httpConfiguration);
            this.ConfigureOAuth(app);
            this.ConfigureDatabase();

            WebApiConfig.Register(httpConfiguration);
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseWebApi(httpConfiguration);
        }

        private void ConfigureDatabase()
        {
            Database.SetInitializer(new DatabaseInitializer());
        }

        private void ConfigureAutofac(HttpConfiguration httpConfiguration)
        {
            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterType<AuthenticationRepository>().As<IAuthenticationRepository>().InstancePerRequest();
            builder.RegisterModule<LogRequestModule>();

            var container = builder.Build();
            
            var resolver = new AutofacWebApiDependencyResolver(container);
            GlobalConfiguration.Configuration.DependencyResolver = httpConfiguration.DependencyResolver = resolver;
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