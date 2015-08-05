namespace Fifthweek.GarbageCollection
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Dapper;

    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;

    [AutoConstructor]
    public partial class GetAllChannelIdsDbStatement : IGetAllChannelIdsDbStatement
    {
        private static readonly string Sql = string.Format(
            @"SELECT {0} FROM {1}",
            Channel.Fields.Id,
            Channel.Table);

        private readonly IFifthweekDbConnectionFactory connectionFactory;

        public async Task<IReadOnlyList<ChannelId>> ExecuteAsync()
        {
            using (var connection = this.connectionFactory.CreateConnection())
            {
                var items = await connection.QueryAsync<Guid>(Sql);

                return items.Select(v => new ChannelId(v)).ToList();
            }
        }
    }
}