namespace Fifthweek.Azure
{
    using System;
    using System.Net;
    using System.Threading.Tasks;

    using Fifthweek.CodeGeneration;

    using Microsoft.WindowsAzure.Storage;

    [AutoConstructor]
    public partial class BlobLeaseHelper : IBlobLeaseHelper
    {
        private readonly ICloudStorageAccount cloudStorageAccount;

        public ICloudBlockBlob GetLeaseBlob(string leaseObjectName)
        {
            var blobClient = this.cloudStorageAccount.CreateCloudBlobClient();
            var leaseContainer = blobClient.GetContainerReference(Azure.Constants.AzureLeaseObjectsContainerName);
            var blob = leaseContainer.GetBlockBlobReference(leaseObjectName);
            return blob;
        }

        public bool IsLeaseConflictException(Exception t)
        {
            var storageException = t as StorageException;
            if (storageException != null)
            {
                return storageException.RequestInformation.HttpStatusCode == (int)HttpStatusCode.Conflict;
            }

            return false;
        }
    }
}