namespace Fifthweek.Payments.Tests.Services.Credit
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Api.Persistence.Tests.Shared;
    using Fifthweek.Payments.Services.Credit;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class IncrementBillingStatusDbStatementTests : PersistenceTestsBase
    {
        private static readonly UserId UserId1 = UserId.Random();
        private static readonly UserId UserId2 = UserId.Random();
        private static readonly UserId UserId3 = UserId.Random();
        private static readonly UserId UserId4 = UserId.Random();
        private static readonly UserId UserId5 = UserId.Random();
        private static readonly UserId UserId6 = UserId.Random();
        private static readonly List<UserId> UserIdsToUpdate = new List<UserId>
        {
            UserId2,
            UserId3,
            UserId4,
            UserId5,
            UserId6,
        };

        private static readonly string TaxamoTransactionKey = "taxamoTransactionKey";
        private static readonly string StripeCustomerId = "stripeCustomerId";
        private static readonly string CountryCode = "GB";
        private static readonly string CreditCardPrefix = "123456";
        private static readonly string IpAddress = "1.1.1.1";

        private IncrementBillingStatusDbStatement target;

        [TestInitialize]
        public void Initialize()
        {
            this.target = new IncrementBillingStatusDbStatement(new Mock<FifthweekDbConnectionFactory>(MockBehavior.Strict).Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenUserIdIsNull_ItShouldThrowAnException()
        {
            await this.target.ExecuteAsync(null);
        }

        [TestMethod]
        public async Task WhenUserIdsPassedIn_ItShouldUpdateData()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new IncrementBillingStatusDbStatement(testDatabase);
                await this.CreateDataAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                await this.target.ExecuteAsync(UserIdsToUpdate);

                return new ExpectedSideEffects
                {
                    Updates = new List<IIdentityEquatable>
                    {
                        new UserPaymentOrigin(
                            UserId2.Value,
                            null,
                            StripeCustomerId,
                            CountryCode,
                            CreditCardPrefix,
                            IpAddress,
                            TaxamoTransactionKey,
                            BillingStatus.Retry1),
                        new UserPaymentOrigin(
                            UserId3.Value,
                            null,
                            StripeCustomerId,
                            CountryCode,
                            CreditCardPrefix,
                            IpAddress,
                            TaxamoTransactionKey,
                            BillingStatus.Failed),
                    }
                };
            });
        }

        [TestMethod]
        public async Task WhenNoUserIdsPassed_ItShouldDoNothing()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new IncrementBillingStatusDbStatement(testDatabase);
                await this.CreateDataAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                await this.target.ExecuteAsync(new List<UserId>());

                return ExpectedSideEffects.None;
            });
        }

        private async Task CreateDataAsync(TestDatabaseContext testDatabase)
        {
            using (var databaseContext = testDatabase.CreateContext())
            {
                var random = new Random();
                this.CreateUser(random, databaseContext, UserId1);
                this.CreateUser(random, databaseContext, UserId2);
                this.CreateUser(random, databaseContext, UserId3);
                this.CreateUser(random, databaseContext, UserId4);
                this.CreateUser(random, databaseContext, UserId5);
                this.CreateUser(random, databaseContext, UserId6);
                await databaseContext.SaveChangesAsync();
            }

            using (var connection = testDatabase.CreateConnection())
            {
                // Not in list of user ids to update.
                await connection.InsertAsync(new UserPaymentOrigin(
                    UserId1.Value,
                    null,
                    StripeCustomerId,
                    CountryCode,
                    CreditCardPrefix,
                    IpAddress,
                    TaxamoTransactionKey,
                    BillingStatus.Retry1));

                // Should be updated to Retry1.
                await connection.InsertAsync(new UserPaymentOrigin(
                    UserId2.Value,
                    null,
                    StripeCustomerId,
                    CountryCode,
                    CreditCardPrefix,
                    IpAddress,
                    TaxamoTransactionKey,
                    BillingStatus.None));

                // Should be updated to Failed.
                await connection.InsertAsync(new UserPaymentOrigin(
                    UserId3.Value,
                    null,
                    StripeCustomerId,
                    CountryCode,
                    CreditCardPrefix,
                    IpAddress,
                    TaxamoTransactionKey,
                    BillingStatus.Retry3));

                // Should not be updated because it is failed.
                await connection.InsertAsync(new UserPaymentOrigin(
                    UserId4.Value,
                    null,
                    StripeCustomerId,
                    CountryCode,
                    CreditCardPrefix,
                    IpAddress,
                    TaxamoTransactionKey,
                    BillingStatus.Failed));

                // Should not be updated because it doesn't have a stripe customer id.
                await connection.InsertAsync(new UserPaymentOrigin(
                    UserId5.Value,
                    null,
                    null,
                    CountryCode,
                    CreditCardPrefix,
                    IpAddress,
                    TaxamoTransactionKey,
                    BillingStatus.None));
            }
        }

        private void CreateUser(Random random, FifthweekDbContext databaseContext, UserId userId)
        {
            var creator = UserTests.UniqueEntity(random);
            creator.Id = userId.Value;

            databaseContext.Users.Add(creator);
        }
    }
}