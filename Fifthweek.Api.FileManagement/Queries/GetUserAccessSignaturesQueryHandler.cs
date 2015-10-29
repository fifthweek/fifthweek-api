namespace Fifthweek.Api.FileManagement.Queries
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Fifthweek.Api.Azure;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class GetUserAccessSignaturesQueryHandler : IQueryHandler<GetUserAccessSignaturesQuery, UserAccessSignatures>
    {
        private readonly ITimestampCreator timestampCreator;
        private readonly IGetAccessSignatureExpiryInformation getAccessSignatureExpiryInformation;
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

            var now = this.timestampCreator.Now();
            var expiry = this.getAccessSignatureExpiryInformation.Execute(now);

            // Get public files access information.
            var publicSignature = await this.blobService.GetBlobContainerSharedAccessInformationForReadingAsync(
                Shared.Constants.PublicFileBlobContainerName, expiry.Public);

            var privateSignatures = new List<UserAccessSignatures.PrivateAccessSignature>();
            if (query.RequestedUserId != null)
            {
                var channelIds = query.CreatorChannelIds.EmptyIfNull()
                    .Concat(query.SubscribedChannelIds.EmptyIfNull())
                    .Concat(query.FreeAccessChannelIds.EmptyIfNull())
                    .Distinct().ToList();

                foreach (var channelId in channelIds)
                {
                    var containerName = this.blobLocationGenerator.GetBlobContainerName(channelId);
                    var result = await this.blobService.GetBlobContainerSharedAccessInformationForReadingAsync(containerName, expiry.Private);
                    var creatorInformation = new UserAccessSignatures.PrivateAccessSignature(channelId, result);
                    privateSignatures.Add(creatorInformation);
                }
            }

            // Doing a ceiling here means that the client may wait a fraction longer
            // than required, which is fine. If we did a floor, then the client may
            // request new signatures fractionally early, and get the same set back.
            var timeToLiveSeconds = (int)Math.Ceiling((expiry.Private - now).TotalSeconds);
            return new UserAccessSignatures(
                timeToLiveSeconds,
                publicSignature,
                privateSignatures);
        }
    }
}