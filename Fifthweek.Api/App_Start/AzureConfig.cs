namespace Fifthweek.Api
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Web.Hosting;
    using System.Web.Http;

    using Fifthweek.Azure;

    using Microsoft.WindowsAzure.Storage.Shared.Protocol;

    using Constants = Fifthweek.Api.FileManagement.Shared.Constants;

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
                await CreateQueueIfNotExists(cloudQueueClient, Fifthweek.GarbageCollection.Shared.Constants.GarbageCollectionQueueName);
                await CreateQueueIfNotExists(cloudQueueClient, Payments.Shared.Constants.RequestSnapshotQueueName);
                await CreateQueueIfNotExists(cloudQueueClient, Payments.Shared.Constants.RequestProcessPaymentsQueueName);

                var cloudBlobClient = storageAccount.CreateCloudBlobClient();
                await CreateBlobContainerIfNotExists(cloudBlobClient, Payments.Shared.Constants.PaymentProcessingDataContainerName);
                await CreateBlobIfNotExists(cloudBlobClient, Fifthweek.Azure.Constants.AzureLeaseObjectsContainerName, Payments.Shared.Constants.ProcessPaymentsLeaseObjectName);
                await CreateBlobIfNotExists(cloudBlobClient, Fifthweek.Azure.Constants.AzureLeaseObjectsContainerName, WebJobs.GarbageCollection.Shared.Constants.LeaseObjectName);
                await CreateBlobContainerIfNotExists(cloudBlobClient, Constants.PublicFileBlobContainerName);
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

        private static async Task CreateBlobContainerIfNotExists(ICloudBlobClient cloudBlobClient, string containerName)
        {
            var container = cloudBlobClient.GetContainerReference(containerName);
            await container.CreateIfNotExistsAsync();
        }

        private static async Task CreateBlobIfNotExists(ICloudBlobClient cloudBlobClient, string containerName, string blobName)
        {
            var container = cloudBlobClient.GetContainerReference(containerName);
            await container.CreateIfNotExistsAsync();
            var blob = container.GetBlockBlobReference(blobName);
            if (await blob.ExistsAsync())
            {
                await blob.UploadTextAsync(string.Empty);
            }
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