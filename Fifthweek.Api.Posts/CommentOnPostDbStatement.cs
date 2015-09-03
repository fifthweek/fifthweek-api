namespace Fifthweek.Api.Posts
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class CommentOnPostDbStatement : ICommentOnPostDbStatement
    {
        private readonly IFifthweekDbConnectionFactory connectionFactory;

        public async Task ExecuteAsync(UserId userId, PostId postId, CommentId commentId, Shared.Comment content, DateTime timestamp)
        {
            userId.AssertNotNull("userId");
            postId.AssertNotNull("postId");
            commentId.AssertNotNull("commentId");
            content.AssertNotNull("content");

            var comment = new Persistence.Comment(commentId.Value, postId.Value, null, userId.Value, null, content.Value, timestamp);
            using (var connection = this.connectionFactory.CreateConnection())
            {
                await connection.UpsertAsync(
                    comment,
                    Persistence.Comment.Fields.PostId
                    | Persistence.Comment.Fields.UserId
                    | Persistence.Comment.Fields.Content
                    | Persistence.Comment.Fields.CreationDate);
            }
        }
    }
}