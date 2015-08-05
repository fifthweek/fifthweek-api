namespace Fifthweek.Azure
{
    using System.Collections.Generic;

    using Microsoft.WindowsAzure.Storage.Blob;

    public interface IContainerResultSegment
    {
        BlobContinuationToken ContinuationToken { get; }

        IReadOnlyList<ICloudBlobContainer> Results { get; }
    }
}