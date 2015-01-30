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
                "Collection " + random.Next(),
                DateTime.UtcNow,
                DateTime.UtcNow);
        }

        public static Collection UniqueEntityWithForeignEntities(
            Guid newUserId,
            Guid newChannelId,
            Guid newCollectionId)
        {
            var random = new Random();
            var creator = UserTests.UniqueEntity(random);
            creator.Id = newUserId;

            var subscription = SubscriptionTests.UniqueEntity(random);
            subscription.Creator = creator;
            subscription.CreatorId = creator.Id;

            var channel = ChannelTests.UniqueEntity(random);
            channel.Id = newChannelId;
            channel.Subscription = subscription;
            channel.SubscriptionId = subscription.Id;

            var collection = CollectionTests.UniqueEntity(random);
            collection.Id = newCollectionId;
            collection.Channel = channel;
            collection.ChannelId = channel.Id;

            return collection;
        }

        public static async Task CreatePopulatedTestCollectionAsync(
            this IFifthweekDbContext databaseContext,
            Guid newUserId,
            Guid newChannelId,
            Guid newCollectionId)
        {
            var random = new Random();
            var creator = UserTests.UniqueEntity(random);
            creator.Id = newUserId;

            var subscription = SubscriptionTests.UniqueEntity(random);
            subscription.Creator = creator;
            subscription.CreatorId = creator.Id;

            var channel = ChannelTests.UniqueEntity(random);
            channel.Id = newChannelId;
            channel.Subscription = subscription;
            channel.SubscriptionId = subscription.Id;

            var collection = CollectionTests.UniqueEntity(random);
            collection.Id = newCollectionId;
            collection.Channel = channel;
            collection.ChannelId = channel.Id;

            var file = FileTests.UniqueEntity(random);
            file.User = creator;
            file.UserId = creator.Id;

            var post = PostTests.UniqueFileOrImage(random);
            post.Channel = channel;
            post.ChannelId = channel.Id;
            post.Collection = collection;
            post.CollectionId = collection.Id;
            post.File = file;
            post.FileId = file.Id;

            databaseContext.Posts.Add(post);
            await databaseContext.SaveChangesAsync();

            var releaseTime = WeeklyReleaseTimeTests.UniqueEntity(random, collection.Id);
            await databaseContext.Database.Connection.InsertAsync(releaseTime);
        }

        public static Task CreateTestCollectionAsync(
            this IFifthweekDbContext databaseContext, 
            Guid newUserId, 
            Guid newChannelId, 
            Guid newCollectionId)
        {
            var collection = UniqueEntityWithForeignEntities(newUserId, newChannelId, newCollectionId);
            databaseContext.Collections.Add(collection);
            return databaseContext.SaveChangesAsync();
        }
    }
}