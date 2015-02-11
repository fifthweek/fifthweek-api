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
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;

    [AutoConstructor]
    public partial class GetChannelsAndCollectionsDbStatement : IGetChannelsAndCollectionsDbStatement
    {
        private readonly IFifthweekDbConnectionFactory connectionFactory;

        public async Task<ChannelsAndCollections> ExecuteAsync(UserId userId)
        {
            userId.AssertNotNull("userId");

            var channelsQuery = string.Format(
                @"SELECT c.{0}, c.{1} FROM {2} c 
                INNER JOIN {3} s ON c.{4} = s.{5} 
                WHERE s.{6} = @CreatorId",
                Persistence.Channel.Fields.Id,
                Persistence.Channel.Fields.Name,
                Persistence.Channel.Table,
                Persistence.Subscription.Table,
                Persistence.Channel.Fields.SubscriptionId,
                Persistence.Subscription.Fields.Id,
                Persistence.Subscription.Fields.CreatorId);

            var collectionsQuery = string.Format(
                @"SELECT col.{0}, col.{1}, ch.{2} AS {3} FROM {4} col 
                INNER JOIN {5} ch ON col.{6} = ch.{7} 
                INNER JOIN {8} s ON ch.{9} = s.{10} 
                WHERE s.{11} = @CreatorId",
                Persistence.Collection.Fields.Id,
                Persistence.Collection.Fields.Name,
                Persistence.Channel.Fields.Id,
                Persistence.Collection.Fields.ChannelId,
                Persistence.Collection.Table,
                Persistence.Channel.Table,
                Persistence.Collection.Fields.ChannelId,
                Persistence.Channel.Fields.Id,
                Persistence.Subscription.Table,
                Persistence.Channel.Fields.SubscriptionId,
                Persistence.Subscription.Fields.Id,
                Persistence.Subscription.Fields.CreatorId);

            var sb = new StringBuilder();
            sb.AppendLine(channelsQuery);
            sb.AppendLine(collectionsQuery);
            var query = sb.ToString();

            List<Channel> channels;
            List<Collection> collections;
            using (var connection = this.connectionFactory.CreateConnection())
            using (var multi = await connection.QueryMultipleAsync(
                query,
                new { CreatorId = userId.Value }))
            {
                channels = multi.Read<Channel>().ToList();
                collections = multi.Read<Collection>().ToList();
            }

            var result = new ChannelsAndCollections(
                (from v in channels
                 select new ChannelsAndCollections.Channel(
                    new ChannelId(v.Id),
                    v.Name,
                    (from c in collections 
                     where c.ChannelId == v.Id
                     select new ChannelsAndCollections.Collection(new Shared.CollectionId(c.Id), c.Name))
                        .AsReadOnlyList())).AsReadOnlyList());

            return result;
        }

        public partial class Channel
        {
            public Guid Id { get; set; }

            public string Name { get; set; }
        }

        public partial class Collection
        {
            public Guid Id { get; set; }

            public Guid ChannelId { get; set; }

            public string Name { get; set; }
        }
    }
}