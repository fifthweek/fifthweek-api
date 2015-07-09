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
    public class GetUserPaymentOriginDbStatementTests : PersistenceTestsBase
    {
        private static readonly UserId UserId = UserId.Random();

        private static readonly string StripeCustomerId = "stripeCustomerId";
        private static readonly string CountryCode = "USA";
        private static readonly string CreditCardPrefix = "162534";
        private static readonly string IpAddress = "1.1.1.1";
        private static readonly string OriginalTaxamoTransactionKey = "originalTaxamoTransactionKey";
        private static readonly PaymentStatus PaymentStatus = PaymentStatus.Retry1;

        private static readonly UserPaymentOriginResult Origin = new UserPaymentOriginResult(
            StripeCustomerId,
            CountryCode,
            CreditCardPrefix,
            IpAddress,
            OriginalTaxamoTransactionKey,
            PaymentStatus);

        private GetUserPaymentOriginDbStatement target;

        [TestInitialize]
        public void Initialize()
        {
            this.target = new GetUserPaymentOriginDbStatement(
                new Mock<IFifthweekDbConnectionFactory>(MockBehavior.Strict).Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenUserIdIsNull_ItShouldThrowAnException()
        {
            await this.target.ExecuteAsync(null);
        }

        [TestMethod]
        public async Task WhenDataExists_ItShouldReturnOrigin()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new GetUserPaymentOriginDbStatement(testDatabase);
                await this.CreateDataAsync(UserId, testDatabase);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.ExecuteAsync(UserId);

                Assert.AreEqual(Origin, result);

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenDataDoesNotExist_ItShouldReturnEmptyOrigin()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new GetUserPaymentOriginDbStatement(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.ExecuteAsync(UserId);

                Assert.AreEqual(UserPaymentOriginResult.Empty, result);

                return ExpectedSideEffects.None;
            });
        }

        private async Task CreateDataAsync(UserId newUserId, TestDatabaseContext testDatabase)
        {
            var random = new Random();
            var user = UserTests.UniqueEntity(random);
            user.Id = newUserId.Value;

            var origin = new UserPaymentOrigin(
                UserId.Value,
                user,
                StripeCustomerId,
                CountryCode,
                CreditCardPrefix,
                IpAddress,
                OriginalTaxamoTransactionKey,
                PaymentStatus);

            using (var databaseContext = testDatabase.CreateContext())
            {
                databaseContext.Users.Add(user);
                databaseContext.UserPaymentOrigins.Add(origin);
                await databaseContext.SaveChangesAsync();
            }
        }
    }
}