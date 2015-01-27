namespace Fifthweek.Api.FileManagement
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;

    public interface IFileOwnership
    {
        Task<bool> IsOwnerAsync(UserId userId, Shared.FileId fileId);
    }
}