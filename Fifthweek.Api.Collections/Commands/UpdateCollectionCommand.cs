namespace Fifthweek.Api.Collections.Commands
{
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers]
    public partial class UpdateCollectionCommand
    {
        public Requester Requester { get; private set; }

        public CollectionId CollectionId { get; private set; }

        public ValidCollectionName Name { get; private set; }

        public WeeklyReleaseSchedule WeeklyReleaseSchedule { get; private set; } 
    }
}