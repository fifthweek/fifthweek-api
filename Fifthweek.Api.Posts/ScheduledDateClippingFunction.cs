namespace Fifthweek.Api.Posts
{
    using System;
    using System.Data.SqlTypes;

    public class ScheduledDateClippingFunction : IScheduledDateClippingFunction
    {
        public DateTime Apply(DateTime now, DateTime? scheduled)
        {
            if (scheduled == null)
            {
                return now;
            }

            if (scheduled.Value < SqlDateTime.MinValue.Value)
            {
                return SqlDateTime.MinValue.Value;
            }

            if (scheduled.Value > SqlDateTime.MaxValue.Value)
            {
                return SqlDateTime.MaxValue.Value;
            }

            return scheduled.Value;
        }
    }
}