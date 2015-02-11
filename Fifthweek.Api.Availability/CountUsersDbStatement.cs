namespace Fifthweek.Api.Availability
{
    using System.Threading.Tasks;

    using Dapper;

    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.CodeGeneration;

    [AutoConstructor]
    public partial class CountUsersDbStatement : ICountUsersDbStatement
    {
        private static readonly string Sql = string.Format(
            @"SELECT COUNT(*) FROM {0}",
            FifthweekUser.Table);

        private readonly IFifthweekDbConnectionFactory connectionFactory;

        public async Task<int> ExecuteAsync()
        {
            using (var connection = this.connectionFactory.CreateConnection())
            {
                return await connection.ExecuteScalarAsync<int>(Sql);
            }
        }
    }
}