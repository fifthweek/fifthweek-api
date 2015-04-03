namespace Fifthweek.WebJobs.Thumbnails
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using Fifthweek.Azure;
    using Fifthweek.CodeGeneration;
    using Fifthweek.WebJobs.Shared;
    using Fifthweek.WebJobs.Thumbnails.Shared;

    [AutoConstructor]
    public partial class LoggingThumbnailProcessorWrapper : ILoggingThumbnailProcessorWrapper
    {
        private readonly IThumbnailProcessor thumbnailProcessor;

        private readonly ISetFileProcessingCompleteDbStatement setFileProcessingComplete;

        public async Task CreateThumbnailSetAsync(
            CreateThumbnailsMessage message,
            ICloudBlockBlob input,
            ICloudStorageAccount cloudStorageAccount,
            ILogger logger,
            int dequeueCount,
            CancellationToken cancellationToken)
        {
            try
            {
                var startProcessingTime = DateTime.UtcNow;
                var result = await this.thumbnailProcessor.CreateThumbnailSetAsync(
                    message,
                    input,
                    cloudStorageAccount,
                    logger,
                    cancellationToken);
                var completeProcessingTime = DateTime.UtcNow;

                if (result != null)
                {
                    await this.setFileProcessingComplete.ExecuteAsync(
                        message.FileId,
                        dequeueCount,
                        startProcessingTime,
                        completeProcessingTime,
                        result.RenderWidth,
                        result.RenderHeight);
                }
            }
            catch (Exception t)
            {
                logger.Error(t);
                throw;
            }
        }

        public async Task CreatePoisonThumbnailSetAsync(
            CreateThumbnailsMessage message,
            ICloudStorageAccount cloudStorageAccount,
            ILogger logger,
            int dequeueCount,
            CancellationToken cancellationToken)
        {
            try
            {
                logger.Warn("Failed to resize image from blob {0}/{1}", message.ContainerName, message.InputBlobName);
                var startProcessingTime = DateTime.UtcNow;
                await this.thumbnailProcessor.CreatePoisonThumbnailSetAsync(
                    message,
                    cloudStorageAccount,
                    logger,
                    cancellationToken);
                var completeProcessingTime = DateTime.UtcNow;

                await this.setFileProcessingComplete.ExecuteAsync(
                    message.FileId,
                    dequeueCount * -1,
                    startProcessingTime,
                    completeProcessingTime,
                    1,
                    1);
            }
            catch (Exception t)
            {
                logger.Error(t);
                throw;
            }
        }
    }
}