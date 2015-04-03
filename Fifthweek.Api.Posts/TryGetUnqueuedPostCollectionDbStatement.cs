namespace Fifthweek.Api.Posts
{
    using System;
    using System.Threading.Tasks;

    using Dapper;

    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class TryGetUnqueuedPostCollectionDbStatement : ITryGetUnqueuedPostCollectionDbStatement
    {
        private static readonly string Sql = string.Format(
            @"
            SELECT  {1}
            FROM    {0}
            WHERE   {2} = @PostId
            AND     {3} = 0",
            Post.Table, 
            Post.Fields.CollectionId, 
            Post.Fields.Id, 
            Post.Fields.ScheduledByQueue);

        private readonly IFifthweekDbConnectionFactory connectionFactory;

        public async Task<CollectionId> ExecuteAsync(PostId postId)
        {
            postId.AssertNotNull("postId");

            var parameters = new
            {
                PostId = postId.Value,
            };

            using (var connection = this.connectionFactory.CreateConnection())
            {
                return await connection.ExecuteScalarAsync<CollectionId>(Sql, parameters);
            }
        }
    }
}