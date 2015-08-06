namespace Fifthweek.Payments.Tests.Services.Credit
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Azure;
    using Fifthweek.Payments.Services.Credit;
    using Fifthweek.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class CommitTestUserCreditToDatabaseTests
    {
        private static readonly DateTime Now = DateTime.UtcNow;
        private static readonly UserId UserId = UserId.Random();
        private static readonly PositiveInt Amount = PositiveInt.Parse(10);

        private Mock<ITimestampCreator> timestampCreator;
        private Mock<ISetTestUserAccountBalanceDbStatement> setTestUserAccountBalance;

        private CommitTestUserCreditToDatabase target;

        [TestInitialize]
        public void Initialize()
        {
            this.timestampCreator = new Mock<ITimestampCreator>(MockBehavior.Strict);
            this.setTestUserAccountBalance = new Mock<ISetTestUserAccountBalanceDbStatement>(MockBehavior.Strict);

            this.target = new CommitTestUserCreditToDatabase(
                this.timestampCreator.Object,
                this.setTestUserAccountBalance.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenUserIdIsNull_ItShouldThrowAnException()
        {
            await this.target.HandleAsync(
                null,
                Amount);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenAmountIsNull_ItShouldThrowAnException()
        {
            await this.target.HandleAsync(
                UserId,
                null);
        }

        [TestMethod]
        public async Task ItShouldSetUserAccountBalance()
        {
            this.timestampCreator.Setup(v => v.Now()).Returns(Now);

            this.setTestUserAccountBalance.Setup(v => v.ExecuteAsync(UserId, Now, Amount))
                .Returns(Task.FromResult(0))
                .Verifiable();

            await this.target.HandleAsync(UserId, Amount);

            this.setTestUserAccountBalance.Verify();
        }
    }
}