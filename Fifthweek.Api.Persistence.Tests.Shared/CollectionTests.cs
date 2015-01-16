namespace Fifthweek.Api.Persistence.Tests.Shared
{
    using System;
    using System.Threading.Tasks;

    public static class CollectionTests
    {
        public static Collection UniqueEntity(Random random)
        {
            return new Collection(
                Guid.NewGuid(),
                default(Guid),
                null,
                "Collection " + random.Next());
        }

        public static Task CreateTestCollectionAsync(this IFifthweekDbContext databaseContext, Guid newUserId, Guid newCollectionId)
        {
            var random = new Random();
            var creator = UserTests.UniqueEntity(random);
            creator.Id = newUserId;

            var subscription = SubscriptionTests.UniqueEntity(random);
            subscription.Creator = creator;
            subscription.CreatorId = creator.Id;

            var channel = ChannelTests.UniqueEntity(random);
            channel.Subscription = subscription;
            channel.SubscriptionId = subscription.Id;

            var collection = CollectionTests.UniqueEntity(random);
            collection.Id = newCollectionId;
            collection.Channel = channel;
            collection.ChannelId = channel.Id;

            databaseContext.Collections.Add(collection);
            return databaseContext.SaveChangesAsync();
        }
    }
}