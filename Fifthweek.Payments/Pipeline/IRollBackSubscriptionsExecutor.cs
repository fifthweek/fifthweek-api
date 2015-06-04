namespace Fifthweek.Payments.Pipeline
{
    using System.Collections.Generic;

    public interface IRollBackSubscriptionsExecutor
    {
        IReadOnlyList<MergedSnapshot> Execute(IReadOnlyList<MergedSnapshot> snapshots);
    }
}