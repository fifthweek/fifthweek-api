namespace Fifthweek.Api.Collections
{
    using System;
    using System.Threading.Tasks;

    using Dapper;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;

    [AutoConstructor]
    public partial class GetQueueLowerBoundDbStatement : IGetQueueLowerBoundDbStatement
    {
        private static readonly string Sql = string.Format(
                @"SELECT {2} 
                 FROM    {0} 
                 WHERE   {1} = @CollectionId",
                Collection.Table,
                Collection.Fields.Id,
                Collection.Fields.QueueExclusiveLowerBound);

        private readonly IFifthweekDbConnectionFactory connectionFactory;

        public async Task<DateTime> ExecuteAsync(Shared.CollectionId collectionId, DateTime now)
        {
            collectionId.AssertNotNull("collectionId");
            now.AssertUtc("now");

            var parameters = new
            {
                CollectionId = collectionId.Value
            };

            using (var connection = this.connectionFactory.CreateConnection())
            {
                var queueLowerBound = await connection.ExecuteScalarAsync<DateTime?>(Sql, parameters);
                if (!queueLowerBound.HasValue)
                {
                    throw new Exception("Collection does not exist " + collectionId);
                }
                
                var queueLowerBoundUtc = DateTime.SpecifyKind(queueLowerBound.Value, DateTimeKind.Utc);

                return queueLowerBoundUtc > now ? queueLowerBoundUtc : now;
            }
        }
    }
}