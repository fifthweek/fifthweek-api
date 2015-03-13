namespace Fifthweek.Api.Collections
{
    using System;
    using System.Threading.Tasks;

    public interface IGetQueueLowerBoundDbStatement
    {
        Task<DateTime> ExecuteAsync(Shared.CollectionId collectionId, DateTime now);
    }
}