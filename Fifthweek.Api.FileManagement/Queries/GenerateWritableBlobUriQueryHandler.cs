namespace Fifthweek.Api.FileManagement.Queries
{
    using System.Security;
    using System.Threading.Tasks;

    using Fifthweek.Api.Azure;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;

    [AutoConstructor]
    public partial class GenerateWritableBlobUriQueryHandler : IQueryHandler<GenerateWritableBlobUriQuery, string>
    {
        private readonly IBlobService blobService;

        private readonly IBlobLocationGenerator blobLocationGenerator;

        private readonly IFileSecurity fileSecurity;

        public async Task<string> HandleAsync(GenerateWritableBlobUriQuery query)
        {
            query.AssertNotNull("query");
            query.AuthenticatedUserId.AssertAuthenticated();
            await this.fileSecurity.AssertFileBelongsToUserAsync(query.AuthenticatedUserId, query.FileId);

            var blobLocation = this.blobLocationGenerator.GetBlobLocation(query.AuthenticatedUserId, query.FileId, query.Purpose);
            var url = await this.blobService.GetBlobSasUriForWritingAsync(blobLocation.ContainerName, blobLocation.BlobName);
            return url;
        }
    }
}