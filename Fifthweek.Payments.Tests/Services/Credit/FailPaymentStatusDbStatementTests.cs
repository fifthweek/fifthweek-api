namespace Fifthweek.Payments.Tests.Services.Credit
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Api.Persistence.Tests.Shared;
    using Fifthweek.Payments.Services.Credit;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class FailPaymentStatusDbStatementTests : PersistenceTestsBase
    {
        private static readonly UserId UserId = UserId.Random();

        private static readonly string TaxamoTransactionKey = "taxamoTransactionKey";
        private static readonly string PaymentOriginType = "stripeCustomerId";
        private static readonly PaymentOriginKeyType PaymentOriginKeyType = PaymentOriginKeyType.Stripe;
        private static readonly string CountryCode = "GB";
        private static readonly string CreditCardPrefix = "123456";
        private static readonly string IpAddress = "1.1.1.1";
        private static readonly PaymentStatus PaymentStatus = PaymentStatus.Retry1;

        private FailPaymentStatusDbStatement target;

        [TestInitialize]
        public void Initialize()
        {
            this.target = new FailPaymentStatusDbStatement(new Mock<FifthweekDbConnectionFactory>(MockBehavior.Strict).Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenUserIdIsNull_ItShouldThrowAnException()
        {
            await this.target.ExecuteAsync(null);
        }

        [TestMethod]
        public async Task WhenDataExists_ItShouldSaveData()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new FailPaymentStatusDbStatement(testDatabase);
                await this.CreateDataAsync(UserId, testDatabase, true);
                await testDatabase.TakeSnapshotAsync();

                await this.target.ExecuteAsync(UserId);

                return new ExpectedSideEffects
                {
                    Update = new UserPaymentOrigin(
                        UserId.Value,
                        null,
                        PaymentOriginType,
                        PaymentOriginKeyType,
                        CountryCode,
                        CreditCardPrefix,
                        IpAddress,
                        TaxamoTransactionKey,
                        PaymentStatus.Failed)
                };
            });
        }

        [TestMethod]
        public async Task WhenDataDoesNotExist_ItShouldSaveData()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new FailPaymentStatusDbStatement(testDatabase);
                await this.CreateDataAsync(UserId, testDatabase, false);
                await testDatabase.TakeSnapshotAsync();

                await this.target.ExecuteAsync(UserId);

                return new ExpectedSideEffects
                {
                    Insert = new UserPaymentOrigin(
                        UserId.Value,
                        null,
                        null,
                        PaymentOriginKeyType.None,
                        null,
                        null,
                        null,
                        null,
                        PaymentStatus.Failed)
                };
            });
        }

        private async Task CreateDataAsync(UserId newUserId, TestDatabaseContext testDatabase, bool createOrigin)
        {
            var random = new Random();
            var user = UserTests.UniqueEntity(random);
            user.Id = newUserId.Value;

            UserPaymentOrigin origin = null;
            if (createOrigin)
            {
                origin = new UserPaymentOrigin(
                    UserId.Value,
                    user,
                    PaymentOriginType,
                    PaymentOriginKeyType,
                    CountryCode,
                    CreditCardPrefix,
                    IpAddress,
                    TaxamoTransactionKey,
                    PaymentStatus);
            }

            using (var databaseContext = testDatabase.CreateContext())
            {
                databaseContext.Users.Add(user);
                if (createOrigin)
                {
                    databaseContext.UserPaymentOrigins.Add(origin);
                }

                await databaseContext.SaveChangesAsync();
            }
        }
    }
}