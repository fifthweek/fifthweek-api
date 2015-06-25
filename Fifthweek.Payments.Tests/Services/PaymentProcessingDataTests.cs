namespace Fifthweek.Payments.Tests.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Payments.Services;
    using Fifthweek.Payments.Snapshots;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class PaymentProcessingDataTests
    {
        private static readonly UserId SubscriberId = UserId.Random();
        private static readonly UserId CreatorId = UserId.Random();
        private static readonly DateTime Now = DateTime.UtcNow;

        [TestMethod]
        public void ItShouldReturnOrderedSnapshots()
        {
            var data = this.CreateData();

            var orderedSnapshots = data.GetOrderedSnapshots();

            CollectionAssert.AreEqual(
                new List<ISnapshot>
                {
                    data.CreatorChannelsSnapshots[0],
                    data.SubscriberSnapshots[1],
                    data.CreatorFreeAccessUsersSnapshots[0],
                    data.SubscriberChannelsSnapshots[0],
                    data.SubscriberSnapshots[0],
                    data.CalculatedAccountBalanceSnapshots[0],
                },
                orderedSnapshots.ToList());
        }

        [TestMethod]
        public void ItShouldCacheOrderedSnapshots()
        {
            var data = this.CreateData();

            var orderedSnapshots = data.GetOrderedSnapshots();
            var orderedSnapshots2 = data.GetOrderedSnapshots();
            Assert.AreSame(orderedSnapshots, orderedSnapshots2);
        }

        private PaymentProcessingData CreateData()
        {
            return new PaymentProcessingData(
                SubscriberId,
                CreatorId,
                Now,
                Now.AddMonths(1),
                new List<SubscriberChannelsSnapshot> 
                {
                    new SubscriberChannelsSnapshot(
                        Now.AddDays(5), 
                        UserId.Random(),
                        new List<SubscriberChannelsSnapshotItem>
                        {
                            new SubscriberChannelsSnapshotItem(ChannelId.Random(), 100, Now.AddDays(4)),
                            new SubscriberChannelsSnapshotItem(ChannelId.Random(), 110, Now.AddDays(8)),
                        }),
                },
                            new List<SubscriberSnapshot>
                {
                    new SubscriberSnapshot(Now.AddDays(7), UserId.Random(), "a@b.com"),
                    new SubscriberSnapshot(Now.AddDays(2), UserId.Random(), "x@y.com"),
                },
                new List<CalculatedAccountBalanceSnapshot>
                {
                    new CalculatedAccountBalanceSnapshot(
                        Now.AddDays(9),
                        UserId.Random(),
                        LedgerAccountType.Fifthweek,
                        10.1m),
                },
                new List<CreatorChannelsSnapshot>
                {
                    new CreatorChannelsSnapshot(
                        Now.AddDays(1), 
                        UserId.Random(),
                        new List<CreatorChannelsSnapshotItem>
                        {
                            new CreatorChannelsSnapshotItem(ChannelId.Random(), 200),
                            new CreatorChannelsSnapshotItem(ChannelId.Random(), 300),
                        }),
                },
                            new List<CreatorFreeAccessUsersSnapshot>
                {
                    new CreatorFreeAccessUsersSnapshot(
                        Now.AddDays(3), 
                        UserId.Random(), 
                        new List<string> { "b@c.com", "c@d.com" }),
                },
                            new List<CreatorPost>
                {
                    new CreatorPost(ChannelId.Random(), Now.AddDays(2)),
                    new CreatorPost(ChannelId.Random(), Now.AddDays(6)),
                },
                new CreatorPercentageOverrideData(0.9m, null));
        }

    }
}