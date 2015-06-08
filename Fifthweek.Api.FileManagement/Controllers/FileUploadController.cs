namespace Fifthweek.Api.FileManagement.Controllers
{
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Description;

    using Fifthweek.Api.Azure;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Commands;
    using Fifthweek.Api.FileManagement.Queries;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    [RoutePrefix("files")]
    public partial class FileUploadController : ApiController
    {
        private readonly IGuidCreator guidCreator;

        private readonly ICommandHandler<InitiateFileUploadCommand> initiateFileUpload;

        private readonly IQueryHandler<GenerateWritableBlobUriQuery, BlobSharedAccessInformation> generateWritableBlobUri;

        private readonly ICommandHandler<CompleteFileUploadCommand> completeFileUpload;

        private readonly IRequesterContext requesterContext;

        [ResponseType(typeof(GrantedUpload))]
        [Route("uploadRequests")]
        public async Task<GrantedUpload> PostUploadRequestAsync(UploadRequest data)
        {
            data.AssertBodyProvided("data");
            data.FilePath.AssertBodyProvided("filePath");
            data.Purpose.AssertBodyProvided("purpose");

            var channelId = data.ChannelId == null ? null : new ChannelId(data.ChannelId.DecodeGuid());
            var fileId = new FileId(this.guidCreator.CreateSqlSequential());
            var requester = this.requesterContext.GetRequester();

            await this.initiateFileUpload.HandleAsync(new InitiateFileUploadCommand(requester, channelId, fileId, data.FilePath, data.Purpose));
            var accessInformation = await this.generateWritableBlobUri.HandleAsync(new GenerateWritableBlobUriQuery(requester, channelId, fileId, data.Purpose));

            return new GrantedUpload(fileId, accessInformation);
        }

        [Route("uploadCompleteNotifications")]
        public async Task PostUploadCompleteNotificationAsync(UploadCompleteNotification data)
        {
            data.AssertBodyProvided("data");
            data.FileId.AssertBodyProvided("fileId");

            var channelId = data.ChannelId == null ? null : new ChannelId(data.ChannelId.DecodeGuid());
            var fileId = new FileId(data.FileId.DecodeGuid());
            var requester = this.requesterContext.GetRequester();
            await this.completeFileUpload.HandleAsync(new CompleteFileUploadCommand(requester, channelId, fileId));
        }
    }
}