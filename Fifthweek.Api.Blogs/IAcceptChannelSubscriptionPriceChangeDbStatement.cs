namespace Fifthweek.Api.Blogs
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;

    public interface IAcceptChannelSubscriptionPriceChangeDbStatement
    {
        Task ExecuteAsync(
            UserId userId, 
            ChannelId channelId,
            ValidAcceptedChannelPriceInUsCentsPerWeek acceptedPrice,
            DateTime now);
    }
}