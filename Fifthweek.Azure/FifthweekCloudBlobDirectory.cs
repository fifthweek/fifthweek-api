namespace Fifthweek.Azure
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.WindowsAzure.Storage.Blob;

    public class FifthweekCloudBlobDirectory : ICloudBlobDirectory
    {
        private readonly CloudBlobDirectory directory;

        public FifthweekCloudBlobDirectory(CloudBlobDirectory directory)
        {
            this.directory = directory;
        }

        public ICloudBlockBlob GetBlockBlobReference(string blobName)
        {
            return new FifthweekCloudBlockBlob(this.directory.GetBlockBlobReference(blobName));
        }

        public async Task<IReadOnlyList<ICloudBlockBlob>> ListCloudBlockBlobsAsync(bool useFlatBlobListing = false)
        {
            var result = await this.directory.ListBlobsSegmentedAsync(null);

            if (result.ContinuationToken != null)
            {
                throw new InvalidOperationException("This method does not suppot pagenation.");
            }

            return result.Results
                .OfType<CloudBlockBlob>()
                .Select(v => new FifthweekCloudBlockBlob(v)).ToList();
        }
    }
}