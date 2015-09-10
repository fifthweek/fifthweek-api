namespace Fifthweek.Api.Blogs.Commands
{
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;

    [AutoEqualityMembers, AutoConstructor]
    public partial class CreateBlogCommand
    {
        public Requester Requester { get; private set; }
        
        public BlogId NewBlogId { get; private set; }

        public ChannelId FirstChannelId { get; private set; }

        public ValidBlogName BlogName { get; private set; }
        
        public ValidIntroduction Introduction { get; private set; }

        public ValidChannelPrice BasePrice { get; private set; }
    }
}