namespace Fifthweek.Api.Persistence.Tests.Shared
{
    using System.Threading.Tasks;

    public abstract class PersistenceTestsBase
    {
        private TemporaryDatabase temporaryDatabase;
        private TemporaryDatabaseState databaseState;

        public Task InitializeDatabaseAsync()
        {
            return this.temporaryDatabase.ReadyAsync();
        }

        public virtual void Initialize()
        {
            this.temporaryDatabase = TemporaryDatabase.CreateNew();
            this.databaseState = new TemporaryDatabaseState(this.temporaryDatabase);
        }

        public virtual void Cleanup()
        {
            if (this.temporaryDatabase == null)
            {
                return;
            }

            this.temporaryDatabase.Dispose();
            this.temporaryDatabase = null;
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