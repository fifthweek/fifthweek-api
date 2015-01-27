namespace Fifthweek.Api.Collections
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Collections.Queries;
    using Fifthweek.Api.Identity.Membership;

    using UserId = Fifthweek.Api.Identity.Shared.Membership.UserId;

    public interface IGetChannelsAndCollectionsDbStatement
    {
        Task<ChannelsAndCollections> ExecuteAsync(UserId userId);
    }
}