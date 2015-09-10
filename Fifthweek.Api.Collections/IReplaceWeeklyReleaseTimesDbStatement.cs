namespace Fifthweek.Api.Collections
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Collections.Shared;

    public interface IReplaceWeeklyReleaseTimesDbStatement
    {
        Task ExecuteAsync(QueueId queueId, WeeklyReleaseSchedule weeklyReleaseSchedule);
    }
}