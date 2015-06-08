namespace Fifthweek.Payments.SnapshotCreation
{
    using System;

    public interface ISnapshotTimestampCreator
    {
        DateTime Create();
    }
}