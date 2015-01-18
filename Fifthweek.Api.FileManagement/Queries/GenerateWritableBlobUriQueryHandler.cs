namespace Fifthweek.Api.FileManagement.Queries
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Azure;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.CodeGeneration;

    [AutoConstructor]
    public partial class GenerateWritableBlobUriQueryHandler : IQueryHandler<GenerateWritableBlobUriQuery, string>
    {
        private readonly IBlobService blobService;
        private readonly IBlobLocationGenerator blobLocationGenerator;
        private readonly IFileSecurity fileSecurity;

        public async Task<string> HandleAsync(GenerateWritableBlobUriQuery query)
        {
            query.AssertNotNull("query");

            UserId userId;
            query.Requester.AssertAuthenticated(out userId);

            await this.fileSecurity.AssertUsageAllowedAsync(userId, query.FileId);

            var blobLocation = this.blobLocationGenerator.GetBlobLocation(userId, query.FileId, query.Purpose);
            var url = await this.blobService.GetBlobSasUriForWritingAsync(blobLocation.ContainerName, blobLocation.BlobName);

            return url;
        }
    }
}