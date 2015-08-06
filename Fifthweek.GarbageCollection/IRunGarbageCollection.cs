namespace Fifthweek.GarbageCollection
{
    using System.Threading;
    using System.Threading.Tasks;

    using Fifthweek.Azure;
    using Fifthweek.WebJobs.Shared;

    public interface IRunGarbageCollection
    {
        Task ExecuteAsync(ILogger logger, IKeepAliveHandler keepAliveHandler, CancellationToken cancellationToken);
    }
}