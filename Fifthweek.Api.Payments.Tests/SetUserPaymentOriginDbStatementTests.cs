namespace Fifthweek.Api.Payments.Tests
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Api.Persistence.Tests.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class SetUserPaymentOriginDbStatementTests : PersistenceTestsBase
    {
        private static readonly UserId UserId = UserId.Random();

        private static readonly string StripeCustomerId = "stripeCustomerId";
        private static readonly ValidCountryCode CountryCode = ValidCountryCode.Parse("GB");
        private static readonly ValidCreditCardPrefix CreditCardPrefix = ValidCreditCardPrefix.Parse("123456");
        private static readonly ValidIpAddress IpAddress = ValidIpAddress.Parse("1.1.1.1");
        private static readonly PaymentStatus PaymentStatus = PaymentStatus.Retry1;

        private SetUserPaymentOriginDbStatement target;

        [TestInitialize]
        public void Initialize()
        {
            this.target = new SetUserPaymentOriginDbStatement(new Mock<FifthweekDbConnectionFactory>(MockBehavior.Strict).Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenUserIdIsNull_ItShouldThrowAnException()
        {
            await this.target.ExecuteAsync(
                null,
                StripeCustomerId,
                CountryCode,
                CreditCardPrefix,
                IpAddress);
        }

        [TestMethod]
        public async Task WhenValuesAreNull_ItShouldSaveData()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new SetUserPaymentOriginDbStatement(testDatabase);
                await this.CreateDataAsync(UserId, testDatabase, false);
                await testDatabase.TakeSnapshotAsync();

                await this.target.ExecuteAsync(
                    UserId,
                    null,
                    null,
                    null,
                    null);

                return new ExpectedSideEffects
                {
                    Insert = new UserPaymentOrigin(
                        UserId.Value,
                        null,
                        null,
                        null,
                        null,
                        null,
                        null,
                        default(PaymentStatus))
                };
            });
        }

        [TestMethod]
        public async Task WhenDataExistsAndNewValuesAreNull_ItShouldSaveDataAndClearOriginalTransactionKey()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new SetUserPaymentOriginDbStatement(testDatabase);
                await this.CreateDataAsync(UserId, testDatabase, true);
                await testDatabase.TakeSnapshotAsync();

                await this.target.ExecuteAsync(
                    UserId,
                    null,
                    null,
                    null,
                    null);

                return new ExpectedSideEffects
                {
                    Update = new UserPaymentOrigin(
                        UserId.Value,
                        null,
                        null,
                        null,
                        null,
                        null,
                        null,
                        PaymentStatus)
                };
            });
        }

        [TestMethod]
        public async Task WhenValuesArePopulated_ItShouldSaveData()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new SetUserPaymentOriginDbStatement(testDatabase);
                await this.CreateDataAsync(UserId, testDatabase, false);
                await testDatabase.TakeSnapshotAsync();

                await this.target.ExecuteAsync(
                    UserId,
                    StripeCustomerId,
                    CountryCode,
                    CreditCardPrefix,
                    IpAddress);

                return new ExpectedSideEffects
                {
                    Insert = new UserPaymentOrigin(
                        UserId.Value,
                        null,
                        StripeCustomerId,
                        CountryCode.Value,
                        CreditCardPrefix.Value,
                        IpAddress.Value,
                        null,
                        default(PaymentStatus))
                };
            });
        }

        [TestMethod]
        public async Task WhenDataExistsAndNewValuesArePopulated_ItShouldSaveDataAndClearOriginalTransactionKey()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new SetUserPaymentOriginDbStatement(testDatabase);
                await this.CreateDataAsync(UserId, testDatabase, true);
                await testDatabase.TakeSnapshotAsync();

                await this.target.ExecuteAsync(
                    UserId,
                    StripeCustomerId,
                    CountryCode,
                    CreditCardPrefix,
                    IpAddress);

                return new ExpectedSideEffects
                {
                    Update = new UserPaymentOrigin(
                        UserId.Value,
                        null,
                        StripeCustomerId,
                        CountryCode.Value,
                        CreditCardPrefix.Value,
                        IpAddress.Value,
                        null,
                        PaymentStatus)
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
                    "anotherStripeCustomerId",
                    "USA",
                    "999999",
                    "9.9.9.999",
                    "anotherKey",
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