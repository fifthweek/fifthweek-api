namespace Fifthweek.Api.Collections.Shared
{
    using System;
    using System.Threading.Tasks;

    public interface IGetLiveDateOfNewQueuedPostDbStatement
    {
        Task<DateTime> ExecuteAsync(Shared.CollectionId collectionId);
    }
}