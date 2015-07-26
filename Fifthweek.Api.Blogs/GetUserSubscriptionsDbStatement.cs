namespace Fifthweek.Api.Blogs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Dapper;

    using Fifthweek.Api.Blogs.Queries;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class GetUserSubscriptionsDbStatement : IGetUserSubscriptionsDbStatement
    {
        private static readonly string SubscriptionsQuery = string.Format(
            @"SELECT 
                blog.{4} AS BlogId, 
                blog.{11} AS BlogName, 
                creator.{9} AS CreatorId, 
                creator.{12} AS CreatorUsername,
                creator.{18} AS ProfileImageFileId,
                channel.{6} AS ChannelId,
                channel.{13} AS ChannelName,
                channel.{14} AS CurrentPrice,
                channel.{15} AS PriceLastSetDate,
                channel.{19} AS IsVisibleToNonSubscribers,
                subscription.{16} AS AcceptedPrice,
                subscription.{17} AS SubscriptionStartDate
                FROM {0} blog
                INNER JOIN {1} channel ON blog.{4} = channel.{5}
                INNER JOIN {2} subscription ON channel.{6} = subscription.{7}
                INNER JOIN {3} creator ON blog.{8} = creator.{9}
                INNER JOIN {3} subscriber ON subscription.{10} = subscriber.{9}
                WHERE subscriber.{9} = @UserId",
            Blog.Table,
            Channel.Table,
            ChannelSubscription.Table,
            FifthweekUser.Table,
            Blog.Fields.Id,
            Channel.Fields.BlogId,
            Channel.Fields.Id,
            ChannelSubscription.Fields.ChannelId,
            Blog.Fields.CreatorId,
            FifthweekUser.Fields.Id,
            ChannelSubscription.Fields.UserId,
            Blog.Fields.Name,
            FifthweekUser.Fields.UserName,
            Channel.Fields.Name,
            Channel.Fields.Price,
            Channel.Fields.PriceLastSetDate,
            ChannelSubscription.Fields.AcceptedPrice,
            ChannelSubscription.Fields.SubscriptionStartDate,
            FifthweekUser.Fields.ProfileImageFileId,
            Channel.Fields.IsVisibleToNonSubscribers);

        private static readonly string FreeAccessQuery = string.Format(
            @"SELECT 
                blog.{3} AS BlogId, 
                blog.{9} AS BlogName, 
                creator.{6} AS CreatorId, 
                creator.{10} AS CreatorUsername,
                creator.{11} AS ProfileImageFileId
                FROM {0} blog
                INNER JOIN {1} freeaccess ON blog.{3} = freeaccess.{4}
                INNER JOIN {2} creator ON blog.{5} = creator.{6}
                INNER JOIN {2} subscriber ON freeaccess.{7} = subscriber.{8}
                WHERE subscriber.{6} = @UserId",
            Blog.Table,
            FreeAccessUser.Table,
            FifthweekUser.Table,
            Blog.Fields.Id,
            FreeAccessUser.Fields.BlogId,
            Blog.Fields.CreatorId,
            FifthweekUser.Fields.Id,
            FreeAccessUser.Fields.Email,
            FifthweekUser.Fields.Email,
            Blog.Fields.Name,
            FifthweekUser.Fields.UserName,
            FifthweekUser.Fields.ProfileImageFileId);


        private static readonly string CollectionsQuery = string.Format(
            @"SELECT collection.{0}, collection.{1}, channel.{2} AS ChannelId
                FROM {4} collection 
                INNER JOIN {5} channel ON collection.{6} = channel.{7} 
                INNER JOIN {8} subscription ON channel.{7} = subscription.{9}
                INNER JOIN {11} subscriber ON subscription.{10} = subscriber.{12}
                WHERE subscriber.{12} = @UserId",
            Collection.Fields.Id,
            Collection.Fields.Name,
            Channel.Fields.Id,
            Collection.Fields.ChannelId,
            Collection.Table,
            Channel.Table,
            Collection.Fields.ChannelId,
            Channel.Fields.Id,
            ChannelSubscription.Table,
            ChannelSubscription.Fields.ChannelId,
            ChannelSubscription.Fields.UserId,
            FifthweekUser.Table,
            FifthweekUser.Fields.Id);

        private static readonly string Sql = SubscriptionsQuery + ";" + FreeAccessQuery + ";" + CollectionsQuery;

        private readonly IFifthweekDbConnectionFactory connectionFactory;

        public async Task<IReadOnlyList<BlogSubscriptionDbResult>> ExecuteAsync(UserId userId)
        {
            userId.AssertNotNull("userId");

            List<DbResult> subscriptionResults;
            List<DbResult> freeAccessResults;
            List<PartialCollection> collectionResults;
            using (var connection = this.connectionFactory.CreateConnection())
            {
                using (var multi = await connection.QueryMultipleAsync(Sql, new { UserId = userId.Value }))
                {
                    subscriptionResults = multi.Read<DbResult>().ToList();
                    freeAccessResults = multi.Read<DbResult>().ToList();
                    collectionResults = multi.Read<PartialCollection>().ToList();
                } 
            }

            var groupdedCollections = collectionResults.ToLookup(v => v.ChannelId);

            var blogs = new Dictionary<BlogId, BlogSubscriptionDbResult>();

            foreach (var item in freeAccessResults)
            {
                var blogId = new BlogId(item.BlogId);
                var blog = new BlogSubscriptionDbResult(
                    blogId,
                    item.BlogName,
                    new UserId(item.CreatorId),
                    new Username(item.CreatorUsername),
                    item.ProfileImageFileId.HasValue ? new FileId(item.ProfileImageFileId.Value) : null,
                    true,
                    new List<ChannelSubscriptionStatus>());
      
                blogs.Add(blogId, blog);
            }

            foreach (var item in subscriptionResults)
            {
                item.PriceLastSetDate = DateTime.SpecifyKind(item.PriceLastSetDate, DateTimeKind.Utc);
                item.SubscriptionStartDate = DateTime.SpecifyKind(item.SubscriptionStartDate, DateTimeKind.Utc);

                var blogId = new BlogId(item.BlogId);
                BlogSubscriptionDbResult blog;
                if (!blogs.TryGetValue(blogId, out blog))
                {
                    blog = new BlogSubscriptionDbResult(
                        blogId,
                        item.BlogName,
                        new UserId(item.CreatorId),
                        new Username(item.CreatorUsername),
                        item.ProfileImageFileId.HasValue ? new FileId(item.ProfileImageFileId.Value) : null,
                        false,
                        new List<ChannelSubscriptionStatus>());

                    blogs.Add(blogId, blog);
                }

                var collections = groupdedCollections[item.ChannelId].Select(v => new CollectionSubscriptionStatus(new CollectionId(v.Id), v.Name)).ToList();
                var channel = new ChannelSubscriptionStatus(
                    new ChannelId(item.ChannelId),
                    item.ChannelName,
                    item.AcceptedPrice,
                    item.CurrentPrice,
                    item.ChannelId == item.BlogId,
                    item.PriceLastSetDate,
                    item.SubscriptionStartDate,
                    item.IsVisibleToNonSubscribers,
                    collections);

                ((List<ChannelSubscriptionStatus>)blog.Channels).Add(channel);
            }

            return blogs.Values.ToList();
        }

        private class DbResult
        {
            public Guid BlogId { get; set; }

            public string BlogName { get; set; }

            public Guid CreatorId { get; set; }

            public string CreatorUsername { get; set; }

            public Guid? ProfileImageFileId { get; set; }

            public Guid ChannelId { get; set; }

            public string ChannelName { get; set; }

            public int AcceptedPrice { get; set; }

            public int CurrentPrice { get; set; }

            public DateTime PriceLastSetDate { get; set; }

            public DateTime SubscriptionStartDate { get; set; }

            public bool IsVisibleToNonSubscribers { get; set; }
        }

        public class PartialCollection
        {
            public Guid Id { get; set; }

            public Guid ChannelId { get; set; }

            public string Name { get; set; }
        }
    }
}