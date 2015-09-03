namespace Fifthweek.Api.Posts.Controllers
{
    using System;
    using System.Collections.Generic;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers]
    public partial class CommentsResult
    {
        public IReadOnlyList<Item> Comments { get; private set; }

        [AutoConstructor, AutoEqualityMembers]
        public partial class Item
        {
            public CommentId Id { get;  private set; }

            public PostId PostId { get; private set; }

            public UserId UserId { get; private set; }

            public Username Username { get; private set; }

            public Comment Content { get; private set; }

            public DateTime CreationDate { get; private set; }
        }
    }
}