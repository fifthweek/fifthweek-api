﻿namespace Fifthweek.Api.Posts.Queries
{
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    using Fifthweek.Api.Azure;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    using Newtonsoft.Json;

    [AutoConstructor]
    public partial class GetPostQueryHandler : IQueryHandler<GetPostQuery, GetPostQueryResult>
    {
        private readonly IRequesterSecurity requesterSecurity;
        private readonly IPostSecurity postSecurity;
        private readonly IGetPostDbStatement getPostDbStatement;
        private readonly IGetAccessSignatureExpiryInformation getAccessSignatureExpiryInformation;
        private readonly IRequestFreePost requestFreePost;
        private readonly IGetPostQueryAggregator getPostQueryAggregator;

        public async Task<GetPostQueryResult> HandleAsync(GetPostQuery query)
        {
            query.AssertNotNull("query");

            var requestingUserId = await this.requesterSecurity.TryAuthenticateAsync(query.Requester);

            var result = await this.getPostDbStatement.ExecuteAsync(requestingUserId, query.PostId);
            if (result == null)
            {
                return null;
            }

            var access = PostSecurityResult.Denied;
            if (requestingUserId != null)
            {
                access = await this.postSecurity.IsReadAllowedAsync(requestingUserId, query.PostId, query.Timestamp);
                if (access == PostSecurityResult.Denied && query.RequestFreePost)
                {
                    await this.requestFreePost.ExecuteAsync(requestingUserId, query.PostId, query.Timestamp);
                    access = PostSecurityResult.FreePost;
                }
            }

            var expiry = this.getAccessSignatureExpiryInformation.Execute(query.Timestamp);

            return await this.getPostQueryAggregator.ExecuteAsync(result, access, expiry);
        }
    }
}