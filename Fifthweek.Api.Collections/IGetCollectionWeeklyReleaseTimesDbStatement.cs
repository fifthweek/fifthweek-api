namespace Fifthweek.Api.Collections
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Fifthweek.Api.Persistence;

    /// <remarks>
    /// Entities are sorted ascending by: DayOfWeek, TimeOfDay.
    /// Result will contain at least one element.
    /// </remarks>
    public interface IGetCollectionWeeklyReleaseTimesDbStatement
    {
        Task<IReadOnlyList<WeeklyReleaseTime>> ExecuteAsync(Shared.CollectionId collectionId);
    }
}