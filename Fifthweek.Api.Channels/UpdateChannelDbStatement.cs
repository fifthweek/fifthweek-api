namespace Fifthweek.Api.Channels
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;

    using Dapper;

    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.SnapshotCreation;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class UpdateChannelDbStatement : IUpdateChannelDbStatement
    {
        private static readonly string UpdateStatement = string.Format(
            @"
            UPDATE {0} 
            SET 
                {1} = @Name, 
                {2} = @Price,
                {4} = @IsVisibleToNonSubscribers,
                {5} =
                (
                    CASE
                        WHEN 
                            {2} != @Price
                        THEN
                            @PriceLastSetDate
                        ELSE
                            {5}
                    END
                )
                WHERE {6}=@Id",
            Channel.Table,
            Channel.Fields.Name,
            Channel.Fields.Price,
            Channel.Fields.Id,
            Channel.Fields.IsVisibleToNonSubscribers,
            Channel.Fields.PriceLastSetDate,
            Channel.Fields.Id,
            Channel.Fields.BlogId);

        private readonly IFifthweekDbConnectionFactory connectionFactory;
        private readonly IRequestSnapshotService requestSnapshot;

        public async Task ExecuteAsync(
            UserId userId,
            ChannelId channelId,
            ValidChannelName name,
            ValidChannelPrice price, 
            bool isVisibleToNonSubscribers, 
            DateTime now)
        {
            using (var transaction = TransactionScopeBuilder.CreateAsync())
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
                            Price = price.Value,
                            PriceLastSetDate = now
                        });
                }

                await this.requestSnapshot.ExecuteAsync(userId, SnapshotType.CreatorChannels);

                transaction.Complete();
            }
        }
    }
}