namespace Fifthweek.Api.Posts
{
    using System.Threading.Tasks;

    using Fifthweek.Api.FileManagement.Shared;

    public interface IPostFileTypeChecks
    {
        Task<bool> IsValidForFilePostAsync(FileId fileId);

        Task<bool> IsValidForImagePostAsync(FileId fileId);

        Task AssertValidForFilePostAsync(FileId fileId);

        Task AssertValidForImagePostAsync(FileId fileId);
    }
}