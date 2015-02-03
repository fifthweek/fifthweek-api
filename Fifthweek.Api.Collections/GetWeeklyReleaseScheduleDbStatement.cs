namespace Fifthweek.Api.Collections
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Dapper;

    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;

    [AutoConstructor]
    public partial class GetWeeklyReleaseScheduleDbStatement : IGetWeeklyReleaseScheduleDbStatement
    {
        private static readonly string Sql = string.Format(
            @"SELECT * 
            FROM    {0} 
            WHERE   {1}=@CollectionId",
            WeeklyReleaseTime.Table,
            WeeklyReleaseTime.Fields.CollectionId);
        
        private readonly IFifthweekDbContext databaseContext;

        public async Task<WeeklyReleaseSchedule> ExecuteAsync(CollectionId collectionId)
        {
            collectionId.AssertNotNull("collectionId");

            var parameters = new
            {
                CollectionId = collectionId.Value
            };

            var releaseTimes = await this.databaseContext.Database.Connection.QueryAsync<WeeklyReleaseTime>(Sql, parameters);
            var hoursOfWeek = releaseTimes.Select(_ => HourOfWeek.Parse(_.HourOfWeek)).ToArray();

            if (hoursOfWeek.Length == 0)
            {
                throw new Exception(string.Format(
                    "Collection does not have any weekly release times defined. At least one should exist per collection at all times. {0}",
                    collectionId));
            }

            return WeeklyReleaseSchedule.Parse(hoursOfWeek);
        }
    }
}