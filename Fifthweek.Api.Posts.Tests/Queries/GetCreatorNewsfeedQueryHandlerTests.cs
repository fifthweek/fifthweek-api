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
    using Fifthweek.Api.Persistence.Tests.Shared;
    using Fifthweek.Api.Posts.Queries;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class GetCreatorNewsfeedQueryHandlerTests : PersistenceTestsBase
    {
        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly Requester Requester = Requester.Authenticated(UserId);
        private static readonly NonNegativeInt StartIndex = NonNegativeInt.Parse(10);
        private static readonly PositiveInt Count = PositiveInt.Parse(5);
        private static readonly Comment Comment = new Comment("Hey guys!");
        private static readonly DateTime Now = new SqlDateTime(DateTime.UtcNow).Value;
        private static readonly IReadOnlyList<NewsfeedPost> SortedNewsfeedPosts = GetSortedNewsfeedPosts().ToList();

        private Mock<IRequesterSecurity> requesterSecurity;
        private Mock<IGetCreatorNewsfeedDbStatement> getCreatorNewsfeedDbStatement;
        private Mock<IFileInformationAggregator> fileInformationAggregator;

        private GetCreatorNewsfeedQueryHandler target;

        [TestInitialize]
        public void Initialize()
        {
            DapperTypeHandlerRegistration.Register(FifthweekAssembliesResolver.Assemblies);

            this.requesterSecurity = new Mock<IRequesterSecurity>();
            this.getCreatorNewsfeedDbStatement = new Mock<IGetCreatorNewsfeedDbStatement>(MockBehavior.Strict);
            this.fileInformationAggregator = new Mock<IFileInformationAggregator>();

            this.target = new GetCreatorNewsfeedQueryHandler(
                this.requesterSecurity.Object, 
                this.getCreatorNewsfeedDbStatement.Object,
                this.fileInformationAggregator.Object);
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

            await this.target.HandleAsync(new GetCreatorNewsfeedQuery(Requester.Unauthenticated, UserId, StartIndex, Count));
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedException))]
        public async Task ItShouldRequireAuthenticatedUserToMatchRequestedUser()
        {
            var otherUserId = new UserId(Guid.NewGuid());

            this.requesterSecurity.Setup(_ => _.AuthenticateAsAsync(Requester, otherUserId)).Throws<UnauthorizedException>();

            await this.target.HandleAsync(new GetCreatorNewsfeedQuery(Requester, otherUserId, StartIndex, Count));
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedException))]
        public async Task ItShouldRequireUserToBeCreator()
        {
            this.requesterSecurity.Setup(_ => _.AssertInRoleAsync(Requester, FifthweekRole.Creator)).Throws<UnauthorizedException>();

            await this.target.HandleAsync(new GetCreatorNewsfeedQuery(Requester, UserId, StartIndex, Count));
        }

        [TestMethod]
        [ExpectedException(typeof(DivideByZeroException))]
        public async Task ItShouldPropogateExceptionsFromGetCreatorBacklogDbStatement()
        {
            this.getCreatorNewsfeedDbStatement.Setup(v => v.ExecuteAsync(UserId, It.IsAny<DateTime>(), StartIndex, Count)).ThrowsAsync(new DivideByZeroException());
            await this.target.HandleAsync(new GetCreatorNewsfeedQuery(Requester, UserId, StartIndex, Count));
        }

        [TestMethod]
        public async Task ItShouldReturnPosts()
        {
            this.getCreatorNewsfeedDbStatement.Setup(v => v.ExecuteAsync(UserId, It.IsAny<DateTime>(), StartIndex, Count))
                .ReturnsAsync(SortedNewsfeedPosts);

            this.fileInformationAggregator.Setup(v => v.GetFileInformationAsync(UserId, It.IsAny<FileId>(), It.IsAny<string>()))
                .Returns<UserId, FileId, string>((u, f, p) => Task.FromResult(new FileInformation(f, string.Empty, string.Empty, string.Empty)));
            
            var result = await this.target.HandleAsync(new GetCreatorNewsfeedQuery(Requester, UserId, StartIndex, Count));

            Assert.AreEqual(SortedNewsfeedPosts.Count, result.Count);
            foreach (var item in result.Zip(SortedNewsfeedPosts, (a, b) => new { Output = a, Input = b }))
            {
                if (item.Input.FileId != null)
                {
                    Assert.AreEqual(item.Input.FileId, item.Output.File.FileId);
                }

                if (item.Input.ImageId != null)
                {
                    Assert.AreEqual(item.Input.ImageId, item.Output.Image.FileId);
                }

                Assert.AreEqual(item.Input.PostId, item.Output.PostId);
                Assert.AreEqual(item.Input.ChannelId, item.Output.ChannelId);
                Assert.AreEqual(item.Input.CollectionId, item.Output.CollectionId);
                Assert.AreEqual(item.Input.Comment, item.Output.Comment);
                Assert.AreEqual(item.Input.LiveDate, item.Output.LiveDate);
            }
        }

        private static IEnumerable<NewsfeedPost> GetSortedNewsfeedPosts()
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
                    var collectionId = collectionIndex == 0 ? null : new CollectionId(Guid.NewGuid());
                    for (var i = 0; i < Posts; i++)
                    {
                        // Ensure we have one post that is `now` (i.e. AddDays(0)).
                        var liveDate = new SqlDateTime(Now.AddDays(day--)).Value;

                        result.Add(
                        new NewsfeedPost(
                            new PostId(Guid.NewGuid()),
                            channelId,
                            collectionId,
                            i % 2 == 0 ? Comment : null,
                            i % 3 == 1 ? new FileId(Guid.NewGuid()) : null,
                            i % 3 == 2 ? new FileId(Guid.NewGuid()) : null,
                            liveDate));
                    }
                }
            }

            return result.OrderByDescending(_ => _.LiveDate);
        }
    }
}