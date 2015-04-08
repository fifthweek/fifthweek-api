namespace Fifthweek.Api.Persistence.Tests.Shared
{
    using System;
    using System.Threading.Tasks;

    public static class BlogTests
    {
        public static Blog UniqueEntity(Random random)
        {
            return new Blog(
                Guid.NewGuid(),
                default(Guid),
                null,
                "Name " + random.Next(),
                "Blog tagline " + random.Next(),
                "Blog intro " + random.Next(),
                random.Next(1) == 1 ? null : "Blog description " + random.Next(),
                random.Next(1) == 1 ? null : "http://external/video" + random.Next(),
                null,
                null,
                DateTime.UtcNow.AddDays(random.NextDouble() * -100));
        }

        public static Task CreateTestBlogAsync(this IFifthweekDbContext databaseContext, Guid newUserId, Guid newBlogId, Guid? headerImageFileId = null)
        {
            var random = new Random();
            var creator = UserTests.UniqueEntity(random);
            creator.Id = newUserId;

            var blog = BlogTests.UniqueEntity(random);
            blog.Id = newBlogId;
            blog.Creator = creator;
            blog.CreatorId = creator.Id;

            if (headerImageFileId.HasValue)
            {
                var file = FileTests.UniqueEntity(random);
                file.Id = headerImageFileId.Value;
                file.User = creator;
                file.UserId = creator.Id;

                blog.HeaderImageFile = file;
                blog.HeaderImageFileId = file.Id;
            }

            databaseContext.Blogs.Add(blog);
            return databaseContext.SaveChangesAsync();
        }
    }
}