namespace Fifthweek.WebJobs.GarbageCollection
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using Fifthweek.Azure;
    using Fifthweek.CodeGeneration;
    using Fifthweek.GarbageCollection;
    using Fifthweek.Shared;
    using Fifthweek.WebJobs.GarbageCollection.Shared;
    using Fifthweek.WebJobs.Shared;

    [AutoConstructor]
    public partial class GarbageCollectionProcessor
    {
        private readonly IRunGarbageCollection runGarbageCollection;
        private readonly IBlobLeaseFactory blobLeaseFactory;

        public async Task RunGarbageCollectionAsync(
            RunGarbageCollectionMessage message,
            ILogger logger,
            CancellationToken cancellationToken)
        {
            message.AssertNotNull("message");
            logger.AssertNotNull("logger");

            var lease = this.blobLeaseFactory.Create(Shared.Constants.LeaseObjectName, cancellationToken);
            try
            {
                if (await lease.TryAcquireLeaseAsync())
                {
                    await this.runGarbageCollection.ExecuteAsync(logger, lease, cancellationToken);
                    await lease.UpdateTimestampsAsync();
                    await lease.ReleaseLeaseAsync();
                }
            }
            catch (Exception t)
            {
                logger.Error(t);
                throw;
            }
        }

        public Task HandlePoisonMessageAsync(
            string message,
            ILogger logger,
            CancellationToken cancellationToken)
        {
            logger.Warn("Failed to run garbage collection.");
            return Task.FromResult(0);
        }
    }
}