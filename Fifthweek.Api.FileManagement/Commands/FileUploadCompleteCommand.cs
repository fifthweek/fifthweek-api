namespace Fifthweek.Api.FileManagement.Commands
{
    public class FileUploadCompleteCommand
    {
        public FileUploadCompleteCommand(FileId fileId)
        {
            this.FileId = fileId;
        }

        public FileId FileId { get; private set; }
    }
}