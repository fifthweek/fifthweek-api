namespace Fifthweek.Api.Posts.Queries
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Dapper;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class GetCreatorNewsfeedQueryHandler : IQueryHandler<GetCreatorNewsfeedQuery, IReadOnlyList<GetCreatorNewsfeedQueryResult>>
    {
        private readonly IRequesterSecurity requesterSecurity;
        private readonly IGetCreatorNewsfeedDbStatement getCreatorNewsfeedDbStatement;
        private readonly IFileInformationAggregator fileInformationAggregator;

        public async Task<IReadOnlyList<GetCreatorNewsfeedQueryResult>> HandleAsync(GetCreatorNewsfeedQuery query)
        {
            query.AssertNotNull("query");

            // In future, this query will need to allow users who are subscribed to this creator too. This will require a 
            // IsAuthenticatedAs || IsSubscribedTo authorization.
            await this.requesterSecurity.AuthenticateAsAsync(query.Requester, query.RequestedUserId);
            await this.requesterSecurity.AssertInRoleAsync(query.Requester, FifthweekRole.Creator);

            var posts = await this.getCreatorNewsfeedDbStatement.ExecuteAsync(
              query.RequestedUserId,
              DateTime.UtcNow,
              query.StartIndex,
              query.Count);

            var result = new List<GetCreatorNewsfeedQueryResult>();
            foreach (var post in posts)
            {
                FileInformation file = null;
                if (post.FileId != null)
                {
                    file = await this.fileInformationAggregator.GetFileInformationAsync(
                        query.RequestedUserId,
                        post.FileId,
                        FilePurposes.PostFile);
                }

                FileInformation image = null;
                if (post.ImageId != null)
                {
                    image = await this.fileInformationAggregator.GetFileInformationAsync(
                       query.RequestedUserId,
                       post.ImageId,
                       FilePurposes.PostImage);
                }

                var completePost = new GetCreatorNewsfeedQueryResult(
                    post.PostId,
                    post.ChannelId,
                    post.CollectionId,
                    post.Comment,
                    file,
                    file == null ? null : new FileSourceInformation(post.FileName, post.FileExtension, post.FileSize ?? 0),
                    image,
                    image == null ? null : new FileSourceInformation(post.ImageName, post.ImageExtension, post.ImageSize ?? 0),
                    post.LiveDate);

                result.Add(completePost);
            }

            return result;
        }
    }
}