namespace Fifthweek.Api.Collections
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Collections.Queries;
    using Fifthweek.Api.Identity.Shared.Membership;

    public interface IGetChannelsAndCollectionsDbStatement
    {
        Task<ChannelsAndCollections> ExecuteAsync(UserId userId);
    }
}