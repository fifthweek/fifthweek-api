namespace Fifthweek.WebJobs.Files
{
    using System.Threading.Tasks;

    using Fifthweek.Azure;
    using Fifthweek.WebJobs.Files.Shared;

    using Microsoft.Azure.WebJobs;

    public interface ICloudQueueResolver
    {
        Task<ICloudQueue> GetQueueAsync(IBinder binder, string queueName);
    }
}