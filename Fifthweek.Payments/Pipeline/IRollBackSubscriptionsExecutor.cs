namespace Fifthweek.Payments.Pipeline
{
    using System.Collections.Generic;

    using Fifthweek.Payments.Snapshots;

    public interface IRollBackSubscriptionsExecutor
    {
        IReadOnlyList<MergedSnapshot> Execute(IReadOnlyList<MergedSnapshot> snapshots);
    }
}