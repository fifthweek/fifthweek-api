namespace Fifthweek.Api.Channels
{
    using System.Threading.Tasks;

    using Dapper;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class ChannelOwnership : IChannelOwnership
    {
        private readonly IFifthweekDbConnectionFactory connectionFactory;

        public async Task<bool> IsOwnerAsync(UserId userId, Shared.ChannelId channelId)
        {
            userId.AssertNotNull("userId");
            channelId.AssertNotNull("channelId");

            using (var connection = this.connectionFactory.CreateConnection())
            {
                return await connection.ExecuteScalarAsync<bool>(
                    @"IF EXISTS(SELECT *
                                FROM        Channels channel
                                INNER JOIN  Blogs blog
                                    ON      channel.BlogId  = blog.Id
                                WHERE       channel.Id              = @ChannelId
                                AND         blog.CreatorId  = @CreatorId)
                        SELECT 1 AS FOUND
                    ELSE
                        SELECT 0 AS FOUND",
                    new
                    {
                        ChannelId = channelId.Value,
                        CreatorId = userId.Value
                    });
            }
        }
    }
}