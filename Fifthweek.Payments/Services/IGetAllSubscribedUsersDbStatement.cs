namespace Fifthweek.Payments.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;

    public interface IGetAllSubscribedUsersDbStatement
    {
        Task<IReadOnlyList<UserId>> ExecuteAsync(IReadOnlyList<ChannelId> channelIds);
    }
}