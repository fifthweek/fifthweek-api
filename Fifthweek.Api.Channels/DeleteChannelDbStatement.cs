namespace Fifthweek.Api.Channels
{
    using System.Threading.Tasks;

    using Dapper;

    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.SnapshotCreation;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class DeleteChannelDbStatement : IDeleteChannelDbStatement
    {
        // We have to explicitly delete the posts because otherwise deleting the
        // channel can cascade delete the collection first, causing a FK violation on posts
        // in that channel. Channel deletions don't cascade delete posts because of
        // multiple cascade paths.
        private static readonly string DeleteSql = string.Format(
            @"
            DELETE post FROM {4} post INNER JOIN {2} channel ON post.{5} = channel.{3}
                WHERE channel.{3} = @ChannelId;
            DELETE FROM {0} WHERE {1} = @ChannelId;
            DELETE FROM {2} WHERE {3} = @ChannelId;",
            ChannelSubscription.Table,
            ChannelSubscription.Fields.ChannelId,
            Channel.Table,
            Channel.Fields.Id,
            Post.Table,
            Post.Fields.ChannelId);

        private readonly IFifthweekDbConnectionFactory connectionFactory;
        private readonly IRequestSnapshotService requestSnapshot;

        public async Task ExecuteAsync(UserId userId, ChannelId channelId)
        {
            userId.AssertNotNull("userId");
            channelId.AssertNotNull("channelId");
            var channelIdParameter = new
            {
                ChannelId = channelId.Value
            };

            using (var transaction = TransactionScopeBuilder.CreateAsync())
            {
                using (var connection = this.connectionFactory.CreateConnection())
                {
                    await connection.ExecuteAsync(DeleteSql, channelIdParameter);
                }

                await this.requestSnapshot.ExecuteAsync(userId, SnapshotType.CreatorChannels);

                transaction.Complete();
            }
        }
    }
}