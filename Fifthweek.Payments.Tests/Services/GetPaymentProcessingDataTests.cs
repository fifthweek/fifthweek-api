namespace Fifthweek.Payments.Tests.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Payments.Services;
    using Fifthweek.Payments.Snapshots;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class GetPaymentProcessingDataTests
    {
        private static readonly UserId SubscriberId = UserId.Random();
        private static readonly UserId CreatorId = UserId.Random();
        private static readonly UserId PreviousSubscriberId = UserId.Random();
        private static readonly UserId PreviousCreatorId = UserId.Random();

        private static readonly ChannelId ChannelId1 = ChannelId.Random();
        private static readonly ChannelId ChannelId2 = ChannelId.Random();
        private static readonly ChannelId ChannelId3 = ChannelId.Random();

        private static readonly DateTime StartTimeInclusive = DateTime.UtcNow;
        private static readonly DateTime EndTimeExclusive = StartTimeInclusive.AddDays(7);

        private static readonly List<SubscriberChannelsSnapshot> SubscriberChannelsSnapshots = new List<SubscriberChannelsSnapshot>();
        private static readonly List<SubscriberSnapshot> SubscriberSnapshots = new List<SubscriberSnapshot>();
        private static readonly List<CalculatedAccountBalanceSnapshot> CalculatedAccountBalanceSnapshots = new List<CalculatedAccountBalanceSnapshot>();

        private static readonly List<CreatorChannelsSnapshot> CreatorChannelsSnapshots = new List<CreatorChannelsSnapshot>();
        private static readonly List<CreatorChannelsSnapshot> CreatorChannelsSnapshotsWithChannels = new List<CreatorChannelsSnapshot>
        {
            new CreatorChannelsSnapshot(
                DateTime.UtcNow, 
                CreatorId, 
                new List<CreatorChannelsSnapshotItem>
                {
                    new CreatorChannelsSnapshotItem(ChannelId1, 10),
                }),
            new CreatorChannelsSnapshot(
                DateTime.UtcNow, 
                CreatorId, 
                new List<CreatorChannelsSnapshotItem>
                {
                    new CreatorChannelsSnapshotItem(ChannelId2, 10),
                    new CreatorChannelsSnapshotItem(ChannelId3, 10),
                }),
        };
        
        private static readonly List<CreatorFreeAccessUsersSnapshot> CreatorFreeAccessUsersSnapshots = new List<CreatorFreeAccessUsersSnapshot>();

        private static readonly List<CreatorPost> CreatorPosts = new List<CreatorPost>();
        private static readonly CreatorPercentageOverrideData CreatorPercentageOverride = new CreatorPercentageOverrideData(10.0m, DateTime.UtcNow);

        private Mock<IGetCreatorChannelsSnapshotsDbStatement> getCreatorChannelsSnapshots;
        private Mock<IGetCreatorFreeAccessUsersSnapshotsDbStatement> getCreatorFreeAccessUsersSnapshots;
        private Mock<IGetCreatorPostsDbStatement> getCreatorPosts;
        private Mock<IGetSubscriberChannelsSnapshotsDbStatement> getSubscriberChannelsSnapshots;
        private Mock<IGetSubscriberSnapshotsDbStatement> getSubscriberSnapshots;
        private Mock<IGetCalculatedAccountBalancesDbStatement> getCalculatedAccountBalances;
        private Mock<IGetCreatorPercentageOverrideDbStatement> getCreatorPercentageOverride;

        private GetPaymentProcessingData target;

        [TestInitialize]
        public void Initialize()
        {
            this.getCreatorChannelsSnapshots = new Mock<IGetCreatorChannelsSnapshotsDbStatement>(MockBehavior.Strict);
            this.getCreatorFreeAccessUsersSnapshots = new Mock<IGetCreatorFreeAccessUsersSnapshotsDbStatement>(MockBehavior.Strict);
            this.getCreatorPosts = new Mock<IGetCreatorPostsDbStatement>(MockBehavior.Strict);
            this.getSubscriberChannelsSnapshots = new Mock<IGetSubscriberChannelsSnapshotsDbStatement>(MockBehavior.Strict);
            this.getSubscriberSnapshots = new Mock<IGetSubscriberSnapshotsDbStatement>(MockBehavior.Strict);
            this.getCalculatedAccountBalances = new Mock<IGetCalculatedAccountBalancesDbStatement>(MockBehavior.Strict);
            this.getCreatorPercentageOverride = new Mock<IGetCreatorPercentageOverrideDbStatement>(MockBehavior.Strict);

            this.target = new GetPaymentProcessingData(
                this.getCreatorChannelsSnapshots.Object,
                this.getCreatorFreeAccessUsersSnapshots.Object,
                this.getCreatorPosts.Object,
                this.getSubscriberChannelsSnapshots.Object,
                this.getSubscriberSnapshots.Object,
                this.getCalculatedAccountBalances.Object,
                this.getCreatorPercentageOverride.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenSubscriberIdIsNull_ItShouldThrowAnException()
        {
            await this.target.ExecuteAsync(null, CreatorId, StartTimeInclusive, EndTimeExclusive);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenCreatorIdIsNull_ItShouldThrowAnException()
        {
            await this.target.ExecuteAsync(SubscriberId, null, StartTimeInclusive, EndTimeExclusive);
        }

        [TestMethod]
        public async Task WhenCalledForFirstTime_ItShouldRequestAllData()
        {
            this.SetupMocks(SubscriberId, CreatorId, StartTimeInclusive, EndTimeExclusive);

            var result = await this.target.ExecuteAsync(SubscriberId, CreatorId, StartTimeInclusive, EndTimeExclusive);

            this.VerifySubscriberMocks(SubscriberId, StartTimeInclusive, EndTimeExclusive, Times.Once());
            this.VerifyCreatorMocks(CreatorId, StartTimeInclusive, EndTimeExclusive, Times.Once());

            this.VerifyResult(result, SubscriberId, CreatorId, StartTimeInclusive, EndTimeExclusive);
        }

        [TestMethod]
        public async Task WhenCalledNoChannels_ItShouldNotRequestCreatorPosts()
        {
            this.SetupMocks(SubscriberId, CreatorId, StartTimeInclusive, EndTimeExclusive, false);

            var result = await this.target.ExecuteAsync(SubscriberId, CreatorId, StartTimeInclusive, EndTimeExclusive);

            this.VerifySubscriberMocks(SubscriberId, StartTimeInclusive, EndTimeExclusive, Times.Once());
            this.VerifyCreatorMocks(CreatorId, StartTimeInclusive, EndTimeExclusive, Times.Once(), false);

            this.VerifyResult(result, SubscriberId, CreatorId, StartTimeInclusive, EndTimeExclusive, false);
        }

        [TestMethod]
        public async Task WhenCalledWithDifferentCreatorAndSubscriber_ItShouldRequestAllData()
        {
            this.SetupMocks(PreviousSubscriberId, PreviousCreatorId, StartTimeInclusive, EndTimeExclusive, false);
            await this.target.ExecuteAsync(PreviousSubscriberId, PreviousCreatorId, StartTimeInclusive, EndTimeExclusive);

            this.VerifySubscriberMocks(PreviousSubscriberId, StartTimeInclusive, EndTimeExclusive, Times.Once());
            this.VerifyCreatorMocks(PreviousCreatorId, StartTimeInclusive, EndTimeExclusive, Times.Once(), false);

            this.SetupMocks(SubscriberId, CreatorId, StartTimeInclusive, EndTimeExclusive);
            var result = await this.target.ExecuteAsync(SubscriberId, CreatorId, StartTimeInclusive, EndTimeExclusive);

            this.VerifySubscriberMocks(PreviousSubscriberId, StartTimeInclusive, EndTimeExclusive, Times.Once());
            this.VerifyCreatorMocks(PreviousCreatorId, StartTimeInclusive, EndTimeExclusive, Times.Once(), false);

            this.VerifySubscriberMocks(SubscriberId, StartTimeInclusive, EndTimeExclusive, Times.Once());
            this.VerifyCreatorMocks(CreatorId, StartTimeInclusive, EndTimeExclusive, Times.Once());

            this.VerifyResult(result, SubscriberId, CreatorId, StartTimeInclusive, EndTimeExclusive);
        }

        [TestMethod]
        public async Task WhenCalledWithDifferentCreator_ItShouldRequestNewCreatorData()
        {
            this.SetupMocks(PreviousSubscriberId, PreviousCreatorId, StartTimeInclusive, EndTimeExclusive, false);
            await this.target.ExecuteAsync(PreviousSubscriberId, PreviousCreatorId, StartTimeInclusive, EndTimeExclusive);

            this.VerifySubscriberMocks(PreviousSubscriberId, StartTimeInclusive, EndTimeExclusive, Times.Once());
            this.VerifyCreatorMocks(PreviousCreatorId, StartTimeInclusive, EndTimeExclusive, Times.Once(), false);

            this.SetupMocks(null, CreatorId, StartTimeInclusive, EndTimeExclusive);
            var result = await this.target.ExecuteAsync(PreviousSubscriberId, CreatorId, StartTimeInclusive, EndTimeExclusive);

            this.VerifySubscriberMocks(PreviousSubscriberId, StartTimeInclusive, EndTimeExclusive, Times.Once());
            this.VerifyCreatorMocks(PreviousCreatorId, StartTimeInclusive, EndTimeExclusive, Times.Once(), false);

            this.VerifySubscriberMocks(SubscriberId, StartTimeInclusive, EndTimeExclusive, Times.Never());
            this.VerifyCreatorMocks(CreatorId, StartTimeInclusive, EndTimeExclusive, Times.Once());

            this.VerifyResult(result, PreviousSubscriberId, CreatorId, StartTimeInclusive, EndTimeExclusive);
        }

        [TestMethod]
        public async Task WhenCalledWithDifferentSubscriber_ItShouldRequestNewSubscriberData()
        {
            this.SetupMocks(PreviousSubscriberId, PreviousCreatorId, StartTimeInclusive, EndTimeExclusive, false);
            await this.target.ExecuteAsync(PreviousSubscriberId, PreviousCreatorId, StartTimeInclusive, EndTimeExclusive);

            this.VerifySubscriberMocks(PreviousSubscriberId, StartTimeInclusive, EndTimeExclusive, Times.Once());
            this.VerifyCreatorMocks(PreviousCreatorId, StartTimeInclusive, EndTimeExclusive, Times.Once(), false);

            this.SetupMocks(SubscriberId, null, StartTimeInclusive, EndTimeExclusive);
            var result = await this.target.ExecuteAsync(SubscriberId, PreviousCreatorId, StartTimeInclusive, EndTimeExclusive);

            this.VerifySubscriberMocks(PreviousSubscriberId, StartTimeInclusive, EndTimeExclusive, Times.Once());
            this.VerifyCreatorMocks(PreviousCreatorId, StartTimeInclusive, EndTimeExclusive, Times.Once(), false);

            this.VerifySubscriberMocks(SubscriberId, StartTimeInclusive, EndTimeExclusive, Times.Once());
            this.VerifyCreatorMocks(CreatorId, StartTimeInclusive, EndTimeExclusive, Times.Never());

            this.VerifyResult(result, SubscriberId, PreviousCreatorId, StartTimeInclusive, EndTimeExclusive, false);
        }

        [TestMethod]
        public async Task WhenCalledWithSubsetOfTime_ItShouldReturnCachedData()
        {
            this.SetupMocks(SubscriberId, CreatorId, StartTimeInclusive, EndTimeExclusive);
            await this.target.ExecuteAsync(SubscriberId, CreatorId, StartTimeInclusive, EndTimeExclusive);

            this.VerifySubscriberMocks(SubscriberId, StartTimeInclusive, EndTimeExclusive, Times.Once());
            this.VerifyCreatorMocks(CreatorId, StartTimeInclusive, EndTimeExclusive, Times.Once());

            var result = await this.target.ExecuteAsync(SubscriberId, CreatorId, StartTimeInclusive.AddDays(1), EndTimeExclusive.AddDays(-1));

            this.VerifySubscriberMocks(SubscriberId, StartTimeInclusive, EndTimeExclusive, Times.Once());
            this.VerifyCreatorMocks(CreatorId, StartTimeInclusive, EndTimeExclusive, Times.Once());

            this.VerifyResult(result, SubscriberId, CreatorId, StartTimeInclusive.AddDays(1), EndTimeExclusive.AddDays(-1));
        }

        [TestMethod]
        public async Task WhenCalledWithSubsetOfTime_ItShouldCacheOriginalData()
        {
            this.SetupMocks(SubscriberId, CreatorId, StartTimeInclusive, EndTimeExclusive);
            await this.target.ExecuteAsync(SubscriberId, CreatorId, StartTimeInclusive, EndTimeExclusive);

            this.VerifySubscriberMocks(SubscriberId, StartTimeInclusive, EndTimeExclusive, Times.Once());
            this.VerifyCreatorMocks(CreatorId, StartTimeInclusive, EndTimeExclusive, Times.Once());

            await this.target.ExecuteAsync(SubscriberId, CreatorId, StartTimeInclusive.AddDays(1), EndTimeExclusive.AddDays(-1));

            this.VerifySubscriberMocks(SubscriberId, StartTimeInclusive, EndTimeExclusive, Times.Once());
            this.VerifyCreatorMocks(CreatorId, StartTimeInclusive, EndTimeExclusive, Times.Once());

            await this.target.ExecuteAsync(SubscriberId, CreatorId, StartTimeInclusive, EndTimeExclusive);

            this.VerifySubscriberMocks(SubscriberId, StartTimeInclusive, EndTimeExclusive, Times.Once());
            this.VerifyCreatorMocks(CreatorId, StartTimeInclusive, EndTimeExclusive, Times.Once());
        }

        [TestMethod]
        public async Task WhenCalledWithSupersetOfTime_ItShouldRequestAllData()
        {
            this.SetupMocks(SubscriberId, CreatorId, StartTimeInclusive, EndTimeExclusive);
            await this.target.ExecuteAsync(SubscriberId, CreatorId, StartTimeInclusive, EndTimeExclusive);

            this.VerifySubscriberMocks(SubscriberId, StartTimeInclusive, EndTimeExclusive, Times.Once());
            this.VerifyCreatorMocks(CreatorId, StartTimeInclusive, EndTimeExclusive, Times.Once());

            this.SetupMocks(SubscriberId, CreatorId, StartTimeInclusive.AddDays(-1), EndTimeExclusive);
            var result = await this.target.ExecuteAsync(SubscriberId, CreatorId, StartTimeInclusive.AddDays(-1), EndTimeExclusive);

            this.VerifySubscriberMocks(SubscriberId, StartTimeInclusive, EndTimeExclusive, Times.Once());
            this.VerifyCreatorMocks(CreatorId, StartTimeInclusive, EndTimeExclusive, Times.Once());

            this.VerifySubscriberMocks(SubscriberId, StartTimeInclusive.AddDays(-1), EndTimeExclusive, Times.Once());
            this.VerifyCreatorMocks(CreatorId, StartTimeInclusive.AddDays(-1), EndTimeExclusive, Times.Once());

            this.VerifyResult(result, SubscriberId, CreatorId, StartTimeInclusive.AddDays(-1), EndTimeExclusive);
        }

        [TestMethod]
        public async Task ItShouldCacheLastDataIfNewDataFetched()
        {
            this.SetupMocks(PreviousSubscriberId, PreviousCreatorId, StartTimeInclusive, EndTimeExclusive, false);
            await this.target.ExecuteAsync(PreviousSubscriberId, PreviousCreatorId, StartTimeInclusive, EndTimeExclusive);

            this.VerifySubscriberMocks(PreviousSubscriberId, StartTimeInclusive, EndTimeExclusive, Times.Once());
            this.VerifyCreatorMocks(PreviousCreatorId, StartTimeInclusive, EndTimeExclusive, Times.Once(), false);

            this.SetupMocks(SubscriberId, CreatorId, StartTimeInclusive, EndTimeExclusive);
            await this.target.ExecuteAsync(SubscriberId, CreatorId, StartTimeInclusive, EndTimeExclusive);

            this.VerifySubscriberMocks(PreviousSubscriberId, StartTimeInclusive, EndTimeExclusive, Times.Once());
            this.VerifyCreatorMocks(PreviousCreatorId, StartTimeInclusive, EndTimeExclusive, Times.Once(), false);

            this.VerifySubscriberMocks(SubscriberId, StartTimeInclusive, EndTimeExclusive, Times.Once());
            this.VerifyCreatorMocks(CreatorId, StartTimeInclusive, EndTimeExclusive, Times.Once());

            await this.target.ExecuteAsync(SubscriberId, CreatorId, StartTimeInclusive, EndTimeExclusive);

            this.VerifySubscriberMocks(PreviousSubscriberId, StartTimeInclusive, EndTimeExclusive, Times.Once());
            this.VerifyCreatorMocks(PreviousCreatorId, StartTimeInclusive, EndTimeExclusive, Times.Once(), false);

            this.VerifySubscriberMocks(SubscriberId, StartTimeInclusive, EndTimeExclusive, Times.Once());
            this.VerifyCreatorMocks(CreatorId, StartTimeInclusive, EndTimeExclusive, Times.Once());

            await this.target.ExecuteAsync(PreviousSubscriberId, PreviousCreatorId, StartTimeInclusive, EndTimeExclusive);

            this.VerifySubscriberMocks(PreviousSubscriberId, StartTimeInclusive, EndTimeExclusive, Times.Exactly(2));
            this.VerifyCreatorMocks(PreviousCreatorId, StartTimeInclusive, EndTimeExclusive, Times.Exactly(2), false);

            this.VerifySubscriberMocks(SubscriberId, StartTimeInclusive, EndTimeExclusive, Times.Once());
            this.VerifyCreatorMocks(CreatorId, StartTimeInclusive, EndTimeExclusive, Times.Once());
        }

        private void SetupMocks(UserId subscriberId, UserId creatorId, DateTime startTime, DateTime endTime, bool withChannels = true)
        {
            if (subscriberId != null)
            {
                this.getSubscriberChannelsSnapshots.Setup(v => v.ExecuteAsync(subscriberId, startTime, endTime))
                    .ReturnsAsync(SubscriberChannelsSnapshots);

                this.getSubscriberSnapshots.Setup(v => v.ExecuteAsync(subscriberId, startTime, endTime))
                    .ReturnsAsync(SubscriberSnapshots);

                this.getCalculatedAccountBalances.Setup(v => v.ExecuteAsync(subscriberId, LedgerAccountType.Fifthweek, startTime, endTime))
                    .ReturnsAsync(CalculatedAccountBalanceSnapshots);
            }

            if (creatorId != null)
            {
                this.getCreatorChannelsSnapshots.Setup(v => v.ExecuteAsync(creatorId, startTime, endTime))
                    .ReturnsAsync(withChannels ? CreatorChannelsSnapshotsWithChannels : CreatorChannelsSnapshots);

                this.getCreatorFreeAccessUsersSnapshots.Setup(v => v.ExecuteAsync(creatorId, startTime, endTime))
                    .ReturnsAsync(CreatorFreeAccessUsersSnapshots);

                if (withChannels)
                {
                    this.getCreatorPosts.Setup(
                        v =>
                        v.ExecuteAsync(
                            new List<ChannelId> { ChannelId1, ChannelId2, ChannelId3 },
                            startTime,
                            endTime.AddDays(7))).ReturnsAsync(CreatorPosts);
                }

                this.getCreatorPercentageOverride.Setup(v => v.ExecuteAsync(creatorId, startTime))
                    .ReturnsAsync(CreatorPercentageOverride);
            }
        }

        private void VerifyResult(PaymentProcessingData result, UserId subscriberId, UserId creatorId, DateTime startTime, DateTime endTime, bool withChannels = true)
        {
            this.AssertReferencesEqual(
                new PaymentProcessingData(
                    subscriberId,
                    creatorId,
                    startTime,
                    endTime,
                    SubscriberChannelsSnapshots,
                    SubscriberSnapshots,
                    CalculatedAccountBalanceSnapshots,
                    withChannels ? CreatorChannelsSnapshotsWithChannels : CreatorChannelsSnapshots,
                    CreatorFreeAccessUsersSnapshots,
                    withChannels ? CreatorPosts : new List<CreatorPost>(),
                    CreatorPercentageOverride),
                result,
                withChannels);
        }


        private void VerifySubscriberMocks(UserId subscriberId, DateTime startTime, DateTime endTime, Times times)
        {
            this.getSubscriberChannelsSnapshots.Verify(v => v.ExecuteAsync(subscriberId, startTime, endTime), times);
            this.getSubscriberSnapshots.Verify(v => v.ExecuteAsync(subscriberId, startTime, endTime), times);
            this.getCalculatedAccountBalances.Verify(v => v.ExecuteAsync(subscriberId, LedgerAccountType.Fifthweek, startTime, endTime), times);
        }

        private void VerifyCreatorMocks(UserId creatorId, DateTime startTime, DateTime endTime, Times times, bool withChannels = true)
        {
            this.getCreatorChannelsSnapshots.Verify(v => v.ExecuteAsync(creatorId, startTime, endTime), times);

            this.getCreatorFreeAccessUsersSnapshots.Verify(v => v.ExecuteAsync(creatorId, startTime, endTime), times);

            if (withChannels)
            {
                this.getCreatorPosts.Verify(
                    v =>
                    v.ExecuteAsync(
                        new List<ChannelId> { ChannelId1, ChannelId2, ChannelId3 },
                        startTime,
                        endTime.AddDays(7)),
                    times);
            }

            this.getCreatorPercentageOverride.Verify(v => v.ExecuteAsync(creatorId, startTime), times);
        }

        private void AssertReferencesEqual(PaymentProcessingData expected, PaymentProcessingData actual, bool withChannels = true)
        {
            Assert.AreEqual(expected, actual);
            Assert.AreSame(expected.SubscriberChannelsSnapshots, actual.SubscriberChannelsSnapshots);
            Assert.AreSame(expected.SubscriberSnapshots, actual.SubscriberSnapshots);
            Assert.AreSame(expected.CalculatedAccountBalanceSnapshots, actual.CalculatedAccountBalanceSnapshots);
            Assert.AreSame(expected.CreatorChannelsSnapshots, actual.CreatorChannelsSnapshots);
            Assert.AreSame(expected.CreatorFreeAccessUsersSnapshots, actual.CreatorFreeAccessUsersSnapshots);

            if (withChannels)
            {
                Assert.AreSame(expected.CreatorPosts, actual.CreatorPosts);
            }

            Assert.AreSame(expected.CreatorPercentageOverride, actual.CreatorPercentageOverride);
        }
    }
}
