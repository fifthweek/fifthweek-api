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
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class GetBlogChannelsAndQueuesDbStatement : IGetBlogChannelsAndQueuesDbStatement
    {
        private static readonly string BlogIdQuery = string.Format(
            @"  DECLARE @BlogId uniqueidentifier;
                SELECT TOP 1 @BlogId = {0}
                FROM {1} WHERE {2}=@UserId ORDER BY blog.{3} DESC;",
            Blog.Fields.Id,
            Blog.Table,
            Blog.Fields.CreatorId,
            Blog.Fields.CreationDate);

        public static readonly string BlogQuery = string.Format(
            @"SELECT * FROM {0} WHERE {1}=@BlogId;",
            Blog.Table,
            Blog.Fields.Id);

        public static readonly string ChannelsQuery = string.Format(
            @"SELECT * FROM {0} WHERE {1} = @BlogId;",
            Channel.Table,
            Channel.Fields.BlogId);

        private static readonly string QueuesQuery = string.Format(
            @"SELECT {0}, {1} FROM {2} WHERE {3} = @BlogId;",
            Queue.Fields.Id,
            Queue.Fields.Name,
            Queue.Table,
            Queue.Fields.BlogId);

        private static readonly string WeeklyReleaseScheduleQuery = string.Format(
            @"SELECT wrt.* FROM {0} wrt
                INNER JOIN {1} q ON wrt.{2} = q.{3} 
                WHERE q.{4} = @BlogId;",
            WeeklyReleaseTime.Table,
            Queue.Table,
            WeeklyReleaseTime.Fields.QueueId,
            Queue.Fields.Id,
            Queue.Fields.BlogId);

        private static readonly string Query = BlogQuery + ChannelsQuery + QueuesQuery + WeeklyReleaseScheduleQuery;

        private readonly IFifthweekDbConnectionFactory connectionFactory;

        public async Task<GetBlogChannelsAndQueuesDbResult> ExecuteAsync(UserId userId)
        {
            userId.AssertNotNull("userId");

            List<Blog> blogs;
            List<Channel> channels;
            List<PartialQueue> queues;
            List<WeeklyReleaseTime> releaseTimes;
            using (var connection = this.connectionFactory.CreateConnection())
            {
                using (var multi = await connection.QueryMultipleAsync(Query, new { UserId = userId.Value }))
                {
                    blogs = multi.Read<Blog>().ToList();
                    channels = multi.Read<Channel>().ToList();
                    queues = multi.Read<PartialQueue>().ToList();
                    releaseTimes = multi.Read<WeeklyReleaseTime>().ToList();
                }
            }

            var blog = blogs.SingleOrDefault();

            if (blog == null)
            {
                return null;
            }

            var queueResult = queues.Select(
                q => new QueueResult(
                    new QueueId(q.Id),
                    q.Name,
                    releaseTimes.Where(wrt => wrt.QueueId == q.Id).Select(wrt => new HourOfWeek(wrt.HourOfWeek)).ToArray())).ToList();

            var blogResult = GetBlogDbResult(blog, queueResult);

            var channelsAndCollectionsResult = channels.Select(
                c => new ChannelResult(
                    new ChannelId(c.Id),
                    c.Name,
                    c.Price,
                    c.IsVisibleToNonSubscribers)).ToList();

            return new GetBlogChannelsAndQueuesDbResult(blogResult, channelsAndCollectionsResult);
        }

        public static BlogDbResult GetBlogDbResult(Blog blog, IReadOnlyList<QueueResult> queues)
        {
            var blogResult = new BlogDbResult(
                new BlogId(blog.Id),
                new UserId(blog.CreatorId),
                new BlogName(blog.Name),
                new Introduction(blog.Introduction),
                DateTime.SpecifyKind(blog.CreationDate, DateTimeKind.Utc),
                blog.HeaderImageFileId == null ? null : new FileId(blog.HeaderImageFileId.Value),
                blog.ExternalVideoUrl == null ? null : new ExternalVideoUrl(blog.ExternalVideoUrl),
                blog.Description == null ? null : new BlogDescription(blog.Description),
                queues);

            return blogResult;
        }

        public class PartialQueue
        {
            public Guid Id { get; set; }

            public Guid ChannelId { get; set; }

            public string Name { get; set; }
        }

        [AutoConstructor]
        public partial class GetBlogChannelsAndQueuesDbResult
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

            public Introduction Introduction { get; private set; }

            public DateTime CreationDate { get; private set; }

            [Optional]
            public FileId HeaderImageFileId { get; private set; }

            [Optional]
            public ExternalVideoUrl Video { get; private set; }

            [Optional]
            public BlogDescription Description { get; private set; }

            public IReadOnlyList<QueueResult> Queues { get; private set; }
        }
    }
}