namespace Fifthweek.Api.FileManagement.Controllers
{
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Description;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Commands;
    using Fifthweek.Api.FileManagement.Queries;
    using Fifthweek.Api.Identity.OAuth;
    using Fifthweek.CodeGeneration;

    [AutoConstructor]
    [RoutePrefix("files")]
    public partial class FileUploadController : ApiController
    {
        private readonly IGuidCreator guidCreator;

        private readonly ICommandHandler<InitiateFileUploadCommand> initiateFileUpload;

        private readonly IQueryHandler<GenerateWritableBlobUriQuery, string> generateWritableBlobUri;

        private readonly ICommandHandler<CompleteFileUploadCommand> completeFileUpload;

        private readonly IUserContext userContext;

        [Authorize]
        [ResponseType(typeof(GrantedUpload))]
        [Route("uploadRequests")]
        public async Task<GrantedUpload> PostUploadRequestAsync(UploadRequest data)
        {
            var fileId = new FileId(this.guidCreator.CreateSqlSequential());
            var authenticatedUserId = this.userContext.GetUserId();
            
            await this.initiateFileUpload.HandleAsync(new InitiateFileUploadCommand(authenticatedUserId, fileId, data.FilePath, data.Purpose));
            var uri = await this.generateWritableBlobUri.HandleAsync(new GenerateWritableBlobUriQuery(authenticatedUserId, fileId, data.Purpose));

            return new GrantedUpload(fileId.Value.EncodeGuid(), uri);
        }

        [Authorize]
        [Route("uploadCompleteNotifications")]
        public async Task PostUploadCompleteNotificationAsync(string fileId)
        {
            var parsedFileId = new FileId(fileId.DecodeGuid());
            var authenticatedUserId = this.userContext.GetUserId();
            await this.completeFileUpload.HandleAsync(new CompleteFileUploadCommand(authenticatedUserId, parsedFileId));
        }
    }
}