namespace Fifthweek.GarbageCollection
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Azure;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using Fifthweek.WebJobs.Shared;

    using Microsoft.WindowsAzure.Storage.Blob;

    [AutoConstructor]
    public partial class DeleteOrphanedBlobContainers : IDeleteOrphanedBlobContainers
    {
        private readonly IGetAllChannelIdsDbStatement getAllChannelIds;
        private readonly ICloudStorageAccount cloudStorageAccount;

        public async Task ExecuteAsync(ILogger logger, IKeepAliveHandler keepAliveHandler, DateTime endTimeExclusive, CancellationToken cancellationToken)
        {
            logger.AssertNotNull("logger");
            keepAliveHandler.AssertNotNull("keepAliveHandler");

            var channelIds = await this.getAllChannelIds.ExecuteAsync();

            var channelIdsHashSet = new HashSet<Guid>(channelIds.Select(v => v.Value));

            // Delete blob containers that parse as Guids but don't correspond to any channel id.
            var blobClient = this.cloudStorageAccount.CreateCloudBlobClient();

            BlobContinuationToken token = null;
            do
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    break;
                }

                var segment = await blobClient.ListContainersSegmentedAsync(token);
                foreach (var container in segment.Results)
                {
                    Console.Write(".");
                    
                    if (cancellationToken.IsCancellationRequested)
                    {
                        break;
                    }

                    await keepAliveHandler.KeepAliveAsync();

                    Guid channelIdGuid;
                    if (!Guid.TryParse(container.Name, out channelIdGuid))
                    {
                        continue;
                    }
                 
                    if (channelIdsHashSet.Contains(channelIdGuid))
                    {
                        continue;
                    }

                    if (container.Properties.LastModified == null)
                    {
                        logger.Warn("Skipping container {0} because last modified date was unavailable", container.Name);
                        continue;
                    }

                    var lastModified = container.Properties.LastModified.Value.UtcDateTime;
                    if (lastModified >= endTimeExclusive)
                    {
                        continue;
                    }

                    await container.DeleteAsync();
                }

                token = segment.ContinuationToken;
            }
            while (token != null);
        }
    }
}