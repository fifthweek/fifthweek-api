namespace Fifthweek.Payments
{
    using System;

    public interface ISnapshot
    {
        DateTime Timestamp { get; }
    }
}