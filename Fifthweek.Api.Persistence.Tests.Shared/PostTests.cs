namespace Fifthweek.Api.Persistence.Tests.Shared
{
    using System;
    using System.Threading.Tasks;

    public static class PostTests
    {
        public static Post UniqueNote(Random random)
        {
            return new Post(
                Guid.NewGuid(),
                default(Guid),
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                "Note " + random.Next(),
                false,
                DateTime.UtcNow.AddDays((random.NextDouble() * -100) + (random.NextDouble() * 100)),
                DateTime.UtcNow.AddDays(random.NextDouble() * -100));
        }

        public static Post UniqueFileOrImage(Random random)
        {
            var isQueued = random.Next(1) == 1;
            var hasComment = random.Next(1) == 1;

            return new Post(
                Guid.NewGuid(),
                default(Guid),
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                hasComment ? "Comment " + random.Next() : null,
                isQueued,
                DateTime.UtcNow.AddDays((random.NextDouble() * -100) + (random.NextDouble() * 100)),
                DateTime.UtcNow.AddDays(random.NextDouble() * -100));
        }

        public static Task CreateTestPostAsync(this IFifthweekDbContext databaseContext, Guid newUserId, Guid newPostId)
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

            var post = UniqueNote(random);
            post.Id = newPostId;
            post.Channel = channel;
            post.ChannelId = channel.Id;
            
            databaseContext.Posts.Add(post);
            return databaseContext.SaveChangesAsync();
        }
    }
}