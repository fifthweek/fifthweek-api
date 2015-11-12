namespace Fifthweek.Api.Posts.Queries
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Fifthweek.Api.Azure;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class GetPostQueryAggregator : IGetPostQueryAggregator
    {
        private readonly IFileInformationAggregator fileInformationAggregator;
        private readonly IBlobService blobService;
        private readonly IMimeTypeMap mimeTypeMap;
        private readonly IGetPostPreviewContent getPostPreviewContent;

        public async Task<GetPostQueryResult> ExecuteAsync(GetPostDbResult postData, bool hasAccess, bool isPreview, AccessSignatureExpiryInformation expiry)
        {
            postData.AssertNotNull("postData");
            expiry.AssertNotNull("expiry");

            var post = await this.ProcessPost(postData.Post, isPreview);

            var files = new List<GetPostQueryResult.File>();
            foreach (var file in postData.Files)
            {
                var completeFile = await this.ProcessFile(post, file, hasAccess, isPreview, expiry);
                files.Add(completeFile);
            }

            return new GetPostQueryResult(post, files);
        }

        private async Task<GetPostQueryResult.File> ProcessFile(GetPostQueryResult.FullPost post, GetPostDbResult.PostFileDbResult file, bool hasAccess, bool isPreview, AccessSignatureExpiryInformation expiry)
        {
            var fileInformation =
                await this.fileInformationAggregator.GetFileInformationAsync(post.ChannelId, file.FileId, file.Purpose);

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

            BlobSharedAccessInformation accessInformation = null;
            if (!hasAccess)
            {
                if (isPreview)
                {
                    if (file.Purpose == FilePurposes.PostImage)
                    {
                        accessInformation =
                            await
                            this.blobService.GetBlobSharedAccessInformationForReadingAsync(
                                fileInformation.ContainerName,
                                file.FileId.Value.EncodeGuid() + "/" + FileManagement.Shared.Constants.PostPreviewImageThumbnailName,
                                expiry.Private);
                    }
                }
                else
                {
                    if (file.Purpose == FilePurposes.PostImage)
                    {
                        accessInformation =
                           await
                           this.blobService.GetBlobSharedAccessInformationForReadingAsync(
                               fileInformation.ContainerName,
                               file.FileId.Value.EncodeGuid() + "/" + FileManagement.Shared.Constants.PostFeedImageThumbnailName,
                               expiry.Private);
                    }
                    else if (file.Purpose == FilePurposes.PostFile)
                    {
                        accessInformation =
                           await
                           this.blobService.GetBlobSharedAccessInformationForReadingAsync(
                               fileInformation.ContainerName,
                               file.FileId.Value.EncodeGuid(),
                               expiry.Private);
                    }
                }
            }

            var completeFile = new GetPostQueryResult.File(fileInformation, fileSourceInformation, accessInformation);
            return completeFile;
        }

        private async Task<GetPostQueryResult.FullPost> ProcessPost(PreviewNewsfeedPost post, bool isPreview)
        {
            FileInformation profileImage = null;
            if (post.ProfileImageFileId != null)
            {
                profileImage = await this.fileInformationAggregator.GetFileInformationAsync(
                    null,
                    post.ProfileImageFileId,
                    FilePurposes.ProfileImage);
            }

            FileInformation headerImage = null;
            if (post.HeaderImageFileId != null)
            {
                headerImage = await this.fileInformationAggregator.GetFileInformationAsync(
                    null,
                    post.HeaderImageFileId,
                    FilePurposes.ProfileHeaderImage);
            }

            var postContent = post.Content.Value;
            if (isPreview)
            {
                postContent = this.getPostPreviewContent.Execute(postContent, post.PreviewText);
            }

            return new GetPostQueryResult.FullPost(
                post.CreatorId,
                new GetPreviewNewsfeedQueryResult.PreviewPostCreator(new Username(post.Username), profileImage),
                post.PostId,
                post.BlogId,
                new GetPreviewNewsfeedQueryResult.PreviewPostBlog(new BlogName(post.BlogName), headerImage, post.Introduction),
                post.ChannelId,
                new GetPreviewNewsfeedQueryResult.PreviewPostChannel(new ChannelName(post.ChannelName)),
                new Comment(postContent),
                post.PreviewWordCount,
                post.WordCount,
                post.ImageCount,
                post.FileCount,
                post.VideoCount,
                post.LiveDate,
                post.LikesCount,
                post.CommentsCount,
                post.HasLikedPost);
        }
    }
}