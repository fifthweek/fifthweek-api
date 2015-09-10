namespace Fifthweek.Api.Persistence.Tests.Shared
{
    using System;
    using System.Data.SqlTypes;
    using System.Threading.Tasks;

    public static class QueueTests
    {
        public static Queue UniqueEntity(Random random)
        {
            return new Queue(
                Guid.NewGuid(),
                default(Guid),
                null,
                "Collection " + random.Next(),
                new SqlDateTime(DateTime.UtcNow).Value,
                new SqlDateTime(DateTime.UtcNow).Value);
        }

        public static Queue UniqueEntityWithForeignEntities(
            Guid newUserId,
            Guid newChannelId,
            Guid newCollectionId)
        {
            var random = new Random();
            var creator = UserTests.UniqueEntity(random);
            creator.Id = newUserId;

            var subscription = BlogTests.UniqueEntity(random);
            subscription.Creator = creator;
            subscription.CreatorId = creator.Id;

            var channel = ChannelTests.UniqueEntity(random);
            channel.Id = newChannelId;
            channel.Blog = subscription;
            channel.BlogId = subscription.Id;

            var collection = QueueTests.UniqueEntity(random);
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

            var subscription = BlogTests.UniqueEntity(random);
            subscription.Creator = creator;
            subscription.CreatorId = creator.Id;

            var channel = ChannelTests.UniqueEntity(random);
            channel.Id = newChannelId;
            channel.Blog = subscription;
            channel.BlogId = subscription.Id;

            var queue = QueueTests.UniqueEntity(random);
            queue.Id = newCollectionId;
            queue.Channel = channel;
            queue.ChannelId = channel.Id;

            var file = FileTests.UniqueEntity(random);
            file.UserId = creator.Id;

            var post = PostTests.UniqueFileOrImage(random);
            post.Channel = channel;
            post.ChannelId = channel.Id;
            post.Queue = queue;
            post.QueueId = queue.Id;
            post.File = file;
            post.FileId = file.Id;

            databaseContext.Posts.Add(post);
            await databaseContext.SaveChangesAsync();

            var releaseTime = WeeklyReleaseTimeTests.UniqueEntity(random, queue.Id);
            await databaseContext.Database.Connection.InsertAsync(releaseTime);
        }

        public static Task CreateTestCollectionAsync(
            this IFifthweekDbContext databaseContext, 
            Guid newUserId, 
            Guid newChannelId, 
            Guid newCollectionId)
        {
            var collection = UniqueEntityWithForeignEntities(newUserId, newChannelId, newCollectionId);
            databaseContext.Queues.Add(collection);
            return databaseContext.SaveChangesAsync();
        }
    }
}