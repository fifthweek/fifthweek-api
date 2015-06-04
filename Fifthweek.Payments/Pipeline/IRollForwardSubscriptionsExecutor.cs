namespace Fifthweek.Payments.Pipeline
{
    using System;
    using System.Collections.Generic;

    public interface IRollForwardSubscriptionsExecutor
    {
        IReadOnlyList<MergedSnapshot> Execute(DateTime endTimeExclusive,  IReadOnlyList<MergedSnapshot> inputSnapshots);
    }
}