namespace Fifthweek.Api.Collections.Shared
{
    using System.Threading.Tasks;

    public interface IGetWeeklyReleaseScheduleDbStatement
    {
        Task<WeeklyReleaseSchedule> ExecuteAsync(CollectionId collectionId);
    }
}