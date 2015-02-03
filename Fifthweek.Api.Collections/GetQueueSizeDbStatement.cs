namespace Fifthweek.Api.Collections
{
    using System;
    using System.Threading.Tasks;

    using Dapper;

    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;

    [AutoConstructor]
    public partial class GetQueueSizeDbStatement : IGetQueueSizeDbStatement
    {
        private static readonly string Sql = string.Format(
            @"
            SELECT  COUNT(*)
            FROM    {0}
            WHERE   {1} = @CollectionId
            AND     {2} > @Now
            AND     {3} = 1",
            Post.Table, 
            Post.Fields.CollectionId, 
            Post.Fields.LiveDate, 
            Post.Fields.ScheduledByQueue);

        private readonly IFifthweekDbContext databaseContext;

        public Task<int> ExecuteAsync(CollectionId collectionId, DateTime now)
        {
            collectionId.AssertNotNull("collectionId");
            now.AssertUtc("now");

            var parameters = new
            {
                CollectionId = collectionId.Value,
                Now = now
            };

            return this.databaseContext.Database.Connection.ExecuteScalarAsync<int>(Sql, parameters);
        }
    }
}