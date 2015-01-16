namespace Fifthweek.Webjobs.Files
{
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

    using Fifthweek.Webjobs.Files.Shared;
    using Fifthweek.Webjobs.Thumbnails.Shared;

    using Microsoft.Azure.WebJobs;

    public interface IFileProcessor
    {
        Task ProcessFileAsync(
            ProcessFileMessage message,
            IBinder binder,
            TextWriter logger,
            CancellationToken cancellationToken);
    }
}