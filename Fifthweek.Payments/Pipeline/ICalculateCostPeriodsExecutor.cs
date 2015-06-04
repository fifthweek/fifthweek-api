namespace Fifthweek.Payments.Pipeline
{
    using System;
    using System.Collections.Generic;

    public interface ICalculateCostPeriodsExecutor
    {
        IReadOnlyList<CostPeriod> Execute(DateTime startTimeInclusive, DateTime endTimeExclusive, IReadOnlyList<MergedSnapshot> snapshots);
    }
}