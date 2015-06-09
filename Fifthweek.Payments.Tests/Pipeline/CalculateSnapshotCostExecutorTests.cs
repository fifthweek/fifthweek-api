namespace Fifthweek.Payments.Tests.Pipeline
{
    using System;
    using System.Collections.Generic;

    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Payments.Pipeline;
    using Fifthweek.Payments.Snapshots;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class CalculateSnapshotCostExecutorTests
    {
        private static readonly DateTime Now = DateTime.UtcNow;

        private static readonly UserId CreatorId1 = new UserId(Guid.NewGuid());
        private static readonly UserId SubscriberId1 = new UserId(Guid.NewGuid());
        
        private static readonly ChannelId ChannelId1 = new ChannelId(Guid.NewGuid());
        private static readonly ChannelId ChannelId2 = new ChannelId(Guid.NewGuid());
        private static readonly ChannelId ChannelId3 = new ChannelId(Guid.NewGuid());

        private static readonly CreatorFreeAccessUsersSnapshot EmptyGuestList = CreatorFreeAccessUsersSnapshot.Default(Now, CreatorId1);

        private CalculateSnapshotCostExecutor target;

        [TestInitialize]
        public void Initialize()
        {
            this.target = new CalculateSnapshotCostExecutor();
        }

        [TestMethod]
        public void WhenNoSubscribedChannels_ItShouldReturnZero()
        {
            var result = this.target.Execute(
                new MergedSnapshot(
                    new CreatorChannelsSnapshot(
                        Now,
                        CreatorId1,
                        new List<CreatorChannelsSnapshotItem> { new CreatorChannelsSnapshotItem(ChannelId1, 100) }),
                    EmptyGuestList,
                    new SubscriberChannelsSnapshot(
                        Now,
                        SubscriberId1,
                        new List<SubscriberChannelsSnapshotItem>()),
                    new SubscriberSnapshot(
                        Now,
                        SubscriberId1,
                        null)));

            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void WhenOnGuestList_ItShouldReturnZero()
        {
            var result = this.target.Execute(
                new MergedSnapshot(
                    new CreatorChannelsSnapshot(
                        Now,
                        CreatorId1,
                        new List<CreatorChannelsSnapshotItem> { new CreatorChannelsSnapshotItem(ChannelId1, 100) }),
                    new CreatorFreeAccessUsersSnapshot(
                        Now,
                        new UserId(Guid.NewGuid()),
                        new List<string> { "a@a.com", "b@b.com" }),
                    new SubscriberChannelsSnapshot(
                        Now,
                        SubscriberId1,
                        new List<SubscriberChannelsSnapshotItem> { new SubscriberChannelsSnapshotItem(ChannelId1, 100, Now) }),
                    new SubscriberSnapshot(
                        Now,
                        SubscriberId1,
                        "b@b.com")));

            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void WhenNotOnGuestList_ItShouldReturnCorrectCost()
        {
            var result = this.target.Execute(
                new MergedSnapshot(
                    new CreatorChannelsSnapshot(
                        Now,
                        CreatorId1,
                        new List<CreatorChannelsSnapshotItem> { new CreatorChannelsSnapshotItem(ChannelId1, 100) }),
                    new CreatorFreeAccessUsersSnapshot(
                        Now,
                        new UserId(Guid.NewGuid()),
                        new List<string> { "aa@a.com", "bb@b.com" }),
                    new SubscriberChannelsSnapshot(
                        Now,
                        SubscriberId1,
                        new List<SubscriberChannelsSnapshotItem> { new SubscriberChannelsSnapshotItem(ChannelId1, 100, Now) }),
                    new SubscriberSnapshot(
                        Now,
                        SubscriberId1,
                        "b@b.com")));

            Assert.AreEqual(100, result);
        }

        [TestMethod]
        public void WhenSubscribedToChannel_ItShouldReturnCorrectCost()
        {
            var result = this.target.Execute(
                new MergedSnapshot(
                    new CreatorChannelsSnapshot(
                        Now,
                        CreatorId1,
                        new List<CreatorChannelsSnapshotItem> { new CreatorChannelsSnapshotItem(ChannelId1, 100) }),
                    EmptyGuestList,
                    new SubscriberChannelsSnapshot(
                        Now,
                        SubscriberId1,
                        new List<SubscriberChannelsSnapshotItem> { new SubscriberChannelsSnapshotItem(ChannelId1, 100, Now) }),
                    new SubscriberSnapshot(
                        Now,
                        SubscriberId1,
                        null)));

            Assert.AreEqual(100, result);
        }

        [TestMethod]
        public void WhenSubscribedToSubsetOfChannels_ItShouldReturnCorrectCost()
        {
            var result = this.target.Execute(
                new MergedSnapshot(
                    new CreatorChannelsSnapshot(
                        Now,
                        CreatorId1,
                        new List<CreatorChannelsSnapshotItem> 
                        { 
                            new CreatorChannelsSnapshotItem(ChannelId1, 100),
                            new CreatorChannelsSnapshotItem(ChannelId2, 50),
                            new CreatorChannelsSnapshotItem(ChannelId3, 10) 
                        }),
                    EmptyGuestList,
                    new SubscriberChannelsSnapshot(
                        Now,
                        SubscriberId1,
                        new List<SubscriberChannelsSnapshotItem> 
                        { 
                            new SubscriberChannelsSnapshotItem(ChannelId1, 100, Now),
                            new SubscriberChannelsSnapshotItem(ChannelId3, 10, Now) 
                        }),
                    new SubscriberSnapshot(
                        Now,
                        SubscriberId1,
                        null)));

            Assert.AreEqual(110, result);
        }

        [TestMethod]
        public void WhenAcceptedPriceIsHigher_ItShouldReturnCorrectCostAsAskingPrice()
        {
            var result = this.target.Execute(
                new MergedSnapshot(
                    new CreatorChannelsSnapshot(
                        Now,
                        CreatorId1,
                        new List<CreatorChannelsSnapshotItem> 
                        { 
                            new CreatorChannelsSnapshotItem(ChannelId1, 100),
                            new CreatorChannelsSnapshotItem(ChannelId2, 50),
                            new CreatorChannelsSnapshotItem(ChannelId3, 10) 
                        }),
                    EmptyGuestList,
                    new SubscriberChannelsSnapshot(
                        Now,
                        SubscriberId1,
                        new List<SubscriberChannelsSnapshotItem> 
                        { 
                            new SubscriberChannelsSnapshotItem(ChannelId1, 200, Now),
                            new SubscriberChannelsSnapshotItem(ChannelId3, 20, Now) 
                        }),
                    new SubscriberSnapshot(
                        Now,
                        SubscriberId1,
                        null)));

            Assert.AreEqual(110, result);
        }

        [TestMethod]
        public void WhenAcceptedPriceIsLower_ItShouldReturnCorrectCost()
        {
            var result = this.target.Execute(
                new MergedSnapshot(
                    new CreatorChannelsSnapshot(
                        Now,
                        CreatorId1,
                        new List<CreatorChannelsSnapshotItem> 
                        { 
                            new CreatorChannelsSnapshotItem(ChannelId1, 100),
                            new CreatorChannelsSnapshotItem(ChannelId2, 50),
                            new CreatorChannelsSnapshotItem(ChannelId3, 10) 
                        }),
                    EmptyGuestList,
                    new SubscriberChannelsSnapshot(
                        Now,
                        SubscriberId1,
                        new List<SubscriberChannelsSnapshotItem> 
                        { 
                            new SubscriberChannelsSnapshotItem(ChannelId1, 100, Now),
                            new SubscriberChannelsSnapshotItem(ChannelId3, 9, Now) 
                        }),
                    new SubscriberSnapshot(
                        Now,
                        SubscriberId1,
                        null)));

            Assert.AreEqual(100, result);
        }

        [TestMethod]
        public void WhenSubscribedToSupersetOfChannels_ItShouldReturnCorrectCost()
        {
            var result = this.target.Execute(
                new MergedSnapshot(
                    new CreatorChannelsSnapshot(
                        Now,
                        CreatorId1,
                        new List<CreatorChannelsSnapshotItem> 
                        { 
                            new CreatorChannelsSnapshotItem(ChannelId1, 100),
                            new CreatorChannelsSnapshotItem(ChannelId2, 50),
                            new CreatorChannelsSnapshotItem(ChannelId3, 10) 
                        }),
                    EmptyGuestList,
                    new SubscriberChannelsSnapshot(
                        Now,
                        SubscriberId1,
                        new List<SubscriberChannelsSnapshotItem> 
                        { 
                            new SubscriberChannelsSnapshotItem(new ChannelId(Guid.NewGuid()), 1000, Now),
                            new SubscriberChannelsSnapshotItem(ChannelId1, 100, Now),
                            new SubscriberChannelsSnapshotItem(new ChannelId(Guid.NewGuid()), 1000, Now),
                            new SubscriberChannelsSnapshotItem(ChannelId2, 50, Now),
                            new SubscriberChannelsSnapshotItem(new ChannelId(Guid.NewGuid()), 1000, Now) 
                        }),
                    new SubscriberSnapshot(
                        Now,
                        SubscriberId1,
                        null)));

            Assert.AreEqual(150, result);
        }
    }
}