namespace Fifthweek.Api.FileManagement
{
    using System;
    using System.Threading.Tasks;

    public interface ISetFileUploadCompleteDbStatement
    {
        Task ExecuteAsync(FileId fileId, long blobSize, DateTime timeStamp);
    }
}