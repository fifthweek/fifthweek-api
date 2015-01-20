namespace Fifthweek.Api.Posts
{
    using System.Threading.Tasks;

    using Dapper;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;

    [AutoConstructor]
    public partial class PostOwnership : IPostOwnership
    {
        private readonly IFifthweekDbContext databaseContext;

        public Task<bool> IsOwnerAsync(UserId userId, PostId postId)
        {
            userId.AssertNotNull("userId");
            postId.AssertNotNull("postId");

            return this.databaseContext.Database.Connection.ExecuteScalarAsync<bool>(
                @"IF EXISTS(SELECT *
                            FROM        Posts post
                            INNER JOIN  Channels channel
                                ON      post.ChannelId          = channel.Id
                            INNER JOIN  Subscriptions subscription 
                                ON      channel.SubscriptionId  = subscription.Id
                            WHERE       post.Id                 = @PostId
                            AND         subscription.CreatorId  = @CreatorId)
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