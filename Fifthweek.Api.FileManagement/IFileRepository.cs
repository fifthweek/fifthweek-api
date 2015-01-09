namespace Fifthweek.Api.FileManagement
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Persistence;

    public interface IFileRepository
    {
        Task AssertFileBelongsToUserAsync(UserId userId, FileId fileId);

        Task AddNewFileAsync(
            FileId fileId, 
            UserId userId,
            string fileNameWithoutExtension,
            string fileExtension,
            string purpose);

        Task SetFileUploadComplete(FileId fileId, long blobSize);
    }
}