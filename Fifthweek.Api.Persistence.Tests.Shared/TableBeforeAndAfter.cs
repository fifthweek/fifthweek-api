namespace Fifthweek.Api.Persistence.Tests.Shared
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    public class TableBeforeAndAfter<T> : ITableBeforeAndAfter where T : class
    {
        private readonly Func<IFifthweekDbContext, IDbSet<T>> setFactory;

        public TableBeforeAndAfter(Func<IFifthweekDbContext, IDbSet<T>> setFactory)
        {
            if (setFactory == null)
            {
                throw new ArgumentNullException("setFactory");
            }

            this.setFactory = setFactory;
        }

        public IReadOnlyList<IIdentityEquatable> Snapshot { get; private set; }

        public IReadOnlyList<IIdentityEquatable> Database { get; private set; }

        public void InitializeSnapshot(IFifthweekDbContext databaseContext)
        {
            var set = this.setFactory(databaseContext);

            var snapshot = set.ToList();
            var prototype = snapshot.FirstOrDefault();
            Assert.IsTrue(prototype == null || prototype is IIdentityEquatable, "Entities must implement IIdentityEquatable");

            this.Snapshot = snapshot.OfType<IIdentityEquatable>().ToList();
        }

        public void GetLatestFromDatabase(IFifthweekDbContext databaseContext)
        {
            var set = this.setFactory(databaseContext);

            var database = set.ToList();
            var prototype = database.FirstOrDefault();
            Assert.IsTrue(prototype == null || prototype is IIdentityEquatable, "Entities must implement IIdentityEquatable");

            this.Database = database.OfType<IIdentityEquatable>().ToList();
        }
    }

    public interface ITableBeforeAndAfter
    {
        IReadOnlyList<IIdentityEquatable> Snapshot { get; }

        IReadOnlyList<IIdentityEquatable> Database { get; }

        void InitializeSnapshot(IFifthweekDbContext databaseContext);

        void GetLatestFromDatabase(IFifthweekDbContext databaseContext);
    }
}