namespace Fifthweek.Azure
{
    using System;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.WindowsAzure.Storage.Blob;

    public interface ICloudBlockBlob
    {
        Uri Uri { get; }

        IBlobProperties Properties { get; }

        string GetSharedAccessSignature(SharedAccessBlobPolicy policy, SharedAccessBlobHeaders headers);

        string GetSharedAccessSignature(SharedAccessBlobPolicy policy);

        Task FetchAttributesAsync();

        Task FetchAttributesAsync(CancellationToken cancellationToken);

        Task SetPropertiesAsync();

        Task SetPropertiesAsync(CancellationToken cancellationToken);

        Task<CloudBlobStream> OpenWriteAsync(CancellationToken cancellationToken);
    }
}