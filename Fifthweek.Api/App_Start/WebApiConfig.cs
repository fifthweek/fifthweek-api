namespace Fifthweek.Api
{
    using System.Linq;
    using System.Net.Http.Formatting;
    using System.Web.Http;

    using Fifthweek.Api.Core;

    using Newtonsoft.Json.Serialization;

    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Filters.Add(new RequireHttpsAttribute());
            config.Filters.Add(new ValidateModelAttribute());
            config.Filters.Add(new ConvertExceptionsToResponsesAttribute());

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes
                .MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });

            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            jsonFormatter.SerializerSettings.Converters.Add(new GuidJsonConverter());
        }
    }
}
