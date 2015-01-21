namespace Fifthweek.Api.Collections
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Dapper;

    using Fifthweek.Api.Collections.Queries;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Subscriptions;
    using Fifthweek.CodeGeneration;

    [AutoConstructor]
    public partial class GetChannelsAndCollectionsDbStatement : IGetChannelsAndCollectionsDbStatement
    {
        private readonly IFifthweekDbContext databaseContext;

        public async Task<GetChannelsAndCollectionsResult> ExecuteAsync(UserId userId)
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
                @"SELECT col.{0}, col.{1}, ch.{2} FROM {3} col 
                INNER JOIN {4} ch ON col.{5} = ch.{6} 
                INNER JOIN {7} s ON ch.{8} = s.{9} 
                WHERE s.{10} = @CreatorId",
                Persistence.Collection.Fields.Id,
                Persistence.Collection.Fields.Name,
                Persistence.Channel.Fields.Id,
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
            using (var multi = await this.databaseContext.Database.Connection.QueryMultipleAsync(
                query))
            {
                channels = multi.Read<Channel>().ToList();
                collections = multi.Read<Collection>().ToList();
            }

            var result = new GetChannelsAndCollectionsResult(
                (from v in channels
                 select new GetChannelsAndCollectionsResult.Channel(
                    new ChannelId(v.Id),
                    v.Name,
                    (from c in collections 
                     where c.ChannelId == v.Id
                     select new GetChannelsAndCollectionsResult.Collection(new CollectionId(c.Id), c.Name))
                        .AsReadOnlyList())).AsReadOnlyList());

            return result;
        }

        [AutoConstructor]
        public partial class Channel
        {
            public Guid Id { get; set; }

            public string Name { get; set; }
        }

        [AutoConstructor]
        public partial class Collection
        {
            public Guid Id { get; set; }

            public Guid ChannelId { get; set; }

            public string Name { get; set; }
        }
    }
}