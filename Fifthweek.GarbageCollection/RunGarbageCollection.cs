namespace Fifthweek.GarbageCollection
{
    using System;
    using System.Diagnostics;
    using System.Threading;
    using System.Threading.Tasks;

    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using Fifthweek.WebJobs.Shared;

    [AutoConstructor]
    public partial class RunGarbageCollection : IRunGarbageCollection
    {
        private readonly ITimestampCreator timestampCreator;
        private readonly IDeleteTestUserAccountsDbStatement deleteTestUserAccounts;
        private readonly IGetFilesEligibleForGarbageCollectionDbStatement getFilesEligibleForGarbageCollection;
        private readonly IDeleteBlobsForFile deleteBlobsForFile;
        private readonly IDeleteFileDbStatement deleteFileDbStatement;
        private readonly IDeleteOrphanedBlobContainers deleteOrphanedBlobContainers;

        public async Task ExecuteAsync(ILogger logger, CancellationToken cancellationToken)
        {
            logger.AssertNotNull("logger");

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var endTimeExclusive = this.timestampCreator.Now().Subtract(Shared.Constants.GarbageCollectionMinimumAge);

            logger.Info("Deleting test user accounts");
            await this.deleteTestUserAccounts.ExecuteAsync(endTimeExclusive);

            logger.Info("Deleting orphaned files");
            var files = await this.getFilesEligibleForGarbageCollection.ExecuteAsync(endTimeExclusive);

            foreach (var file in files)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    break;
                }

                try
                {
                    await this.deleteBlobsForFile.ExecuteAsync(file);
                    await this.deleteFileDbStatement.ExecuteAsync(file.FileId);
                }
                catch (Exception t)
                {
                    logger.Warn("Failed to delete orphaned file {0}.", file.FileId);
                    logger.Error(t);
                }
            }

            if (!cancellationToken.IsCancellationRequested)
            {
                logger.Info("Deleting orphaned blob containers");
                await this.deleteOrphanedBlobContainers.ExecuteAsync(logger, endTimeExclusive, cancellationToken);
            }

            stopwatch.Stop();
            logger.Info("Finished garbage collection in {0}s", stopwatch.Elapsed.TotalSeconds);
        }
    }
}
