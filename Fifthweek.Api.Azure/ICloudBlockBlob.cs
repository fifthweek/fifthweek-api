namespace Fifthweek.Api.Azure
{
    using System;

    using Microsoft.WindowsAzure.Storage.Blob;

    public interface ICloudBlockBlob
    {
        string GetSharedAccessSignature(SharedAccessBlobPolicy policy, SharedAccessBlobHeaders headers);

        string GetSharedAccessSignature(SharedAccessBlobPolicy policy);

        Uri Uri { get; }
    }
}