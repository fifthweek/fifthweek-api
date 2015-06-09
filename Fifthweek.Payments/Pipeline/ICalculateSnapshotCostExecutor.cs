namespace Fifthweek.Payments.Pipeline
{
    using Fifthweek.Payments.Snapshots;

    public interface ICalculateSnapshotCostExecutor
    {
        int Execute(MergedSnapshot snapshot);
    }
}