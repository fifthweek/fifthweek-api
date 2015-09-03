namespace Fifthweek.Api.Posts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Dapper;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Posts.Controllers;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class GetCommentsDbStatement : IGetCommentsDbStatement
    {
        private static readonly string Sql = string.Format(
            @"SELECT u.{4}, c.* FROM {0} c
            INNER JOIN {1} u ON c.{2}=u.{3}
            WHERE c.{5}=@PostId ORDER BY c.{6} ASC",
            Persistence.Comment.Table,
            FifthweekUser.Table,
            Persistence.Comment.Fields.UserId,
            FifthweekUser.Fields.Id,
            FifthweekUser.Fields.UserName,
            Persistence.Comment.Fields.PostId,
            Persistence.Comment.Fields.CreationDate);

        private readonly IFifthweekDbConnectionFactory connectionFactory;

        public async Task<CommentsResult> ExecuteAsync(PostId postId)
        {
            postId.AssertNotNull("postId");

            using (var connection = this.connectionFactory.CreateConnection())
            {
                var result = await connection.QueryAsync<CommentWithUser>(
                    Sql,
                    new
                    {
                        PostId = postId.Value
                    });

                return new CommentsResult(
                    new List<CommentsResult.Item>(
                        result.Select(v => new CommentsResult.Item(
                            new CommentId(v.Id),
                            new PostId(v.PostId),
                            new UserId(v.UserId),
                            new Username(v.UserName),
                            new Shared.Comment(v.Content),
                            DateTime.SpecifyKind(v.CreationDate, DateTimeKind.Utc)))));
            }
        }

        private class CommentWithUser : Persistence.Comment
        {
            public string UserName { get; set; }
        }
    }
}