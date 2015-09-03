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
        // We don't delete files because we need garbage collection to take care of those.
        private static readonly string DeleteSql = string.Format(
            @"
            DELETE c FROM {9} c INNER JOIN {4} p ON c.{10} = p.{8} WHERE p.{5} = @ChannelId;
            DELETE l FROM {6} l INNER JOIN {4} p ON l.{7} = p.{8} WHERE p.{5} = @ChannelId;
            DELETE FROM {4} WHERE {5} = @ChannelId;
            DELETE FROM {0} WHERE {1} = @ChannelId;
            DELETE FROM {2} WHERE {3} = @ChannelId;",
            ChannelSubscription.Table,
            ChannelSubscription.Fields.ChannelId,
            Channel.Table,
            Channel.Fields.Id,
            Post.Table,
            Post.Fields.ChannelId,
            Like.Table,
            Like.Fields.PostId,
            Post.Fields.Id,
            Comment.Table,
            Comment.Fields.PostId);

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