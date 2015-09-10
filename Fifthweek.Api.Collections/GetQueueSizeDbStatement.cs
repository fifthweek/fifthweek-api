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
            WHERE   {1} = @QueueId
            AND     {2} > @Now",
            Post.Table, 
            Post.Fields.QueueId, 
            Post.Fields.LiveDate);

        private readonly IFifthweekDbConnectionFactory connectionFactory;

        public async Task<int> ExecuteAsync(QueueId queueId, DateTime now)
        {
            queueId.AssertNotNull("queueId");
            now.AssertUtc("now");

            var parameters = new
            {
                QueueId = queueId.Value,
                Now = now
            };

            using (var connection = this.connectionFactory.CreateConnection())
            {
                return await connection.ExecuteScalarAsync<int>(Sql, parameters);
            }
        }
    }
}