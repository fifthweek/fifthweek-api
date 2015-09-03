namespace Fifthweek.Api.Collections
{
    using System.Threading.Tasks;

    using Dapper;

    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class DeleteCollectionDbStatement : IDeleteCollectionDbStatement
    {
        private static readonly string DeleteSql = string.Format(
            @"
            DELETE c FROM {7} c INNER JOIN {0} p ON c.{8} = p.{6} WHERE p.{1} = @CollectionId;
            DELETE l FROM {4} l INNER JOIN {0} p ON l.{5} = p.{6} WHERE p.{1} = @CollectionId;
            DELETE FROM {0} WHERE {1} = @CollectionId;
            DELETE FROM {2} WHERE {3} = @CollectionId;",
            Post.Table,
            Post.Fields.CollectionId,
            Collection.Table,
            Collection.Fields.Id,
            Like.Table,
            Like.Fields.PostId,
            Post.Fields.Id,
            Comment.Table,
            Comment.Fields.PostId);

        private readonly IFifthweekDbConnectionFactory connectionFactory;

        public async Task ExecuteAsync(CollectionId collectionId)
        {
            collectionId.AssertNotNull("collectionId");
            var collectionIdParameter = new
            {
                CollectionId = collectionId.Value
            };

            using (var connection = this.connectionFactory.CreateConnection())
            {
                await connection.ExecuteAsync(DeleteSql, collectionIdParameter);
            }
        }
    }
}