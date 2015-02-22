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
            HostingEnvironment.QueueBackgroundWorkItem(ct => ConfigureAsync());
        }

        private static async Task ConfigureAsync()
        {
            try
            {
                var storageAccount = (ICloudStorageAccount)GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(ICloudStorageAccount));

                await ConfigureStorage(storageAccount);
                await ConfigureCors(storageAccount);
            }
            catch (Exception t)
            {
                ExceptionHandlerUtilities.ReportExceptionAsync(t, null);
            }
        }

        private static async Task ConfigureStorage(ICloudStorageAccount storageAccount)
        {
            try
            {
                var cloudQueueClient = storageAccount.CreateCloudQueueClient();
                await CreateQueueIfNotExists(cloudQueueClient, WebJobs.Thumbnails.Shared.Constants.ThumbnailsQueueName);
                await CreateQueueIfNotExists(cloudQueueClient, WebJobs.GarbageCollection.Shared.Constants.GarbageCollectionQueueName);

                var cloudBlobClient = storageAccount.CreateCloudBlobClient();
                await CreateBlobIfNotExists(cloudBlobClient, FileManagement.Constants.PublicFileBlobContainerName);
            }
            catch (Exception t)
            {
                ExceptionHandlerUtilities.ReportExceptionAsync(t, null);
            }
        }

        private static async Task CreateQueueIfNotExists(ICloudQueueClient cloudQueueClient, string queueName)
        {
            var queue = cloudQueueClient.GetQueueReference(queueName);
            await queue.CreateIfNotExistsAsync();
        }

        private static async Task CreateBlobIfNotExists(ICloudBlobClient cloudBlobClient, string containerName)
        {
            var container = cloudBlobClient.GetContainerReference(containerName);
            await container.CreateIfNotExistsAsync();
        }

        private static async Task ConfigureCors(ICloudStorageAccount storageAccount)
        {
            try
            {
                var blobClient = storageAccount.CreateCloudBlobClient();

                var blobServiceProperties = await blobClient.GetServicePropertiesAsync();
                ConfigureCors(blobServiceProperties);
                await blobClient.SetServicePropertiesAsync(blobServiceProperties);
            }
            catch (Exception t)
            {
                ExceptionHandlerUtilities.ReportExceptionAsync(t, null);
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