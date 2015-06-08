namespace Fifthweek.Payments.Tests.Pipeline
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Fifthweek.Payments.Pipeline;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class TrimSnapshotsExecutorTests
    {
        private static readonly DateTime Now = DateTime.UtcNow;

        private TrimSnapshotsExecutor target;

        [TestInitialize]
        public void Initialize()
        {
            this.target = new TrimSnapshotsExecutor();
        }

        [TestMethod]
        public void WhenNoSnapshots_ItShouldReturnEmptyList()
        {
            var result = this.Execute(Now, Enumerable.Empty<MergedSnapshot>());
            CollectionAssert.AreEqual(new List<MergedSnapshot>(), result.ToList());
        }

        [TestMethod]
        public void WhenFirstSnapshotIsAtStartTime_ItShouldReturnIdenticalList()
        {
            var input = CreateSnapshots(Now, Now.AddDays(1));
            var result = this.Execute(Now, input);
            CollectionAssert.AreEqual(input, result.ToList());
        }


        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void WhenFirstSnapshotIsAfterStartTime_ItShouldThrowAnException()
        {
            var input = CreateSnapshots(Now.AddSeconds(1), Now.AddDays(1));
            this.Execute(Now, input);
        }

        [TestMethod]
        public void WhenFirstSnapshotsAreBeforeStartTimeButLaterSnapshotAtStartTime_ItShouldReturnSubset()
        {
            var input = CreateSnapshots(Now.AddDays(-1), Now.AddHours(-5), Now, Now.AddDays(1));
            var result = this.Execute(Now, input);
            CollectionAssert.AreEqual(input.Skip(2).ToList(), result.ToList());
        }

        [TestMethod]
        public void WhenFirstSnapshotsEitherSideOfStartTime_ItShouldReturnAdjustedSubset()
        {
            var input = CreateSnapshots(Now.AddDays(-1), Now.AddHours(-5), Now.AddDays(1));

            var expectedOutput = new[] 
            {
                new MergedSnapshot(Now, input[1].CreatorChannels, input[1].CreatorGuestList, input[1].SubscriberChannels, input[1].Subscriber),
                input[2]
            };

            var result = this.Execute(Now, input);

            CollectionAssert.AreEqual(expectedOutput, result.ToList());
        }

        [TestMethod]
        public void WhenOnlySnapshotIsBeforeStartTime_ItShouldReturnAdjustedSubset()
        {
            var input = CreateSnapshots(Now.AddDays(-1));

            var expectedOutput = new[] 
            {
                new MergedSnapshot(Now, input[0].CreatorChannels, input[0].CreatorGuestList, input[0].SubscriberChannels, input[0].Subscriber)
            };

            var result = this.Execute(Now, input);

            CollectionAssert.AreEqual(expectedOutput, result.ToList());
        }

        private static List<MergedSnapshot> CreateSnapshots(params DateTime[] timestamps)
        {
            var result = new List<MergedSnapshot>();
            foreach (var timestamp in timestamps)
            {
                result.Add(CreateSnapshot(timestamp));
            }
        
            return result;
        }

        private static MergedSnapshot CreateSnapshot(DateTime timestamp)
        {
            return new MergedSnapshot(
                timestamp,
                CreatorChannelSnapshot.Default(Now, Guid.NewGuid()),
                CreatorGuestListSnapshot.Default(Now, Guid.NewGuid()),
                SubscriberChannelSnapshot.Default(Now, Guid.NewGuid()),
                SubscriberSnapshot.Default(Now, Guid.NewGuid()));
        }

        private IReadOnlyList<MergedSnapshot> Execute(DateTime startTimeInclusive, IEnumerable<MergedSnapshot> snapshots)
        {
            return this.target.Execute(startTimeInclusive, snapshots.ToList().AsReadOnly());
        }
    }
}