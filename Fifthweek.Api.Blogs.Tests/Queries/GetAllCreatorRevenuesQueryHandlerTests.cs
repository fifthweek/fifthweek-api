namespace Fifthweek.Api.Blogs.Tests.Queries
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Fifthweek.Api.Blogs.Queries;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Identity.Tests.Shared.Membership;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class GetAllCreatorRevenuesQueryHandlerTests
    {
        private static readonly UserId UserId = UserId.Random();
        private static readonly Requester Requester = Requester.Authenticated(UserId);
        private static readonly DateTime Now = DateTime.UtcNow;
        private static readonly DateTime ReleasableRevenueDate = Now.AddDays(-BlogsConstants.ReleaseableRevenueDays);

        private static readonly GetAllCreatorRevenuesQuery Query = new GetAllCreatorRevenuesQuery(Requester);
        
        private Mock<IRequesterSecurity> requesterSecurity;
        private Mock<IGetAllCreatorRevenuesDbStatement> getAllCreatorRevenuesDbStatement;
        private Mock<ITimestampCreator> timestampCreator;

        private GetAllCreatorRevenuesQueryHandler target;

        [TestInitialize]
        public void Initialize()
        {
            this.requesterSecurity = new Mock<IRequesterSecurity>();
            this.getAllCreatorRevenuesDbStatement = new Mock<IGetAllCreatorRevenuesDbStatement>(MockBehavior.Strict);
            this.timestampCreator = new Mock<ITimestampCreator>(MockBehavior.Strict);

            this.requesterSecurity.SetupFor(Requester);

            this.target = new GetAllCreatorRevenuesQueryHandler(
                this.requesterSecurity.Object,
                this.getAllCreatorRevenuesDbStatement.Object,
                this.timestampCreator.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenQueryIsNull_ItShouldThrowAnException()
        {
            await this.target.HandleAsync(null);
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedException))]
        public async Task WhenUserIsNotAdministrator_ItShouldThrowAnException()
        {
            this.requesterSecurity.Setup(v => v.AssertInRoleAsync(Requester, FifthweekRole.Administrator))
                .Throws(new UnauthorizedException());

            await this.target.HandleAsync(Query);
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedException))]
        public async Task WhenUserIsNotAuthenticated_ItShouldThrowAnException()
        {
            await this.target.HandleAsync(new GetAllCreatorRevenuesQuery(Requester.Unauthenticated));
        }

        [TestMethod]
        public async Task ItShouldReturnCreatorRevenues()
        {
            this.timestampCreator.Setup(v => v.Now()).Returns(Now);

            var expectedResult = new GetAllCreatorRevenuesResult(new List<GetAllCreatorRevenuesResult.Creator> 
            {
                new GetAllCreatorRevenuesResult.Creator(UserId.Random(), 1, 2, 3, new Username("username"), new Email("email@mail.com"), true),
                new GetAllCreatorRevenuesResult.Creator(UserId.Random(), 4, 5, 6, new Username("username2"), new Email("email2@mail.com"), false),
            });

            this.getAllCreatorRevenuesDbStatement.Setup(v => v.ExecuteAsync(ReleasableRevenueDate))
                .ReturnsAsync(expectedResult);

            var result = await this.target.HandleAsync(Query);

            Assert.AreEqual(expectedResult, result);
        }
    }
}