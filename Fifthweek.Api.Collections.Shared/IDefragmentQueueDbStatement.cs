namespace Fifthweek.Api.Collections.Shared
{
    using System;
    using System.Threading.Tasks;

    public interface IDefragmentQueueDbStatement
    {
        Task ExecuteAsync(QueueId queueId, WeeklyReleaseSchedule weeklyReleaseSchedule, DateTime now);
    }
}