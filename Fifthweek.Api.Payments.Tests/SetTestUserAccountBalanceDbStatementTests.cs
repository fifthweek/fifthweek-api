namespace Fifthweek.Api.Payments.Tests
{
    using System;
    using System.Data.SqlTypes;
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Api.Persistence.Tests.Shared;
    using Fifthweek.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class SetTestUserAccountBalanceDbStatementTests : PersistenceTestsBase
    {
        private static readonly UserId UserId = UserId.Random();
        private static readonly DateTime Timestamp = new SqlDateTime(DateTime.UtcNow).Value;
        private static readonly PositiveInt Amount = PositiveInt.Parse(10);

        private SetTestUserAccountBalanceDbStatement target;

        [TestInitialize]
        public void Initialize()
        {
            this.target = new SetTestUserAccountBalanceDbStatement(new Mock<FifthweekDbConnectionFactory>(MockBehavior.Strict).Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenUserIdIsNull_ItShouldThrowAnException()
        {
            await this.target.ExecuteAsync(
                null,
                Timestamp,
                Amount);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenAmountIsNull_ItShouldThrowAnException()
        {
            await this.target.ExecuteAsync(
                UserId,
                Timestamp,
                null);
        }

        [TestMethod]
        public async Task WhenValuesArePopulated_ItShouldSaveData()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new SetTestUserAccountBalanceDbStatement(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                await this.target.ExecuteAsync(
                    UserId,
                    Timestamp,
                    Amount);

                return new ExpectedSideEffects
                {
                    Insert = new CalculatedAccountBalance(
                        UserId.Value,
                        LedgerAccountType.Fifthweek,
                        Timestamp,
                        Amount.Value)
                };
            });
        }
    }
}