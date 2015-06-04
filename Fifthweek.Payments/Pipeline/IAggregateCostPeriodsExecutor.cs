namespace Fifthweek.Payments.Pipeline
{
    using System.Collections.Generic;

    public interface IAggregateCostPeriodsExecutor
    {
        AggregateCostSummary Execute(IReadOnlyList<CostPeriod> costPeriods);
    }
}