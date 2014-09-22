[assembly: Microsoft.Owin.OwinStartup(typeof(Dexter.Api.Startup))]

namespace Dexter.Api
{
    using System.Web.Http;

    using Owin;

    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var httpConfiguration = new HttpConfiguration();

            AutofacConfig.Register(httpConfiguration, app);
            OAuthConfig.Register(httpConfiguration, app);
            DatabaseConfig.Register();
            WebApiConfig.Register(httpConfiguration);

            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseWebApi(httpConfiguration);
        }
    }
}