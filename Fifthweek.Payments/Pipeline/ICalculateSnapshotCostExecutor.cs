namespace Fifthweek.Payments.Pipeline
{
    public interface ICalculateSnapshotCostExecutor
    {
        int Execute(MergedSnapshot snapshot);
    }
}