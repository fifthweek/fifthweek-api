namespace Fifthweek.WebJobs.Snapshots
{
    using System.Threading;
    using System.Threading.Tasks;

    using Fifthweek.Azure;
    using Fifthweek.Payments.Services;
    using Fifthweek.WebJobs.Shared;

    public interface ISnapshotProcessor
    {
        Task CreateSnapshotAsync(
            CreateSnapshotMessage message,
            ICloudStorageAccount cloudStorageAccount,
            ILogger logger,
            CancellationToken cancellationToken);

        Task HandlePoisonMessageAsync(
            string message,
            ICloudStorageAccount cloudStorageAccount,
            ILogger logger,
            CancellationToken cancellationToken);
    }
}