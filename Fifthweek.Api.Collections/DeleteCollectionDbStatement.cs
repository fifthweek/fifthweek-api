namespace Fifthweek.Api.Collections
{
    using System.Threading.Tasks;

    using Dapper;

    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;

    [AutoConstructor]
    public partial class DeleteCollectionDbStatement : IDeleteCollectionDbStatement
    {
        private static readonly string DeleteSql = string.Format(
            @"
            DELETE FROM {0} WHERE {1} = @CollectionId;
            DELETE FROM {2} WHERE {3} = @CollectionId;",
            Post.Table,
            Post.Fields.CollectionId,
            Collection.Table,
            Collection.Fields.Id);

        private readonly IFifthweekDbContext databaseContext;

        public Task ExecuteAsync(CollectionId collectionId)
        {
            collectionId.AssertNotNull("collectionId");
            var collectionIdParameter = new
            {
                CollectionId = collectionId.Value
            };

            return this.databaseContext.Database.Connection.ExecuteAsync(DeleteSql, collectionIdParameter);
        }
    }
}