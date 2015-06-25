namespace Fifthweek.Payments.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Dapper;

    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.CodeGeneration;

    [AutoConstructor]
    public partial class GetCreatorPercentageOverrideDbStatement : IGetCreatorPercentageOverrideDbStatement
    {
        private static readonly string Sql = string.Format(
            @"SELECT TOP 1 {0}, {1} FROM {2} 
              WHERE {3} = @UserId AND ({1} IS NULL OR {1} > @Timestamp)",
            CreatorPercentageOverride.Fields.Percentage,
            CreatorPercentageOverride.Fields.ExpiryDate,
            CreatorPercentageOverride.Table,
            CreatorPercentageOverride.Fields.UserId);

        private readonly IFifthweekDbConnectionFactory connectionFactory;

        public async Task<CreatorPercentageOverrideData> ExecuteAsync(UserId userId, DateTime timestamp)
        {   
            using (var connection = this.connectionFactory.CreateConnection())
            {
                var databaseResults = await connection.QueryAsync<DbResult>(
                    Sql,
                    new
                    {
                        UserId = userId.Value,
                        Timestamp = timestamp
                    });

                var databaseResult = databaseResults.SingleOrDefault();

                if (databaseResult == null)
                {
                    return null;
                }

                var expiryDate = databaseResult.ExpiryDate == null
                                     ? (DateTime?)null
                                     : DateTime.SpecifyKind(databaseResult.ExpiryDate.Value, DateTimeKind.Utc);

                return new CreatorPercentageOverrideData(
                    databaseResult.Percentage,
                    expiryDate);
            }
        }

        private class DbResult
        {
            public decimal Percentage { get; set; }

            public DateTime? ExpiryDate { get; set; }
        }
    }
}