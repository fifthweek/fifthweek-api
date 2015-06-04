namespace Fifthweek.Payments.Pipeline
{
    using System.Collections.Generic;

    public class AggregateCostPeriodsExecutor : IAggregateCostPeriodsExecutor
    {
        public AggregateCostSummary Execute(IReadOnlyList<CostPeriod> costPeriods)
        {
            decimal cost = 0;
            foreach (var costPeriod in costPeriods)
            {
                var period = costPeriod.EndTimeExclusive - costPeriod.StartTimeInclusive;
                cost += costPeriod.Cost * ((decimal)period.TotalDays / 7m);
            }

            return new AggregateCostSummary(cost);
        }
    }
}