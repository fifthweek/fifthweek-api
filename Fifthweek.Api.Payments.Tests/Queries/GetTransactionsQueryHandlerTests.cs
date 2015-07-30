namespace Fifthweek.Api.Payments.Tests.Queries
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Identity.Tests.Shared.Membership;
    using Fifthweek.Api.Payments.Commands;
    using Fifthweek.Api.Payments.Queries;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Payments.Services.Administration;
    using Fifthweek.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class GetTransactionsQueryHandlerTests
    {
        private static readonly UserId UserId = UserId.Random();
        private static readonly Requester Requester = Requester.Authenticated(UserId);
        private static readonly DateTime Now = DateTime.UtcNow;

        private static readonly GetTransactionsQuery Query = new GetTransactionsQuery(
            Requester,
            UserId.Random(),
            Now.AddDays(-5),
            Now.AddDays(-2));

        private static readonly GetTransactionsResult Result =
            new GetTransactionsResult(new List<GetTransactionsResult.Item>());

        private Mock<IRequesterSecurity> requesterSecurity;
        private Mock<ITimestampCreator> timestampCreator;
        private Mock<IGetTransactionsDbStatement> getTransactions;

        private GetTransactionsQueryHandler target;

        [TestInitialize]
        public void Initialize()
        {
            this.requesterSecurity = new Mock<IRequesterSecurity>();
            this.timestampCreator = new Mock<ITimestampCreator>(MockBehavior.Strict);
            this.getTransactions = new Mock<IGetTransactionsDbStatement>(MockBehavior.Strict);

            this.requesterSecurity.SetupFor(Requester);

            this.target = new GetTransactionsQueryHandler(
                this.requesterSecurity.Object,
                this.timestampCreator.Object,
                this.getTransactions.Object);
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
            await this.target.HandleAsync(new GetTransactionsQuery(
                Requester.Unauthenticated,
                Query.UserId,
                Query.StartTimeInclusive,
                Query.EndTimeExclusive));
        }

        [TestMethod]
        public async Task ItShouldGetTransactions()
        {
            this.getTransactions.Setup(v => v.ExecuteAsync(Query.UserId, Query.StartTimeInclusive.Value, Query.EndTimeExclusive.Value))
                .ReturnsAsync(Result);

            var result = await this.target.HandleAsync(Query);

            Assert.AreEqual(Result, result);
        }

        [TestMethod]
        public async Task WhenUserIdIsNull_ItShouldGetTransactions()
        {
            this.getTransactions.Setup(v => v.ExecuteAsync(null, Query.StartTimeInclusive.Value, Query.EndTimeExclusive.Value))
                .ReturnsAsync(Result);

            var result = await this.target.HandleAsync(new GetTransactionsQuery(
                Query.Requester,
                null,
                Query.StartTimeInclusive,
                Query.EndTimeExclusive));

            Assert.AreEqual(Result, result);
        }

        [TestMethod]
        public async Task WhenStartTimeIsNull_ItShouldGetTransactions()
        {
            this.getTransactions.Setup(v => v.ExecuteAsync(Query.UserId, It.Is<DateTime>(dt => dt < Query.EndTimeExclusive), Query.EndTimeExclusive.Value))
                .ReturnsAsync(Result);

            var result = await this.target.HandleAsync(new GetTransactionsQuery(
                Query.Requester,
                Query.UserId,
                null,
                Query.EndTimeExclusive));

            Assert.AreEqual(Result, result);
        }

        [TestMethod]
        public async Task WhenEndTimeIsNull_ItShouldGetTransactions()
        {
            this.getTransactions.Setup(v => v.ExecuteAsync(Query.UserId, Query.StartTimeInclusive.Value, It.Is<DateTime>(dt => dt > Query.StartTimeInclusive)))
                .ReturnsAsync(Result);

            var result = await this.target.HandleAsync(new GetTransactionsQuery(
                Query.Requester,
                Query.UserId,
                Query.StartTimeInclusive,
                null));

            Assert.AreEqual(Result, result);
        }

        [TestMethod]
        public async Task WhenStartAndEndTimeIsNull_ItShouldGetTransactions()
        {
            this.timestampCreator.Setup(v => v.Now()).Returns(Now);

            this.getTransactions.Setup(v => v.ExecuteAsync(Query.UserId, It.Is<DateTime>(dt => dt < Now), It.Is<DateTime>(dt => dt > Now)))
                .ReturnsAsync(Result);

            var result = await this.target.HandleAsync(new GetTransactionsQuery(
                Query.Requester,
                Query.UserId,
                null,
                null));

            Assert.AreEqual(Result, result);
        }
    }
}