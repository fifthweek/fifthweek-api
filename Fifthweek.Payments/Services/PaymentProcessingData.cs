namespace Fifthweek.Payments.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Snapshots;

    [AutoConstructor, AutoEqualityMembers]
    public partial class PaymentProcessingData
    {
        private IReadOnlyList<ISnapshot> orderedSnapshots;

        public UserId SubscriberId { get; private set; }

        public UserId CreatorId { get; private set; }

        public DateTime StartTimeInclusive { get; private set; }

        public DateTime EndTimeExclusive { get; private set; }

        public CommittedAccountBalance CommittedAccountBalance { get; private set; }

        public IReadOnlyList<SubscriberChannelsSnapshot> SubscriberChannelsSnapshots { get; private set; }

        public IReadOnlyList<SubscriberSnapshot> SubscriberSnapshots { get; private set; }

        public IReadOnlyList<CalculatedAccountBalanceSnapshot> CalculatedAccountBalanceSnapshots { get; private set; }

        public IReadOnlyList<CreatorChannelsSnapshot> CreatorChannelsSnapshots { get; private set; }

        public IReadOnlyList<CreatorFreeAccessUsersSnapshot> CreatorFreeAccessUsersSnapshots { get; private set; }

        public IReadOnlyList<CreatorPost> CreatorPosts { get; private set; }

        [Optional]
        public CreatorPercentageOverrideData CreatorPercentageOverride { get; private set; }

        public IReadOnlyList<ISnapshot> GetOrderedSnapshots()
        {
            if (this.orderedSnapshots == null)
            {
                var newOrderedSnapshots =
                    this.CreatorChannelsSnapshots.Cast<ISnapshot>()
                        .Concat(this.CreatorFreeAccessUsersSnapshots)
                        .Concat(this.SubscriberChannelsSnapshots)
                        .Concat(this.SubscriberSnapshots)
                        .Concat(this.CalculatedAccountBalanceSnapshots)
                        .OrderBy(v => v.Timestamp)
                        .ToList();

                Interlocked.Exchange(ref this.orderedSnapshots, newOrderedSnapshots);
            }

            return this.orderedSnapshots;
        }
    }
}