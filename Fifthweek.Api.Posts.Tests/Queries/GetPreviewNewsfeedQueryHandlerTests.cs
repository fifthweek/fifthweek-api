namespace Fifthweek.Api.Posts.Tests.Queries
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlTypes;
    using System.Linq;
    using System.Threading.Tasks;

    using Fifthweek.Api.AssemblyResolution;
    using Fifthweek.Api.Azure;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Persistence.Tests.Shared;
    using Fifthweek.Api.Posts.Queries;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    using Comment = Fifthweek.Api.Posts.Shared.Comment;

    [TestClass]
    public class GetPreviewNewsfeedQueryHandlerTests : PersistenceTestsBase
    {
        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly UserId CreatorId = new UserId(Guid.NewGuid());
        private static readonly BlogId BlogId = new BlogId(Guid.NewGuid());
        private static readonly Requester Requester = Requester.Authenticated(UserId);
        private static readonly List<ChannelId> ChannelIds = new List<ChannelId> { new ChannelId(Guid.NewGuid()) };
        private static readonly List<QueueId> CollectionIds = new List<QueueId> { new QueueId(Guid.NewGuid()) };
        private static readonly DateTime? Origin = DateTime.UtcNow;
        private static readonly bool SearchForwards = true;
        private static readonly NonNegativeInt StartIndex = NonNegativeInt.Parse(10);
        private static readonly PositiveInt Count = PositiveInt.Parse(5);
        private static readonly PreviewText PreviewText = new PreviewText("Hey guys!");
        private static readonly DateTime Now = new SqlDateTime(DateTime.UtcNow).Value;
        private static readonly AccessSignatureExpiryInformation Expiry = new AccessSignatureExpiryInformation(Now.AddDays(1), Now.AddDays(2));
        private static readonly string FileName = "FileName";
        private static readonly string FileExtension = "FileExtension";
        private static readonly long FileSize = 1024;
        private static readonly int FileWidth = 800;
        private static readonly int FileHeight = 600;
        private static readonly string ContentType = "ContentType";
        private static readonly string Signature = "signature";
        private static readonly IReadOnlyList<PreviewNewsfeedPost> NewsfeedPosts = GetNewsfeedPosts().ToList();

        private Mock<IRequesterSecurity> requesterSecurity;
        private Mock<IGetPreviewNewsfeedDbStatement> getPreviewNewsfeedDbStatement;
        private Mock<IFileInformationAggregator> fileInformationAggregator;
        private Mock<IMimeTypeMap> mimeTypeMap;
        private Mock<IBlobService> blobService;
        private Mock<ITimestampCreator> timestampCreator;
        private Mock<IGetAccessSignatureExpiryInformation> getAccessSignatureExpiryInformation;

        private GetPreviewNewsfeedQueryHandler target;

        [TestInitialize]
        public void Initialize()
        {
            DapperTypeHandlerRegistration.Register(FifthweekAssembliesResolver.Assemblies);

            this.requesterSecurity = new Mock<IRequesterSecurity>();
            this.getPreviewNewsfeedDbStatement = new Mock<IGetPreviewNewsfeedDbStatement>(MockBehavior.Strict);
            this.fileInformationAggregator = new Mock<IFileInformationAggregator>();
            this.mimeTypeMap = new Mock<IMimeTypeMap>();
            this.blobService = new Mock<IBlobService>(MockBehavior.Strict);
            this.timestampCreator = new Mock<ITimestampCreator>();
            this.getAccessSignatureExpiryInformation = new Mock<IGetAccessSignatureExpiryInformation>();

            this.timestampCreator.Setup(v => v.Now()).Returns(Now);
            this.getAccessSignatureExpiryInformation.Setup(v => v.Execute(Now)).Returns(Expiry);
            this.mimeTypeMap.Setup(v => v.GetMimeType(FileExtension)).Returns(ContentType);

            this.target = new GetPreviewNewsfeedQueryHandler(
                this.requesterSecurity.Object,
                this.getPreviewNewsfeedDbStatement.Object,
                this.fileInformationAggregator.Object,
                this.mimeTypeMap.Object,
                this.blobService.Object,
                this.timestampCreator.Object,
                this.getAccessSignatureExpiryInformation.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task ItShouldRequireQuery()
        {
            await this.target.HandleAsync(null);
        }

        [TestMethod]
        [ExpectedException(typeof(DivideByZeroException))]
        public async Task ItShouldPropogateExceptionsFromGetCreatorBacklogDbStatement()
        {
            this.requesterSecurity.Setup(_ => _.TryAuthenticateAsync(Requester)).ReturnsAsync(UserId);
            this.getPreviewNewsfeedDbStatement.Setup(v => v.ExecuteAsync(UserId, CreatorId, ChannelIds, Now, Origin.Value, SearchForwards, StartIndex, Count)).ThrowsAsync(new DivideByZeroException());
            await this.target.HandleAsync(new GetNewsfeedQuery(Requester, CreatorId, ChannelIds, Origin, SearchForwards, StartIndex, Count));
        }

        [TestMethod]
        public async Task ItShouldReturnPosts()
        {
            this.requesterSecurity.Setup(_ => _.TryAuthenticateAsync(Requester)).ReturnsAsync(UserId);

            this.getPreviewNewsfeedDbStatement.Setup(v => v.ExecuteAsync(UserId, CreatorId, ChannelIds, Now, Origin.Value, SearchForwards, StartIndex, Count))
                .ReturnsAsync(new GetPreviewNewsfeedDbResult(NewsfeedPosts));

            this.fileInformationAggregator.Setup(v => v.GetFileInformationAsync(It.IsAny<ChannelId>(), It.IsAny<FileId>(), It.IsAny<string>()))
                .Returns<ChannelId, FileId, string>((c, f, p) => Task.FromResult(new FileInformation(f, c == null ? "public" : c.ToString())));

            this.blobService.Setup(v => v.GetBlobSharedAccessInformationForReadingAsync(It.IsAny<string>(), It.IsAny<string>(), Expiry.Public))
                .Returns<string, string, DateTime>((c, b, e) => Task.FromResult(new BlobSharedAccessInformation(c, b, c + "/" + b, Signature, e)));

            var result = await this.target.HandleAsync(new GetNewsfeedQuery(Requester, CreatorId, ChannelIds, Origin, SearchForwards, StartIndex, Count));

            Assert.AreEqual(NewsfeedPosts.Count, result.Posts.Count);
            foreach (var item in result.Posts.Zip(NewsfeedPosts, (a, b) => new { Output = a, Input = b }))
            {
                if (item.Input.ProfileImageFileId != null)
                {
                    Assert.AreEqual(item.Input.ProfileImageFileId, item.Output.Creator.ProfileImage.FileId);
                    Assert.AreEqual("public", item.Output.Creator.ProfileImage.ContainerName);
                }
                else
                {
                    Assert.IsNull(item.Output.Creator.ProfileImage);
                }

                if (item.Input.ImageId != null)
                {
                    Assert.AreEqual(item.Input.ImageId, item.Output.Image.FileId);
                    Assert.AreEqual(item.Input.ChannelId.ToString(), item.Output.Image.ContainerName);
                    Assert.AreEqual(item.Input.ImageName, item.Output.ImageSource.FileName);
                    Assert.AreEqual(item.Input.ImageExtension, item.Output.ImageSource.FileExtension);
                    Assert.AreEqual(item.Input.ImageSize, item.Output.ImageSource.Size);
                    Assert.AreEqual(ContentType, item.Output.ImageSource.ContentType);
                    Assert.AreEqual(item.Input.ChannelId.ToString(), item.Output.ImageAccessInformation.ContainerName);
                    Assert.AreEqual(
                        item.Input.ImageId.Value.EncodeGuid() + "/" + FileManagement.Shared.Constants.PostPreviewImageThumbnailName, 
                        item.Output.ImageAccessInformation.BlobName);
                    Assert.AreEqual(Signature, item.Output.ImageAccessInformation.Signature);
                    Assert.AreEqual(Expiry.Public, item.Output.ImageAccessInformation.Expiry);
                }
                else
                {
                    Assert.IsNull(item.Output.ImageSource);
                    Assert.IsNull(item.Output.Image);
                    Assert.IsNull(item.Output.ImageAccessInformation);
                }

                Assert.AreEqual(item.Input.PostId, item.Output.PostId);
                Assert.AreEqual(item.Input.ChannelId, item.Output.ChannelId);
                Assert.AreEqual(item.Input.PreviewText, item.Output.PreviewText);
                Assert.AreEqual(item.Input.PreviewWordCount, item.Output.PreviewWordCount);
                Assert.AreEqual(item.Input.WordCount, item.Output.WordCount);
                Assert.AreEqual(item.Input.ImageCount, item.Output.ImageCount);
                Assert.AreEqual(item.Input.FileCount, item.Output.FileCount);
                Assert.AreEqual(item.Input.LiveDate, item.Output.LiveDate);

                Assert.AreEqual(item.Input.Username, item.Output.Creator.Username.Value);
                Assert.AreEqual(item.Input.BlogName, item.Output.Blog.Name.Value);
                Assert.AreEqual(item.Input.ChannelName, item.Output.Channel.Name.Value);
            }
        }

        [TestMethod]
        public async Task WhenOriginIsNullItShouldPassInTheCurrentDate()
        {
            this.requesterSecurity.Setup(_ => _.TryAuthenticateAsync(Requester)).ReturnsAsync(UserId);

            this.getPreviewNewsfeedDbStatement.Setup(v => v.ExecuteAsync(UserId, CreatorId, ChannelIds, Now, Now, SearchForwards, StartIndex, Count))
                .ReturnsAsync(new GetPreviewNewsfeedDbResult(NewsfeedPosts));

            this.fileInformationAggregator.Setup(v => v.GetFileInformationAsync(It.IsAny<ChannelId>(), It.IsAny<FileId>(), It.IsAny<string>()))
                .Returns<ChannelId, FileId, string>((c, f, p) => Task.FromResult(new FileInformation(f, c == null ? "public" : c.ToString())));

            this.blobService.Setup(v => v.GetBlobSharedAccessInformationForReadingAsync(It.IsAny<string>(), It.IsAny<string>(), Expiry.Public))
                .Returns<string, string, DateTime>((c, b, e) => Task.FromResult(new BlobSharedAccessInformation(c, b, c + "/" + b, Signature, e)));

            await this.target.HandleAsync(new GetNewsfeedQuery(Requester, CreatorId, ChannelIds, null, SearchForwards, StartIndex, Count));
        }

        private static IEnumerable<PreviewNewsfeedPost> GetNewsfeedPosts()
        {
            // 1 in 3 chance of coincidental ordering being correct, yielding a false positive when implementation fails to order explicitly.
            const int ChannelCount = 3;
            const int CollectionsPerChannel = 3;
            const int Posts = 6;

            var day = 0;
            var result = new List<PreviewNewsfeedPost>();
            for (var channelIndex = 0; channelIndex < ChannelCount; channelIndex++)
            {
                var channelId = new ChannelId(Guid.NewGuid());
                for (var collectionIndex = 0; collectionIndex < CollectionsPerChannel; collectionIndex++)
                {
                    var collectionId = collectionIndex == 0 ? null : new QueueId(Guid.NewGuid());
                    for (var i = 0; i < Posts; i++)
                    {
                        // Ensure we have one post that is `now` (i.e. AddDays(0)).
                        var liveDate = new SqlDateTime(Now.AddDays(day--)).Value;

                        result.Add(
                        new PreviewNewsfeedPost(
                            CreatorId,
                            Guid.NewGuid().ToString(),
                            i % 3 == 1 ? new FileId(Guid.NewGuid()) : null,
                            null,
                            null,
                            new PostId(Guid.NewGuid()),
                            BlogId,
                            Guid.NewGuid().ToString(),
                            channelId,
                            Guid.NewGuid().ToString(),
                            i % 2 == 0 ? PreviewText : null,
                            null,
                            i % 3 == 2 ? new FileId(Guid.NewGuid()) : null,
                            i % 2 == 0 ? PreviewText.Value.Length : 0,
                            i % 2 == 0 ? PreviewText.Value.Length : 0,
                            i % 3 == 2 ? 1 : 0,
                            i % 3 == 1 ? 1 : 0,
                            liveDate,
                            i % 3 == 2 ? FileName : null,
                            i % 3 == 2 ? FileExtension : null,
                            i % 3 == 2 ? FileSize : (long?)null,
                            i % 3 == 2 ? FileWidth : (int?)null,
                            i % 3 == 2 ? FileHeight : (int?)null, 
                            0,
                            0, 
                            false,
                            liveDate));
                    }
                }
            }

            return result;
        }
    }
}