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
    public class DeleteUserPaymentInformationDbStatementTests : PersistenceTestsBase
    {
        private static readonly UserId UserId = UserId.Random();

        private static readonly string StripeCustomerId = "stripeCustomerId";
        private static readonly ValidCountryCode CountryCode = ValidCountryCode.Parse("GB");
        private static readonly ValidCreditCardPrefix CreditCardPrefix = ValidCreditCardPrefix.Parse("123456");
        private static readonly ValidIpAddress IpAddress = ValidIpAddress.Parse("1.1.1.1");
        private static readonly string TaxamoOriginalTransactionKey = "taxamoKey";
        private static readonly PaymentStatus PaymentStatus = PaymentStatus.Retry1;

        private DeleteUserPaymentInformationDbStatement target;

        [TestInitialize]
        public void Initialize()
        {
            this.target = new DeleteUserPaymentInformationDbStatement(new Mock<FifthweekDbConnectionFactory>(MockBehavior.Strict).Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenUserIdIsNull_ItShouldThrowAnException()
        {
            await this.target.ExecuteAsync(null);
        }

        [TestMethod]
        public async Task ItShouldSaveData()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new DeleteUserPaymentInformationDbStatement(testDatabase);
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
                        default(PaymentStatus))
                };
            });
        }

        [TestMethod]
        public async Task WhenDataExists_ItShouldSaveData()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new DeleteUserPaymentInformationDbStatement(testDatabase);
                await this.CreateDataAsync(UserId, testDatabase, true);
                await testDatabase.TakeSnapshotAsync();

                await this.target.ExecuteAsync(UserId);

                return new ExpectedSideEffects
                {
                    Update = new UserPaymentOrigin(
                        UserId.Value,
                        null,
                        null,
                        PaymentOriginKeyType.None,
                        CountryCode.Value,
                        CreditCardPrefix.Value,
                        IpAddress.Value,
                        TaxamoOriginalTransactionKey,
                        PaymentStatus)
                };
            });
        }

        private async Task CreateDataAsync(UserId newUserId, PersistenceTestsBase.TestDatabaseContext testDatabase, bool createOrigin)
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
                    PaymentOriginKeyType.Stripe,
                    CountryCode.Value,
                    CreditCardPrefix.Value,
                    IpAddress.Value,
                    TaxamoOriginalTransactionKey,
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