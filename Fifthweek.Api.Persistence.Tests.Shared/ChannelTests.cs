namespace Fifthweek.Api.Persistence.Tests.Shared
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Validation;
    using System.Data.SqlTypes;
    using System.Linq;
    using System.Threading.Tasks;

    public static class ChannelTests
    {
        private static readonly Random Random = new Random();

        public static Channel UniqueEntity(Random random)
        {
            var dateCreated = DateTime.UtcNow.AddDays(random.NextDouble() * -100);
            var datePriceLastSet = DateTime.UtcNow.AddDays(random.NextDouble() * -100);
            datePriceLastSet = new DateTime(Math.Max(dateCreated.Ticks, datePriceLastSet.Ticks));

            dateCreated = new SqlDateTime(dateCreated).Value;
            datePriceLastSet = new SqlDateTime(datePriceLastSet).Value;

            return new Channel(
                Guid.NewGuid(),
                default(Guid),
                null,
                "Name " + random.Next(1000),
                random.Next(1, 100),
                random.Next(2) == 0,
                dateCreated,
                datePriceLastSet,
                false);
        }

        public static async Task<IReadOnlyList<Channel>> CreateTestChannelsAsync(
            this IFifthweekDbContext databaseContext, 
            Guid newUserId, 
            Guid newBlogId, 
            params Guid[] newChannelIds)
        {
            var creator = UserTests.UniqueEntity(Random);
            creator.Id = newUserId;

            var blog = BlogTests.UniqueEntity(Random);
            blog.Id = newBlogId;
            blog.Creator = creator;
            blog.CreatorId = creator.Id;

            var channels = new List<Channel>();
            foreach (var newChannelId in newChannelIds)
            {
                var channel = ChannelTests.UniqueEntity(Random);
                channel.Id = newChannelId;
                channel.Blog = blog;
                channel.BlogId = blog.Id;
                databaseContext.Channels.Add(channel);
                channels.Add(channel);
            }

            await databaseContext.SaveChangesAsync();
            return channels;
        }


        public static async Task<Channel> CreateTestChannelAsync(
            this IFifthweekDbContext databaseContext,
            Guid newUserId,
            Guid newBlogId,
            Guid newChannelId)
        {
            var result = await CreateTestChannelsAsync(databaseContext, newUserId, newBlogId, newChannelId);
            return result.First();
        }

        public static Task CreateTestChannelAsync(this IFifthweekDbContext databaseContext, Guid newUserId, Guid newChannelId)
        {
            return CreateTestChannelsAsync(databaseContext, newUserId, Guid.NewGuid(), newChannelId);
        }
    }
}