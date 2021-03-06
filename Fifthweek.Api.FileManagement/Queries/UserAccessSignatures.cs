﻿namespace Fifthweek.Api.FileManagement.Queries
{
    using System.Collections.Generic;

    using Fifthweek.Api.Azure;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;

    [AutoConstructor]
    public partial class UserAccessSignatures
    {
        public int TimeToLiveSeconds { get; private set; }

        public BlobContainerSharedAccessInformation PublicSignature { get; private set; }

        public IReadOnlyList<PrivateAccessSignature> PrivateSignatures { get; private set; }

        [AutoConstructor]
        public partial class PrivateAccessSignature
        {
            public ChannelId ChannelId { get; private set; }

            public BlobContainerSharedAccessInformation Information { get; private set; }
        }
    }
}