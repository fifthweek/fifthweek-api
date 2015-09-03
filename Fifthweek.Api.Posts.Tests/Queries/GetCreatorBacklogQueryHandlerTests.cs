namespace Fifthweek.Api.Posts.Tests.Queries
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlTypes;
    using System.Linq;
    using System.Threading.Tasks;

    using Fifthweek.Api.AssemblyResolution;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Posts.Queries;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    using Comment = Fifthweek.Api.Posts.Shared.Comment;

    [TestClass]
    public class GetCreatorBacklogQueryHandlerTests
    {
        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly Requester Requester = Requester.Authenticated(UserId);
        private static readonly Comment Comment = new Comment("Hey guys!");
        private static readonly DateTime Now = DateTime.UtcNow;
        private static readonly string FileName = "FileName";
        private static readonly string FileExtension = "FileExtension";
        private static readonly long FileSize = 1024;
        private static readonly int FileWidth = 800;
        private static readonly int FileHeight = 600;
        private static readonly string ContentType = "ContentType";
        private static readonly IReadOnlyList<BacklogPost> BacklogPosts = GetBacklogPosts().ToList();

        private Mock<IRequesterSecurity> requesterSecurity;
        private Mock<IGetCreatorBacklogDbStatement> getCreatorBacklogDbStatement;
        private Mock<IFileInformationAggregator> fileInformationAggregator;
        private Mock<IMimeTypeMap> mimeTypeMap;

        private GetCreatorBacklogQueryHandler target;

        [TestInitialize]
        public void Initialize()
        {
            DapperTypeHandlerRegistration.Register(FifthweekAssembliesResolver.Assemblies);

            this.requesterSecurity = new Mock<IRequesterSecurity>();
            this.getCreatorBacklogDbStatement = new Mock<IGetCreatorBacklogDbStatement>(MockBehavior.Strict);
            this.fileInformationAggregator = new Mock<IFileInformationAggregator>();
            this.mimeTypeMap = new Mock<IMimeTypeMap>();

            this.mimeTypeMap.Setup(v => v.GetMimeType(FileExtension)).Returns(ContentType);

            this.target = new GetCreatorBacklogQueryHandler(
                this.requesterSecurity.Object, 
                this.getCreatorBacklogDbStatement.Object,
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
            this.requesterSecurity.Setup(_ => _.AuthenticateAsAsync(Requester.Unauthenticated, UserId)).Throws<UnauthorizedException>();

            await this.target.HandleAsync(new GetCreatorBacklogQuery(Requester.Unauthenticated, UserId));
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedException))]
        public async Task ItShouldRequireAuthenticatedUserToMatchRequestedUser()
        {
            var otherUserId = new UserId(Guid.NewGuid());

            this.requesterSecurity.Setup(_ => _.AuthenticateAsAsync(Requester, otherUserId)).Throws<UnauthorizedException>();

            await this.target.HandleAsync(new GetCreatorBacklogQuery(Requester, otherUserId));
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedException))]
        public async Task ItShouldRequireUserToBeCreator()
        {
            this.requesterSecurity.Setup(_ => _.AssertInRoleAsync(Requester, FifthweekRole.Creator)).Throws<UnauthorizedException>();

            await this.target.HandleAsync(new GetCreatorBacklogQuery(Requester, UserId));
        }

        [TestMethod]
        [ExpectedException(typeof(DivideByZeroException))]
        public async Task ItShouldPropogateExceptionsFromGetCreatorBacklogDbStatement()
        {
            this.getCreatorBacklogDbStatement.Setup(v => v.ExecuteAsync(UserId, It.IsAny<DateTime>())).ThrowsAsync(new DivideByZeroException());
            await this.target.HandleAsync(new GetCreatorBacklogQuery(Requester, UserId));
        }

        [TestMethod]
        public async Task ItShouldReturnPosts()
        {
            this.getCreatorBacklogDbStatement.Setup(v => v.ExecuteAsync(UserId, It.IsAny<DateTime>()))
                .ReturnsAsync(BacklogPosts);

            this.fileInformationAggregator.Setup(v => v.GetFileInformationAsync(It.IsAny<ChannelId>(), It.IsAny<FileId>(), It.IsAny<string>()))
                .Returns<ChannelId, FileId, string>((c, f, p) => Task.FromResult(new FileInformation(f, c.ToString())));

            var result = await this.target.HandleAsync(new GetCreatorBacklogQuery(Requester, UserId));

            Assert.AreEqual(BacklogPosts.Count, result.Count);
            foreach (var item in result.Zip(BacklogPosts, (a, b) => new { Output = a, Input = b } ))
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
                Assert.AreEqual(item.Input.CollectionId, item.Output.CollectionId);
                Assert.AreEqual(item.Input.Comment, item.Output.Comment);
                Assert.AreEqual(item.Input.ScheduledByQueue, item.Output.ScheduledByQueue);
                Assert.AreEqual(item.Input.LiveDate, item.Output.LiveDate);
            }
        }

        private static IEnumerable<BacklogPost> GetBacklogPosts()
        {
            // 1 in 3 chance of coincidental ordering being correct, yielding a false positive when implementation fails to order explicitly.
            const int Days = 3;
            const int ChannelCount = 3;
            const int CollectionsPerChannel = 3;
            const int Posts = 6;

            var result = new List<BacklogPost>();
            for (var day = 0; day < Days; day++)
            {
                var liveDate = new SqlDateTime(Now.AddDays(1 + day)).Value;
                for (var channelIndex = 0; channelIndex < ChannelCount; channelIndex++)
                {
                    var channelId = new ChannelId(Guid.NewGuid());
                    for (var collectionIndex = 0; collectionIndex < CollectionsPerChannel; collectionIndex++)
                    {
                        var collectionId = collectionIndex == 0 ? null : new CollectionId(Guid.NewGuid());
                        for (var i = 0; i < Posts; i++)
                        {
                            result.Add(
                            new BacklogPost(
                                new PostId(Guid.NewGuid()),
                                channelId,
                                collectionId,
                                i % 2 == 0 ? Comment : null,
                                i % 3 == 1 ? new FileId(Guid.NewGuid()) : null,
                                i % 3 == 2 ? new FileId(Guid.NewGuid()) : null,
                                i % 2 == 0,
                                liveDate,
                                i % 3 == 1 ? FileName : null,
                                i % 3 == 1 ? FileExtension : null,
                                i % 3 == 1 ? FileSize : (long?)null,
                                i % 3 == 2 ? FileName : null,
                                i % 3 == 2 ? FileExtension : null,
                                i % 3 == 2 ? FileSize : (long?)null,
                                i % 3 == 2 ? FileWidth : (int?)null,
                                i % 3 == 2 ? FileHeight : (int?)null,
                                liveDate));
                        }
                    }
                }
            }

            return result;
        }
    }
}