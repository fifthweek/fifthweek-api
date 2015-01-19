namespace Fifthweek.WebJobs.Files
{
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

    using Fifthweek.WebJobs.Files.Shared;
    using Fifthweek.WebJobs.Shared;

    using Microsoft.Azure.WebJobs;

    public interface IFileProcessor
    {
        Task ProcessFileAsync(
            ProcessFileMessage message,
            IBinder binder,
            ILogger logger,
            CancellationToken cancellationToken);
    }
}