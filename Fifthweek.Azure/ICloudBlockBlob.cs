namespace Fifthweek.Azure
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.WindowsAzure.Storage.Blob;

    public interface ICloudBlockBlob
    {
        string GetSharedAccessSignature(SharedAccessBlobPolicy policy, SharedAccessBlobHeaders headers);

        string GetSharedAccessSignature(SharedAccessBlobPolicy policy);

        Uri Uri { get; }

        IBlobProperties Properties { get; }

        Task FetchAttributesAsync();

        Task SetPropertiesAsync();
    }
}