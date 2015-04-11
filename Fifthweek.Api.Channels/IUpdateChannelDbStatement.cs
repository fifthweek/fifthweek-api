namespace Fifthweek.Api.Channels
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Channels.Shared;

    public interface IUpdateChannelDbStatement
    {
        Task ExecuteAsync(
            ChannelId channelId,
            ValidChannelName name,
            ValidChannelDescription description,
            ValidChannelPriceInUsCentsPerWeek price, 
            bool isVisibleToNonSubscribers, 
            DateTime now);
    }
}