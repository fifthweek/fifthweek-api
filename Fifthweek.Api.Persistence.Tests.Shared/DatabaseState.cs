using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Fifthweek.Api.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Fifthweek.Api.Persistence.Tests.Shared
{
    [AutoConstructor]
    public partial class DatabaseState
    {
        private readonly TemporaryDatabase temporaryDatabase;

        private IFifthweekDbContext loadedSnapshotContext; // 'Local' properties on each DbSet will represent Snapshot state.

        public async Task TakeSnapshotAsync()
        {
            this.loadedSnapshotContext = this.temporaryDatabase.NewDbContext();
            await this.loadedSnapshotContext.Users.LoadAsync();
            await this.loadedSnapshotContext.Subscriptions.LoadAsync();
            await this.loadedSnapshotContext.Channels.LoadAsync();
        }

        public async Task AssertSideEffectsAsync(ExpectedSideEffects sideEffects)
        {
            var tables = new List<TableBeforeAndAfter>();
            using (var dbContext = this.temporaryDatabase.NewDbContext())
            {
                tables.Add(await TableBeforeAndAfter.GenerateAsync(this.loadedSnapshotContext.Users, dbContext.Users));
                tables.Add(await TableBeforeAndAfter.GenerateAsync(this.loadedSnapshotContext.Subscriptions, dbContext.Subscriptions));
                tables.Add(await TableBeforeAndAfter.GenerateAsync(this.loadedSnapshotContext.Channels, dbContext.Channels));
            }

            var snapshot = tables.SelectMany(_ => _.Snapshot).ToList();
            var database = tables.SelectMany(_ => _.Database).ToList();

            this.AssertNoUnexpectedSideEffects(snapshot, database, sideEffects);
            this.AssertExpectedSideEffects(snapshot, database, sideEffects);
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
                    Assert.IsTrue(database.Any(_ => AreEntitiesEqual(insert, _)), "Insert was not applied");
                    Assert.IsTrue(snapshot.All(_ => !_.IdentityEquals(insert)), "Cannot assert insert on entity that already exists in snapshot");
                }
            }

            if (sideEffects.Updates != null)
            {
                foreach (var update in sideEffects.Updates)
                {
                    Assert.IsTrue(database.Any(_ => AreEntitiesEqual(update, _)), "Update was not applied");
                    Assert.IsTrue(snapshot.All(_ => !AreEntitiesEqual(update, _)), "Cannot assert update on entity that has not changed since the snapshot");
                    Assert.IsTrue(snapshot.Any(_ => _.IdentityEquals(update)), "Cannot assert update on entity that does not exist in snapshot");
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

        private bool AreEntitiesEqual(object possibleWildcard, object standardEntity)
        {
            var wildcardEntity = possibleWildcard as IWildcardEntity;
            if (wildcardEntity != null)
            {
                return wildcardEntity.WildcardEquals(standardEntity);
            }

            return Equals(possibleWildcard, standardEntity);
        }
    }
}