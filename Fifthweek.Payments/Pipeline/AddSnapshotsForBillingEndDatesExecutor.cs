namespace Fifthweek.Payments.Pipeline
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Fifthweek.Payments.Snapshots;

    public class AddSnapshotsForBillingEndDatesExecutor : IAddSnapshotsForBillingEndDatesExecutor
    {
        /// <summary>
        /// Ensures there is a snapshot on each billing week end date.
        /// This makes it easier to discard periods of time due to the creator
        /// not posting in a channel, as we know the period won't span billing weeks.
        /// </summary>
        public IReadOnlyList<MergedSnapshot> Execute(IReadOnlyList<MergedSnapshot> snapshots)
        {
            if (snapshots.Count == 0)
            {
                return snapshots;
            }

            var uniqueStartDates = (from snapshot in snapshots
                                    from subscribedChannel in snapshot.SubscriberChannels.SubscribedChannels
                                    select subscribedChannel.SubscriptionStartDate).Distinct().ToList();

            var maximumDate = snapshots.Max(v => v.Timestamp);
            var minimumDate = snapshots.Min(v => v.Timestamp);


            var billingEndDates = new List<DateTime>();
            foreach (var startDate in uniqueStartDates)
            {
                var billingEndDate = BillingWeekUtilities.CalculateBillingWeekEndDateExclusive(startDate, minimumDate);
                while (billingEndDate < maximumDate)
                {
                    billingEndDates.Add(billingEndDate);
                    billingEndDate = billingEndDate.AddDays(7);
                }
            }

            billingEndDates = billingEndDates.Distinct().OrderBy(v => v).ToList();

            var outputSnapshots = snapshots.ToList();
            int index = 1;
            foreach (var billingEndDate in billingEndDates)
            {
                for (; index < outputSnapshots.Count; index++)
                {
                    var snapshot = outputSnapshots[index];
                    if (snapshot.Timestamp == billingEndDate)
                    {
                        break;
                    }

                    if (snapshot.Timestamp > billingEndDate)
                    {
                        var source = outputSnapshots[index - 1];
                        outputSnapshots.Insert(
                            index, 
                            new MergedSnapshot(
                                billingEndDate,
                                source.CreatorChannels,
                                source.CreatorFreeAccessUsers,
                                source.SubscriberChannels,
                                source.Subscriber,
                                source.CalculatedAccountBalance));
                        ++index;
                        break;
                    }
                }
            }

            return outputSnapshots;
        } 
    }
}