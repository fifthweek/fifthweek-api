namespace Fifthweek.Api.Identity.Membership.Controllers
{
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;

    [AutoEqualityMembers]
    public partial class FeedbackData
    {
        [Parsed(typeof(ValidComment))]
        public string Message { get; set; }

        [Optional, Parsed(typeof(ValidEmail))]
        public string Email { get; set; }
    }
}