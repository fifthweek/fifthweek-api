namespace Fifthweek.Api.Blogs
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;

    public interface IUnsubscribeFromChannelDbStatement
    {
        Task ExecuteAsync(UserId userId, ChannelId channelId);
    }
}