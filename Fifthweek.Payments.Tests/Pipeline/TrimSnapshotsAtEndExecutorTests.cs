namespace Fifthweek.Payments.Tests.Pipeline
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Payments.Pipeline;
    using Fifthweek.Payments.Snapshots;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class TrimSnapshotsAtEndExecutorTests
    {
        private TrimSnapshotsAtEndExecutor target;

        [TestInitialize]
        public void Initialize()
        {
            this.target = new TrimSnapshotsAtEndExecutor();
        }

        [TestMethod]
        public void WhenNoSnapshots_ItShouldReturnEmptyList()
        {
            Assert.AreEqual(0, this.target.Execute(DateTime.UtcNow, new List<ISnapshot>()).Count);
        }

        [TestMethod]
        public void WhenAllSnapshotsBeforeEndTime_ItShouldReturnOriginalList()
        {
            var now = DateTime.UtcNow;
            var snapshots = new List<ISnapshot>
            {
                SubscriberSnapshot.Default(now.AddDays(-3), UserId.Random()),
                SubscriberSnapshot.Default(now.AddDays(-2), UserId.Random()),
                SubscriberSnapshot.Default(now.AddDays(-1), UserId.Random()),
            };

            var result = this.target.Execute(now, snapshots);

            CollectionAssert.AreEqual(snapshots, result.ToList());
        }

        [TestMethod]
        public void WhenSnapshotsRequireTrim_ItShouldReturnTrimmedList()
        {
            var now = DateTime.UtcNow;
            var snapshots = new List<ISnapshot>
            {
                SubscriberSnapshot.Default(now.AddDays(-3), UserId.Random()),
                SubscriberSnapshot.Default(now.AddDays(-2), UserId.Random()),
                SubscriberSnapshot.Default(now.AddDays(-1), UserId.Random()),
                SubscriberSnapshot.Default(now, UserId.Random()),
            };

            var result = this.target.Execute(now, snapshots);

            CollectionAssert.AreEqual(snapshots.Take(3).ToList(), result.ToList());
        }
    }
}