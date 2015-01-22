namespace Fifthweek.Api.Collections
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Collections.Queries;
    using Fifthweek.Api.Identity.Membership;

    public interface IGetChannelsAndCollectionsDbStatement
    {
        Task<ChannelsAndCollections> ExecuteAsync(UserId userId);
    }
}