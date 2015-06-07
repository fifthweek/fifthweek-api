namespace Fifthweek.WebJobs.Snapshots
{
    using System.Threading;
    using System.Threading.Tasks;

    using Fifthweek.Api.Persistence;
    using Fifthweek.Azure;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Services;
    using Fifthweek.WebJobs.Shared;

    [AutoConstructor]
    public partial class SnapshotProcessor : ISnapshotProcessor
    {
        private readonly IFifthweekDbConnectionFactory connectionFactory;

        public Task CreateThumbnailSetAsync(
            CreateSnapshotMessage message,
            ICloudStorageAccount cloudStorageAccount,
            ILogger logger,
            CancellationToken cancellationToken)
        {
            return Task.FromResult(0);
        }


        public Task CreatePoisonThumbnailSetAsync(
            CreateSnapshotMessage message,
            ICloudStorageAccount cloudStorageAccount,
            ILogger logger,
            CancellationToken cancellationToken)
        {
            return Task.FromResult(0);
        }
    }
}