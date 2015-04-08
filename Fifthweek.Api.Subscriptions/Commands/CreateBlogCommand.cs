namespace Fifthweek.Api.Subscriptions.Commands
{
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Subscriptions.Shared;
    using Fifthweek.CodeGeneration;

    [AutoEqualityMembers, AutoConstructor]
    public partial class CreateBlogCommand
    {
        public Requester Requester { get; private set; }
        
        public BlogId NewBlogId { get; private set; }

        public ValidBlogName BlogName { get; private set; }

        public ValidTagline Tagline { get; private set; }

        public ValidChannelPriceInUsCentsPerWeek BasePrice { get; private set; }
    }
}