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
        private readonly IFifthweekDbConnectionFactory connectionFactory;

        public async Task ExecuteAsync(
            ChannelId channelId,
            ValidChannelName name,
            ValidChannelDescription description,
            ValidChannelPriceInUsCentsPerWeek price, 
            bool isVisibleToNonSubscribers, 
            DateTime now)
        {
            var channel = new Channel(channelId.Value)
            {
                IsVisibleToNonSubscribers = isVisibleToNonSubscribers,
                Name = name.Value,
                PriceInUsCentsPerWeek = price.Value,
                Description = description.Value,
                PriceLastSetDate = now
            };

            var updatedFields =
                Channel.Fields.IsVisibleToNonSubscribers |
                Channel.Fields.Name |
                Channel.Fields.PriceInUsCentsPerWeek |
                Channel.Fields.Description |
                Channel.Fields.PriceLastSetDate;

            using (var context = this.connectionFactory.CreateContext())
            {
                // Do not update visibility for the default channel: it must always be visible.
                var channelIdGuid = channelId.Value;
                var subscriptionId = await context.Channels.Where(_ => _.Id == channelIdGuid).Select(_ => _.BlogId).FirstAsync();
                if (subscriptionId == channelIdGuid)
                {
                    updatedFields &= ~Channel.Fields.IsVisibleToNonSubscribers;
                }

                await context.Database.Connection.UpdateAsync(channel, updatedFields);
            }
        }
    }
}