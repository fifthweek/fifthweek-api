namespace Fifthweek.Api.Persistence.Tests.Shared
{
    using System;
    using System.Threading.Tasks;
    using System.Transactions;

    using Fifthweek.Api.Core;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    public abstract partial class PersistenceTestsBase
    {
        protected async Task NewTestDatabaseAsync(Func<TestDatabaseContext, Task<ExpectedSideEffects>> databaseTest)
        {
            var database = await TestDatabase.CreateNewAsync();
            var databaseSnapshot = new TestDatabaseSnapshot(database);
            var databaseContext = new TestDatabaseContext(database, databaseSnapshot);
            
            using (new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var sideEffects = await databaseTest(databaseContext);
                await databaseSnapshot.AssertSideEffectsAsync(sideEffects);
            }
        }

        [AutoConstructor]
        public partial class TestDatabaseContext
        {
            private readonly TestDatabase testDatabase;
            private readonly TestDatabaseSnapshot testDatabaseSnapshot;

            public Task TakeSnapshotAsync()
            {
                return this.testDatabaseSnapshot.InitializeAsync();
            }

            public IFifthweekDbContext NewContext()
            {
                return this.testDatabase.NewDatabaseContext();
            }
        }
    }
}