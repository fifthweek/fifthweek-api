namespace Fifthweek.Payments.Tests.Pipeline
{
    using System;
    using System.Collections.Generic;

    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Payments.Pipeline;
    using Fifthweek.Payments.Services;
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
        private static readonly List<CreatorPost> validCreatorPosts = new List<CreatorPost>
        {
            new CreatorPost(ChannelId1, Now.AddDays(1)),
            new CreatorPost(ChannelId2, Now.AddDays(1)),
            new CreatorPost(ChannelId3, Now.AddDays(1)),
        };

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
                        null),
                    new CalculatedAccountBalanceSnapshot(Now, SubscriberId1, LedgerAccountType.FifthweekCredit, 10)),
                validCreatorPosts);

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
                        "b@b.com"),
                    new CalculatedAccountBalanceSnapshot(Now, SubscriberId1, LedgerAccountType.FifthweekCredit, 10)),
                validCreatorPosts);

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
                        "b@b.com"),
                    new CalculatedAccountBalanceSnapshot(Now, SubscriberId1, LedgerAccountType.FifthweekCredit, 10)),
                validCreatorPosts);

            Assert.AreEqual(100, result);
        }

        [TestMethod]
        public void WhenAccountBalanceIsZero_ItShouldReturnZero()
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
                        "b@b.com"),
                    new CalculatedAccountBalanceSnapshot(Now, SubscriberId1, LedgerAccountType.FifthweekCredit, 0)),
                validCreatorPosts);

            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void WhenAccountBalanceIsLessThanZero_ItShouldReturnZero()
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
                        "b@b.com"),
                    new CalculatedAccountBalanceSnapshot(Now, SubscriberId1, LedgerAccountType.FifthweekCredit, -5)),
                validCreatorPosts);

            Assert.AreEqual(0, result);
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
                        null),
                    new CalculatedAccountBalanceSnapshot(Now, SubscriberId1, LedgerAccountType.FifthweekCredit, 10)),
                validCreatorPosts);

            Assert.AreEqual(100, result);
        }

        [TestMethod]
        public void WhenSubscribedToChannel_AndNoCreatorPostInformation_ItShouldReturnCorrectCost()
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
                        null),
                    new CalculatedAccountBalanceSnapshot(Now, SubscriberId1, LedgerAccountType.FifthweekCredit, 10)),
                null);

            Assert.AreEqual(100, result);
        }

        [TestMethod]
        public void WhenSubscribedToChannel_AndCreatorDidNotPostToChannelInBillingWeek_ItShouldReturnCorrectCost()
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
                        null),
                    new CalculatedAccountBalanceSnapshot(Now, SubscriberId1, LedgerAccountType.FifthweekCredit, 10)),
                new List<CreatorPost>
                {
                    new CreatorPost(ChannelId1, Now.AddTicks(-1)),
                    new CreatorPost(ChannelId1, Now.AddDays(7)),
                });

            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void WhenSubscribedToChannel_AndCreatorDidPostToChannelInBillingWeek_ItShouldReturnCorrectCost1()
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
                        null),
                    new CalculatedAccountBalanceSnapshot(Now, SubscriberId1, LedgerAccountType.FifthweekCredit, 10)),
                new List<CreatorPost>
                {
                    new CreatorPost(ChannelId1, Now.AddTicks(-1)),
                    new CreatorPost(ChannelId1, Now),
                    new CreatorPost(ChannelId1, Now.AddDays(7)),
                });

            Assert.AreEqual(100, result);
        }

        [TestMethod]
        public void WhenSubscribedToChannel_AndCreatorDidPostToChannelInBillingWeek_ItShouldReturnCorrectCost2()
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
                        null),
                    new CalculatedAccountBalanceSnapshot(Now, SubscriberId1, LedgerAccountType.FifthweekCredit, 10)),
                new List<CreatorPost>
                {
                    new CreatorPost(ChannelId1, Now.AddTicks(-1)),
                    new CreatorPost(ChannelId1, Now.AddDays(7).AddTicks(-1)),
                    new CreatorPost(ChannelId1, Now.AddDays(7)),
                });

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
                        null),
                    new CalculatedAccountBalanceSnapshot(Now, SubscriberId1, LedgerAccountType.FifthweekCredit, 10)),
                validCreatorPosts);

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
                        null),
                    new CalculatedAccountBalanceSnapshot(Now, SubscriberId1, LedgerAccountType.FifthweekCredit, 10)),
                validCreatorPosts);

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
                        null),
                    new CalculatedAccountBalanceSnapshot(Now, SubscriberId1, LedgerAccountType.FifthweekCredit, 10)),
                validCreatorPosts);

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
                        null),
                    new CalculatedAccountBalanceSnapshot(Now, SubscriberId1, LedgerAccountType.FifthweekCredit, 10)),
                validCreatorPosts);

            Assert.AreEqual(150, result);
        }
    }
}