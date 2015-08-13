namespace Fifthweek.WebJobs.Payments
{
    using System;
    using System.Data.SqlClient;
    using System.Threading;
    using System.Threading.Tasks;

    using Fifthweek.Api.Persistence;
    using Fifthweek.Azure;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.SnapshotCreation;
    using Fifthweek.WebJobs.Shared;

    [AutoConstructor]
    public partial class SnapshotProcessor : ISnapshotProcessor
    {
        private readonly ICreateSnapshotMultiplexer createSnapshotMultiplexer;
        private readonly ICreateAllSnapshotsProcessor createAllSnapshots;

        public async Task CreateSnapshotAsync(
            CreateSnapshotMessage message,
            ILogger logger,
            CancellationToken cancellationToken)
        {
            try
            {
                if (message.UserId == null)
                {
                    await this.createAllSnapshots.ExecuteAsync();
                }
                else
                {
                    await this.createSnapshotMultiplexer.ExecuteAsync(message.UserId, message.SnapshotType);
                }
            }
            catch (SqlException t)
            {
                // 2627 indicates a primary key violation, which means two snapshots
                // occured at the same time. It will be automatically retried
                // so no need to log this.
                if (t.Number != 2627)
                {
                    logger.Error(t);
                }

                throw;
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
            logger.Warn("Failed to create snapshot for message: {0}", message);
            return Task.FromResult(0);
        }
    }
}