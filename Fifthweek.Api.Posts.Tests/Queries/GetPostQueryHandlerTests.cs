namespace Fifthweek.Api.Posts.Tests.Queries
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlTypes;
    using System.Threading.Tasks;

    using Fifthweek.Api.Azure;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Collections.Shared;
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
    public class GetPostQueryHandlerTests
    {
        private static readonly PostId PostId = new PostId(Guid.NewGuid());
        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly UserId CreatorId = new UserId(Guid.NewGuid());
        private static readonly BlogId BlogId = new BlogId(Guid.NewGuid());
        private static readonly Requester Requester = Requester.Authenticated(UserId);
        private static readonly ChannelId ChannelId = ChannelId.Random();
        private static readonly Comment Content = new Comment("Hey guys!");
        private static readonly DateTime Now = new SqlDateTime(DateTime.UtcNow).Value;
        private static readonly DateTime Timestamp = Now;
        private static readonly int PreviewWordCount = 11;
        private static readonly int WordCount = 22;
        private static readonly int ImageCount = 33;
        private static readonly int FileCount = 44;
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

        private static readonly BlobSharedAccessInformation BlobSharedAccessInformation2 =
            new BlobSharedAccessInformation(ContainerName2, "blob2", "uri2", "sig2", PrivateExpiry);
        
        private static readonly GetPostDbResult Result = new GetPostDbResult(
            new NewsfeedPost(
                CreatorId, PostId, BlogId, ChannelId, null, Content, FileId.Random(), PreviewWordCount, WordCount, ImageCount, FileCount,
                LiveDate, null, null, null, null, null, LikesCount, CommentsCount, true, CreationDate),
                new List<GetPostDbResult.PostFileDbResult>
                {
                    new GetPostDbResult.PostFileDbResult(FileId1, FileName1, FileExtension1, FilePurpose1, FileSize1, FileWidth1, FileHeight1),
                    new GetPostDbResult.PostFileDbResult(FileId2, FileName2, FileExtension2, FilePurpose2, FileSize2, FileWidth2, FileHeight2),
                });

        private Mock<IRequesterSecurity> requesterSecurity;
        private Mock<IPostSecurity> postSecurity;
        private Mock<IGetPostDbStatement> getPostDbStatement;
        private Mock<IGetAccessSignatureExpiryInformation> getAccessSignatureExpiryInformation;
        private Mock<IFileInformationAggregator> fileInformationAggregator;
        private Mock<IBlobService> blobService;
        private Mock<IMimeTypeMap> mimeTypeMap;
        private Mock<IRequestFreePostDbStatement> requestFreePost;

        private GetPostQueryHandler target;

        [TestInitialize]
        public void Initialize()
        {
            this.requesterSecurity = new Mock<IRequesterSecurity>();
            this.postSecurity = new Mock<IPostSecurity>();
            this.getPostDbStatement = new Mock<IGetPostDbStatement>();
            this.getAccessSignatureExpiryInformation = new Mock<IGetAccessSignatureExpiryInformation>();
            this.fileInformationAggregator = new Mock<IFileInformationAggregator>();
            this.blobService = new Mock<IBlobService>(MockBehavior.Strict);
            this.mimeTypeMap = new Mock<IMimeTypeMap>();
            this.requestFreePost = new Mock<IRequestFreePostDbStatement>(MockBehavior.Strict);

            this.requesterSecurity.SetupFor(Requester);

            this.getAccessSignatureExpiryInformation.Setup(v => v.Execute(Now))
                .Returns(new AccessSignatureExpiryInformation(PublicExpiry, PrivateExpiry));

            this.mimeTypeMap.Setup(v => v.GetMimeType(FileExtension1)).Returns(ContentType1);
            this.mimeTypeMap.Setup(v => v.GetMimeType(FileExtension2)).Returns(ContentType2);

            this.fileInformationAggregator.Setup(v => v.GetFileInformationAsync(ChannelId, FileId1, FilePurpose1))
                .ReturnsAsync(new FileInformation(FileId1, ContainerName1));
            this.fileInformationAggregator.Setup(v => v.GetFileInformationAsync(ChannelId, FileId2, FilePurpose2))
                .ReturnsAsync(new FileInformation(FileId2, ContainerName2));

            this.blobService.Setup(v => v.GetBlobSharedAccessInformationForReadingAsync(
                ContainerName2, 
                FileId2.Value.EncodeGuid() + "/" + FileManagement.Shared.Constants.PostFeedImageThumbnailName,
                PrivateExpiry)).ReturnsAsync(BlobSharedAccessInformation2);

            this.target = new GetPostQueryHandler(
                this.requesterSecurity.Object,
                this.postSecurity.Object,
                this.getPostDbStatement.Object,
                this.getAccessSignatureExpiryInformation.Object,
                this.fileInformationAggregator.Object,
                this.blobService.Object,
                this.mimeTypeMap.Object,
                this.requestFreePost.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task ItShouldRequireQuery()
        {
            await this.target.HandleAsync(null);
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedException))]
        public async Task ItShouldRequireAuthenticatedUser()
        {
            this.requesterSecurity.Setup(_ => _.AuthenticateAsync(Requester.Unauthenticated)).Throws<UnauthorizedException>();

            await this.target.HandleAsync(new GetPostQuery(Requester.Unauthenticated, PostId, Timestamp));
        }

        [TestMethod]
        public async Task WhenPostDoesNotExist_ItShouldReturnNull()
        {
            this.requesterSecurity.Setup(v => v.TryAuthenticateAsync(Requester)).ReturnsAsync(UserId);
            this.getPostDbStatement.Setup(v => v.ExecuteAsync(UserId, PostId)).ReturnsAsync(null);

            var result = await this.target.HandleAsync(new GetPostQuery(Requester, PostId, Timestamp));

            Assert.IsNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(DivideByZeroException))]
        public async Task WhenFreePostRejected_ItShouldPropagateException()
        {
            this.requesterSecurity.Setup(v => v.TryAuthenticateAsync(Requester)).ReturnsAsync(UserId);
            this.getPostDbStatement.Setup(v => v.ExecuteAsync(UserId, PostId)).ReturnsAsync(Result);
            this.postSecurity.Setup(v => v.IsReadAllowedAsync(UserId, PostId, Timestamp)).ReturnsAsync(false);
            this.requestFreePost.Setup(v => v.ExecuteAsync(UserId, PostId)).Throws(new DivideByZeroException());

            await this.target.HandleAsync(new GetPostQuery(Requester, PostId, Timestamp));
        }

        [TestMethod]
        public async Task ItShouldReturnPostWhenReadAllowed()
        {
            this.requesterSecurity.Setup(v => v.TryAuthenticateAsync(Requester)).ReturnsAsync(UserId);
            this.getPostDbStatement.Setup(v => v.ExecuteAsync(UserId, PostId)).ReturnsAsync(Result);
            this.postSecurity.Setup(v => v.IsReadAllowedAsync(UserId, PostId, Timestamp)).ReturnsAsync(true);

            var result = await this.target.HandleAsync(new GetPostQuery(Requester, PostId, Timestamp));

            Assert.AreEqual(
                new GetPostQueryResult(
                    new GetPostQueryResult.FullPost(
                        CreatorId,
                        PostId,
                        BlogId,
                        ChannelId,
                        Content,
                        PreviewWordCount,
                        WordCount,
                        ImageCount,
                        FileCount,
                        LiveDate,
                        LikesCount,
                        CommentsCount,
                        true),
                    new List<GetPostQueryResult.File>
                    {
                        new GetPostQueryResult.File(
                            new FileInformation(FileId1, ContainerName1),
                            new FileSourceInformation(FileName1, FileExtension1, ContentType1, FileSize1, new RenderSize(FileWidth1, FileHeight1)),
                            null),
                        new GetPostQueryResult.File(
                            new FileInformation(FileId2, ContainerName2),
                            new FileSourceInformation(FileName2, FileExtension2, ContentType2, FileSize2, new RenderSize(FileWidth2, FileHeight2)),
                            null),
                    }),
                result);
        }

        [TestMethod]
        public async Task ItShouldReturnPostWhenFreePostRequestSucceeds()
        {
            this.requesterSecurity.Setup(v => v.TryAuthenticateAsync(Requester)).ReturnsAsync(UserId);
            this.getPostDbStatement.Setup(v => v.ExecuteAsync(UserId, PostId)).ReturnsAsync(Result);
            this.postSecurity.Setup(v => v.IsReadAllowedAsync(UserId, PostId, Timestamp)).ReturnsAsync(false);
            this.requestFreePost.Setup(v => v.ExecuteAsync(UserId, PostId)).Returns(Task.FromResult(0));

            var result = await this.target.HandleAsync(new GetPostQuery(Requester, PostId, Timestamp));

            Assert.AreEqual(
                new GetPostQueryResult(
                    new GetPostQueryResult.FullPost(
                        CreatorId,
                        PostId,
                        BlogId,
                        ChannelId,
                        Content,
                        PreviewWordCount,
                        WordCount,
                        ImageCount,
                        FileCount,
                        LiveDate,
                        LikesCount,
                        CommentsCount,
                        true),
                    new List<GetPostQueryResult.File>
                    {
                        new GetPostQueryResult.File(
                            new FileInformation(FileId1, ContainerName1),
                            new FileSourceInformation(FileName1, FileExtension1, ContentType1, FileSize1, new RenderSize(FileWidth1, FileHeight1)),
                            null),
                        new GetPostQueryResult.File(
                            new FileInformation(FileId2, ContainerName2),
                            new FileSourceInformation(FileName2, FileExtension2, ContentType2, FileSize2, new RenderSize(FileWidth2, FileHeight2)),
                            BlobSharedAccessInformation2),
                    }),
                result);
        }
    }
}