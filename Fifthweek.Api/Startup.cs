[assembly: Microsoft.Owin.OwinStartup(typeof(Fifthweek.Api.Startup))]

namespace Fifthweek.Api
{
    using System;
    using System.Web.Http;
    using System.Web.Http.Dispatcher;

    using Fifthweek.Api.Core;

    using Owin;

    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            try
            {
                var httpConfiguration = new HttpConfiguration();

                AutofacConfig.Register(httpConfiguration, app);
                OAuthConfig.Register(httpConfiguration, app);
                DatabaseConfig.Register();
                WebApiConfig.Register(httpConfiguration);

                app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
                app.UseWebApi(httpConfiguration);
            }
            catch (Exception t)
            {
                ExceptionHandlerUtilities.ReportExceptionAsync(t);
                throw;
            }
        }
    }
}