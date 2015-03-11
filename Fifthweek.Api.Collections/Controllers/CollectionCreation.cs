namespace Fifthweek.Api.Collections.Controllers
{
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers]
    public partial class CollectionCreation
    {
        public CollectionId CollectionId { get; private set; }

        public HourOfWeek DefaultWeeklyReleaseTime { get; private set; }
    }
}