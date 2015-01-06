namespace Fifthweek.Api.FileManagement.Commands
{
    public class InitiateFileUploadRequestCommand
    {
        public InitiateFileUploadRequestCommand(FileId fileId)
        {
            this.FileId = fileId;
        }

        public FileId FileId { get; private set; }
    }
}