namespace Fifthweek.GarbageCollection
{
    using System.Threading;
    using System.Threading.Tasks;

    using Fifthweek.WebJobs.Shared;

    public interface IRunGarbageCollection
    {
        Task ExecuteAsync(ILogger logger, CancellationToken cancellationToken);
    }
}