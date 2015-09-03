namespace Fifthweek.Api.Posts.Queries
{
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Controllers;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers]
    public partial class GetCommentsQuery : IQuery<CommentsResult>
    {
        public Requester Requester { get; private set; }

        public PostId PostId { get; private set; }
    }
}