namespace Fifthweek.Api.Persistence.Tests.Shared
{
    using System;
    using System.Data.SqlTypes;
    using System.Threading.Tasks;

    public static class ChannelTests
    {
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
                "Description " + random.Next(1000),
                random.Next(1, 100),
                random.Next(2) == 0,
                dateCreated,
                datePriceLastSet);
        }

        public static async Task<Channel> CreateTestChannelAsync(
            this IFifthweekDbContext databaseContext,
            Guid newUserId,
            Guid newChannelId,
            Guid subscriptionId)
        {
            var random = new Random();
            var creator = UserTests.UniqueEntity(random);
            creator.Id = newUserId;

            var subscription = BlogTests.UniqueEntity(random);
            subscription.Id = subscriptionId;
            subscription.Creator = creator;
            subscription.CreatorId = creator.Id;

            var channel = ChannelTests.UniqueEntity(random);
            channel.Id = newChannelId;
            channel.Blog = subscription;
            channel.BlogId = subscription.Id;

            databaseContext.Channels.Add(channel);
            await databaseContext.SaveChangesAsync();
            return channel;
        }

        public static Task CreateTestChannelAsync(this IFifthweekDbContext databaseContext, Guid newUserId, Guid newChannelId)
        {
            return CreateTestChannelAsync(databaseContext, newUserId, newChannelId, Guid.NewGuid());
        }
    }
}