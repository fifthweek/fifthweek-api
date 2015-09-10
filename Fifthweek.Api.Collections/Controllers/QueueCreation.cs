namespace Fifthweek.Api.Collections.Controllers
{
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers]
    public partial class QueueCreation
    {
        public QueueId QueueId { get; private set; }

        public HourOfWeek DefaultWeeklyReleaseTime { get; private set; }
    }
}