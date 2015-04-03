namespace Fifthweek.Azure
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

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

        Task<bool> ExistsAsync(CancellationToken cancellationToken);

        Task<Stream> OpenReadAsync(CancellationToken cancellationToken);
    }
}