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
        private static readonly Requester Requester = Requester.Authenticated(UserId);
        private static readonly ChannelId ChannelId = ChannelId.Random();
        private static readonly string ChannelName = "channel-name";
        private static readonly GetPreviewNewsfeedQueryResult.PreviewPostChannel Channel =
            new GetPreviewNewsfeedQueryResult.PreviewPostChannel(
                new ChannelName(ChannelName));
        private static readonly PreviewText PreviewText = new PreviewText("Hey guys preview!");
        private static readonly Comment Content = new Comment("Hey guys!");
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
        
        private static readonly GetPostDbResult Result = new GetPostDbResult(
            new PreviewNewsfeedPost(
                CreatorId, Username, ProfileImageFileId, HeaderImageFileId, Introduction, PostId, BlogId, BlogName, ChannelId, ChannelName,
                PreviewText, Content, FileId.Random(), PreviewWordCount, WordCount, ImageCount, FileCount, VideoCount,
                LiveDate, null, null, null, null, null, LikesCount, CommentsCount, true, true, CreationDate),
                new List<GetPostDbResult.PostFileDbResult>
                {
                    new GetPostDbResult.PostFileDbResult(FileId1, FileName1, FileExtension1, FilePurpose1, FileSize1, FileWidth1, FileHeight1),
                    new GetPostDbResult.PostFileDbResult(FileId2, FileName2, FileExtension2, FilePurpose2, FileSize2, FileWidth2, FileHeight2),
                });

        private static readonly GetPostQueryResult QueryResult = new GetPostQueryResult(
            new GetPostQueryResult.FullPost(
                CreatorId,
                Creator,
                PostId,
                BlogId,
                Blog,
                ChannelId,
                Channel,
                Content,
                PreviewWordCount,
                WordCount,
                ImageCount,
                FileCount,
                VideoCount,
                LiveDate,
                LikesCount,
                CommentsCount,
                true,
                true,
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
            });

        private Mock<IRequesterSecurity> requesterSecurity;
        private Mock<IPostSecurity> postSecurity;
        private Mock<IGetPostDbStatement> getPostDbStatement;
        private Mock<IGetAccessSignatureExpiryInformation> getAccessSignatureExpiryInformation;
        private Mock<IRequestFreePost> requestFreePost;
        private Mock<IGetPostQueryAggregator> getPostQueryAggregator;

        private GetPostQueryHandler target;

        [TestInitialize]
        public void Initialize()
        {
            this.requesterSecurity = new Mock<IRequesterSecurity>();
            this.postSecurity = new Mock<IPostSecurity>(MockBehavior.Strict);
            this.getPostDbStatement = new Mock<IGetPostDbStatement>(MockBehavior.Strict);
            this.getAccessSignatureExpiryInformation = new Mock<IGetAccessSignatureExpiryInformation>();
            this.requestFreePost = new Mock<IRequestFreePost>(MockBehavior.Strict);
            this.getPostQueryAggregator = new Mock<IGetPostQueryAggregator>(MockBehavior.Strict);

            this.requesterSecurity.SetupFor(Requester);

            this.getAccessSignatureExpiryInformation.Setup(v => v.Execute(Now))
                .Returns(Expiry);

            this.target = new GetPostQueryHandler(
                this.requesterSecurity.Object,
                this.postSecurity.Object,
                this.getPostDbStatement.Object,
                this.getAccessSignatureExpiryInformation.Object,
                this.requestFreePost.Object,
                this.getPostQueryAggregator.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task ItShouldRequireQuery()
        {
            await this.target.HandleAsync(null);
        }

        [TestMethod]
        public async Task WhenPostDoesNotExist_ItShouldReturnNull()
        {
            this.requesterSecurity.Setup(v => v.TryAuthenticateAsync(Requester)).ReturnsAsync(UserId);
            this.getPostDbStatement.Setup(v => v.ExecuteAsync(UserId, PostId)).ReturnsAsync(null);

            var result = await this.target.HandleAsync(new GetPostQuery(Requester, PostId, false, Timestamp));

            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task WhenNotSignedIn_ItShoudlReturnPreviewPost()
        {
            this.requesterSecurity.Setup(v => v.TryAuthenticateAsync(Requester)).ReturnsAsync(null);
            this.getPostDbStatement.Setup(v => v.ExecuteAsync(null, PostId)).ReturnsAsync(Result);

            this.getPostQueryAggregator.Setup(v => v.ExecuteAsync(Result, PostSecurityResult.Denied, Expiry))
                .ReturnsAsync(QueryResult)
                .Verifiable();

            var result = await this.target.HandleAsync(new GetPostQuery(Requester, PostId, false, Timestamp));

            Assert.AreEqual(QueryResult, result);
            this.getPostQueryAggregator.Verify();
        }

        [TestMethod]
        public async Task WhenReadAllowed_ItShouldReturnFullPost()
        {
            this.requesterSecurity.Setup(v => v.TryAuthenticateAsync(Requester)).ReturnsAsync(UserId);
            this.getPostDbStatement.Setup(v => v.ExecuteAsync(UserId, PostId)).ReturnsAsync(Result);
            this.postSecurity.Setup(v => v.IsReadAllowedAsync(UserId, PostId, Timestamp)).ReturnsAsync(PostSecurityResult.Subscriber);

            this.getPostQueryAggregator.Setup(v => v.ExecuteAsync(Result, PostSecurityResult.Subscriber, Expiry))
                .ReturnsAsync(QueryResult)
                .Verifiable();

            var result = await this.target.HandleAsync(new GetPostQuery(Requester, PostId, false, Timestamp));

            Assert.AreEqual(QueryResult, result);
            this.getPostQueryAggregator.Verify();
        }

        [TestMethod]
        public async Task WhenReadNotAllowed_AndNotRequestingFreePost_ItShouldReturnPreviewPost()
        {
            this.requesterSecurity.Setup(v => v.TryAuthenticateAsync(Requester)).ReturnsAsync(UserId);
            this.getPostDbStatement.Setup(v => v.ExecuteAsync(UserId, PostId)).ReturnsAsync(Result);
            this.postSecurity.Setup(v => v.IsReadAllowedAsync(UserId, PostId, Timestamp)).ReturnsAsync(PostSecurityResult.Denied);

            this.getPostQueryAggregator.Setup(v => v.ExecuteAsync(Result, PostSecurityResult.Denied, Expiry))
                .ReturnsAsync(QueryResult)
                .Verifiable();

            var result = await this.target.HandleAsync(new GetPostQuery(Requester, PostId, false, Timestamp));

            Assert.AreEqual(QueryResult, result);
            this.getPostQueryAggregator.Verify();
        }

        [TestMethod]
        public async Task WhenReadNotAllowed_AndRequestingFreePost_ItShouldReturnFullPost()
        {
            this.requesterSecurity.Setup(v => v.TryAuthenticateAsync(Requester)).ReturnsAsync(UserId);
            this.getPostDbStatement.Setup(v => v.ExecuteAsync(UserId, PostId)).ReturnsAsync(Result);
            this.postSecurity.Setup(v => v.IsReadAllowedAsync(UserId, PostId, Timestamp)).ReturnsAsync(PostSecurityResult.Denied);
            this.requestFreePost.Setup(v => v.ExecuteAsync(UserId, PostId, Timestamp)).Returns(Task.FromResult(0));

            this.getPostQueryAggregator.Setup(v => v.ExecuteAsync(Result, PostSecurityResult.FreePost, Expiry))
                .ReturnsAsync(QueryResult)
                .Verifiable();

            var result = await this.target.HandleAsync(new GetPostQuery(Requester, PostId, true, Timestamp));

            Assert.AreEqual(QueryResult, result);
            this.getPostQueryAggregator.Verify();
        }

        [TestMethod]
        [ExpectedException(typeof(DivideByZeroException))]
        public async Task WhenReadNotAllowed_AndFreePostRejected_ItShouldPropagateException()
        {
            this.requesterSecurity.Setup(v => v.TryAuthenticateAsync(Requester)).ReturnsAsync(UserId);
            this.getPostDbStatement.Setup(v => v.ExecuteAsync(UserId, PostId)).ReturnsAsync(Result);
            this.postSecurity.Setup(v => v.IsReadAllowedAsync(UserId, PostId, Timestamp)).ReturnsAsync(PostSecurityResult.Denied);
            this.requestFreePost.Setup(v => v.ExecuteAsync(UserId, PostId, Timestamp)).Throws(new DivideByZeroException());

            await this.target.HandleAsync(new GetPostQuery(Requester, PostId, true, Timestamp));
        }
    }
}