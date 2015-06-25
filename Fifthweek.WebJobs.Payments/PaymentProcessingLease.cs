namespace Fifthweek.WebJobs.Payments
{
    using System;
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;

    using Fifthweek.Azure;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Shared;
    using Fifthweek.WebJobs.Shared;

    using Microsoft.WindowsAzure.Storage;

    [AutoConstructor]
    public partial class PaymentProcessingLease : IKeepAliveHandler, IPaymentProcessingLease
    {
        private readonly ICloudStorageAccount cloudStorageAccount;
        private readonly CancellationToken cancellationToken;

        private string leaseId;
        private ICloudBlockBlob blob;

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
            catch (WebException t)
            {
                if ((t.Response == null) || ((HttpWebResponse)t.Response).StatusCode != HttpStatusCode.Conflict)
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
            this.blob = leaseContainer.GetBlockBlobReference(Constants.ProcessPaymentsLeaseObjectName);

            this.leaseId = await this.blob.AcquireLeaseAsync(TimeSpan.FromMinutes(1), null, this.cancellationToken);
        }

        public Task RenewLeaseAsync()
        {
            return this.blob.RenewLeaseAsync(new AccessCondition { LeaseId = this.leaseId }, this.cancellationToken);
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