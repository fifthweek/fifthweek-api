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
    public partial class GetNewsfeedQueryHandler : IQueryHandler<GetNewsfeedQuery, GetNewsfeedQueryResult>
    {
        private readonly IRequesterSecurity requesterSecurity;
        private readonly IGetNewsfeedDbStatement getNewsfeedDbStatement;
        private readonly IFileInformationAggregator fileInformationAggregator;
        private readonly IMimeTypeMap mimeTypeMap;
        private readonly ITimestampCreator timestampCreator;

        public async Task<GetNewsfeedQueryResult> HandleAsync(GetNewsfeedQuery query)
        {
            query.AssertNotNull("query");

            var requestingUserId = await this.requesterSecurity.AuthenticateAsync(query.Requester);

            var now = this.timestampCreator.Now();
            var postsResult = await this.getNewsfeedDbStatement.ExecuteAsync(
                requestingUserId,
                query.CreatorId,
                query.ChannelIds,
                now,
                query.Origin ?? now,
                query.SearchForwards,
                query.StartIndex,
                query.Count);

            var posts = postsResult.Posts;

            var results = new List<GetNewsfeedQueryResult.Post>();
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

                var completePost = new GetNewsfeedQueryResult.Post(
                    post.CreatorId,
                    post.PostId,
                    post.BlogId,
                    post.ChannelId,
                    post.PreviewText,
                    image,
                    image == null ? null : new FileSourceInformation(post.ImageName, post.ImageExtension, this.mimeTypeMap.GetMimeType(post.ImageExtension), post.ImageSize ?? 0, imageRenderSize),
                    post.PreviewWordCount,
                    post.WordCount,
                    post.ImageCount,
                    post.FileCount,
                    post.LiveDate,
                    post.LikesCount,
                    post.CommentsCount,
                    post.HasLikedPost);

                results.Add(completePost);
            }

            return new GetNewsfeedQueryResult(results, postsResult.AccountBalance);
        }
    }
}