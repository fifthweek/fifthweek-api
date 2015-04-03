namespace Fifthweek.Api.FileManagement.Queries
{
    using System;
    using System.Collections.Generic;
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

            var now = DateTime.UtcNow;
            var expiry = this.GetNextExpiry(now);

            // Get public files access information.
            var publicSignature = await this.blobService.GetBlobContainerSharedAccessInformationForReadingAsync(
                FileManagement.Constants.PublicFileBlobContainerName, expiry.Public);

            var privateSignatures = new List<UserAccessSignatures.PrivateAccessSignature>();
            if (query.RequestedUserId != null)
            {
                // Get requester's access information for their own files.
                var requesterContainerName = this.blobLocationGenerator.GetBlobContainerName(query.RequestedUserId);

                var requesterResult = await this.blobService.GetBlobContainerSharedAccessInformationForReadingAsync(
                    requesterContainerName, expiry.Private);

                var requesterInformation = new UserAccessSignatures.PrivateAccessSignature(query.RequestedUserId, requesterResult);

                privateSignatures.Add(requesterInformation);

                // TODO: Get subscribed creator access inforamtion.
                var subscribedCreatorsInformation = new List<UserAccessSignatures.PrivateAccessSignature>();

                privateSignatures.AddRange(subscribedCreatorsInformation);
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

        internal DateTime GetNextExpiry(DateTime now, bool isPublic)
        {
            var baseTimeSpan 
                = isPublic
                ? FileManagement.Constants.PublicReadSignatureTimeSpan
                : FileManagement.Constants.PrivateReadSignatureTimeSpan;

            var expiry = this.RoundUp(now, baseTimeSpan);

            if ((expiry - now) <= FileManagement.Constants.ReadSignatureMinimumExpiryTime)
            {
                expiry = expiry.Add(baseTimeSpan);
            }

            return expiry;
        }

        private ExpiryInformation GetNextExpiry(DateTime now)
        {
            return new ExpiryInformation(
                this.GetNextExpiry(now, true),
                this.GetNextExpiry(now, false));
        }

        private DateTime RoundUp(DateTime dt, TimeSpan d)
        {
            if (dt.Kind != DateTimeKind.Utc)
            {
                throw new InvalidOperationException("Expiry time must be in UTC");
            }

            return new DateTime(((dt.Ticks + d.Ticks - 1) / d.Ticks) * d.Ticks, DateTimeKind.Utc);
        }

        internal class ExpiryInformation
        {
            public ExpiryInformation(DateTime @public, DateTime @private)
            {
                this.Public = @public;
                this.Private = @private;
            }

            public DateTime Public { get; private set; }

            public DateTime Private { get; private set; }
        }
    }
}