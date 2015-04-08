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
    public partial class PostOwnership : IPostOwnership
    {
        private readonly IFifthweekDbConnectionFactory connectionFactory;

        public async Task<bool> IsOwnerAsync(UserId userId, Shared.PostId postId)
        {
            userId.AssertNotNull("userId");
            postId.AssertNotNull("postId");

            using (var connection = this.connectionFactory.CreateConnection())
            {
                return await connection.ExecuteScalarAsync<bool>(
                    @"IF EXISTS(SELECT *
                                FROM        Posts post
                                INNER JOIN  Channels channel
                                    ON      post.ChannelId          = channel.Id
                                INNER JOIN  Blogs blog 
                                    ON      channel.BlogId  = blog.Id
                                WHERE       post.Id                 = @PostId
                                AND         blog.CreatorId  = @CreatorId)
                        SELECT 1 AS FOUND
                    ELSE
                        SELECT 0 AS FOUND",
                    new
                    {
                        PostId = postId.Value,
                        CreatorId = userId.Value
                    });
            }
        }
    }
}