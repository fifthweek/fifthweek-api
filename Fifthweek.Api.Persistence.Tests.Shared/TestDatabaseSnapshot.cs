namespace Fifthweek.Api.Persistence.Tests.Shared
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    public class TestDatabaseSnapshot
    {
        private readonly TestDatabase testDatabase;
        private readonly List<ITableBeforeAndAfter> tables = new List<ITableBeforeAndAfter>();

        public TestDatabaseSnapshot(TestDatabase testDatabase)
        {
            if (testDatabase == null)
            {
                throw new ArgumentNullException("testDatabase");
            }

            this.testDatabase = testDatabase;
        }

        public async Task InitializeAsync()
        {
            var stopwatch = Stopwatch.StartNew();

            this.tables.Add(this.Load(databaseContext => databaseContext.Users));
            this.tables.Add(this.Load(databaseContext => databaseContext.Subscriptions));
            this.tables.Add(this.Load(databaseContext => databaseContext.Channels));
            this.tables.Add(this.Load(databaseContext => databaseContext.Collections));
            this.tables.Add(this.Load(databaseContext => databaseContext.WeeklyReleaseTimes));
            this.tables.Add(this.Load(databaseContext => databaseContext.Posts));
            this.tables.Add(this.Load(databaseContext => databaseContext.Files));

            Trace.WriteLine(string.Format("Snapshot taken in {0}s", Math.Round(stopwatch.Elapsed.TotalSeconds, 2)));
        }

        public async Task AssertSideEffectsAsync(ExpectedSideEffects sideEffects)
        {
            if (this.tables.Count == 0)
            {
                throw new Exception("No snapshot found. Did you forget to take a snapshot?");
            }

            var stopwatch = Stopwatch.StartNew();

            using (var databaseContext = this.testDatabase.NewDatabaseContext())
            {
                databaseContext.Configuration.AutoDetectChangesEnabled = false;
                databaseContext.Configuration.LazyLoadingEnabled = false;
                databaseContext.Configuration.ValidateOnSaveEnabled = false;
                databaseContext.Configuration.ProxyCreationEnabled = false;
               
                foreach (var table in this.tables)
                {
                    table.GetLatestFromDatabase(databaseContext);
                }
            }

            Trace.WriteLine(string.Format("Queried latest in {0}s", Math.Round(stopwatch.Elapsed.TotalSeconds, 2)));

            stopwatch = Stopwatch.StartNew();

            foreach (var table in this.tables)
            {
                this.AssertNoUnexpectedSideEffects(table.Snapshot, table.Database, sideEffects);
            }

            Trace.WriteLine(string.Format("Asserted no unexpected side effects in {0}s", Math.Round(stopwatch.Elapsed.TotalSeconds, 2)));

            stopwatch = Stopwatch.StartNew();

            var snapshot = this.tables.SelectMany(_ => _.Snapshot).ToList();
            var database = this.tables.SelectMany(_ => _.Database).ToList();
            this.AssertExpectedSideEffects(snapshot, database, sideEffects);

            Trace.WriteLine(string.Format("Asserted expected side effects in {0}s", Math.Round(stopwatch.Elapsed.TotalSeconds, 2)));
        }

        private static bool AreEntitiesEqual(object possibleWildcard, object standardEntity)
        {
            var wildcardEntity = possibleWildcard as IWildcardEntity;
            if (wildcardEntity != null && standardEntity != null)
            {
                if (wildcardEntity.EntityType != standardEntity.GetType())
                {
                    // Ensure we do not try to get expected value using an actual value of different type.
                    return false;
                }

                possibleWildcard = wildcardEntity.GetExpectedValue(standardEntity);
            }

            return Equals(possibleWildcard, standardEntity);
        }

        private void AssertNoUnexpectedSideEffects(IReadOnlyCollection<IIdentityEquatable> snapshot, IReadOnlyCollection<IIdentityEquatable> database, ExpectedSideEffects sideEffects)
        {
            foreach (var databaseEntity in database)
            {
                if (sideEffects.ExcludedFromTest != null && sideEffects.ExcludedFromTest(databaseEntity))
                {
                    continue;
                }

                var snapshotEntity = snapshot.FirstOrDefault(_ => _.IdentityEquals(databaseEntity));
                if (snapshotEntity == null)
                {
                    if (sideEffects.Inserts == null)
                    {
                        Assert.Fail("Insert was unexpected");
                    }
                    else
                    {
                        Assert.IsTrue(sideEffects.Inserts.Any(_ => AreEntitiesEqual(_, databaseEntity)), "Insert was unexpected");
                    }
                }
                else if (!snapshotEntity.Equals(databaseEntity))
                {
                    if (sideEffects.Updates == null)
                    {
                        Assert.Fail("Update was unexpected");
                    }
                    else
                    {
                        Assert.IsTrue(sideEffects.Updates.Any(_ => AreEntitiesEqual(_, databaseEntity)), "Update was unexpected");
                    }
                }
            }

            foreach (var snapshotEntity in snapshot)
            {
                if (sideEffects.ExcludedFromTest != null && sideEffects.ExcludedFromTest(snapshotEntity))
                {
                    continue;
                }

                if (database.All(_ => !_.IdentityEquals(snapshotEntity)))
                {
                    if (sideEffects.Deletes == null)
                    {
                        Assert.Fail("Delete was unexpected");
                    }
                    else
                    {
                        Assert.IsTrue(sideEffects.Deletes.Any(_ => AreEntitiesEqual(_, snapshotEntity)), "Delete was unexpected");
                    }
                }
            }
        }

        private void AssertExpectedSideEffects(IReadOnlyCollection<IIdentityEquatable> snapshot, IReadOnlyCollection<IIdentityEquatable> database, ExpectedSideEffects sideEffects)
        {
            if (sideEffects.Inserts != null)
            {
                foreach (var insert in sideEffects.Inserts)
                {
                    // We call insert.IdentityEquals because it doesn't enforce type checking (it may be an IWildcardEntity).
                    Assert.IsTrue(database.Any(_ => insert.IdentityEquals(_)), "Insert was not applied");
                    Assert.IsTrue(database.Any(_ => AreEntitiesEqual(insert, _)), "Insert was applied, but not with expected fields");
                    Assert.IsTrue(snapshot.All(_ => !insert.IdentityEquals(_)), "Cannot assert insert on entity that already exists in snapshot");
                }
            }

            if (sideEffects.Updates != null)
            {
                foreach (var update in sideEffects.Updates)
                {
                    Assert.IsTrue(database.Any(_ => AreEntitiesEqual(update, _)), "Update was not applied");
                    Assert.IsTrue(snapshot.All(_ => !AreEntitiesEqual(update, _)), "Cannot assert update on entity that has not changed since the snapshot");
                    Assert.IsTrue(snapshot.Any(_ => update.IdentityEquals(_)), "Cannot assert update on entity that does not exist in snapshot");
                }
            }

            if (sideEffects.Deletes != null)
            {
                foreach (var delete in sideEffects.Deletes)
                {
                    Assert.IsTrue(database.All(_ => !_.IdentityEquals(delete)), "Delete was not applied");
                }
            }
        }

        private ITableBeforeAndAfter Load<T>(Func<IFifthweekDbContext, IDbSet<T>> setFactory) where T : class
        {
            var table = new TableBeforeAndAfter<T>(setFactory);

            using (var databaseContext = this.testDatabase.NewDatabaseContext())
            {
                databaseContext.Configuration.AutoDetectChangesEnabled = false;
                databaseContext.Configuration.LazyLoadingEnabled = false;
                databaseContext.Configuration.ValidateOnSaveEnabled = false;
                databaseContext.Configuration.ProxyCreationEnabled = false;

                table.InitializeSnapshot(databaseContext);
            }

            return table;
        }
    }
}