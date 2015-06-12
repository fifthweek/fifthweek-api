namespace Fifthweek.Payments.Tests.SnapshotCreation
{
    using System;

    using Fifthweek.Payments.SnapshotCreation;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class SnapshotTimestampCreatorTests
    {
        private SnapshotTimestampCreator target;

        [TestInitialize]
        public void Initialize()
        {
            this.target = new SnapshotTimestampCreator();
        }

        [TestMethod]
        public void ItShouldGenerateUtcTimestamps()
        {
            Assert.AreEqual(DateTimeKind.Utc, this.target.Create().Kind);
        }
    }
}