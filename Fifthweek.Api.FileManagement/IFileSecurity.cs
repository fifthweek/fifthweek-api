namespace Fifthweek.Api.FileManagement
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Membership;

    public interface IFileSecurity
    {
        Task<bool> IsUsageAllowedAsync(UserId requester, FileId fileId);

        Task AssertUsageAllowedAsync(UserId requester, FileId fileId);
    }
}