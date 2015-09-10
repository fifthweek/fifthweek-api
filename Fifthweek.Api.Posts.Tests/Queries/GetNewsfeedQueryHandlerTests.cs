namespace Fifthweek.Api.Posts.Tests.Queries
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlTypes;
    using System.Linq;
    using System.Threading.Tasks;

    using Fifthweek.Api.AssemblyResolution;
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
    public class GetNewsfeedQueryHandlerTests : PersistenceTestsBase
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
        private static readonly Comment Comment = new Comment("Hey guys!");
        private static readonly DateTime Now = new SqlDateTime(DateTime.UtcNow).Value;
        private static readonly string FileName = "FileName";
        private static readonly string FileExtension = "FileExtension";
        private static readonly long FileSize = 1024;
        private static readonly int FileWidth = 800;
        private static readonly int FileHeight = 600;
        private static readonly string ContentType = "ContentType";
        private static readonly IReadOnlyList<NewsfeedPost> NewsfeedPosts = GetNewsfeedPosts().ToList();

        private Mock<IRequesterSecurity> requesterSecurity;
        private Mock<IGetNewsfeedDbStatement> getNewsfeedDbStatement;
        private Mock<IFileInformationAggregator> fileInformationAggregator;
        private Mock<IMimeTypeMap> mimeTypeMap;

        private GetNewsfeedQueryHandler target;

        [TestInitialize]
        public void Initialize()
        {
            DapperTypeHandlerRegistration.Register(FifthweekAssembliesResolver.Assemblies);

            this.requesterSecurity = new Mock<IRequesterSecurity>();
            this.getNewsfeedDbStatement = new Mock<IGetNewsfeedDbStatement>(MockBehavior.Strict);
            this.fileInformationAggregator = new Mock<IFileInformationAggregator>();
            this.mimeTypeMap = new Mock<IMimeTypeMap>();

            this.mimeTypeMap.Setup(v => v.GetMimeType(FileExtension)).Returns(ContentType);

            this.target = new GetNewsfeedQueryHandler(
                this.requesterSecurity.Object, 
                this.getNewsfeedDbStatement.Object,
                this.fileInformationAggregator.Object,
                this.mimeTypeMap.Object);
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

            await this.target.HandleAsync(new GetNewsfeedQuery(Requester.Unauthenticated, CreatorId, ChannelIds, CollectionIds, Origin, SearchForwards, StartIndex, Count));
        }

        [TestMethod]
        [ExpectedException(typeof(DivideByZeroException))]
        public async Task ItShouldPropogateExceptionsFromGetCreatorBacklogDbStatement()
        {
            this.requesterSecurity.Setup(_ => _.AuthenticateAsync(Requester)).ReturnsAsync(UserId);
            this.getNewsfeedDbStatement.Setup(v => v.ExecuteAsync(UserId, CreatorId, ChannelIds, CollectionIds, It.IsAny<DateTime>(), Origin.Value, SearchForwards, StartIndex, Count)).ThrowsAsync(new DivideByZeroException());
            await this.target.HandleAsync(new GetNewsfeedQuery(Requester, CreatorId, ChannelIds, CollectionIds, Origin, SearchForwards, StartIndex, Count));
        }

        [TestMethod]
        public async Task ItShouldReturnPosts()
        {
            this.requesterSecurity.Setup(_ => _.AuthenticateAsync(Requester)).ReturnsAsync(UserId);

            this.getNewsfeedDbStatement.Setup(v => v.ExecuteAsync(UserId, CreatorId, ChannelIds, CollectionIds, It.IsAny<DateTime>(), Origin.Value, SearchForwards, StartIndex, Count))
                .ReturnsAsync(new GetNewsfeedDbResult(NewsfeedPosts, 10));

            this.fileInformationAggregator.Setup(v => v.GetFileInformationAsync(It.IsAny<ChannelId>(), It.IsAny<FileId>(), It.IsAny<string>()))
                .Returns<ChannelId, FileId, string>((c, f, p) => Task.FromResult(new FileInformation(f, c.ToString())));

            var result = await this.target.HandleAsync(new GetNewsfeedQuery(Requester, CreatorId, ChannelIds, CollectionIds, Origin, SearchForwards, StartIndex, Count));

            Assert.AreEqual(10, result.AccountBalance);
            Assert.AreEqual(NewsfeedPosts.Count, result.Posts.Count);
            foreach (var item in result.Posts.Zip(NewsfeedPosts, (a, b) => new { Output = a, Input = b }))
            {
                if (item.Input.FileId != null)
                {
                    Assert.AreEqual(item.Input.FileId, item.Output.File.FileId);
                    Assert.AreEqual(item.Input.ChannelId.ToString(), item.Output.File.ContainerName);
                    Assert.AreEqual(item.Input.FileName, item.Output.FileSource.FileName);
                    Assert.AreEqual(item.Input.FileExtension, item.Output.FileSource.FileExtension);
                    Assert.AreEqual(item.Input.FileSize, item.Output.FileSource.Size);
                    Assert.AreEqual(ContentType, item.Output.FileSource.ContentType);
                }

                if (item.Input.ImageId != null)
                {
                    Assert.AreEqual(item.Input.ImageId, item.Output.Image.FileId);
                    Assert.AreEqual(item.Input.ChannelId.ToString(), item.Output.Image.ContainerName);
                    Assert.AreEqual(item.Input.ImageName, item.Output.ImageSource.FileName);
                    Assert.AreEqual(item.Input.ImageExtension, item.Output.ImageSource.FileExtension);
                    Assert.AreEqual(item.Input.ImageSize, item.Output.ImageSource.Size);
                    Assert.AreEqual(ContentType, item.Output.ImageSource.ContentType);
                }

                Assert.AreEqual(item.Input.PostId, item.Output.PostId);
                Assert.AreEqual(item.Input.ChannelId, item.Output.ChannelId);
                Assert.AreEqual(item.Input.QueueId, item.Output.QueueId);
                Assert.AreEqual(item.Input.Comment, item.Output.Comment);
                Assert.AreEqual(item.Input.LiveDate, item.Output.LiveDate);
            }
        }

        [TestMethod]
        public async Task WhenOriginIsNullItShouldPassInTheCurrentDate()
        {
            this.requesterSecurity.Setup(_ => _.AuthenticateAsync(Requester)).ReturnsAsync(UserId);

            DateTime nowDate = DateTime.MinValue;
            DateTime originDate = DateTime.MaxValue;
            this.getNewsfeedDbStatement.Setup(v => v.ExecuteAsync(UserId, CreatorId, ChannelIds, CollectionIds, It.IsAny<DateTime>(), It.IsAny<DateTime>(), SearchForwards, StartIndex, Count))
                .Callback<UserId, UserId, IReadOnlyList<ChannelId>, IReadOnlyList<QueueId>, DateTime, DateTime, bool, NonNegativeInt, PositiveInt>(
                (a, b, c, d, now, origin, e, f, g) => 
                {
                    nowDate = now;
                    originDate = origin;
                })
                .ReturnsAsync(new GetNewsfeedDbResult(NewsfeedPosts, 10));

            this.fileInformationAggregator.Setup(v => v.GetFileInformationAsync(It.IsAny<ChannelId>(), It.IsAny<FileId>(), It.IsAny<string>()))
                .Returns<ChannelId, FileId, string>((c, f, p) => Task.FromResult(new FileInformation(f, c.ToString())));

            await this.target.HandleAsync(new GetNewsfeedQuery(Requester, CreatorId, ChannelIds, CollectionIds, null, SearchForwards, StartIndex, Count));

            Assert.AreEqual(nowDate, originDate);
        }

        private static IEnumerable<NewsfeedPost> GetNewsfeedPosts()
        {
            // 1 in 3 chance of coincidental ordering being correct, yielding a false positive when implementation fails to order explicitly.
            const int ChannelCount = 3;
            const int CollectionsPerChannel = 3;
            const int Posts = 6;

            var day = 0;
            var result = new List<NewsfeedPost>();
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
                        new NewsfeedPost(
                            CreatorId,
                            new PostId(Guid.NewGuid()),
                            BlogId,
                            channelId,
                            collectionId,
                            i % 2 == 0 ? Comment : null,
                            i % 3 == 1 ? new FileId(Guid.NewGuid()) : null,
                            i % 3 == 2 ? new FileId(Guid.NewGuid()) : null,
                            liveDate,
                            i % 3 == 1 ? FileName : null,
                            i % 3 == 1 ? FileExtension : null,
                            i % 3 == 1 ? FileSize : (long?)null,
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