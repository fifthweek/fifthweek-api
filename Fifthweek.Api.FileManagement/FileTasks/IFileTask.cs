﻿namespace Fifthweek.Api.FileManagement.FileTasks
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Azure;
    using Fifthweek.Api.FileManagement.Shared;

    public interface IFileTask
    {
        Task HandleAsync(IQueueService queueService, FileId fileId, string containerName, string blobName, string filePurpose);
    }
}