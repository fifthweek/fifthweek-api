namespace Fifthweek.Api.FileManagement.Queries
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Azure;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;

    [AutoConstructor]
    public partial class GenerateWritableBlobUriQueryHandler : IQueryHandler<GenerateWritableBlobUriQuery, BlobSharedAccessInformation>
    {
        private readonly IBlobService blobService;
        private readonly IBlobLocationGenerator blobLocationGenerator;
        private readonly IFileSecurity fileSecurity;
        private readonly IRequesterSecurity requesterSecurity;

        public async Task<BlobSharedAccessInformation> HandleAsync(GenerateWritableBlobUriQuery query)
        {
            query.AssertNotNull("query");

            var userId = await this.requesterSecurity.AuthenticateAsync(query.Requester);

            await this.fileSecurity.AssertReferenceAllowedAsync(userId, query.FileId);

            var blobLocation = this.blobLocationGenerator.GetBlobLocation(userId, query.FileId, query.Purpose);
            var expiry = DateTime.UtcNow.Add(FileManagement.Constants.WriteSignatureTimeSpan);
            return await this.blobService.GetBlobSharedAccessInformationForWritingAsync(blobLocation.ContainerName, blobLocation.BlobName, expiry);
        }
    }
}