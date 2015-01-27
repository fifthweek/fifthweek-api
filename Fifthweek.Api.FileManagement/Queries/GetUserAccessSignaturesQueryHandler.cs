namespace Fifthweek.Api.FileManagement.Queries
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Fifthweek.Api.Azure;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;

    [AutoConstructor]
    public partial class GetUserAccessSignaturesQueryHandler : IQueryHandler<GetUserAccessSignaturesQuery, UserAccessSignatures>
    {
        private readonly IBlobService blobService;
        private readonly IBlobLocationGenerator blobLocationGenerator;
        private readonly IRequesterSecurity requesterSecurity;

        public async Task<UserAccessSignatures> HandleAsync(GetUserAccessSignaturesQuery query)
        {
            query.AssertNotNull("query");

            if (query.RequestedUserId != null)
            {
                await this.requesterSecurity.AuthenticateAsAsync(query.Requester, query.RequestedUserId);
            }

            // Get public files access information.
            var publicSignature = await this.blobService.GetBlobContainerSharedAccessInformationForReadingAsync(
                FileManagement.Constants.PublicFileBlobContainerName);

            var privateSignatures = new List<UserAccessSignatures.PrivateAccessSignature>();
            if (query.RequestedUserId != null)
            {
                // Get requester's access information for their own files.
                var requesterContainerName = this.blobLocationGenerator.GetBlobContainerName(query.RequestedUserId);

                var requesterResult = await this.blobService.GetBlobContainerSharedAccessInformationForReadingAsync(
                    requesterContainerName);

                var requesterInformation = new UserAccessSignatures.PrivateAccessSignature(query.RequestedUserId, requesterResult);

                privateSignatures.Add(requesterInformation);

                // TODO: Get subscribed creator access inforamtion.
                var subscribedCreatorsInformation = new List<UserAccessSignatures.PrivateAccessSignature>();

                privateSignatures.AddRange(subscribedCreatorsInformation);
            }

            return new UserAccessSignatures(
                publicSignature,
                privateSignatures);
        }
    }
}