namespace Fifthweek.Api.FileManagement
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;

    public interface IAddNewFileDbStatement
    {
        Task ExecuteAsync(
            Shared.FileId fileId,
            UserId userId,
            ChannelId channelId,
            string fileNameWithoutExtension,
            string fileExtension,
            string purpose,
            DateTime timeStamp);
    }
}