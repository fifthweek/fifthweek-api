namespace Fifthweek.Api.Posts.Queries
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Fifthweek.Api.Azure;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class GetPostQueryHandler : IQueryHandler<GetPostQuery, GetPostQueryResult>
    {
        private readonly IRequesterSecurity requesterSecurity;
        private readonly IPostSecurity postSecurity;
        private readonly IGetPostDbStatement getPostDbStatement;
        private readonly IGetAccessSignatureExpiryInformation getAccessSignatureExpiryInformation;
        private readonly IFileInformationAggregator fileInformationAggregator;
        private readonly IBlobService blobService;
        private readonly IMimeTypeMap mimeTypeMap;
        private readonly IRequestFreePostDbStatement requestFreePost;

        public async Task<GetPostQueryResult> HandleAsync(GetPostQuery query)
        {
            query.AssertNotNull("query");

            var requestingUserId = await this.requesterSecurity.AuthenticateAsync(query.Requester);

            var result = await this.getPostDbStatement.ExecuteAsync(requestingUserId, query.PostId);
            if (result == null)
            {
                return null;
            }

            var hasAccess = await this.postSecurity.IsReadAllowedAsync(requestingUserId, query.PostId, query.Timestamp);
            if (!hasAccess)
            {
                await this.requestFreePost.ExecuteAsync(requestingUserId, query.PostId);
            }

            var post = GetFullPost(result.Post);

            var expiry = this.getAccessSignatureExpiryInformation.Execute(query.Timestamp);

            var files = new List<GetPostQueryResult.File>();
            foreach (var file in result.Files)
            {
                var fileInformation = await this.fileInformationAggregator.GetFileInformationAsync(
                    post.ChannelId,
                    file.FileId,
                    file.Purpose);
                
                RenderSize renderSize = null;
                if (file.RenderWidth.HasValue && file.RenderHeight.HasValue)
                {
                    renderSize = new RenderSize(file.RenderWidth.Value, file.RenderHeight.Value);
                }

                var fileSourceInformation = new FileSourceInformation(
                    file.FileName,
                    file.FileExtension,
                    this.mimeTypeMap.GetMimeType(file.FileExtension),
                    file.FileSize,
                    renderSize);

                BlobSharedAccessInformation imageAccessInformation = null;
                if (!hasAccess && file.Purpose == FilePurposes.PostImage)
                {
                    imageAccessInformation = await this.blobService.GetBlobSharedAccessInformationForReadingAsync(
                        fileInformation.ContainerName,
                        file.FileId.Value.EncodeGuid() + "/" + FileManagement.Shared.Constants.PostFeedImageThumbnailName,
                        expiry.Private);
                }

                var completeFile = new GetPostQueryResult.File(
                    fileInformation,
                    fileSourceInformation,
                    imageAccessInformation);

                files.Add(completeFile);
            }

            return new GetPostQueryResult(post, files);
        }

        private static GetPostQueryResult.FullPost GetFullPost(NewsfeedPost input)
        {
            return new GetPostQueryResult.FullPost(
                input.CreatorId,
                input.PostId,
                input.BlogId,
                input.ChannelId,
                input.Content,
                input.PreviewWordCount,
                input.WordCount,
                input.ImageCount,
                input.FileCount,
                input.LiveDate,
                input.LikesCount,
                input.CommentsCount,
                input.HasLikedPost);
        }
    }
}