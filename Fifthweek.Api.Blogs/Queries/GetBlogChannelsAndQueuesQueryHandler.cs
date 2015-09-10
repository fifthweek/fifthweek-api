namespace Fifthweek.Api.Blogs.Queries
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class GetBlogChannelsAndQueuesQueryHandler : IQueryHandler<GetBlogChannelsAndQueuesQuery, GetBlogChannelsAndQueuesResult>
    {
        private readonly IFileInformationAggregator fileInformationAggregator;
        private readonly IGetBlogChannelsAndQueuesDbStatement getBlogChannelsAndQueues;

        public async Task<GetBlogChannelsAndQueuesResult> HandleAsync(GetBlogChannelsAndQueuesQuery query)
        {
            query.AssertNotNull("query");

            var queryResult = await this.getBlogChannelsAndQueues.ExecuteAsync(query.UserId);

            if (queryResult == null)
            {
                return null;
            }

            var blog = queryResult.Blog;

            FileInformation headerFileInformation = null;

            if (blog.HeaderImageFileId != null)
            {
                headerFileInformation = await this.fileInformationAggregator.GetFileInformationAsync(
                        null,
                        blog.HeaderImageFileId,
                        FilePurposes.ProfileHeaderImage);
            }

            var blogWithFileInformation = new BlogWithFileInformation(
                blog.BlogId,
                blog.BlogName,
                blog.Introduction,
                blog.CreationDate,
                headerFileInformation,
                blog.Video,
                blog.Description,
                queryResult.Channels,
                blog.Queues);

            return new GetBlogChannelsAndQueuesResult(blogWithFileInformation);
        }
    }
}