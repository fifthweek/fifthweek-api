namespace Fifthweek.Api.Collections
{
    using System.Threading.Tasks;

    using Dapper;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;

    [AutoConstructor]
    public partial class CollectionOwnership : ICollectionOwnership
    {
        private readonly IFifthweekDbConnectionFactory connectionFactory;

        public async Task<bool> IsOwnerAsync(UserId userId, Shared.CollectionId collectionId)
        {
            userId.AssertNotNull("userId");
            collectionId.AssertNotNull("collectionId");

            using (var connection = this.connectionFactory.CreateConnection())
            {
                return await connection.ExecuteScalarAsync<bool>(
                    @"IF EXISTS(SELECT *
                                FROM        Collections collection
                                INNER JOIN  Channels channel        
                                    ON      collection.ChannelId = channel.Id
                                INNER JOIN  Subscriptions subscription
                                    ON      channel.SubscriptionId  = subscription.Id
                                WHERE       collection.Id           = @CollectionId
                                AND         subscription.CreatorId  = @CreatorId)
                        SELECT 1 AS FOUND
                    ELSE
                        SELECT 0 AS FOUND",
                    new
                    {
                        CollectionId = collectionId.Value,
                        CreatorId = userId.Value
                    });
            }
        }
    }
}