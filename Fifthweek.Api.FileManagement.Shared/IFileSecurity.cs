namespace Fifthweek.Api.FileManagement.Shared
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;

    public interface IFileSecurity
    {
        Task<bool> IsReferenceAllowedAsync(UserId requester, FileId fileId);

        Task AssertReferenceAllowedAsync(UserId requester, FileId fileId);
    }
}