namespace Fifthweek.Shared
{
    using System;

    public class TimestampCreator : ITimestampCreator
    {
        public DateTime Now()
        {
            return DateTime.UtcNow;
        }
    }
}