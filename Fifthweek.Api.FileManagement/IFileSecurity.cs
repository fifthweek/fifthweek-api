namespace Fifthweek.Api.FileManagement
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Membership;

    public interface IFileSecurity
    {
        Task AssertFileBelongsToUserAsync(UserId userId, FileId fileId);

        Task<bool> CheckFileBelongsToUserAsync(UserId userId, FileId fileId);
    }
}