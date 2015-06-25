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
                new MergedSnapshot(Now, input[1].CreatorChannels, input[1].CreatorFreeAccessUsers, input[1].SubscriberChannels, input[1].Subscriber, input[1].CalculatedAccountBalance),
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
                new MergedSnapshot(Now, input[0].CreatorChannels, input[0].CreatorFreeAccessUsers, input[0].SubscriberChannels, input[0].Subscriber, input[0].CalculatedAccountBalance)
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
                CreatorChannelsSnapshot.Default(Now, UserId.Random()),
                CreatorFreeAccessUsersSnapshot.Default(Now, UserId.Random()),
                SubscriberChannelsSnapshot.Default(Now, UserId.Random()),
                SubscriberSnapshot.Default(Now, UserId.Random()),
                CalculatedAccountBalanceSnapshot.DefaultFifthweekAccount(Now, UserId.Random()));
        }

        private IReadOnlyList<MergedSnapshot> Execute(DateTime startTimeInclusive, IEnumerable<MergedSnapshot> snapshots)
        {
            return this.target.Execute(startTimeInclusive, snapshots.ToList().AsReadOnly());
        }
    }
}