namespace Fifthweek.Api.Channels
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;

    public interface IDeleteChannelDbStatement
    {
        Task ExecuteAsync(UserId userId, ChannelId channelId); 
    }
}