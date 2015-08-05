namespace Fifthweek.GarbageCollection
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Fifthweek.Api.Channels.Shared;

    public interface IGetAllChannelIdsDbStatement
    {
        Task<IReadOnlyList<ChannelId>> ExecuteAsync();
    }
}