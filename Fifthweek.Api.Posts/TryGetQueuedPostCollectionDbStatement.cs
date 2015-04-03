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
    public partial class TryGetQueuedPostCollectionDbStatement : ITryGetQueuedPostCollectionDbStatement
    {
        private static readonly string Sql = string.Format(
            @"
            SELECT  {1}
            FROM    {0}
            WHERE   {2} = @PostId
            AND     {3} > @Now
            AND     {4} = 1",
            Post.Table, 
            Post.Fields.CollectionId, 
            Post.Fields.Id, 
            Post.Fields.LiveDate,
            Post.Fields.ScheduledByQueue);

        private readonly IFifthweekDbConnectionFactory connectionFactory;

        public async Task<CollectionId> ExecuteAsync(PostId postId, DateTime now)
        {
            postId.AssertNotNull("postId");
            now.AssertUtc("now");

            var parameters = new
            {
                PostId = postId.Value,
                Now = now
            };

            using (var connection = this.connectionFactory.CreateConnection())
            {
                return await connection.ExecuteScalarAsync<CollectionId>(Sql, parameters);
            }
        }
    }
}