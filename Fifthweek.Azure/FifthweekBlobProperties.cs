namespace Fifthweek.Azure
{
    using Microsoft.WindowsAzure.Storage.Blob;

    public class FifthweekBlobProperties : IBlobProperties
    {
        private readonly BlobProperties blobProperties;

        public FifthweekBlobProperties(BlobProperties blobProperties)
        {
            this.blobProperties = blobProperties;
        }

        public long Length
        {
            get
            {
                return this.blobProperties.Length;
            }
        }

        public string ContentType
        {
            get
            {
                return this.blobProperties.ContentType;
            }

            set
            {
                this.blobProperties.ContentType = value;
            }
        }

        public string CacheControl 
        {
            get
            {
                return this.blobProperties.CacheControl;
            }

            set
            {
                this.blobProperties.CacheControl = value;
            }
        }
    }
}