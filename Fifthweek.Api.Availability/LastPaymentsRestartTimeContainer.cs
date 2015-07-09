namespace Fifthweek.Api.Availability
{
    using System;
    using System.Threading;

    public class LastPaymentsRestartTimeContainer : ILastPaymentsRestartTimeContainer
    {
        private long lastRestartTime = 0;

        public DateTime LastRestartTime
        {
            get
            {
                return new DateTime(Interlocked.Read(ref this.lastRestartTime), DateTimeKind.Utc);
            }

            set
            {
                Interlocked.Exchange(ref this.lastRestartTime, value.Ticks);
            }
        }
    }
}