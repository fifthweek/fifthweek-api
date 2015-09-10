namespace Fifthweek.Api.Collections
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Collections.Shared;

    public interface IUpdateWeeklyReleaseScheduleDbStatement
    {
        Task ExecuteAsync(QueueId queueId, WeeklyReleaseSchedule weeklyReleaseSchedule, DateTime now);
    }
}