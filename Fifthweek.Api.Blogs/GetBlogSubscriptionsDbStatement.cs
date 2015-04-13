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
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class GetBlogSubscriptionsDbStatement : IGetBlogSubscriptionsDbStatement
    {
        private static readonly string SubscriptionsQuery = string.Format(
            @"SELECT 
                blog.{4} AS BlogId, 
                blog.{11} AS BlogName, 
                creator.{9} AS CreatorId, 
                creator.{12} AS CreatorUsername,
                channel.{6} AS ChannelId,
                channel.{13} AS ChannelName,
                channel.{14} AS CurrentPrice,
                channel.{15} AS PriceLastSetDate,
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
            Channel.Fields.PriceInUsCentsPerWeek,
            Channel.Fields.PriceLastSetDate,
            ChannelSubscription.Fields.AcceptedPriceInUsCentsPerWeek,
            ChannelSubscription.Fields.SubscriptionStartDate);

        private static readonly string FreeAccessQuery = string.Format(
            @"SELECT 
                blog.{3} AS BlogId, 
                blog.{9} AS BlogName, 
                creator.{6} AS CreatorId, 
                creator.{10} AS CreatorUsername
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
            FifthweekUser.Fields.UserName);

        private static readonly string Sql = SubscriptionsQuery + ";" + FreeAccessQuery;

        private readonly IFifthweekDbConnectionFactory connectionFactory;

        public async Task<GetBlogSubscriptionsResult> ExecuteAsync(UserId userId)
        {
            userId.AssertNotNull("userId");

            List<DbResult> subscriptionResults;
            List<DbResult> freeAccessResults;
            using (var connection = this.connectionFactory.CreateConnection())
            {
                using (var multi = await connection.QueryMultipleAsync(Sql, new { UserId = userId.Value }))
                {
                    subscriptionResults = multi.Read<DbResult>().ToList();
                    freeAccessResults = multi.Read<DbResult>().ToList();
                } 
            }

            var blogs = new Dictionary<BlogId, BlogSubscriptionStatus>();

            foreach (var item in freeAccessResults)
            {
                var blogId = new BlogId(item.BlogId);
                var blog = new BlogSubscriptionStatus(
                    blogId,
                    item.BlogName,
                    new UserId(item.CreatorId),
                    new Username(item.CreatorUsername),
                    true,
                    new List<ChannelSubscriptionStatus>());
      
                blogs.Add(blogId, blog);
            }

            foreach (var item in subscriptionResults)
            {
                item.PriceLastSetDate = DateTime.SpecifyKind(item.PriceLastSetDate, DateTimeKind.Utc);
                item.SubscriptionStartDate = DateTime.SpecifyKind(item.SubscriptionStartDate, DateTimeKind.Utc);

                var blogId = new BlogId(item.BlogId);
                BlogSubscriptionStatus blog;
                if (!blogs.TryGetValue(blogId, out blog))
                {
                    blog = new BlogSubscriptionStatus(
                        blogId,
                        item.BlogName,
                        new UserId(item.CreatorId),
                        new Username(item.CreatorUsername),
                        false,
                        new List<ChannelSubscriptionStatus>());

                    blogs.Add(blogId, blog);
                }

                var channel = new ChannelSubscriptionStatus(
                    new ChannelId(item.ChannelId),
                    item.ChannelName,
                    item.AcceptedPrice,
                    item.CurrentPrice,
                    item.PriceLastSetDate,
                    item.SubscriptionStartDate);

                ((List<ChannelSubscriptionStatus>)blog.Channels).Add(channel);
            }

            return new GetBlogSubscriptionsResult(blogs.Values.ToList());
        }

        private class DbResult
        {
            public Guid BlogId { get; set; }

            public string BlogName { get; set; }

            public Guid CreatorId { get; set; }

            public string CreatorUsername { get; set; }

            public Guid ChannelId { get; set; }

            public string ChannelName { get; set; }

            public int AcceptedPrice { get; set; }

            public int CurrentPrice { get; set; }

            public DateTime PriceLastSetDate { get; set; }

            public DateTime SubscriptionStartDate { get; set; }
        }
    }
}