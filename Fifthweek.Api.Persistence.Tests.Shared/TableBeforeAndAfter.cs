using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Fifthweek.Api.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Fifthweek.Api.Persistence.Tests.Shared
{
    [AutoConstructor]
    public partial class TableBeforeAndAfter
    {
        public IReadOnlyList<IIdentityEquatable> Snapshot { get; private set; }
        public IReadOnlyList<IIdentityEquatable> Database { get; private set; }

        public static async Task<TableBeforeAndAfter> GenerateAsync<T>(IDbSet<T> snapshot, IDbSet<T> database) where T : class
        {
            var snapshotList = snapshot.Local.ToList();
            var databaseList = await database.ToListAsync();

            Assert.IsTrue(snapshotList.All(_ => _ is IIdentityEquatable), "Entities must implement IIdentityEquatable");
            Assert.IsTrue(databaseList.All(_ => _ is IIdentityEquatable), "Entities must implement IIdentityEquatable");

            return new TableBeforeAndAfter(
                snapshotList.OfType<IIdentityEquatable>().ToList(),
                databaseList.OfType<IIdentityEquatable>().ToList());
        }
    }
}