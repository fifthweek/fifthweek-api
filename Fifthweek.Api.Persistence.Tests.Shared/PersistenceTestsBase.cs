namespace Fifthweek.Api.Persistence.Tests.Shared
{
    using System;
    using System.Data.Common;
    using System.Threading.Tasks;
    using System.Transactions;

    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    public abstract partial class PersistenceTestsBase
    {
        protected async Task DatabaseTestAsync(Func<TestDatabaseContext, Task<ExpectedSideEffects>> databaseTest)
        {
            var database = await TestDatabase.CreateNewAsync();
            var databaseSnapshot = new TestDatabaseSnapshot(database);
            var databaseContext = new TestDatabaseContext(database, databaseSnapshot);
            
            using (TransactionScopeBuilder.CreateAsync())
            {
                var sideEffects = await databaseTest(databaseContext);

                if (Transaction.Current.TransactionInformation.Status == TransactionStatus.Aborted
                    && object.ReferenceEquals(sideEffects, ExpectedSideEffects.TransactionAborted))
                {
                    // The transaction was aborted, which means we can't test anything except that is
                    // the intension of the test.
                }
                else
                {
                    await databaseSnapshot.AssertSideEffectsAsync(sideEffects);
                }
            }
        }

        [AutoConstructor]
        public partial class TestDatabaseContext : IFifthweekDbConnectionFactory
        {
            private readonly TestDatabase testDatabase;
            private readonly TestDatabaseSnapshot testDatabaseSnapshot;

            public Task TakeSnapshotAsync()
            {
                return this.testDatabaseSnapshot.InitializeAsync();
            }

            public DbConnection CreateConnection()
            {
                return this.testDatabase.CreateConnection();
            }

            public FifthweekDbContext CreateContext()
            {
                return this.testDatabase.CreateContext();
            }
        }
    }
}