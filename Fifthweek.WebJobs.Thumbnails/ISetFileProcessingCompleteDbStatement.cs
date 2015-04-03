namespace Fifthweek.WebJobs.Thumbnails
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.FileManagement.Shared;

    public interface ISetFileProcessingCompleteDbStatement
    {
        Task ExecuteAsync(FileId fileId, int dequeueCount, DateTime processingStartedDate, DateTime processingCompletedDate, int renderWidth, int renderHeight);
    }
}