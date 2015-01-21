namespace Fifthweek.Api.Collections
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Collections.Queries;
    using Fifthweek.Api.Identity.Membership;

    public interface IGetChannelsAndCollectionsDbStatement
    {
        Task<GetChannelsAndCollectionsResult> ExecuteAsync(UserId userId);
    }
}