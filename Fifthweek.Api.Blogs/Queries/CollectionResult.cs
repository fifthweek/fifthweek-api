namespace Fifthweek.Api.Blogs.Queries
{
    using System.Collections.Generic;

    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.CodeGeneration;

    [AutoConstructor]
    public partial class CollectionResult
    {
        public CollectionId CollectionId { get; private set; }

        public string Name { get; private set; }

        public IReadOnlyList<HourOfWeek> WeeklyReleaseSchedule { get; private set; }
    }
}