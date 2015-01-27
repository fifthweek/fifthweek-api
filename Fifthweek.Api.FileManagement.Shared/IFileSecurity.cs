namespace Fifthweek.Api.FileManagement.Shared
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;

    public interface IFileSecurity
    {
        Task<bool> IsUsageAllowedAsync(UserId requester, FileId fileId);

        Task AssertUsageAllowedAsync(UserId requester, FileId fileId);
    }
}