namespace Fifthweek.WebJobs.Snapshots
{
    using System.Threading;
    using System.Threading.Tasks;

    using Fifthweek.Azure;
    using Fifthweek.Payments.Services;
    using Fifthweek.WebJobs.Shared;

    public interface ISnapshotProcessor
    {
        Task CreateThumbnailSetAsync(
            CreateSnapshotMessage message,
            ICloudStorageAccount cloudStorageAccount,
            ILogger logger,
            CancellationToken cancellationToken);

        Task CreatePoisonThumbnailSetAsync(
            CreateSnapshotMessage message,
            ICloudStorageAccount cloudStorageAccount,
            ILogger logger,
            CancellationToken cancellationToken);
    }
}