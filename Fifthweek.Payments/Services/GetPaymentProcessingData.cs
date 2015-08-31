namespace Fifthweek.Payments.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Snapshots;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class GetPaymentProcessingData : IGetPaymentProcessingData
    {
        private readonly IGetCreatorChannelsSnapshotsDbStatement getCreatorChannelsSnapshots;
        private readonly IGetCreatorFreeAccessUsersSnapshotsDbStatement getCreatorFreeAccessUsersSnapshots;
        private readonly IGetCreatorPostsDbStatement getCreatorPosts;
        private readonly IGetSubscriberChannelsSnapshotsDbStatement getSubscriberChannelsSnapshots;
        private readonly IGetSubscriberSnapshotsDbStatement getSubscriberSnapshots;
        private readonly IGetCalculatedAccountBalancesDbStatement getCalculatedAccountBalances;
        private readonly IGetCreatorPercentageOverrideDbStatement getCreatorPercentageOverride;

        private CachedData cachedData;

        public async Task<PaymentProcessingData> ExecuteAsync(UserId subscriberId, UserId creatorId, DateTime startTimeInclusive, DateTime endTimeExclusive, CommittedAccountBalance committedAccountBalance)
        {
            using (PaymentsPerformanceLogger.Instance.Log(typeof(GetPaymentProcessingData)))
            {
                subscriberId.AssertNotNull("subscriberId");
                creatorId.AssertNotNull("creatorId");
                committedAccountBalance.AssertNotNull("committedAccountBalance");

                IReadOnlyList<SubscriberChannelsSnapshot> subscriberChannelsSnapshots;
                IReadOnlyList<SubscriberSnapshot> subscriberSnapshots;
                IReadOnlyList<CalculatedAccountBalanceSnapshot> calculatedAccountBalances;
                IReadOnlyList<CreatorChannelsSnapshot> creatorChannelsSnapshots;
                IReadOnlyList<CreatorFreeAccessUsersSnapshot> creatorFreeAccessUsersSnapshots;
                IReadOnlyList<CreatorPost> creatorPosts;
                CreatorPercentageOverrideData creatorPercentageOverride;

                bool updateCache = false;
                if (this.cachedData != null
                    && subscriberId.Equals(this.cachedData.SubscriberId)
                    && startTimeInclusive >= this.cachedData.StartTimeInclusive
                    && endTimeExclusive <= this.cachedData.EndTimeExclusive)
                {
                    subscriberChannelsSnapshots = this.cachedData.SubscriberChannelsSnapshots;
                    subscriberSnapshots = this.cachedData.SubscriberSnapshots;
                    calculatedAccountBalances = this.cachedData.CalculatedAccountBalanceSnapshots;
                }
                else
                {
                    updateCache = true;
                    subscriberChannelsSnapshots = await this.getSubscriberChannelsSnapshots.ExecuteAsync(subscriberId, startTimeInclusive, endTimeExclusive);
                    subscriberSnapshots = await this.getSubscriberSnapshots.ExecuteAsync(subscriberId, startTimeInclusive, endTimeExclusive);
                    calculatedAccountBalances = await this.getCalculatedAccountBalances.ExecuteAsync(subscriberId, LedgerAccountType.FifthweekCredit, startTimeInclusive, endTimeExclusive);
                }

                if (this.cachedData != null
                    && creatorId.Equals(this.cachedData.CreatorId)
                    && startTimeInclusive >= this.cachedData.StartTimeInclusive
                    && endTimeExclusive <= this.cachedData.EndTimeExclusive)
                {
                    creatorChannelsSnapshots = this.cachedData.CreatorChannelsSnapshots;
                    creatorFreeAccessUsersSnapshots = this.cachedData.CreatorFreeAccessUsersSnapshots;
                    creatorPosts = this.cachedData.CreatorPosts;
                    creatorPercentageOverride = this.cachedData.CreatorPercentageOverride;
                }
                else
                {
                    updateCache = true;
                    creatorChannelsSnapshots = await this.getCreatorChannelsSnapshots.ExecuteAsync(creatorId, startTimeInclusive, endTimeExclusive);
                    creatorFreeAccessUsersSnapshots = await this.getCreatorFreeAccessUsersSnapshots.ExecuteAsync(creatorId, startTimeInclusive, endTimeExclusive);
                    creatorPosts = await this.GetCreatorPosts(creatorChannelsSnapshots, startTimeInclusive, endTimeExclusive);
                    creatorPercentageOverride = await this.getCreatorPercentageOverride.ExecuteAsync(creatorId, startTimeInclusive);
                }

                if (updateCache)
                {
                    this.cachedData = new CachedData(
                        subscriberId,
                        creatorId,
                        startTimeInclusive,
                        endTimeExclusive,
                        subscriberChannelsSnapshots,
                        subscriberSnapshots,
                        calculatedAccountBalances,
                        creatorChannelsSnapshots,
                        creatorFreeAccessUsersSnapshots,
                        creatorPosts,
                        creatorPercentageOverride);
                }

                return new PaymentProcessingData(
                    subscriberId, 
                    creatorId, 
                    startTimeInclusive,
                    endTimeExclusive,
                    committedAccountBalance,
                    subscriberChannelsSnapshots,
                    subscriberSnapshots,
                    calculatedAccountBalances,
                    creatorChannelsSnapshots,
                    creatorFreeAccessUsersSnapshots,
                    creatorPosts,
                    creatorPercentageOverride);
            }
        }

        private async Task<IReadOnlyList<CreatorPost>> GetCreatorPosts(
            IEnumerable<CreatorChannelsSnapshot> creatorChannelsSnapshots, 
            DateTime startTimeInclusive,
            DateTime endTimeExclusive)
        {
            var allChannelIds = creatorChannelsSnapshots.SelectMany(v => v.CreatorChannels).Select(v => v.ChannelId).Distinct().ToList();
            if (allChannelIds.Count == 0)
            {
                return new List<CreatorPost>();
            }

            // Expand by a week as we need to know if there were any posts within the subscriber's
            // billing week, which may be up to a week before and after the requested end time.
            var poststartTimeInclusive = startTimeInclusive.AddDays(-7);
            var postsEndTimeExclusive = endTimeExclusive.AddDays(7);
            return await this.getCreatorPosts.ExecuteAsync(allChannelIds, poststartTimeInclusive, postsEndTimeExclusive);
        }

        [AutoConstructor]
        public partial class CachedData
        {
            public UserId SubscriberId { get; private set; }
            
            public UserId CreatorId { get; private set; }
            
            public DateTime StartTimeInclusive { get; private set; }
            
            public DateTime EndTimeExclusive { get; private set; }
            
            public IReadOnlyList<SubscriberChannelsSnapshot> SubscriberChannelsSnapshots { get; private set; }

            public IReadOnlyList<SubscriberSnapshot> SubscriberSnapshots { get; private set; }

            public IReadOnlyList<CalculatedAccountBalanceSnapshot> CalculatedAccountBalanceSnapshots { get; private set; }
            
            public IReadOnlyList<CreatorChannelsSnapshot> CreatorChannelsSnapshots { get; private set; }
            
            public IReadOnlyList<CreatorFreeAccessUsersSnapshot> CreatorFreeAccessUsersSnapshots { get; private set; }
            
            public IReadOnlyList<CreatorPost> CreatorPosts { get; private set; }

            [Optional]
            public CreatorPercentageOverrideData CreatorPercentageOverride { get; private set; }
        }
    }
}