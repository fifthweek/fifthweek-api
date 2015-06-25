namespace Fifthweek.Payments.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Dapper;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Snapshots;
    using Fifthweek.CodeGeneration;

    [AutoConstructor]
    public partial class GetAllSubscribersDbStatement : IGetAllSubscribersDbStatement
    {
        private static readonly string Sql = string.Format(
            @"SELECT DISTINCT {1} FROM {0}",
            SubscriberChannelsSnapshot.Table,
            SubscriberChannelsSnapshot.Fields.SubscriberId);

        private readonly IFifthweekDbConnectionFactory connectionFactory;

        public async Task<IReadOnlyList<UserId>> ExecuteAsync()
        {
            using (var connection = this.connectionFactory.CreateConnection())
            {
                var result = await connection.QueryAsync<Guid>(Sql);
                return result.Select(v => new UserId(v)).ToList();
            }
        }
    }
}