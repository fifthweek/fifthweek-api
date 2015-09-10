namespace Fifthweek.Api.Blogs.Queries
{
    using System.Collections.Generic;

    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.CodeGeneration;

    [AutoConstructor]
    public partial class QueueResult
    {
        public QueueId QueueId { get; private set; }

        public string Name { get; private set; }

        public IReadOnlyList<HourOfWeek> WeeklyReleaseSchedule { get; private set; }
    }
}