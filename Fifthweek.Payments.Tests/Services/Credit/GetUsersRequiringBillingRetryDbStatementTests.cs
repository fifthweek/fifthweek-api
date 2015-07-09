namespace Fifthweek.Payments.Tests.Services.Credit
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Api.Persistence.Tests.Shared;
    using Fifthweek.Payments.Services.Credit;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class GetUsersRequiringBillingRetryDbStatementTests : PersistenceTestsBase
    {
        private static readonly UserId UserId1 = UserId.Random();
        private static readonly UserId UserId2 = UserId.Random();
        private static readonly UserId UserId3 = UserId.Random();
        private static readonly UserId UserId4 = UserId.Random();
        private static readonly UserId UserId5 = UserId.Random();
        private static readonly UserId UserId6 = UserId.Random();
        private static readonly UserId UserId7 = UserId.Random();

        private static readonly string TaxamoTransactionKey = "taxamoTransactionKey";
        private static readonly string StripeCustomerId = "stripeCustomerId";
        private static readonly string CountryCode = "GB";
        private static readonly string CreditCardPrefix = "123456";
        private static readonly string IpAddress = "1.1.1.1";

        private GetUsersRequiringBillingRetryDbStatement target;

        [TestInitialize]
        public void Initialize()
        {
            this.target = new GetUsersRequiringBillingRetryDbStatement(new Mock<FifthweekDbConnectionFactory>(MockBehavior.Strict).Object);
        }

        [TestMethod]
        public async Task ItShouldReturnUsersRequiringBillingRetry()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new GetUsersRequiringBillingRetryDbStatement(testDatabase);

                var initialResults = await this.target.ExecuteAsync();
                
                await this.CreateDataAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                var results = await this.target.ExecuteAsync();

                CollectionAssert.AreEquivalent(
                    initialResults.Concat(new List<UserId> { UserId2, UserId3, UserId4 }).ToList(),
                    results.ToList());
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
                this.CreateUser(random, databaseContext, UserId7);
                await databaseContext.SaveChangesAsync();
            }

            using (var connection = testDatabase.CreateConnection())
            {
                await connection.InsertAsync(new UserPaymentOrigin(
                    UserId1.Value,
                    null,
                    StripeCustomerId,
                    CountryCode,
                    CreditCardPrefix,
                    IpAddress,
                    TaxamoTransactionKey,
                    BillingStatus.None));

                await connection.InsertAsync(new UserPaymentOrigin(
                    UserId2.Value,
                    null,
                    StripeCustomerId,
                    CountryCode,
                    CreditCardPrefix,
                    IpAddress,
                    TaxamoTransactionKey,
                    BillingStatus.Retry1));

                await connection.InsertAsync(new UserPaymentOrigin(
                    UserId3.Value,
                    null,
                    StripeCustomerId,
                    CountryCode,
                    CreditCardPrefix,
                    IpAddress,
                    TaxamoTransactionKey,
                    BillingStatus.Retry2));

                await connection.InsertAsync(new UserPaymentOrigin(
                    UserId4.Value,
                    null,
                    StripeCustomerId,
                    CountryCode,
                    CreditCardPrefix,
                    IpAddress,
                    TaxamoTransactionKey,
                    BillingStatus.Retry3));

                await connection.InsertAsync(new UserPaymentOrigin(
                    UserId5.Value,
                    null,
                    StripeCustomerId,
                    CountryCode,
                    CreditCardPrefix,
                    IpAddress,
                    TaxamoTransactionKey,
                    BillingStatus.Failed));

                await connection.InsertAsync(new UserPaymentOrigin(
                    UserId6.Value,
                    null,
                    null,
                    CountryCode,
                    CreditCardPrefix,
                    IpAddress,
                    TaxamoTransactionKey,
                    BillingStatus.Retry2));
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