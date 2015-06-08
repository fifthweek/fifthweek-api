namespace Fifthweek.Payments.SnapshotCreation
{
    using System;

    public class SnapshotTimestampCreator : ISnapshotTimestampCreator
    {
        public DateTime Create()
        {
            return DateTime.UtcNow;
        }
    }
}