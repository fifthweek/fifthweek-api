namespace Fifthweek.WebJobs.GarbageCollection
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using Fifthweek.CodeGeneration;
    using Fifthweek.GarbageCollection;
    using Fifthweek.WebJobs.GarbageCollection.Shared;
    using Fifthweek.WebJobs.Shared;

    [AutoConstructor]
    public partial class GarbageCollectionProcessor
    {
        private readonly IRunGarbageCollection runGarbageCollection;

        public async Task RunGarbageCollectionAsync(
            RunGarbageCollectionMessage message,
            ILogger logger,
            CancellationToken cancellationToken)
        {
            try
            {
                await this.runGarbageCollection.ExecuteAsync(logger, cancellationToken);
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