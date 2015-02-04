namespace Fifthweek.Api.Collections
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Collections.Shared;

    public interface IReplaceWeeklyReleaseTimesDbStatement
    {
        Task ExecuteAsync(CollectionId collectionId, WeeklyReleaseSchedule weeklyReleaseSchedule);
    }
}