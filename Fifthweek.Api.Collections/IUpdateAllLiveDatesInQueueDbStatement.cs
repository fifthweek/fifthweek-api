namespace Fifthweek.Api.Collections
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Fifthweek.Api.Collections.Shared;

    public interface IUpdateAllLiveDatesInQueueDbStatement
    {
        Task ExecuteAsync(QueueId queueId, IReadOnlyList<DateTime> ascendingLiveDates, DateTime now);
    }
}