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

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class GetCreatorBacklogQueryHandlerTests
    {
        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly Requester Requester = Requester.Authenticated(UserId);
        private static readonly Comment Comment = new Comment("Hey guys!");
        private static readonly DateTime Now = DateTime.UtcNow;
        private static readonly IReadOnlyList<BacklogPost> SortedBacklogPosts = GetSortedBacklogPosts().ToList();

        private Mock<IRequesterSecurity> requesterSecurity;
        private Mock<IGetCreatorBacklogDbStatement> getCreatorBacklogDbStatement;

        private GetCreatorBacklogQueryHandler target;

        [TestInitialize]
        public void Initialize()
        {
            DapperTypeHandlerRegistration.Register(FifthweekAssembliesResolver.Assemblies);

            this.requesterSecurity = new Mock<IRequesterSecurity>();
            this.getCreatorBacklogDbStatement = new Mock<IGetCreatorBacklogDbStatement>(MockBehavior.Strict);

            this.target = new GetCreatorBacklogQueryHandler(this.requesterSecurity.Object, this.getCreatorBacklogDbStatement.Object);
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
            this.getCreatorBacklogDbStatement.Setup(v => v.ExecuteAsync(UserId, It.IsAny<DateTime>())).ReturnsAsync(SortedBacklogPosts);
            var result = await this.target.HandleAsync(new GetCreatorBacklogQuery(Requester, UserId));

            Assert.AreEqual(SortedBacklogPosts, result);
        }

        private static IEnumerable<BacklogPost> GetSortedBacklogPosts()
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
                                liveDate));
                        }
                    }
                }
            }

            return result.OrderByDescending(_ => _.LiveDate).ThenByDescending(_ => _.ScheduledByQueue);
        }
    }
}