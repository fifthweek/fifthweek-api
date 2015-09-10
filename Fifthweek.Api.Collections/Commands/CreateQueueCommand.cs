namespace Fifthweek.Api.Collections.Commands
{
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers]
    public partial class CreateQueueCommand
    {
        public Requester Requester { get; private set; }

        public QueueId NewQueueId { get; private set; }

        public BlogId BlogId { get; private set; }

        public ValidQueueName Name { get; private set; }

        public HourOfWeek InitialWeeklyReleaseTime { get; private set; }
    }
}