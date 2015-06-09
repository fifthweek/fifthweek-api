namespace Fifthweek.Payments.Pipeline
{
    using System;
    using System.Collections.Generic;

    using Fifthweek.Payments.Snapshots;

    public interface IRollForwardSubscriptionsExecutor
    {
        IReadOnlyList<MergedSnapshot> Execute(DateTime endTimeExclusive,  IReadOnlyList<MergedSnapshot> inputSnapshots);
    }
}