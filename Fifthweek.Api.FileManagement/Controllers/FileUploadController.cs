namespace Fifthweek.Api.FileManagement.Controllers
{
    using System;
    using System.IO;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Description;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Commands;
    using Fifthweek.Api.FileManagement.Queries;

    using Microsoft.WindowsAzure;
    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Blob;

    [RoutePrefix("files")]
    public class FileUploadController : ApiController
    {
        private readonly IGuidCreator guidCreator;

        private readonly ICommandHandler<InitiateFileUploadRequestCommand> initiateFileUploadRequest;

        private readonly IQueryHandler<GetSharedAccessSignatureUriQuery, string> getSharedAccessSignatureUri;

        private readonly ICommandHandler<FileUploadCompleteCommand> fileUploadComplete;

        public FileUploadController(
            IGuidCreator guidCreator,
            ICommandHandler<InitiateFileUploadRequestCommand> initiateFileUploadRequest,
            IQueryHandler<GetSharedAccessSignatureUriQuery, string> getSharedAccessSignatureUri,
            ICommandHandler<FileUploadCompleteCommand> fileUploadComplete)
        {
            this.guidCreator = guidCreator;
            this.initiateFileUploadRequest = initiateFileUploadRequest;
            this.getSharedAccessSignatureUri = getSharedAccessSignatureUri;
            this.fileUploadComplete = fileUploadComplete;
        }

        [Authorize]
        [ResponseType(typeof(GrantedUpload))]
        [Route("uploadRequests")]
        public async Task<GrantedUpload> PostUploadRequestAsync(UploadRequest data)
        {
            var fileId = new FileId(this.guidCreator.CreateSqlSequential());

            await this.initiateFileUploadRequest.HandleAsync(new InitiateFileUploadRequestCommand(fileId));
            var uri = await this.getSharedAccessSignatureUri.HandleAsync(new GetSharedAccessSignatureUriQuery(fileId));

            return new GrantedUpload(fileId.Value, uri);
        }

        [Authorize]
        [Route("uploadCompleteNotifications")]
        public async Task PostUploadCompleteNotificationAsync(Guid fileId)
        {
            await this.fileUploadComplete.HandleAsync(new FileUploadCompleteCommand(new FileId(fileId)));
        }
    }
}