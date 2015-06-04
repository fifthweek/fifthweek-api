namespace Fifthweek.Payments.Tests.Pipeline
{
    using System;
    using System.Collections.Generic;

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

        private SnapshotCostCalculator target;

        [TestInitialize]
        public void Initialize()
        {
            this.target = new SnapshotCostCalculator();
        }

        [TestMethod]
        public void WhenNoSubscribedChannels_ItShouldReturnZero()
        {
            var result = this.target.Execute(
                new MergedSnapshot(
                    new CreatorSnapshot(
                        Now,
                        CreatorId1,
                        new List<CreatorChannelSnapshot> { new CreatorChannelSnapshot(ChannelId1, 100) }),
                    EmptyGuestList,
                    new SubscriberSnapshot(
                        Now,
                        SubscriberId1,
                        null,
                        new List<SubscriberChannelSnapshot>())));

            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void WhenOnGuestList_ItShouldReturnZero()
        {
            var result = this.target.Execute(
                new MergedSnapshot(
                    new CreatorSnapshot(
                        Now,
                        CreatorId1,
                        new List<CreatorChannelSnapshot> { new CreatorChannelSnapshot(ChannelId1, 100) }),
                    new CreatorGuestListSnapshot(
                        Now,
                        Guid.NewGuid(),
                        new List<string> { "a@a.com", "b@b.com" }),
                    new SubscriberSnapshot(
                        Now,
                        SubscriberId1,
                        "b@b.com",
                        new List<SubscriberChannelSnapshot> { new SubscriberChannelSnapshot(ChannelId1, 100, Now) })));

            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void WhenNotOnGuestList_ItShouldReturnCorrectCost()
        {
            var result = this.target.Execute(
                new MergedSnapshot(
                    new CreatorSnapshot(
                        Now,
                        CreatorId1,
                        new List<CreatorChannelSnapshot> { new CreatorChannelSnapshot(ChannelId1, 100) }),
                    new CreatorGuestListSnapshot(
                        Now,
                        Guid.NewGuid(),
                        new List<string> { "aa@a.com", "bb@b.com" }),
                    new SubscriberSnapshot(
                        Now,
                        SubscriberId1,
                        "b@b.com",
                        new List<SubscriberChannelSnapshot> { new SubscriberChannelSnapshot(ChannelId1, 100, Now) })));

            Assert.AreEqual(100, result);
        }

        [TestMethod]
        public void WhenSubscribedToChannel_ItShouldReturnCorrectCost()
        {
            var result = this.target.Execute(
                new MergedSnapshot(
                    new CreatorSnapshot(
                        Now,
                        CreatorId1,
                        new List<CreatorChannelSnapshot> { new CreatorChannelSnapshot(ChannelId1, 100) }),
                    EmptyGuestList,
                    new SubscriberSnapshot(
                        Now,
                        SubscriberId1,
                        null,
                        new List<SubscriberChannelSnapshot> { new SubscriberChannelSnapshot(ChannelId1, 100, Now) })));

            Assert.AreEqual(100, result);
        }

        [TestMethod]
        public void WhenSubscribedToSubsetOfChannels_ItShouldReturnCorrectCost()
        {
            var result = this.target.Execute(
                new MergedSnapshot(
                    new CreatorSnapshot(
                        Now,
                        CreatorId1,
                        new List<CreatorChannelSnapshot> 
                        { 
                            new CreatorChannelSnapshot(ChannelId1, 100),
                            new CreatorChannelSnapshot(ChannelId2, 50),
                            new CreatorChannelSnapshot(ChannelId3, 10) 
                        }),
                    EmptyGuestList,
                    new SubscriberSnapshot(
                        Now,
                        SubscriberId1,
                        null,
                        new List<SubscriberChannelSnapshot> 
                        { 
                            new SubscriberChannelSnapshot(ChannelId1, 100, Now),
                            new SubscriberChannelSnapshot(ChannelId3, 10, Now) 
                        })));

            Assert.AreEqual(110, result);
        }

        [TestMethod]
        public void WhenAcceptedPriceIsHigher_ItShouldReturnCorrectCostAsAskingPrice()
        {
            var result = this.target.Execute(
                new MergedSnapshot(
                    new CreatorSnapshot(
                        Now,
                        CreatorId1,
                        new List<CreatorChannelSnapshot> 
                        { 
                            new CreatorChannelSnapshot(ChannelId1, 100),
                            new CreatorChannelSnapshot(ChannelId2, 50),
                            new CreatorChannelSnapshot(ChannelId3, 10) 
                        }),
                    EmptyGuestList,
                    new SubscriberSnapshot(
                        Now,
                        SubscriberId1,
                        null,
                        new List<SubscriberChannelSnapshot> 
                        { 
                            new SubscriberChannelSnapshot(ChannelId1, 200, Now),
                            new SubscriberChannelSnapshot(ChannelId3, 20, Now) 
                        })));

            Assert.AreEqual(110, result);
        }

        [TestMethod]
        public void WhenAcceptedPriceIsLower_ItShouldReturnCorrectCost()
        {
            var result = this.target.Execute(
                new MergedSnapshot(
                    new CreatorSnapshot(
                        Now,
                        CreatorId1,
                        new List<CreatorChannelSnapshot> 
                        { 
                            new CreatorChannelSnapshot(ChannelId1, 100),
                            new CreatorChannelSnapshot(ChannelId2, 50),
                            new CreatorChannelSnapshot(ChannelId3, 10) 
                        }),
                    EmptyGuestList,
                    new SubscriberSnapshot(
                        Now,
                        SubscriberId1,
                        null,
                        new List<SubscriberChannelSnapshot> 
                        { 
                            new SubscriberChannelSnapshot(ChannelId1, 100, Now),
                            new SubscriberChannelSnapshot(ChannelId3, 9, Now) 
                        })));

            Assert.AreEqual(100, result);
        }

        [TestMethod]
        public void WhenSubscribedToSupersetOfChannels_ItShouldReturnCorrectCost()
        {
            var result = this.target.Execute(
                new MergedSnapshot(
                    new CreatorSnapshot(
                        Now,
                        CreatorId1,
                        new List<CreatorChannelSnapshot> 
                        { 
                            new CreatorChannelSnapshot(ChannelId1, 100),
                            new CreatorChannelSnapshot(ChannelId2, 50),
                            new CreatorChannelSnapshot(ChannelId3, 10) 
                        }),
                    EmptyGuestList,
                    new SubscriberSnapshot(
                        Now,
                        SubscriberId1,
                        null,
                        new List<SubscriberChannelSnapshot> 
                        { 
                            new SubscriberChannelSnapshot(Guid.NewGuid(), 1000, Now),
                            new SubscriberChannelSnapshot(ChannelId1, 100, Now),
                            new SubscriberChannelSnapshot(Guid.NewGuid(), 1000, Now),
                            new SubscriberChannelSnapshot(ChannelId2, 50, Now),
                            new SubscriberChannelSnapshot(Guid.NewGuid(), 1000, Now) 
                        })));

            Assert.AreEqual(150, result);
        }
    }
}