namespace Fifthweek.Api.Persistence.Tests.Shared
{
    using System;
    using System.Data.SqlTypes;
    using System.Threading.Tasks;

    using Fifthweek.Api.Persistence.Identity;

    public static class BlogTests
    {
        public static Blog UniqueEntity(Random random)
        {
            return new Blog(
                Guid.NewGuid(),
                default(Guid),
                null,
                "Name " + random.Next(),
                "Blog intro " + random.Next(),
                random.Next(1) == 1 ? null : "Blog description " + random.Next(),
                random.Next(1) == 1 ? null : "http://external/video" + random.Next(),
                null,
                null,
                new SqlDateTime(DateTime.UtcNow.AddDays(random.NextDouble() * -100)).Value);
        }

        public static Task CreateTestBlogAsync(this IFifthweekDbContext databaseContext, Guid newUserId, Guid newBlogId, Guid? headerImageFileId = null, Random random = null)
        {
            if (random == null)
            {
                random = new Random();
            }

            var creator = UserTests.UniqueEntity(random);
            creator.Id = newUserId;

            var blog = UniqueEntity(random);
            blog.Id = newBlogId;
            blog.Creator = creator;
            blog.CreatorId = creator.Id;

            if (headerImageFileId.HasValue)
            {
                var file = FileTests.UniqueEntity(random);
                file.Id = headerImageFileId.Value;
                file.UserId = creator.Id;

                blog.HeaderImageFile = file;
                blog.HeaderImageFileId = file.Id;
            }

            databaseContext.Blogs.Add(blog);
            return databaseContext.SaveChangesAsync();
        }

        public static BlogEntitiesResult UniqueEntityWithForeignEntities(
            Guid newUserId,
            Guid newChannelId,
            Guid newQueueId,
            Guid newBlogId = default(Guid))
        {
            var random = new Random();
            var creator = UserTests.UniqueEntity(random);
            creator.Id = newUserId;

            var blog = UniqueEntity(random);
            if (newBlogId != default(Guid))
            {
                blog.Id = newBlogId;
            }

            blog.Creator = creator;
            blog.CreatorId = creator.Id;

            var channel = ChannelTests.UniqueEntity(random);
            channel.Id = newChannelId;
            channel.Blog = blog;
            channel.BlogId = blog.Id;

            var queue = QueueTests.UniqueEntity(random);
            queue.Id = newQueueId;
            queue.Blog = blog;
            queue.BlogId = blog.Id;

            return new BlogEntitiesResult(creator, blog, channel, queue);
        }

        public static async Task<BlogEntitiesResult> CreateTestEntitiesAsync(
            this IFifthweekDbContext databaseContext,
            Guid newUserId,
            Guid newChannelId,
            Guid newCollectionId,
            Guid newBlogId = default(Guid))
        {
            var entites = UniqueEntityWithForeignEntities(newUserId, newChannelId, newCollectionId, newBlogId);
            databaseContext.Channels.Add(entites.Channel);
            databaseContext.Queues.Add(entites.Queue);
            await databaseContext.SaveChangesAsync();
            return entites;
        }

        public class BlogEntitiesResult 
        {
            public BlogEntitiesResult(FifthweekUser user, Blog blog, Channel channel, Queue queue)
            {
                this.User = user;
                this.Blog = blog;
                this.Channel = channel;
                this.Queue = queue;
            }

            public FifthweekUser User { get; private set; }

            public Blog Blog { get; private set; }

            public Channel Channel { get; private set; }

            public Queue Queue { get; private set; }
        }
    }
}