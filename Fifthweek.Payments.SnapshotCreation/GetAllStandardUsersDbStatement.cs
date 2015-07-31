namespace Fifthweek.Payments.SnapshotCreation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Dapper;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Persistence.Snapshots;
    using Fifthweek.CodeGeneration;

    [AutoConstructor]
    public partial class GetAllStandardUsersDbStatement : IGetAllStandardUsersDbStatement
    {
        private static readonly string Sql = string.Format(
            @"SELECT DISTINCT u.{1} FROM {0} u
             WHERE u.{1} NOT IN (SELECT r.{3} FROM {2} r WHERE r.{4}='{5}')",
            FifthweekUser.Table,
            FifthweekUser.Fields.Id,
            FifthweekUserRole.Table,
            FifthweekUserRole.Fields.UserId,
            FifthweekUserRole.Fields.RoleId,
            FifthweekRole.TestUserId.ToString());

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