namespace Fifthweek.Azure
{
    using System.Threading;

    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class BlobLeaseFactory : IBlobLeaseFactory
    {
        private readonly ITimestampCreator timestampCreator;
        private readonly ICloudStorageAccount cloudStorageAccount;

        public IBlobLease Create(string leaseObjectName, CancellationToken cancellationToken)
        {
            return new BlobLease(this.timestampCreator, this.cloudStorageAccount, cancellationToken, leaseObjectName);
        }
    }
}