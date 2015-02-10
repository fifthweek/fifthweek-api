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

        private readonly IFifthweekDbContext databaseContext;

        public Task<int> ExecuteAsync()
        {
            return this.databaseContext.Database.Connection.ExecuteScalarAsync<int>(Sql);
        }
    }
}