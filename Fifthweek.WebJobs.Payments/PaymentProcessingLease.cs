namespace Fifthweek.WebJobs.Payments
{
    using System;
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;

    using Fifthweek.Azure;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Shared;
    using Fifthweek.WebJobs.Shared;

    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Blob;
    using Microsoft.WindowsAzure.Storage.Shared.Protocol;

    [AutoConstructor]
    public partial class PaymentProcessingLease : IKeepAliveHandler, IPaymentProcessingLease
    {
        private readonly ITimestampCreator timestampCreator;
        private readonly ICloudStorageAccount cloudStorageAccount;
        private readonly CancellationToken cancellationToken;

        private string leaseId;
        private ICloudBlockBlob blob;

        private DateTime acquiredTimestamp;

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
            var leaseContainer = blobClient.GetContainerReference(Fifthweek.Shared.Constants.AzureLeaseObjectsContainerName);
            this.blob = leaseContainer.GetBlockBlobReference(Fifthweek.Payments.Shared.Constants.ProcessPaymentsLeaseObjectName);

            this.leaseId = await this.blob.AcquireLeaseAsync(TimeSpan.FromMinutes(1), null, this.cancellationToken);
            this.acquiredTimestamp = this.timestampCreator.Now();
        }

        public Task RenewLeaseAsync()
        {
            ++this.renewCount;
            return this.blob.RenewLeaseAsync(new AccessCondition { LeaseId = this.leaseId }, this.cancellationToken);
        }

        public async Task UpdateTimestampsAsync()
        {
            var now = this.timestampCreator.Now();
            await this.blob.FetchAttributesAsync(this.cancellationToken);
            this.blob.Metadata[Fifthweek.Payments.Shared.Constants.LastProcessPaymentsStartTimestampMetadataKey] = this.acquiredTimestamp.ToString("s", System.Globalization.CultureInfo.InvariantCulture);
            this.blob.Metadata[Fifthweek.Payments.Shared.Constants.LastProcessPaymentsEndTimestampMetadataKey] = now.ToString("s", System.Globalization.CultureInfo.InvariantCulture);
            this.blob.Metadata[Fifthweek.Payments.Shared.Constants.LastProcessPaymentsRenewCountMetadataKey] = this.renewCount.ToString(System.Globalization.CultureInfo.InvariantCulture);
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