namespace Fifthweek.Api.Collections
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Dapper;

    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Collections.Queries;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class GetChannelsAndCollectionsDbStatement : IGetChannelsAndCollectionsDbStatement
    {
        private static readonly string ChannelsQuery = string.Format(
                @"SELECT c.* FROM {0} c 
                INNER JOIN {1} s ON c.{2} = s.{3} 
                WHERE s.{4} = @CreatorId;",
                Persistence.Channel.Table,
                Persistence.Blog.Table,
                Persistence.Channel.Fields.BlogId,
                Persistence.Blog.Fields.Id,
                Persistence.Blog.Fields.CreatorId);

        private static readonly string CollectionsQuery = string.Format(
            @"SELECT col.{0}, col.{1}, ch.{2} AS {3} FROM {4} col 
                INNER JOIN {5} ch ON col.{6} = ch.{7} 
                INNER JOIN {8} s ON ch.{9} = s.{10} 
                WHERE s.{11} = @CreatorId;",
            Persistence.Collection.Fields.Id,
            Persistence.Collection.Fields.Name,
            Persistence.Channel.Fields.Id,
            Persistence.Collection.Fields.ChannelId,
            Persistence.Collection.Table,
            Persistence.Channel.Table,
            Persistence.Collection.Fields.ChannelId,
            Persistence.Channel.Fields.Id,
            Persistence.Blog.Table,
            Persistence.Channel.Fields.BlogId,
            Persistence.Blog.Fields.Id,
            Persistence.Blog.Fields.CreatorId);

        private static readonly string WeeklyReleaseScheduleQuery = string.Format(
            @"SELECT wrt.* FROM {0} wrt
                INNER JOIN {1} col ON wrt.{2} = col.{3} 
                INNER JOIN {4} ch ON col.{5} = ch.{6} 
                INNER JOIN {7} s ON ch.{8} = s.{9} 
                WHERE s.{10} = @CreatorId;",
            Persistence.WeeklyReleaseTime.Table,
            Persistence.Collection.Table,
            Persistence.WeeklyReleaseTime.Fields.CollectionId,
            Persistence.Collection.Fields.Id,
            Persistence.Channel.Table,
            Persistence.Collection.Fields.ChannelId,
            Persistence.Channel.Fields.Id,
            Persistence.Blog.Table,
            Persistence.Channel.Fields.BlogId,
            Persistence.Blog.Fields.Id,
            Persistence.Blog.Fields.CreatorId);

        private static readonly string Query = ChannelsQuery + CollectionsQuery + WeeklyReleaseScheduleQuery;

        private readonly IFifthweekDbConnectionFactory connectionFactory;

        public async Task<ChannelsAndCollections> ExecuteAsync(UserId userId)
        {
            userId.AssertNotNull("userId");

            List<Channel> channels;
            List<Collection> collections;
            List<WeeklyReleaseTime> releaseTimes;
            using (var connection = this.connectionFactory.CreateConnection())
            using (var multi = await connection.QueryMultipleAsync(
                Query,
                new { CreatorId = userId.Value }))
            {
                channels = multi.Read<Channel>().ToList();
                collections = multi.Read<Collection>().ToList();
                releaseTimes = multi.Read<WeeklyReleaseTime>().ToList();
            }

            var result = new ChannelsAndCollections(
                (from c in channels
                 select new ChannelsAndCollections.Channel(
                    new ChannelId(c.Id),
                    c.Name,
                    c.Description,
                    c.PriceInUsCentsPerWeek,
                    c.Id == c.SubscriptionId,
                    c.IsVisibleToNonSubscribers,
                    (from col in collections 
                     where col.ChannelId == c.Id
                     select new ChannelsAndCollections.Collection(
                         new CollectionId(col.Id), 
                         col.Name, 
                         releaseTimes.Where(wrt => wrt.CollectionId == col.Id).Select(wrt => new HourOfWeek(wrt.HourOfWeek)).ToArray()))
                        .ToArray())).ToArray());

            return result;
        }

        public partial class Channel
        {
            public Guid Id { get; set; }

            public Guid SubscriptionId { get; set; }

            public string Name { get; set; }

            public string Description { get; set; }

            public int PriceInUsCentsPerWeek { get; set; }

            public bool IsVisibleToNonSubscribers { get; set; }
        }

        public partial class Collection
        {
            public Guid Id { get; set; }

            public Guid ChannelId { get; set; }

            public string Name { get; set; }
        }
    }
}
