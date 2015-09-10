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
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class GetWeeklyReleaseScheduleDbStatement : IGetWeeklyReleaseScheduleDbStatement
    {
        private static readonly string Sql = string.Format(
            @"SELECT * 
            FROM    {0} 
            WHERE   {1}=@QueueId",
            WeeklyReleaseTime.Table,
            WeeklyReleaseTime.Fields.QueueId);

        private readonly IFifthweekDbConnectionFactory connectionFactory;

        public async Task<WeeklyReleaseSchedule> ExecuteAsync(QueueId queueId)
        {
            queueId.AssertNotNull("queueId");

            var parameters = new
            {
                QueueId = queueId.Value
            };

            using (var connection = this.connectionFactory.CreateConnection())
            {
                var releaseTimes = await connection.QueryAsync<WeeklyReleaseTime>(Sql, parameters);
                var hoursOfWeek = releaseTimes.Select(_ => HourOfWeek.Parse(_.HourOfWeek)).ToArray();

                if (hoursOfWeek.Length == 0)
                {
                    throw new Exception(
                        string.Format(
                            "Queue does not have any weekly release times defined. At least one should exist per collection at all times. {0}",
                            queueId));
                }

                return WeeklyReleaseSchedule.Parse(hoursOfWeek);
            }
        }
    }
}