namespace Fifthweek.Api.FileManagement.Shared
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;

    public interface IFileInformationAggregator
    {
        Task<FileInformation> GetFileInformationAsync(ChannelId channelId, FileId fileId, string filePurpose);
    }
}