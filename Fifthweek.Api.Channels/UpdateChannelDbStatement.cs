namespace Fifthweek.Api.Channels
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;

    using Dapper;

    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class UpdateChannelDbStatement : IUpdateChannelDbStatement
    {
        private static readonly string UpdateStatement = string.Format(
            @"
            UPDATE {0} 
            SET 
                {1} = @Name, 
                {2} = @PriceInUsCentsPerWeek,
                {3} = @Description,
                {4} = 
                (
                    CASE
                        WHEN 
                            {6} != {7}
                        THEN
                             @IsVisibleToNonSubscribers
                        ELSE
                            {4}
                    END
                ),
                {5} =
                (
                    CASE
                        WHEN 
                            {2} != @PriceInUsCentsPerWeek
                        THEN
                            @PriceLastSetDate
                        ELSE
                            {5}
                    END
                )
                WHERE {6}=@Id",
            Channel.Table,
            Channel.Fields.Name,
            Channel.Fields.PriceInUsCentsPerWeek,
            Channel.Fields.Description,
            Channel.Fields.IsVisibleToNonSubscribers,
            Channel.Fields.PriceLastSetDate,
            Channel.Fields.Id,
            Channel.Fields.BlogId);

        private readonly IFifthweekDbConnectionFactory connectionFactory;

        public async Task ExecuteAsync(
            ChannelId channelId,
            ValidChannelName name,
            ValidChannelDescription description,
            ValidChannelPriceInUsCentsPerWeek price, 
            bool isVisibleToNonSubscribers, 
            DateTime now)
        {
            using (var connection = this.connectionFactory.CreateConnection())
            {
                await connection.ExecuteAsync(
                    UpdateStatement,
                    new 
                    {
                        Id = channelId.Value,
                        IsVisibleToNonSubscribers = isVisibleToNonSubscribers,
                        Name = name.Value,
                        PriceInUsCentsPerWeek = price.Value,
                        Description = description.Value,
                        PriceLastSetDate = now
                    });
            }
        }
    }
}