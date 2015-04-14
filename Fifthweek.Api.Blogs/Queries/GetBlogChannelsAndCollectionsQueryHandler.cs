namespace Fifthweek.Api.Blogs.Queries
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class GetBlogChannelsAndCollectionsQueryHandler : IQueryHandler<GetBlogChannelsAndCollectionsQuery, GetBlogChannelsAndCollectionsResult>
    {
        private readonly IFileInformationAggregator fileInformationAggregator;
        private readonly IGetBlogChannelsAndCollectionsDbStatement getBlogChannelsAndCollections;

        public async Task<GetBlogChannelsAndCollectionsResult> HandleAsync(GetBlogChannelsAndCollectionsQuery query)
        {
            query.AssertNotNull("query");

            var queryResult = await this.getBlogChannelsAndCollections.ExecuteAsync(query.BlogId);

            var blog = queryResult.Blog;
            var channelsAndCollections = queryResult.ChannelsAndCollections;

            FileInformation headerFileInformation = null;

            if (blog.HeaderImageFileId != null)
            {
                headerFileInformation = await this.fileInformationAggregator.GetFileInformationAsync(
                        blog.CreatorId,
                        blog.HeaderImageFileId,
                        FilePurposes.ProfileHeaderImage);
            }

            var blogWithFileInformation = new BlogWithFileInformation(
                blog.BlogId,
                blog.CreatorId,
                blog.BlogName,
                blog.Tagline,
                blog.Introduction,
                blog.CreationDate,
                headerFileInformation,
                blog.Video,
                blog.Description);

            return new GetBlogChannelsAndCollectionsResult(blogWithFileInformation, channelsAndCollections);
        }
    }
}