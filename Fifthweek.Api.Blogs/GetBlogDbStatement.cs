namespace Fifthweek.Api.Blogs
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Dapper;

    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class GetBlogDbStatement : IGetBlogDbStatement
    {
        private readonly IFifthweekDbConnectionFactory connectionFactory;

        public async Task<GetSubscriptionDataDbResult> ExecuteAsync(BlogId blogId)
        {
            blogId.AssertNotNull("subscriptionId");

            using (var connection = this.connectionFactory.CreateConnection())
            {
                var items = await connection.QueryAsync<Blog>(
                    string.Format(
                        @"SELECT * FROM {0} WHERE {1}=@{1}",
                        Blog.Table,
                        Blog.Fields.Id),
                    new { Id = blogId.Value });

                var result = items.SingleOrDefault();

                if (result == null)
                {
                    throw new InvalidOperationException("The subscription " + blogId + " couldn't be found.");
                }

                return new GetSubscriptionDataDbResult(
                    new BlogId(result.Id), 
                    new UserId(result.CreatorId), 
                    new BlogName(result.Name),
                    new Tagline(result.Tagline),
                    new Introduction(result.Introduction),
                    result.CreationDate,
                    result.HeaderImageFileId == null ? null : new FileId(result.HeaderImageFileId.Value),
                    result.ExternalVideoUrl == null ? null : new ExternalVideoUrl(result.ExternalVideoUrl),
                    result.Description == null ? null : new BlogDescription(result.Description));
            }
        }

        [AutoConstructor]
        public partial class GetSubscriptionDataDbResult
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