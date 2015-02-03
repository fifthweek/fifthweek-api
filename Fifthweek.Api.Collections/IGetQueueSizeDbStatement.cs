namespace Fifthweek.Api.Collections
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Collections.Shared;

    public interface IGetQueueSizeDbStatement
    {
        Task<int> ExecuteAsync(CollectionId collectionId, DateTime now);
    }
}