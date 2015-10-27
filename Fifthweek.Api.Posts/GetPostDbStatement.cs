namespace Fifthweek.Api.Posts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Dapper;

    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Posts.Queries;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class GetPostDbStatement : IGetPostDbStatement
    {
        private static readonly string SqlFilter = string.Format(@"
            WHERE post.{0} = @PostId;",
            Post.Fields.Id);

        private static readonly string GetFilesSql = string.Format(@"
            SELECT 
                f.{0} AS FileId,
                f.{1} as FileName, 
                f.{2}, 
                f.{3} as FileSize, 
                f.{4}, 
                f.{5},
                f.{10}
            FROM {6} pf INNER JOIN {7} f ON pf.{8}=f.{0}
            WHERE pf.{9} = @PostId",
            File.Fields.Id,
            File.Fields.FileNameWithoutExtension,
            File.Fields.FileExtension,
            File.Fields.BlobSizeBytes,
            File.Fields.RenderWidth,
            File.Fields.RenderHeight,
            PostFile.Table,
            File.Table,
            PostFile.Fields.FileId,
            PostFile.Fields.PostId,
            File.Fields.Purpose);

        private readonly IFifthweekDbConnectionFactory connectionFactory;

        public async Task<GetPostDbResult> ExecuteAsync(UserId requestorId, PostId postId)
        {
            postId.AssertNotNull("postId");

            var parameters = new
            {
                PostId = postId.Value,
                RequestorId = requestorId == null ? null : (Guid?)requestorId.Value,
            };

            var query = new StringBuilder();
            query.Append(GetNewsfeedDbStatement.GetSqlStart(requestorId, GetNewsfeedDbStatement.SqlQuerySource.FullPost));
            query.Append(SqlFilter);
            query.Append(GetFilesSql);

            using (var connection = this.connectionFactory.CreateConnection())
            {
                using (var multi = await connection.QueryMultipleAsync(query.ToString(), parameters))
                {
                    var post = (await multi.ReadAsync<NewsfeedPost.Builder>()).SingleOrDefault();

                    if (post == null)
                    {
                        return null;
                    }

                    ProcessNewsfeedResults(post);

                    var files = (await multi.ReadAsync<GetPostDbResult.PostFileDbResult.Builder>()).ToList();

                    return new GetPostDbResult(
                        post.Build(), 
                        files.Select(v => v.Build()).ToList());
                }
            }
        }

        private static void ProcessNewsfeedResults(NewsfeedPost.Builder entity)
        {
            entity.LiveDate = DateTime.SpecifyKind(entity.LiveDate, DateTimeKind.Utc);
            entity.CreationDate = DateTime.SpecifyKind(entity.CreationDate, DateTimeKind.Utc);
        }
    }
}