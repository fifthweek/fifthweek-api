namespace Fifthweek.Api.Azure
{
    using System;

    using Microsoft.WindowsAzure.Storage.Blob;

    public class FifthweekCloudBlockBlob : ICloudBlockBlob
    {
        private readonly CloudBlockBlob blob;

        public FifthweekCloudBlockBlob(CloudBlockBlob blob)
        {
            this.blob = blob;
        }

        public Uri Uri
        {
            get
            {
                return this.blob.Uri;
            }
        }
        
        public string GetSharedAccessSignature(SharedAccessBlobPolicy policy)
        {
            return this.blob.GetSharedAccessSignature(policy);
        }

        public string GetSharedAccessSignature(SharedAccessBlobPolicy policy, SharedAccessBlobHeaders headers)
        {
            return this.blob.GetSharedAccessSignature(policy, headers);
        }
    }
}