namespace Fifthweek.Api.FileManagement
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Membership;

    public interface IFileOwnership
    {
        Task<bool> IsOwnerAsync(UserId userId, FileId fileId);
    }
}