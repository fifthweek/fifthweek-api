namespace Fifthweek.Api.Collections
{
    using System;
    using System.Threading.Tasks;

    using Dapper;

    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

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

        private readonly IFifthweekDbConnectionFactory connectionFactory;

        public async Task<int> ExecuteAsync(CollectionId collectionId, DateTime now)
        {
            collectionId.AssertNotNull("collectionId");
            now.AssertUtc("now");

            var parameters = new
            {
                CollectionId = collectionId.Value,
                Now = now
            };

            using (var connection = this.connectionFactory.CreateConnection())
            {
                return await connection.ExecuteScalarAsync<int>(Sql, parameters);
            }
        }
    }
}