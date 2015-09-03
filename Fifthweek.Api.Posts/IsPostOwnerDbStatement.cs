namespace Fifthweek.Api.Posts
{
    using System.Threading.Tasks;

    using Dapper;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class IsPostOwnerDbStatement : IIsPostOwnerDbStatement
    {
        private static readonly string Sql = string.Format(
            @"IF EXISTS
            (
                SELECT * FROM {0} post
                INNER JOIN {1} channel ON post.{2} = channel.{3}
                INNER JOIN {4} blog ON channel.{5} = blog.{6}
                WHERE post.{7} = @PostId AND blog.{8} = @CreatorId
            )
                SELECT 1 AS FOUND
            ELSE
                SELECT 0 AS FOUND",
            Post.Table,
            Channel.Table,
            Post.Fields.ChannelId,
            Channel.Fields.Id,
            Blog.Table,
            Channel.Fields.BlogId,
            Blog.Fields.Id,
            Post.Fields.Id,
            Blog.Fields.CreatorId);

        private readonly IFifthweekDbConnectionFactory connectionFactory;

        public async Task<bool> ExecuteAsync(UserId userId, Shared.PostId postId)
        {
            userId.AssertNotNull("userId");
            postId.AssertNotNull("postId");

            using (var connection = this.connectionFactory.CreateConnection())
            {
                return await connection.ExecuteScalarAsync<bool>(
                    Sql,
                    new
                    {
                        PostId = postId.Value,
                        CreatorId = userId.Value
                    });
            }
        }
    }
}