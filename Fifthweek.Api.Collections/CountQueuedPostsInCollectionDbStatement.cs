namespace Fifthweek.Api.Collections
{
    using System.Threading.Tasks;

    using Dapper;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;

    [AutoConstructor]
    public partial class CountQueuedPostsInCollectionDbStatement : ICountQueuedPostsInCollectionDbStatement
    {
        private static readonly string Sql = string.Format(
                @"SELECT COUNT(*) 
                FROM     {0}
                WHERE    {1}=@CollectionId
                AND      {2} IS NOT NULL",
                Post.Table,
                Post.Fields.CollectionId,
                Post.Fields.QueuePosition);

        private readonly IFifthweekDbContext databaseContext;

        public Task<int> ExecuteAsync(CollectionId collectionId)
        {
            collectionId.AssertNotNull("collectionId");

            var parameters = new
            {
                CollectionId = collectionId
            };

            return this.databaseContext.Database.Connection.ExecuteScalarAsync<int>(Sql, parameters);
        }
    }
}