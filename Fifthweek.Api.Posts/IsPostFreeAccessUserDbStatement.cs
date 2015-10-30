namespace Fifthweek.Api.Posts
{
    using System.Threading.Tasks;

    using Dapper;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class IsPostFreeAccessUserDbStatement : IIsPostFreeAccessUserDbStatement
    {
        private static readonly string Sql = string.Format(
            @"IF EXISTS
            (
                SELECT * FROM {0} post
                INNER JOIN {1} channel ON post.{2} = channel.{3}
                INNER JOIN {4} freeaccess ON channel.{5} = freeaccess.{6}
                INNER JOIN {9} subscriber ON freeaccess.{10} = subscriber.{11}
                WHERE post.{7} = @PostId AND subscriber.{12} = @UserId
            )
                SELECT 1 AS FOUND
            ELSE
                SELECT 0 AS FOUND",
            Post.Table,
            Channel.Table,
            Post.Fields.ChannelId,
            Channel.Fields.Id,
            FreeAccessUser.Table,
            Channel.Fields.BlogId,
            FreeAccessUser.Fields.BlogId,
            Post.Fields.Id,
            Blog.Fields.CreatorId,
            FifthweekUser.Table,
            FreeAccessUser.Fields.Email,
            FifthweekUser.Fields.Email,
            FifthweekUser.Fields.Id);

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
                        UserId = userId.Value
                    });
            }
        }
    }
}