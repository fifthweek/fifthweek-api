namespace Fifthweek.Api.Blogs.Queries
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class GetBlogQueryHandler : IQueryHandler<GetBlogQuery, GetBlogResult>
    {
        private readonly IFileInformationAggregator fileInformationAggregator;
        private readonly IGetBlogDbStatement getBlog;

        public async Task<GetBlogResult> HandleAsync(GetBlogQuery query)
        {
            query.AssertNotNull("query");

            var blog = await this.getBlog.ExecuteAsync(query.NewBlogId);

            FileInformation headerFileInformation = null;

            if (blog.HeaderImageFileId != null)
            {
                headerFileInformation = await this.fileInformationAggregator.GetFileInformationAsync(
                        blog.CreatorId,
                        blog.HeaderImageFileId,
                        FilePurposes.ProfileHeaderImage);
            }

            return new GetBlogResult(
                blog.BlogId,
                blog.CreatorId,
                blog.BlogName,
                blog.Tagline,
                blog.Introduction,
                blog.CreationDate,
                headerFileInformation,
                blog.Video,
                blog.Description);
        }
    }
}