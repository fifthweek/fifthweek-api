namespace Fifthweek.Payments.Pipeline
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Snapshots;

    [AutoConstructor]
    public partial class CalculateCostPeriodsExecutor : ICalculateCostPeriodsExecutor
    {
        private readonly ICalculateSnapshotCostExecutor costCalculator;

        public IReadOnlyList<CostPeriod> Execute(DateTime startTimeInclusive, DateTime endTimeExclusive, IReadOnlyList<MergedSnapshot> snapshots)
        {
            var result = new List<CostPeriod>();
            MergedSnapshot lastSnapshot = null;
            foreach (var snapshot in snapshots.Concat(new List<MergedSnapshot> { null }))
            {
                if (lastSnapshot != null)
                {
                    var cost = this.costCalculator.Execute(lastSnapshot);

                    if (snapshot == null)
                    {
                        var period = new CostPeriod(lastSnapshot.Timestamp, endTimeExclusive, cost);
                        result.Add(period);
                    }
                    else
                    {
                        var period = new CostPeriod(lastSnapshot.Timestamp, snapshot.Timestamp, cost);
                        result.Add(period);
                    }
                }

                lastSnapshot = snapshot;
            }

            return result;
        }
    }
}