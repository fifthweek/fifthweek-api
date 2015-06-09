namespace Fifthweek.Payments.Snapshots
{
    using System;

    public interface ISnapshot
    {
        DateTime Timestamp { get; }
    }
}