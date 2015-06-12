namespace Fifthweek.Payments.Tests.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Payments.Services;
    using Fifthweek.Payments.Snapshots;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class PaymentProcessorTests
    {
        private Mock<IGetAllCreatorsDbStatement> getAllCreators;
        private Mock<IGetAllSubscribedUsersDbStatement> getAllSubscribedUsers;
        private Mock<IGetCreatorChannelsSnapshotsDbStatement> getCreatorChannelsSnapshots;
        private Mock<IGetCreatorFreeAccessUsersSnapshotsDbStatement> getCreatorFreeAccessUsersSnapshots;
        private Mock<IGetCreatorPostsDbStatement> getCreatorPosts;
        private Mock<IGetSubscriberChannelsSnapshotsDbStatement> getSubscriberChannelsSnapshots;
        private Mock<IGetSubscriberSnapshotsDbStatement> getSubscriberSnapshots;
        private Mock<ISubscriberPaymentPipeline> subscriberPaymentPipeline;
        private PaymentProcessor target;

        [TestInitialize]
        public void Initialize()
        {
            this.getAllCreators = new Mock<IGetAllCreatorsDbStatement>(MockBehavior.Strict);
            this.getAllSubscribedUsers = new Mock<IGetAllSubscribedUsersDbStatement>(MockBehavior.Strict);
            this.getCreatorChannelsSnapshots = new Mock<IGetCreatorChannelsSnapshotsDbStatement>(MockBehavior.Strict);
            this.getCreatorFreeAccessUsersSnapshots = new Mock<IGetCreatorFreeAccessUsersSnapshotsDbStatement>(MockBehavior.Strict);
            this.getCreatorPosts = new Mock<IGetCreatorPostsDbStatement>(MockBehavior.Strict);
            this.getSubscriberSnapshots = new Mock<IGetSubscriberSnapshotsDbStatement>(MockBehavior.Strict);
            this.getSubscriberChannelsSnapshots = new Mock<IGetSubscriberChannelsSnapshotsDbStatement>(MockBehavior.Strict);
            this.subscriberPaymentPipeline = new Mock<ISubscriberPaymentPipeline>(MockBehavior.Strict);

            this.target = new PaymentProcessor(
                this.getAllCreators.Object,
                this.getAllSubscribedUsers.Object,
                this.getCreatorChannelsSnapshots.Object,
                this.getCreatorFreeAccessUsersSnapshots.Object,
                this.getCreatorPosts.Object,
                this.getSubscriberChannelsSnapshots.Object,
                this.getSubscriberSnapshots.Object,
                this.subscriberPaymentPipeline.Object);
        }

        [TestMethod]
        public async Task ItShouldCallServicesInOrder()
        {
            var startTimeInclusive = DateTime.UtcNow;
            var endTimeExclusive = startTimeInclusive.AddDays(1);

            var creatorIds = new List<UserId> { UserId.Random() };
            this.getAllCreators.Setup(v => v.ExecuteAsync()).ReturnsAsync(creatorIds);

            var creatorChannelsSnapshots = new List<CreatorChannelsSnapshot> 
            {
                new CreatorChannelsSnapshot(
                    startTimeInclusive, 
                    creatorIds[0],
                    new List<CreatorChannelsSnapshotItem>
                    {
                        new CreatorChannelsSnapshotItem(ChannelId.Random(), 100),
                    }),
                new CreatorChannelsSnapshot(
                    startTimeInclusive.AddHours(4), 
                    creatorIds[0],
                    new List<CreatorChannelsSnapshotItem>
                    {
                        new CreatorChannelsSnapshotItem(ChannelId.Random(), 100),
                    }),
            };

            this.getCreatorChannelsSnapshots.Setup(
                v => v.ExecuteAsync(creatorIds[0], startTimeInclusive, endTimeExclusive))
                .ReturnsAsync(creatorChannelsSnapshots);

            var subscriberIds = new List<UserId> { UserId.Random() };

            var channelIds = new List<ChannelId> 
            {
                creatorChannelsSnapshots[0].CreatorChannels[0].ChannelId,
                creatorChannelsSnapshots[1].CreatorChannels[0].ChannelId
            };

            this.getAllSubscribedUsers.Setup(v => v.ExecuteAsync(channelIds)).ReturnsAsync(subscriberIds);

            var creatorFreeAccessUsersSnapshots = new List<CreatorFreeAccessUsersSnapshot>
            {
                CreatorFreeAccessUsersSnapshot.Default(startTimeInclusive.AddHours(2), creatorIds[0]),
            };

            this.getCreatorFreeAccessUsersSnapshots.Setup(v => v.ExecuteAsync(creatorIds[0], startTimeInclusive, endTimeExclusive))
                .ReturnsAsync(creatorFreeAccessUsersSnapshots);

            var posts = new List<CreatorPost>
            {
                new CreatorPost(creatorChannelsSnapshots[0].CreatorChannels[0].ChannelId, startTimeInclusive),
            };

            this.getCreatorPosts.Setup(v => v.ExecuteAsync(channelIds, startTimeInclusive, endTimeExclusive.AddDays(7)))
                .ReturnsAsync(posts);

            var subscriberChannelsSnapshots = new List<SubscriberChannelsSnapshot>
            {
                SubscriberChannelsSnapshot.Default(startTimeInclusive.AddHours(3), subscriberIds[0]),
            };

            this.getSubscriberChannelsSnapshots.Setup(v => v.ExecuteAsync(subscriberIds[0], startTimeInclusive, endTimeExclusive))
                .ReturnsAsync(subscriberChannelsSnapshots);

            var subscriberSnapshots = new List<SubscriberSnapshot>
            {
                SubscriberSnapshot.Default(startTimeInclusive.AddHours(1), subscriberIds[0]),
            };

            this.getSubscriberSnapshots.Setup(v => v.ExecuteAsync(subscriberIds[0], startTimeInclusive, endTimeExclusive))
                .ReturnsAsync(subscriberSnapshots);

            var orderedSnapshots = new List<ISnapshot>
            {
                creatorChannelsSnapshots[0],
                subscriberSnapshots[0],
                creatorFreeAccessUsersSnapshots[0],
                subscriberChannelsSnapshots[0],
                creatorChannelsSnapshots[1]
            };

            this.subscriberPaymentPipeline.Setup(v => v.CalculatePayment(
                orderedSnapshots,
                posts,
                subscriberIds[0],
                creatorIds[0],
                startTimeInclusive,
                endTimeExclusive))
                .Returns(new AggregateCostSummary(1000)).Verifiable();

            await this.target.CalculateAllPayments(startTimeInclusive, endTimeExclusive);

            this.subscriberPaymentPipeline.Verify();
        }

        [TestMethod]
        public async Task ItShouldStopProcessingCreatorIfNoChannels()
        {
            var startTimeInclusive = DateTime.UtcNow;
            var endTimeExclusive = startTimeInclusive.AddDays(1);

            var creatorIds = new List<UserId> { UserId.Random() };
            this.getAllCreators.Setup(v => v.ExecuteAsync()).ReturnsAsync(creatorIds);

            var creatorChannelsSnapshots = new List<CreatorChannelsSnapshot> 
            {
                new CreatorChannelsSnapshot(
                    startTimeInclusive, 
                    creatorIds[0],
                    new List<CreatorChannelsSnapshotItem>
                    {
                    }),
                new CreatorChannelsSnapshot(
                    startTimeInclusive.AddHours(4), 
                    creatorIds[0],
                    new List<CreatorChannelsSnapshotItem>
                    {
                    }),
            };

            this.getCreatorChannelsSnapshots.Setup(
                v => v.ExecuteAsync(creatorIds[0], startTimeInclusive, endTimeExclusive))
                .ReturnsAsync(creatorChannelsSnapshots);

            await this.target.CalculateAllPayments(startTimeInclusive, endTimeExclusive);
        }

        [TestMethod]
        public async Task ItShouldStopProcessingCreatorIfNoSnapshots()
        {
            var startTimeInclusive = DateTime.UtcNow;
            var endTimeExclusive = startTimeInclusive.AddDays(1);

            var creatorIds = new List<UserId> { UserId.Random() };
            this.getAllCreators.Setup(v => v.ExecuteAsync()).ReturnsAsync(creatorIds);

            var creatorChannelsSnapshots = new List<CreatorChannelsSnapshot> 
            {
            };

            this.getCreatorChannelsSnapshots.Setup(
                v => v.ExecuteAsync(creatorIds[0], startTimeInclusive, endTimeExclusive))
                .ReturnsAsync(creatorChannelsSnapshots);

            await this.target.CalculateAllPayments(startTimeInclusive, endTimeExclusive);
        }

        [TestMethod]
        public async Task ItShouldStopProcessingCreatorIfNoSubscribers()
        {
            var startTimeInclusive = DateTime.UtcNow;
            var endTimeExclusive = startTimeInclusive.AddDays(1);

            var creatorIds = new List<UserId> { UserId.Random() };
            this.getAllCreators.Setup(v => v.ExecuteAsync()).ReturnsAsync(creatorIds);

            var creatorChannelsSnapshots = new List<CreatorChannelsSnapshot> 
            {
                new CreatorChannelsSnapshot(
                    startTimeInclusive, 
                    creatorIds[0],
                    new List<CreatorChannelsSnapshotItem>
                    {
                        new CreatorChannelsSnapshotItem(ChannelId.Random(), 100),
                    }),
                new CreatorChannelsSnapshot(
                    startTimeInclusive.AddHours(4), 
                    creatorIds[0],
                    new List<CreatorChannelsSnapshotItem>
                    {
                        new CreatorChannelsSnapshotItem(ChannelId.Random(), 100),
                    }),
            };

            this.getCreatorChannelsSnapshots.Setup(
                v => v.ExecuteAsync(creatorIds[0], startTimeInclusive, endTimeExclusive))
                .ReturnsAsync(creatorChannelsSnapshots);

            var subscriberIds = new List<UserId>();

            var channelIds = new List<ChannelId> 
            {
                creatorChannelsSnapshots[0].CreatorChannels[0].ChannelId,
                creatorChannelsSnapshots[1].CreatorChannels[0].ChannelId
            };

            this.getAllSubscribedUsers.Setup(v => v.ExecuteAsync(channelIds)).ReturnsAsync(subscriberIds);

            await this.target.CalculateAllPayments(startTimeInclusive, endTimeExclusive);
        }
    }
}