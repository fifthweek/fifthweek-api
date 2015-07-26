namespace Fifthweek.Api.Blogs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Dapper;

    using Fifthweek.Api.Blogs.Queries;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class GetBlogChannelsAndCollectionsDbStatement : IGetBlogChannelsAndCollectionsDbStatement
    {
        public static readonly string BlogQuery = string.Format(
            @"SELECT * FROM {0} WHERE {1}=@BlogId;",
            Blog.Table,
            Blog.Fields.Id);

        public static readonly string ChannelsQuery = string.Format(
            @"SELECT * FROM {0} WHERE {1} = @BlogId;",
            Channel.Table,
            Channel.Fields.BlogId);

        private static readonly string CollectionsQuery = string.Format(
            @"SELECT col.{0}, col.{1}, ch.{2} AS {3} 
                FROM {4} col 
                INNER JOIN {5} ch ON col.{6} = ch.{7} 
                WHERE ch.{8} = @BlogId;",
            Collection.Fields.Id,
            Collection.Fields.Name,
            Channel.Fields.Id,
            Collection.Fields.ChannelId,
            Collection.Table,
            Channel.Table,
            Collection.Fields.ChannelId,
            Channel.Fields.Id,
            Channel.Fields.BlogId);

        private static readonly string WeeklyReleaseScheduleQuery = string.Format(
            @"SELECT wrt.* FROM {0} wrt
                INNER JOIN {1} col ON wrt.{2} = col.{3} 
                INNER JOIN {4} ch ON col.{5} = ch.{6} 
                WHERE ch.{7} = @BlogId;",
            WeeklyReleaseTime.Table,
            Collection.Table,
            WeeklyReleaseTime.Fields.CollectionId,
            Collection.Fields.Id,
            Channel.Table,
            Collection.Fields.ChannelId,
            Channel.Fields.Id,
            Channel.Fields.BlogId);

        private static readonly string Query = BlogQuery + ChannelsQuery + CollectionsQuery + WeeklyReleaseScheduleQuery;


        private readonly IFifthweekDbConnectionFactory connectionFactory;

        public async Task<GetBlogChannelsAndCollectionsDbResult> ExecuteAsync(BlogId blogId)
        {
            blogId.AssertNotNull("blogId");

            List<Blog> blogs;
            List<Channel> channels;
            List<PartialCollection> collections;
            List<WeeklyReleaseTime> releaseTimes;
            using (var connection = this.connectionFactory.CreateConnection())
            {
                using (var multi = await connection.QueryMultipleAsync(Query, new { BlogId = blogId.Value }))
                {
                    blogs = multi.Read<Blog>().ToList();
                    channels = multi.Read<Channel>().ToList();
                    collections = multi.Read<PartialCollection>().ToList();
                    releaseTimes = multi.Read<WeeklyReleaseTime>().ToList();
                }
            }

            var blog = blogs.SingleOrDefault();

            if (blog == null)
            {
                throw new InvalidOperationException("The blog " + blogId + " couldn't be found.");
            }

            var blogResult = GetBlogDbResult(blog);

            var channelsAndCollectionsResult = 
                (from c in channels
                 select new ChannelResult(
                    new ChannelId(c.Id),
                    c.Name,
                    c.Description,
                    c.Price,
                    c.Id == c.BlogId,
                    c.IsVisibleToNonSubscribers,
                    (from col in collections
                     where col.ChannelId == c.Id
                     select new CollectionResult(
                         new CollectionId(col.Id),
                         col.Name,
                         releaseTimes.Where(wrt => wrt.CollectionId == col.Id).Select(wrt => new HourOfWeek(wrt.HourOfWeek)).ToArray()))
                        .ToArray())).ToList();

            return new GetBlogChannelsAndCollectionsDbResult(blogResult, channelsAndCollectionsResult);
        }

        public static BlogDbResult GetBlogDbResult(Blog blog)
        {
            var blogResult = new BlogDbResult(
                new BlogId(blog.Id),
                new UserId(blog.CreatorId),
                new BlogName(blog.Name),
                new Tagline(blog.Tagline),
                new Introduction(blog.Introduction),
                DateTime.SpecifyKind(blog.CreationDate, DateTimeKind.Utc),
                blog.HeaderImageFileId == null ? null : new FileId(blog.HeaderImageFileId.Value),
                blog.ExternalVideoUrl == null ? null : new ExternalVideoUrl(blog.ExternalVideoUrl),
                blog.Description == null ? null : new BlogDescription(blog.Description));

            return blogResult;
        }

        public class PartialCollection
        {
            public Guid Id { get; set; }

            public Guid ChannelId { get; set; }

            public string Name { get; set; }
        }

        [AutoConstructor]
        public partial class GetBlogChannelsAndCollectionsDbResult
        {
            public BlogDbResult Blog { get; private set; }

            public IReadOnlyList<ChannelResult> Channels { get; private set; }
        }

        [AutoConstructor]
        public partial class BlogDbResult
        {
            public BlogId BlogId { get; private set; }

            public UserId CreatorId { get; private set; }

            public BlogName BlogName { get; private set; }

            public Tagline Tagline { get; private set; }

            public Introduction Introduction { get; private set; }

            public DateTime CreationDate { get; private set; }

            [Optional]
            public FileId HeaderImageFileId { get; private set; }

            [Optional]
            public ExternalVideoUrl Video { get; private set; }

            [Optional]
            public BlogDescription Description { get; private set; }
        }
    }
}