namespace Fifthweek.Api.FileManagement
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Membership;

    using UserId = Fifthweek.Api.Identity.Shared.Membership.UserId;

    public interface IAddNewFileDbStatement
    {
        Task ExecuteAsync(
            FileId fileId,
            UserId userId,
            string fileNameWithoutExtension,
            string fileExtension,
            string purpose,
            DateTime timeStamp);
    }
}