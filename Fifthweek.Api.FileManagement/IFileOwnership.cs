namespace Fifthweek.Api.FileManagement
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Membership;

    using UserId = Fifthweek.Api.Identity.Shared.Membership.UserId;

    public interface IFileOwnership
    {
        Task<bool> IsOwnerAsync(UserId userId, Shared.FileId fileId);
    }
}