namespace Fifthweek.Webjobs.Files
{
    using System.Threading.Tasks;

    using Fifthweek.Azure;
    using Fifthweek.Webjobs.Files.Shared;

    using Microsoft.Azure.WebJobs;

    public interface ICloudQueueResolver
    {
        Task<ICloudQueue> GetQueueAsync(IBinder binder, IFileTask task);
    }
}