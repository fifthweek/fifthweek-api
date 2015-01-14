namespace Fifthweek.Api.Persistence.Tests.Shared
{
    using System.Threading.Tasks;

    public abstract class PersistenceTestsBase
    {
        private static TemporaryDatabase singletonTemporaryDatabase;
        private TemporaryDatabase temporaryDatabase;
        private TemporaryDatabaseState databaseState;

        public virtual void Initialize()
        {
//            if (singletonTemporaryDatabase == null)
//            {
//                singletonTemporaryDatabase = TemporaryDatabase.CreateNew();
//            }

            this.temporaryDatabase = TemporaryDatabase.CreateNew();
            this.databaseState = new TemporaryDatabaseState(this.temporaryDatabase);
        }

        public virtual void Cleanup()
        {
            if (this.temporaryDatabase != null)
            {
                this.temporaryDatabase.Dispose();
                this.temporaryDatabase = null;
            }
        }

        protected IFifthweekDbContext NewDbContext()
        {
            return this.temporaryDatabase.NewDatabaseContext();
        }

        protected Task SnapshotDatabaseAsync()
        {
            return this.databaseState.TakeSnapshotAsync();
        }

        protected Task AssertDatabaseAsync(ExpectedSideEffects sideEffects)
        {
            return this.databaseState.AssertSideEffectsAsync(sideEffects);
        }
    }
}