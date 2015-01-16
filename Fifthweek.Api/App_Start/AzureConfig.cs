namespace Fifthweek.Api
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web.Hosting;
    using System.Web.Http;

    using Fifthweek.Api.Azure;
    using Fifthweek.Azure;

    using Microsoft.WindowsAzure.Storage.Shared.Protocol;

    public class AzureConfig
    {
        public static void Register()
        {
            HostingEnvironment.QueueBackgroundWorkItem(ct => ConfigureCors());
        }

        private static async Task ConfigureCors()
        {
            try
            {
                var storageAccount = (ICloudStorageAccount)GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(ICloudStorageAccount));
                var blobClient = storageAccount.CreateCloudBlobClient();

                var blobServiceProperties = await blobClient.GetServicePropertiesAsync();
                ConfigureCors(blobServiceProperties);
                await blobClient.SetServicePropertiesAsync(blobServiceProperties);
            }
            catch (Exception t)
            {
                ExceptionHandlerUtilities.ReportExceptionAsync(t);
            }
        }

        private static void ConfigureCors(ServiceProperties serviceProperties)
        {
            serviceProperties.Cors = new CorsProperties();
            serviceProperties.Cors.CorsRules.Add(new CorsRule()
            {
                AllowedHeaders = new List<string>() { "*" },
                AllowedMethods = CorsHttpMethods.Put | CorsHttpMethods.Get | CorsHttpMethods.Head | CorsHttpMethods.Post,
                AllowedOrigins = new List<string>() { "*" },
                ExposedHeaders = new List<string>() { "*" },
                MaxAgeInSeconds = 86400 // 24 hours
            });
        }
    }
}