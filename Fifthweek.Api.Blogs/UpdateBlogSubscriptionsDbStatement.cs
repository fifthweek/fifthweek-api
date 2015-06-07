namespace Fifthweek.Api.Blogs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Transactions;

    using Dapper;

    using Fifthweek.Api.Blogs.Commands;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Services;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class UpdateBlogSubscriptionsDbStatement : IUpdateBlogSubscriptionsDbStatement
    {
        // We don't want to delete subscriptions that are being updated
        // because we would lose the subscription start date.
        private static readonly string DeleteStatement = string.Format(
            @"DELETE FROM {0} 
            WHERE {3} IN (
                SELECT sub.{3} FROM {0} sub 
                INNER JOIN {1} ch ON sub.{3} = ch.{4}
                INNER JOIN {2} blog ON ch.{5} = blog.{6}
                WHERE blog.{6} = @BlogId AND sub.{7} = @SubscriberId
            )
            AND {3} NOT IN @KeepChannelIds",
            ChannelSubscription.Table,
            Channel.Table,
            Blog.Table,
            ChannelSubscription.Fields.ChannelId,
            Channel.Fields.Id,
            Channel.Fields.BlogId,
            Blog.Fields.Id,
            ChannelSubscription.Fields.UserId);

        private readonly IFifthweekDbConnectionFactory connectionFactory;
        private readonly IRequestSnapshotService requestSnapshot;

        public async Task ExecuteAsync(UserId userId, BlogId blogId, IReadOnlyList<AcceptedChannelSubscription> channels, DateTime now)
        {
            userId.AssertNotNull("userId");
            blogId.AssertNotNull("blogId");

            if (channels == null || channels.Count == 0)
            {
                channels = new List<AcceptedChannelSubscription>();
            }

            using (var transaction = TransactionScopeBuilder.CreateAsync())
            {
                using (var connection = this.connectionFactory.CreateConnection())
                {
                    await connection.ExecuteAsync(
                        DeleteStatement, 
                        new 
                        {
                            BlogId = blogId.Value,
                            SubscriberId = userId.Value,
                            KeepChannelIds = channels.Select(v => v.ChannelId.Value).ToList()
                        });

                    foreach (var item in channels)
                    {
                        var channelSubscription = new ChannelSubscription(
                            item.ChannelId.Value,
                            null,
                            userId.Value,
                            null,
                            item.AcceptedPrice.Value,
                            now,
                            now);

                        const ChannelSubscription.Fields UpdateFields 
                            = ChannelSubscription.Fields.AcceptedPriceInUsCentsPerWeek
                            | ChannelSubscription.Fields.PriceLastAcceptedDate;

                        await connection.UpsertAsync(
                            channelSubscription,
                            UpdateFields);
                    }
                }

                await this.requestSnapshot.ExecuteAsync(userId, SnapshotType.SubscriberChannels);

                transaction.Complete();
            }
        }
    }
}