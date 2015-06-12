namespace Fifthweek.Payments.Pipeline
{
    using System;

    public static class BillingWeekUtilities
    {
        public static DateTime CalculateBillingWeekEndDateExclusive(DateTime subscriptionStartDate, DateTime snapshotTimestamp)
        {
            var week = TimeSpan.FromDays(7);
            var billingWeekOffset = subscriptionStartDate.Ticks % week.Ticks;
            var firstEndDateInWeek = ((snapshotTimestamp.Ticks / week.Ticks) * week.Ticks) + billingWeekOffset;

            if (firstEndDateInWeek > snapshotTimestamp.Ticks)
            {
                return new DateTime(firstEndDateInWeek);
            }

            return new DateTime(firstEndDateInWeek + week.Ticks);
        }
    }
}
