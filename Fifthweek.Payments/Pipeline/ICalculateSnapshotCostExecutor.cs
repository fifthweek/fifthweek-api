namespace Fifthweek.Payments.Pipeline
{
    using System.Collections.Generic;

    using Fifthweek.Payments.Services;
    using Fifthweek.Payments.Snapshots;

    public interface ICalculateSnapshotCostExecutor
    {
        int Execute(MergedSnapshot snapshot, IReadOnlyList<CreatorPost> creatorPosts);
    }
}