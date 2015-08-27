namespace Fifthweek.Payments.Services
{
    using System;

    public static class PaymentProcessingUtilities
    {
        /// <summary>
        /// We always start the ledger entries on the same date, which doesn't necessarily correspond with
        /// the subscriber's billing weeks for individual channels, because it allows us to display billing
        /// information in a sensible way... e.g. "what you spent this week".
        /// </summary>
        public static DateTime GetPaymentProcessingStartDate(DateTime timestamp)
        {
            var ticksInWeek = TimeSpan.FromDays(7).Ticks;
            return new DateTime(timestamp.Ticks - (timestamp.Ticks % ticksInWeek), DateTimeKind.Utc);
        }

        public static CreatorPercentageOverrideData GetCreatorPercentageOverride(
            CreatorPercentageOverrideData data,
            DateTime paymentProcessingWeekEndTimeExclusive)
        {
            if (data == null || (data.ExpiryDate != null && data.ExpiryDate < paymentProcessingWeekEndTimeExclusive))
            {
                return null;
            }

            return data;
        }
    }
}