namespace Fifthweek.Api.Channels
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;

    public interface IUpdateChannelDbStatement
    {
        Task ExecuteAsync(
            UserId userId,
            ChannelId channelId,
            ValidChannelName name,
            ValidChannelPrice price, 
            bool isVisibleToNonSubscribers, 
            DateTime now);
    }
}