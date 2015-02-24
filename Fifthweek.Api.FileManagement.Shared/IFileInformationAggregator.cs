namespace Fifthweek.Api.FileManagement.Shared
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;

    public interface IFileInformationAggregator
    {
        Task<FileInformation> GetFileInformationAsync(UserId fileOwnerId, FileId fileId, string filePurpose);
    }
}