namespace Fifthweek.Api.FileManagement.Queries
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Azure;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    using Constants = Fifthweek.Api.FileManagement.Shared.Constants;

    [AutoConstructor]
    public partial class GenerateWritableBlobUriQueryHandler : IQueryHandler<GenerateWritableBlobUriQuery, BlobSharedAccessInformation>
    {
        private readonly IBlobService blobService;
        private readonly IBlobLocationGenerator blobLocationGenerator;
        private readonly IFileSecurity fileSecurity;
        private readonly IRequesterSecurity requesterSecurity;
        private readonly IChannelSecurity channelSecurity;

        public async Task<BlobSharedAccessInformation> HandleAsync(GenerateWritableBlobUriQuery query)
        {
            query.AssertNotNull("query");

            var userId = await this.requesterSecurity.AuthenticateAsync(query.Requester);

            await this.fileSecurity.AssertReferenceAllowedAsync(userId, query.FileId);

            if (query.ChannelId != null)
            {
                await this.channelSecurity.AssertWriteAllowedAsync(userId, query.ChannelId);
            }

            var blobLocation = this.blobLocationGenerator.GetBlobLocation(query.ChannelId, query.FileId, query.Purpose);
            var expiry = DateTime.UtcNow.Add(Constants.WriteSignatureTimeSpan);
            return await this.blobService.GetBlobSharedAccessInformationForWritingAsync(blobLocation.ContainerName, blobLocation.BlobName, expiry);
        }
    }
}