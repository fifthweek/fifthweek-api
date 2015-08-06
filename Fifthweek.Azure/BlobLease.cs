namespace Fifthweek.Azure
{
    using System;
    using System.Globalization;
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;

    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    using Microsoft.WindowsAzure.Storage;

    [AutoConstructor]
    public partial class BlobLease : IKeepAliveHandler, IBlobLease
    {
        public const int RenewRateLimitSeconds = 15;

        private readonly ITimestampCreator timestampCreator;
        private readonly ICloudStorageAccount cloudStorageAccount;
        private readonly CancellationToken cancellationToken;
        private readonly string leaseObjectName;

        private string leaseId;
        private ICloudBlockBlob blob;

        private DateTime acquiredTimestamp;
        private DateTime lastRenewedTimestamp;

        private int renewCount = 0;

        public bool GetIsAcquired()
        {
            return this.leaseId != null;
        }

        public async Task<bool> TryAcquireLeaseAsync()
        {
            try
            {
                await this.AcquireLeaseAsync();
                return true;
            }
            catch (StorageException t)
            {
                if (t.RequestInformation.HttpStatusCode != (int)HttpStatusCode.Conflict)
                {
                    throw;
                }

                return false;
            }
        }

        public async Task AcquireLeaseAsync()
        {
            if (this.blob != null)
            {
                throw new InvalidOperationException("The lease has already been acquired.");
            }

            var blobClient = this.cloudStorageAccount.CreateCloudBlobClient();
            var leaseContainer = blobClient.GetContainerReference(Constants.AzureLeaseObjectsContainerName);
            this.blob = leaseContainer.GetBlockBlobReference(this.leaseObjectName);

            this.leaseId = await this.blob.AcquireLeaseAsync(TimeSpan.FromMinutes(1), null, this.cancellationToken);
            this.acquiredTimestamp = this.timestampCreator.Now();
            this.lastRenewedTimestamp = this.acquiredTimestamp;
        }

        public Task RenewLeaseAsync()
        {
            ++this.renewCount;

            var now = this.timestampCreator.Now();
            if ((now - this.lastRenewedTimestamp) >= TimeSpan.FromSeconds(RenewRateLimitSeconds))
            {
                this.lastRenewedTimestamp = now;
                return this.blob.RenewLeaseAsync(new AccessCondition { LeaseId = this.leaseId }, this.cancellationToken);
            }

            return Task.FromResult(0);
        }

        public async Task<TimeSpan> GetTimeSinceLastLeaseAsync()
        {
            await this.blob.FetchAttributesAsync(this.cancellationToken);

            string endTimeString;
            if (!this.blob.Metadata.TryGetValue(Constants.LeaseEndTimestampMetadataKey, out endTimeString))
            {
                return TimeSpan.MaxValue;
            }

            var endTime = endTimeString.FromIso8601String();
            return this.acquiredTimestamp - endTime;
        }

        public async Task UpdateTimestampsAsync()
        {
            var now = this.timestampCreator.Now();
            await this.blob.FetchAttributesAsync(this.cancellationToken);
            this.blob.Metadata[Constants.LeaseStartTimestampMetadataKey] = this.acquiredTimestamp.ToIso8601String();
            this.blob.Metadata[Constants.LeaseEndTimestampMetadataKey] = now.ToIso8601String();
            this.blob.Metadata[Constants.LeaseRenewCountMetadataKey] = this.renewCount.ToString(CultureInfo.InvariantCulture);
            await this.blob.SetMetadataAsync(new AccessCondition { LeaseId = this.leaseId }, null, null, this.cancellationToken);
        }

        public async Task ReleaseLeaseAsync()
        {
            await this.blob.ReleaseLeaseAsync(new AccessCondition { LeaseId = this.leaseId }, this.cancellationToken);
            this.leaseId = null;
        }

        public Task KeepAliveAsync()
        {
            return this.RenewLeaseAsync();
        }
    }
}