namespace Fifthweek.Payments.Services
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Snapshots;

    [AutoConstructor]
    public partial class PaymentProcessor
    {
        private readonly IGetAllCreatorsDbStatement getAllCreatorsDbStatement;
        private readonly IGetAllSubscribedUsersDbStatement getAllSubscribedUsersDbStatement;
        private readonly IGetCreatorChannelsSnapshotsDbStatement getCreatorChannelsSnapshotsDbStatement;
        private readonly IGetCreatorFreeAccessUsersSnapshotsDbStatement getCreatorFreeAccessUsersSnapshotsDbStatement;
        private readonly IGetCreatorPostsDbStatement getCreatorPostsDbStatement;
        private readonly IGetSubscriberChannelsSnapshotsDbStatement getSubscriberChannelsSnapshotsDbStatement;
        private readonly IGetSubscriberSnapshotsDbStatement getSubscriberSnapshotsDbStatement;

        private readonly ISubscriberPaymentPipeline subscriberPaymentPipeline;

        public async Task CalculateAllPayments(DateTime startTimeInclusive, DateTime endTimeExclusive)
        {
            var creatorIds = await this.getAllCreatorsDbStatement.ExecuteAsync();

            foreach (var creatorId in creatorIds)
            {
                var creatorChannelsSnapshots = await this.getCreatorChannelsSnapshotsDbStatement.ExecuteAsync(creatorId, startTimeInclusive, endTimeExclusive);

                var allChannelIds = creatorChannelsSnapshots.SelectMany(v => v.CreatorChannels).Select(v => v.ChannelId).Distinct().ToList();
                if (allChannelIds.Count == 0)
                {
                    continue;
                }

                var subscribedUserIds = await this.getAllSubscribedUsersDbStatement.ExecuteAsync(allChannelIds);
                if (subscribedUserIds.Count == 0)
                {
                    continue;
                }

                var creatorFreeAccessUsersSnapshots = await this.getCreatorFreeAccessUsersSnapshotsDbStatement.ExecuteAsync(creatorId, startTimeInclusive, endTimeExclusive);

                // Add a week as we need to know if there were any posts before the subscriber's
                // billing week end date, which may be up to a week after the requested end time.
                var postsEndTimeExclusive = endTimeExclusive.AddDays(7);
                var posts = await this.getCreatorPostsDbStatement.ExecuteAsync(creatorId, startTimeInclusive, postsEndTimeExclusive);

                foreach (var subscriberId in subscribedUserIds)
                {
                    var subscriberChannelsSnapshots = await this.getSubscriberChannelsSnapshotsDbStatement.ExecuteAsync(subscriberId, startTimeInclusive, endTimeExclusive);
                    var subscriberSnapshots = await this.getSubscriberSnapshotsDbStatement.ExecuteAsync(subscriberId, startTimeInclusive, endTimeExclusive);

                    var orderedSnapshots =
                        creatorChannelsSnapshots.Cast<ISnapshot>()
                            .Concat(creatorFreeAccessUsersSnapshots)
                            .Concat(subscriberChannelsSnapshots)
                            .Concat(subscriberSnapshots)
                            .OrderBy(v => v.Timestamp)
                            .ToList();

                    // Process user.
                    var cost = this.subscriberPaymentPipeline.CalculatePayment(
                        orderedSnapshots,
                        subscriberId,
                        creatorId,
                        startTimeInclusive,
                        endTimeExclusive);

                    // Save data.
                }
            }
        }
    }
}