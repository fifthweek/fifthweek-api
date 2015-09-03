namespace Fifthweek.Api.Posts
{
    using System.Threading.Tasks;

    using Dapper;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class IsPostSubscriberDbStatement : IIsPostSubscriberDbStatement
    {
        private static readonly string Sql = string.Format(
            @"IF EXISTS
            (
                SELECT * FROM {0} post
                INNER JOIN {1} channelSubscription ON post.{2} = channelSubscription.{3}
                WHERE post.{4} = @PostId AND channelSubscription.{5} = @UserId
            )
                SELECT 1 AS FOUND
            ELSE
                SELECT 0 AS FOUND",
            Post.Table,
            ChannelSubscription.Table,
            Post.Fields.ChannelId,
            ChannelSubscription.Fields.ChannelId,
            Post.Fields.Id,
            ChannelSubscription.Fields.UserId);

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