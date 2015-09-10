namespace Fifthweek.Api.Collections.Commands
{
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers]
    public partial class UpdateQueueCommand
    {
        public Requester Requester { get; private set; }

        public QueueId QueueId { get; private set; }

        public ValidQueueName Name { get; private set; }

        public WeeklyReleaseSchedule WeeklyReleaseSchedule { get; private set; } 
    }
}