namespace Fifthweek.Api.Identity.Membership.Commands
{
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers]
    public partial class SubmitFeedbackCommand
    {
        public ValidComment Message { get; private set; }

        [Optional]
        public ValidEmail Email { get; private set; }
    }
}