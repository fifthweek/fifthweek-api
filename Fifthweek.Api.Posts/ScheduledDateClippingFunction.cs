namespace Fifthweek.Api.Posts
{
    using System;

    public class ScheduledDateClippingFunction : IScheduledDateClippingFunction
    {
        public DateTime Apply(DateTime now, DateTime? scheduled)
        {
            if (scheduled == null || scheduled.Value <= now)
            {
                return now;
            }

            return scheduled.Value;
        }
    }
}