namespace Fifthweek.GarbageCollection
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using Fifthweek.WebJobs.Shared;

    public interface IDeleteOrphanedBlobContainers
    {
        Task ExecuteAsync(ILogger logger, DateTime endTimeExclusive, CancellationToken cancellationToken);
    }
}