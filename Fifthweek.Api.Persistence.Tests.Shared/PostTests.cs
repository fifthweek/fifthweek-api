namespace Fifthweek.Api.Persistence.Tests.Shared
{
    using System;
    using System.Data.SqlTypes;
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
                new SqlDateTime(DateTime.UtcNow.AddDays((random.NextDouble() * -1000) + (random.NextDouble() * 1000))).Value,
                new SqlDateTime(DateTime.UtcNow.AddDays(random.NextDouble() * -1000)).Value);
        }

        public static Post UniqueFileOrImage(Random random)
        {
            var isQueued = random.Next(2) == 0;
            var hasComment = random.Next(2) == 0;

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
                new SqlDateTime(DateTime.UtcNow.AddDays((random.NextDouble() * -1000) + (random.NextDouble() * 1000))).Value,
                new SqlDateTime(DateTime.UtcNow.AddDays(random.NextDouble() * -1000)).Value);
        }

        public static Task CreateTestNoteAsync(this IFifthweekDbContext databaseContext, Guid newUserId, Guid newPostId)
        {
            var random = new Random();
            var creator = UserTests.UniqueEntity(random);
            creator.Id = newUserId;

            var subscription = BlogTests.UniqueEntity(random);
            subscription.Creator = creator;
            subscription.CreatorId = creator.Id;

            var channel = ChannelTests.UniqueEntity(random);
            channel.Blog = subscription;
            channel.BlogId = subscription.Id;

            var post = UniqueNote(random);
            post.Id = newPostId;
            post.Channel = channel;
            post.ChannelId = channel.Id;
            
            databaseContext.Posts.Add(post);
            return databaseContext.SaveChangesAsync();
        }
    }
}