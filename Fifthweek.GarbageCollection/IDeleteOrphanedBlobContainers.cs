namespace Fifthweek.GarbageCollection
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using Fifthweek.Azure;
    using Fifthweek.WebJobs.Shared;

    public interface IDeleteOrphanedBlobContainers
    {
        Task ExecuteAsync(ILogger logger, IKeepAliveHandler keepAliveHandler, DateTime endTimeExclusive, CancellationToken cancellationToken);
    }
}