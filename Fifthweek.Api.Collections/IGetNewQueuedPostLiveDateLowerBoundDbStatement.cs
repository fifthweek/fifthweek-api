namespace Fifthweek.Api.Collections
{
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Gets the exclusive lower bound for a hypothetical new post's live date when queued in the given collection.
    /// </summary>
    public interface IGetNewQueuedPostLiveDateLowerBoundDbStatement
    {
        Task<DateTime> ExecuteAsync(Shared.QueueId queueId, DateTime now);
    }
}