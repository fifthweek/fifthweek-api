namespace Fifthweek.Payments.Tests.Pipeline
{
    using System;
    using System.Collections.Generic;

    using Fifthweek.Payments.Pipeline;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class CalculateSnapshotCostExecutorTests
    {
        private static readonly DateTime Now = DateTime.UtcNow;

        private static readonly Guid CreatorId1 = Guid.NewGuid();
        private static readonly Guid SubscriberId1 = Guid.NewGuid();
        
        private static readonly Guid ChannelId1 = Guid.NewGuid();
        private static readonly Guid ChannelId2 = Guid.NewGuid();
        private static readonly Guid ChannelId3 = Guid.NewGuid();

        private static readonly CreatorGuestListSnapshot EmptyGuestList = CreatorGuestListSnapshot.Default(Now, CreatorId1);

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
                    new CreatorChannelSnapshot(
                        Now,
                        CreatorId1,
                        new List<CreatorChannelSnapshotItem> { new CreatorChannelSnapshotItem(ChannelId1, 100) }),
                    EmptyGuestList,
                    new SubscriberChannelSnapshot(
                        Now,
                        SubscriberId1,
                        new List<SubscriberChannelSnapshotItem>()),
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
                    new CreatorChannelSnapshot(
                        Now,
                        CreatorId1,
                        new List<CreatorChannelSnapshotItem> { new CreatorChannelSnapshotItem(ChannelId1, 100) }),
                    new CreatorGuestListSnapshot(
                        Now,
                        Guid.NewGuid(),
                        new List<string> { "a@a.com", "b@b.com" }),
                    new SubscriberChannelSnapshot(
                        Now,
                        SubscriberId1,
                        new List<SubscriberChannelSnapshotItem> { new SubscriberChannelSnapshotItem(ChannelId1, 100, Now) }),
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
                    new CreatorChannelSnapshot(
                        Now,
                        CreatorId1,
                        new List<CreatorChannelSnapshotItem> { new CreatorChannelSnapshotItem(ChannelId1, 100) }),
                    new CreatorGuestListSnapshot(
                        Now,
                        Guid.NewGuid(),
                        new List<string> { "aa@a.com", "bb@b.com" }),
                    new SubscriberChannelSnapshot(
                        Now,
                        SubscriberId1,
                        new List<SubscriberChannelSnapshotItem> { new SubscriberChannelSnapshotItem(ChannelId1, 100, Now) }),
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
                    new CreatorChannelSnapshot(
                        Now,
                        CreatorId1,
                        new List<CreatorChannelSnapshotItem> { new CreatorChannelSnapshotItem(ChannelId1, 100) }),
                    EmptyGuestList,
                    new SubscriberChannelSnapshot(
                        Now,
                        SubscriberId1,
                        new List<SubscriberChannelSnapshotItem> { new SubscriberChannelSnapshotItem(ChannelId1, 100, Now) }),
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
                    new CreatorChannelSnapshot(
                        Now,
                        CreatorId1,
                        new List<CreatorChannelSnapshotItem> 
                        { 
                            new CreatorChannelSnapshotItem(ChannelId1, 100),
                            new CreatorChannelSnapshotItem(ChannelId2, 50),
                            new CreatorChannelSnapshotItem(ChannelId3, 10) 
                        }),
                    EmptyGuestList,
                    new SubscriberChannelSnapshot(
                        Now,
                        SubscriberId1,
                        new List<SubscriberChannelSnapshotItem> 
                        { 
                            new SubscriberChannelSnapshotItem(ChannelId1, 100, Now),
                            new SubscriberChannelSnapshotItem(ChannelId3, 10, Now) 
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
                    new CreatorChannelSnapshot(
                        Now,
                        CreatorId1,
                        new List<CreatorChannelSnapshotItem> 
                        { 
                            new CreatorChannelSnapshotItem(ChannelId1, 100),
                            new CreatorChannelSnapshotItem(ChannelId2, 50),
                            new CreatorChannelSnapshotItem(ChannelId3, 10) 
                        }),
                    EmptyGuestList,
                    new SubscriberChannelSnapshot(
                        Now,
                        SubscriberId1,
                        new List<SubscriberChannelSnapshotItem> 
                        { 
                            new SubscriberChannelSnapshotItem(ChannelId1, 200, Now),
                            new SubscriberChannelSnapshotItem(ChannelId3, 20, Now) 
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
                    new CreatorChannelSnapshot(
                        Now,
                        CreatorId1,
                        new List<CreatorChannelSnapshotItem> 
                        { 
                            new CreatorChannelSnapshotItem(ChannelId1, 100),
                            new CreatorChannelSnapshotItem(ChannelId2, 50),
                            new CreatorChannelSnapshotItem(ChannelId3, 10) 
                        }),
                    EmptyGuestList,
                    new SubscriberChannelSnapshot(
                        Now,
                        SubscriberId1,
                        new List<SubscriberChannelSnapshotItem> 
                        { 
                            new SubscriberChannelSnapshotItem(ChannelId1, 100, Now),
                            new SubscriberChannelSnapshotItem(ChannelId3, 9, Now) 
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
                    new CreatorChannelSnapshot(
                        Now,
                        CreatorId1,
                        new List<CreatorChannelSnapshotItem> 
                        { 
                            new CreatorChannelSnapshotItem(ChannelId1, 100),
                            new CreatorChannelSnapshotItem(ChannelId2, 50),
                            new CreatorChannelSnapshotItem(ChannelId3, 10) 
                        }),
                    EmptyGuestList,
                    new SubscriberChannelSnapshot(
                        Now,
                        SubscriberId1,
                        new List<SubscriberChannelSnapshotItem> 
                        { 
                            new SubscriberChannelSnapshotItem(Guid.NewGuid(), 1000, Now),
                            new SubscriberChannelSnapshotItem(ChannelId1, 100, Now),
                            new SubscriberChannelSnapshotItem(Guid.NewGuid(), 1000, Now),
                            new SubscriberChannelSnapshotItem(ChannelId2, 50, Now),
                            new SubscriberChannelSnapshotItem(Guid.NewGuid(), 1000, Now) 
                        }),
                    new SubscriberSnapshot(
                        Now,
                        SubscriberId1,
                        null)));

            Assert.AreEqual(150, result);
        }
    }
}