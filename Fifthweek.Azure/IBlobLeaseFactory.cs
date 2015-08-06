namespace Fifthweek.Azure
{
    using System.Threading;

    public interface IBlobLeaseFactory
    {
        IBlobLease Create(string leaseObjectName, CancellationToken cancellationToken);
    }
}