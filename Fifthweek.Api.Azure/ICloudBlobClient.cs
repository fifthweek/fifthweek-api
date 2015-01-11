namespace Fifthweek.Api.Azure
{
    using System.Threading.Tasks;

    using Microsoft.WindowsAzure.Storage.Shared.Protocol;

    public interface ICloudBlobClient
    {
        ICloudBlobContainer GetContainerReference(string containerName);

        Task<ServiceProperties> GetServicePropertiesAsync();

        Task SetServicePropertiesAsync(ServiceProperties serviceProperties);
    }
}