namespace Fifthweek.Api.FileManagement.Controllers
{
    public class UploadRequest
    {
        public string FilePath { get; set; }

        public int FileSizeBytes { get; set; }
    }
}