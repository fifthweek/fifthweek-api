namespace Fifthweek.Azure
{
    using System;
    using System.Threading.Tasks;

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

        public IBlobProperties Properties
        {
            get
            {

                return new FifthweekBlobProperties(this.blob.Properties);
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

        public Task FetchAttributesAsync()
        {
            return this.blob.FetchAttributesAsync();
        }

        public Task SetPropertiesAsync()
        {
            return this.blob.SetPropertiesAsync();
        }
    }
}