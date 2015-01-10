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
        private readonly TemporaryDatabase database;

        private IFifthweekDbContext snapshot; // 'Local' properties on each DbSet will represent snapshot state.

        public async Task TakeSnapshotAsync()
        {
            this.snapshot = this.database.NewDbContext();
            await this.snapshot.Users.LoadAsync();
            await this.snapshot.Subscriptions.LoadAsync();
            await this.snapshot.Channels.LoadAsync();
        }

        public Task AssertNoSideEffectsAsync()
        {
            var noDelta = new object[0];
            return this.AssertNoUnexpectedSideEffectsAsync(noDelta, noDelta, noDelta);
        }

        public async Task AssertNoUnexpectedSideEffectsAsync(
            IReadOnlyList<object> expectedInserts,
            IReadOnlyList<object> expectedUpdates,
            IReadOnlyList<object> expectedDeletions)
        {
            using (var dbContext = this.database.NewDbContext())
            {
                await AssertNoUnexpectedSideEffectsAsync(this.snapshot.Users, dbContext.Users, expectedInserts, expectedUpdates, expectedDeletions);
                await AssertNoUnexpectedSideEffectsAsync(this.snapshot.Subscriptions, dbContext.Subscriptions, expectedInserts, expectedUpdates, expectedDeletions);
                await AssertNoUnexpectedSideEffectsAsync(this.snapshot.Channels, dbContext.Channels, expectedInserts, expectedUpdates, expectedDeletions);
            }
        }

        private async Task AssertNoUnexpectedSideEffectsAsync<T>(
            IDbSet<T> snapshot, IDbSet<T> database,
            IReadOnlyList<object> expectedInserts,
            IReadOnlyList<object> expectedUpdates,
            IReadOnlyList<object> expectedDeletions)
            where T : class
        {
            var databaseList = await database.ToListAsync();
            var snapshotList = snapshot.Local.ToList();

            Assert.IsTrue(databaseList.All(_ => _ is IIdentityEquatable), "Entities must implement IIdentityEquatable");
            Assert.IsTrue(snapshotList.All(_ => _ is IIdentityEquatable), "Entities must implement IIdentityEquatable");

            await this.AssertNoUnexpectedSideEffectsAsync(
                snapshotList.OfType<IIdentityEquatable>().ToList(),
                databaseList.OfType<IIdentityEquatable>().ToList(),
                expectedInserts,
                expectedUpdates,
                expectedDeletions);
        }

        private async Task AssertNoUnexpectedSideEffectsAsync(
            IReadOnlyList<IIdentityEquatable> snapshot, IReadOnlyList<IIdentityEquatable> database,
            IReadOnlyList<object> expectedInserts,
            IReadOnlyList<object> expectedUpdates,
            IReadOnlyList<object> expectedDeletions)
        {
            foreach (var databaseEntity in database)
            {
                var snapshotEntity = snapshot.FirstOrDefault(_ => _.IdentityEquals(databaseEntity));
                if (snapshotEntity == null)
                {
                    Assert.IsTrue(expectedInserts.Contains(databaseEntity), "Unexpected insert");
                    
                }
                else if (!snapshotEntity.Equals(databaseEntity))
                {
                    Assert.IsTrue(expectedUpdates.Contains(databaseEntity), "Unexpected update");
                }
            }

            foreach (var snapshotEntity in snapshot)
            {
                if (database.All(_ => !_.IdentityEquals(snapshotEntity)))
                {
                    Assert.IsTrue(expectedDeletions.Contains(snapshotEntity), "Unexpected delete");
                }
            }
        }
    }
}