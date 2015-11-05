namespace Fifthweek.Api.Blogs
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Channels.Shared;

    public interface IGetIsTestUserChannelDbStatement
    {
        Task<bool> ExecuteAsync(ChannelId channelId);
    }
}