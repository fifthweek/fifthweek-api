namespace Fifthweek.Api.Channels
{
    using System.Threading.Tasks;

    using Dapper;

    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;

    [AutoConstructor]
    public partial class DeleteChannelDbStatement : IDeleteChannelDbStatement
    {
        private static readonly string DeleteSql = string.Format(
            @"
            DELETE FROM {0}
            WHERE {1} = @ChannelId",
            Channel.Table,
            Channel.Fields.Id);

        private readonly IFifthweekDbContext databaseContext;

        public Task ExecuteAsync(ChannelId channelId)
        {
            channelId.AssertNotNull("channelId");
            var channelIdParameter = new
            {
                ChannelId = channelId.Value
            };

            return this.databaseContext.Database.Connection.ExecuteAsync(DeleteSql, channelIdParameter);
        }
    }
}