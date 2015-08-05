namespace Fifthweek.Azure
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Blob;

    public interface ICloudBlockBlob
    {
        Uri Uri { get; }

        IBlobProperties Properties { get; }

        IDictionary<string, string> Metadata { get; }

        string GetSharedAccessSignature(SharedAccessBlobPolicy policy, SharedAccessBlobHeaders headers);

        string GetSharedAccessSignature(SharedAccessBlobPolicy policy);

        Task FetchAttributesAsync();

        Task FetchAttributesAsync(CancellationToken cancellationToken);

        Task SetPropertiesAsync();

        Task SetPropertiesAsync(CancellationToken cancellationToken);

        Task<CloudBlobStream> OpenWriteAsync(CancellationToken cancellationToken);

        Task<bool> ExistsAsync();

        Task<bool> ExistsAsync(CancellationToken cancellationToken);

        Task<Stream> OpenReadAsync(CancellationToken cancellationToken);

        Task SetMetadataAsync();

        Task UploadTextAsync(string content);

        Task<string> AcquireLeaseAsync(TimeSpan? leaseTime, string proposedLeaseId, CancellationToken cancellationToken);

        Task RenewLeaseAsync(AccessCondition accessCondition, CancellationToken cancellationToken);

        Task ReleaseLeaseAsync(AccessCondition accessCondition, CancellationToken cancellationToken);

        Task SetMetadataAsync(AccessCondition accessCondition, BlobRequestOptions blobRequestOptions, OperationContext operationContext, CancellationToken cancellationToken);

        Task DeleteAsync();
    }
}