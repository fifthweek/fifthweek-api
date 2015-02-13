[assembly: Microsoft.Owin.OwinStartup(typeof(Fifthweek.Api.Startup))]

namespace Fifthweek.Api
{
    using System;
    using System.Web.Http;

    using Fifthweek.Api.Persistence;

    using Owin;

    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            try
            {
                var httpConfiguration = new HttpConfiguration();

                IdentityConfig.Register(app);
                DapperConfig.Register();
                AutofacConfig.Register(httpConfiguration, app);
                AzureConfig.Register();
                OAuthConfig.Register(httpConfiguration, app);
                DatabaseConfig.Register();
                WebApiConfig.Register(httpConfiguration);

                app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
                app.UseWebApi(httpConfiguration);
            }
            catch (Exception t)
            {
                ExceptionHandlerUtilities.ReportExceptionAsync(t, null);
                throw;
            }
        }
    }
}