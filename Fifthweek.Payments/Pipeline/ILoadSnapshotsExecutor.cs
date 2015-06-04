namespace Fifthweek.Payments.Pipeline
{
    using System;
    using System.Collections.Generic;

    public interface ILoadSnapshotsExecutor
    {
        IReadOnlyList<ISnapshot> Execute(
            Guid subscriberId,
            Guid creatorId,
            DateTime startTimeInclusive,
            DateTime endTimeExclusive);
    }
}