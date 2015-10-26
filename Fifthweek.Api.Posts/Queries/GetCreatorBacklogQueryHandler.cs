namespace Fifthweek.Api.Posts.Queries
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Dapper;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class GetCreatorBacklogQueryHandler : IQueryHandler<GetCreatorBacklogQuery, IReadOnlyList<GetCreatorBacklogQueryResult>>
    {
        private readonly IRequesterSecurity requesterSecurity;
        private readonly IGetCreatorBacklogDbStatement getCreatorBacklogDbStatement;
        private readonly IFileInformationAggregator fileInformationAggregator;
        private readonly IMimeTypeMap mimeTypeMap;

        public async Task<IReadOnlyList<GetCreatorBacklogQueryResult>> HandleAsync(GetCreatorBacklogQuery query)
        {
            query.AssertNotNull("query");

            await this.requesterSecurity.AuthenticateAsAsync(query.Requester, query.RequestedUserId);
            await this.requesterSecurity.AssertInRoleAsync(query.Requester, FifthweekRole.Creator);

            var posts = await this.getCreatorBacklogDbStatement.ExecuteAsync(query.RequestedUserId, DateTime.UtcNow);

            var result = new List<GetCreatorBacklogQueryResult>();
            foreach (var post in posts)
            {
                FileInformation image = null;
                if (post.ImageId != null)
                {
                    image = await this.fileInformationAggregator.GetFileInformationAsync(
                       post.ChannelId,
                       post.ImageId,
                       FilePurposes.PostImage);
                }

                RenderSize imageRenderSize = null;
                if (post.ImageRenderWidth.HasValue && post.ImageRenderHeight.HasValue)
                {
                    imageRenderSize = new RenderSize(post.ImageRenderWidth.Value, post.ImageRenderHeight.Value);
                }

                var completePost = new GetCreatorBacklogQueryResult(
                    post.PostId,
                    post.ChannelId,
                    post.QueueId,
                    post.PreviewText,
                    image,
                    image == null ? null : new FileSourceInformation(post.ImageName, post.ImageExtension, this.mimeTypeMap.GetMimeType(post.ImageExtension), post.ImageSize ?? 0, imageRenderSize),
                    post.PreviewWordCount,
                    post.WordCount,
                    post.ImageCount,
                    post.FileCount,
                    post.LiveDate);
                
                result.Add(completePost);
            }

            return result;
        }
    }
}