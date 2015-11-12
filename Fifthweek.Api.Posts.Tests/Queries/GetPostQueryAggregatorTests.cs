namespace Fifthweek.Api.Posts.Tests.Queries
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlTypes;
    using System.Threading.Tasks;

    using Fifthweek.Api.Azure;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Identity.Tests.Shared.Membership;
    using Fifthweek.Api.Posts.Queries;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class GetPostQueryAggregatorTests
    {
        private static readonly PostId PostId = new PostId(Guid.NewGuid());
        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly UserId CreatorId = new UserId(Guid.NewGuid());
        private static readonly string Username = "username";
        private static readonly FileId ProfileImageFileId = FileId.Random();
        private static readonly FileId HeaderImageFileId = FileId.Random();
        private static readonly Introduction Introduction = new Introduction("intro");
        private static readonly GetPreviewNewsfeedQueryResult.PreviewPostCreator Creator =
            new GetPreviewNewsfeedQueryResult.PreviewPostCreator(
                new Username(Username),
                new FileInformation(ProfileImageFileId, "container"));
        private static readonly BlogId BlogId = new BlogId(Guid.NewGuid());
        private static readonly string BlogName = "blog-name";
        private static readonly GetPreviewNewsfeedQueryResult.PreviewPostBlog Blog =
            new GetPreviewNewsfeedQueryResult.PreviewPostBlog(
                new BlogName(BlogName),
                new FileInformation(HeaderImageFileId, "container"),
                Introduction);
        private static readonly ChannelId ChannelId = ChannelId.Random();
        private static readonly string ChannelName = "channel-name";
        private static readonly GetPreviewNewsfeedQueryResult.PreviewPostChannel Channel =
            new GetPreviewNewsfeedQueryResult.PreviewPostChannel(
                new ChannelName(ChannelName));
        private static readonly PreviewText PreviewText = new PreviewText("Hey guys preview!");
        private static readonly Comment Content = new Comment("Hey guys!");
        private static readonly Comment PreviewContent = new Comment("xxx xxxx!");
        private static readonly DateTime Now = new SqlDateTime(DateTime.UtcNow).Value;
        private static readonly DateTime Timestamp = Now;
        private static readonly int PreviewWordCount = 11;
        private static readonly int WordCount = 22;
        private static readonly int ImageCount = 33;
        private static readonly int FileCount = 44;
        private static readonly int VideoCount = 77;
        private static readonly int CommentsCount = 55;
        private static readonly int LikesCount = 66;

        private static readonly FileId FileId1 = FileId.Random();
        private static readonly string FileName1 = "FileName1";
        private static readonly string FileExtension1 = "FileExtension1";
        private static readonly string FilePurpose1 = FilePurposes.PostFile;
        private static readonly long FileSize1 = 1024;
        private static readonly int FileWidth1 = 800;
        private static readonly int FileHeight1 = 600;

        private static readonly FileId FileId2 = FileId.Random();
        private static readonly string FileName2 = "FileName2";
        private static readonly string FileExtension2 = "FileExtension2";
        private static readonly string FilePurpose2 = FilePurposes.PostImage;
        private static readonly long FileSize2 = 10242;
        private static readonly int FileWidth2 = 8002;
        private static readonly int FileHeight2 = 6002;

        private static readonly string ContentType1 = "ContentType1";
        private static readonly string ContentType2 = "ContentType2";

        private static readonly string ContainerName1 = "ContainerName1";
        private static readonly string ContainerName2 = "ContainerName2";

        private static readonly DateTime CreationDate = Now.AddDays(-12);
        private static readonly DateTime LiveDate = Now.AddDays(-10);
        private static readonly DateTime PublicExpiry = Now.AddDays(2);
        private static readonly DateTime PrivateExpiry = Now.AddDays(1);
        private static readonly AccessSignatureExpiryInformation Expiry = new AccessSignatureExpiryInformation(PublicExpiry, PrivateExpiry);

        private static readonly BlobSharedAccessInformation BlobSharedAccessInformation1 =
            new BlobSharedAccessInformation(ContainerName1, "blob1", "uri1", "sig1", PrivateExpiry);

        private static readonly BlobSharedAccessInformation BlobSharedAccessInformation2 =
            new BlobSharedAccessInformation(ContainerName2, "blob2", "uri2", "sig2", PrivateExpiry);
        private static readonly BlobSharedAccessInformation BlobSharedAccessInformation2Preview =
            new BlobSharedAccessInformation(ContainerName2, "blob2x", "uri2x", "sig2x", PrivateExpiry);

        private static readonly GetPostDbResult PostData = new GetPostDbResult(
            new PreviewNewsfeedPost(
                CreatorId, Username, ProfileImageFileId, HeaderImageFileId, Introduction, PostId, BlogId, BlogName, ChannelId, ChannelName,
                PreviewText, Content, FileId.Random(), PreviewWordCount, WordCount, ImageCount, FileCount, VideoCount,
                LiveDate, null, null, null, null, null, LikesCount, CommentsCount, true, CreationDate),
                new List<GetPostDbResult.PostFileDbResult>
                {
                    new GetPostDbResult.PostFileDbResult(FileId1, FileName1, FileExtension1, FilePurpose1, FileSize1, FileWidth1, FileHeight1),
                    new GetPostDbResult.PostFileDbResult(FileId2, FileName2, FileExtension2, FilePurpose2, FileSize2, FileWidth2, FileHeight2),
                });

        private Mock<IFileInformationAggregator> fileInformationAggregator;
        private Mock<IBlobService> blobService;
        private Mock<IMimeTypeMap> mimeTypeMap;
        private Mock<IGetPostPreviewContent> getPostPreviewContent;

        private GetPostQueryAggregator target;

        [TestInitialize]
        public void Initialize()
        {
            this.fileInformationAggregator = new Mock<IFileInformationAggregator>();
            this.blobService = new Mock<IBlobService>(MockBehavior.Strict);
            this.mimeTypeMap = new Mock<IMimeTypeMap>();
            this.getPostPreviewContent = new Mock<IGetPostPreviewContent>(MockBehavior.Strict);

            this.mimeTypeMap.Setup(v => v.GetMimeType(FileExtension1)).Returns(ContentType1);
            this.mimeTypeMap.Setup(v => v.GetMimeType(FileExtension2)).Returns(ContentType2);

            this.fileInformationAggregator.Setup(v => v.GetFileInformationAsync(null, ProfileImageFileId, FilePurposes.ProfileImage))
                .ReturnsAsync(Creator.ProfileImage);
            this.fileInformationAggregator.Setup(v => v.GetFileInformationAsync(null, HeaderImageFileId, FilePurposes.ProfileHeaderImage))
                .ReturnsAsync(Blog.HeaderImage);
            this.fileInformationAggregator.Setup(v => v.GetFileInformationAsync(ChannelId, FileId1, FilePurpose1))
                .ReturnsAsync(new FileInformation(FileId1, ContainerName1));
            this.fileInformationAggregator.Setup(v => v.GetFileInformationAsync(ChannelId, FileId2, FilePurpose2))
                .ReturnsAsync(new FileInformation(FileId2, ContainerName2));

            this.blobService.Setup(v => v.GetBlobSharedAccessInformationForReadingAsync(
                ContainerName1,
                FileId1.Value.EncodeGuid(),
                PrivateExpiry)).ReturnsAsync(BlobSharedAccessInformation1);

            this.blobService.Setup(v => v.GetBlobSharedAccessInformationForReadingAsync(
                ContainerName2,
                FileId2.Value.EncodeGuid() + "/" + FileManagement.Shared.Constants.PostFeedImageThumbnailName,
                PrivateExpiry)).ReturnsAsync(BlobSharedAccessInformation2);

            this.blobService.Setup(v => v.GetBlobSharedAccessInformationForReadingAsync(
                ContainerName2,
                FileId2.Value.EncodeGuid() + "/" + FileManagement.Shared.Constants.PostPreviewImageThumbnailName,
                PrivateExpiry)).ReturnsAsync(BlobSharedAccessInformation2Preview);

            this.getPostPreviewContent.Setup(v => v.Execute(Content.Value, PreviewText)).Returns(PreviewContent.Value);

            this.target = new GetPostQueryAggregator(
                this.fileInformationAggregator.Object,
                this.blobService.Object,
                this.mimeTypeMap.Object,
                this.getPostPreviewContent.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task ItShouldRequirePostData()
        {
            await this.target.ExecuteAsync(null, true, true, Expiry);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task ItShouldRequireExpiry()
        {
            await this.target.ExecuteAsync(PostData, true, true, null);
        }

        [TestMethod]
        public async Task ItShouldReturnExpectedPost_WhenNotHasAccessAndNotPreview()
        {
            await this.PerformTest(false, false, Content, BlobSharedAccessInformation1, BlobSharedAccessInformation2);
        }

        [TestMethod]
        public async Task ItShouldReturnExpectedPost_WhenHasAccessAndNotPreview()
        {
            await this.PerformTest(true, false, Content, null, null);
        }

        [TestMethod]
        public async Task ItShouldReturnExpectedPost_WhenNotHasAccessAndPreview()
        {
            await this.PerformTest(false, true, PreviewContent, null, BlobSharedAccessInformation2Preview);
        }

        [TestMethod]
        public async Task ItShouldReturnExpectedPost_WhenHasAccessAndPreview()
        {
            await this.PerformTest(true, true, PreviewContent, null, null);
        }

        private async Task PerformTest(
            bool hasAccess,
            bool isPreview,
            Comment expectedContent,
            BlobSharedAccessInformation expectedFileSharedAccessInformation,
            BlobSharedAccessInformation expectedImageSharedAccessInformation)
        {
            var result = await this.target.ExecuteAsync(PostData, hasAccess, isPreview, Expiry);

            Assert.AreEqual(
                new GetPostQueryResult(
                    new GetPostQueryResult.FullPost(
                        CreatorId,
                        Creator,
                        PostId,
                        BlogId,
                        Blog,
                        ChannelId,
                        Channel,
                        expectedContent,
                        PreviewWordCount,
                        WordCount,
                        ImageCount,
                        FileCount,
                        VideoCount,
                        LiveDate,
                        LikesCount,
                        CommentsCount,
                        true),
                    new List<GetPostQueryResult.File>
                    {
                        new GetPostQueryResult.File(
                            new FileInformation(FileId1, ContainerName1),
                            new FileSourceInformation(FileName1, FileExtension1, ContentType1, FileSize1, new RenderSize(FileWidth1, FileHeight1)),
                            expectedFileSharedAccessInformation),
                        new GetPostQueryResult.File(
                            new FileInformation(FileId2, ContainerName2),
                            new FileSourceInformation(FileName2, FileExtension2, ContentType2, FileSize2, new RenderSize(FileWidth2, FileHeight2)),
                            expectedImageSharedAccessInformation),
                    }),
                result);
        }
    }
}