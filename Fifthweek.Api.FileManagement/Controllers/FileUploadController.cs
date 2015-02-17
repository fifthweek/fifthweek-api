namespace Fifthweek.Api.FileManagement.Controllers
{
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Description;

    using Fifthweek.Api.Azure;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Commands;
    using Fifthweek.Api.FileManagement.Queries;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;

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

            var fileId = new FileId(this.guidCreator.CreateSqlSequential());
            var requester = this.requesterContext.GetRequester();

            await this.initiateFileUpload.HandleAsync(new InitiateFileUploadCommand(requester, fileId, data.FilePath, data.Purpose));
            var accessInformation = await this.generateWritableBlobUri.HandleAsync(new GenerateWritableBlobUriQuery(requester, fileId, data.Purpose));

            return new GrantedUpload(fileId, accessInformation);
        }

        [Route("uploadCompleteNotifications/{fileId}")]
        public async Task PostUploadCompleteNotificationAsync(string fileId)
        {
            fileId.AssertBodyProvided("fileId");

            var parsedFileId = new FileId(fileId.DecodeGuid());
            var requester = this.requesterContext.GetRequester();
            await this.completeFileUpload.HandleAsync(new CompleteFileUploadCommand(requester, parsedFileId));
        }
    }
}