namespace Fifthweek.Api.Collections.Shared
{
    using System;
    using System.Threading.Tasks;

    public interface IDefragmentQueueDbStatement
    {
        Task ExecuteAsync(CollectionId collectionId, WeeklyReleaseSchedule weeklyReleaseSchedule, DateTime now);
    }
}