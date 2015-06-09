namespace Fifthweek.Payments.Pipeline
{
    using System;
    using System.Collections.Generic;

    using Fifthweek.Payments.Snapshots;

    public interface ICalculateCostPeriodsExecutor
    {
        IReadOnlyList<CostPeriod> Execute(DateTime startTimeInclusive, DateTime endTimeExclusive, IReadOnlyList<MergedSnapshot> snapshots);
    }
}