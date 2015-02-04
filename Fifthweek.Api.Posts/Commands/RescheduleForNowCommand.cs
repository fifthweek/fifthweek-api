namespace Fifthweek.Api.Posts.Commands
{
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers]
    public partial class RescheduleForNowCommand
    {
        public Requester Requester { get; private set; }

        public PostId PostId { get; private set; } 
    }
}