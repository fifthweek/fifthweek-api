namespace Fifthweek.Api.Channels
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Channels.Shared;

    public interface IDeleteChannelDbStatement
    {
        Task ExecuteAsync(ChannelId channelId); 
    }
}