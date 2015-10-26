namespace Fifthweek.Api.Posts.Queries
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Dapper;

    using Fifthweek.Api.Azure;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class GetPreviewNewsfeedQueryHandler : IQueryHandler<GetNewsfeedQuery, GetPreviewNewsfeedQueryResult>
    {
        private readonly IRequesterSecurity requesterSecurity;
        private readonly IGetPreviewNewsfeedDbStatement getPreviewNewsfeedDbStatement;
        private readonly IFileInformationAggregator fileInformationAggregator;
        private readonly IMimeTypeMap mimeTypeMap;
        private readonly IBlobService blobService;
        private readonly ITimestampCreator timestampCreator;
        private readonly IGetAccessSignatureExpiryInformation getAccessSignatureExpiryInformation;

        public async Task<GetPreviewNewsfeedQueryResult> HandleAsync(GetNewsfeedQuery query)
        {
            query.AssertNotNull("query");

            var requestingUserId = await this.requesterSecurity.TryAuthenticateAsync(query.Requester);

            var now = this.timestampCreator.Now();
            var postsResult = await this.getPreviewNewsfeedDbStatement.ExecuteAsync(
                requestingUserId, 
                query.CreatorId,
                query.ChannelIds,
                now,
                query.Origin ?? now,
                query.SearchForwards,
                query.StartIndex,
                query.Count);

            var posts = postsResult.Posts;

            var expiry = this.getAccessSignatureExpiryInformation.Execute(now);

            var results = new List<GetPreviewNewsfeedQueryResult.PreviewPost>();
            foreach (var post in posts)
            {
                FileInformation profileImage = null;
                if (post.ProfileImageFileId != null)
                {
                    profileImage = await this.fileInformationAggregator.GetFileInformationAsync(
                        null,
                        post.ProfileImageFileId,
                        FilePurposes.ProfileImage);
                }

                FileInformation image = null;
                BlobSharedAccessInformation imageAccessInformation = null;
                if (post.ImageId != null)
                {
                    image = await this.fileInformationAggregator.GetFileInformationAsync(
                       post.ChannelId,
                       post.ImageId,
                       FilePurposes.PostImage);

                    imageAccessInformation = await this.blobService.GetBlobSharedAccessInformationForReadingAsync(
                        image.ContainerName, 
                        image.FileId.Value.EncodeGuid() + "/" + FileManagement.Shared.Constants.PostPreviewImageThumbnailName,
                        expiry.Public);
                }

                RenderSize imageRenderSize = null;
                if (post.ImageRenderWidth.HasValue && post.ImageRenderHeight.HasValue)
                {
                    imageRenderSize = new RenderSize(post.ImageRenderWidth.Value, post.ImageRenderHeight.Value);
                }

                var completePost = new GetPreviewNewsfeedQueryResult.PreviewPost(
                    post.CreatorId,
                    new GetPreviewNewsfeedQueryResult.PreviewPostCreator(new Username(post.Username), profileImage),
                    post.PostId,
                    post.BlogId,
                    new GetPreviewNewsfeedQueryResult.PreviewPostBlog(new BlogName(post.BlogName)),
                    post.ChannelId,
                    new GetPreviewNewsfeedQueryResult.PreviewPostChannel(new ChannelName(post.ChannelName)),
                    post.PreviewText,
                    image,
                    image == null ? null : new FileSourceInformation(post.ImageName, post.ImageExtension, this.mimeTypeMap.GetMimeType(post.ImageExtension), post.ImageSize ?? 0, imageRenderSize),
                    imageAccessInformation,
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

            return new GetPreviewNewsfeedQueryResult(results);
        }
    }
}