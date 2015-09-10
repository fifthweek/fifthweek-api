namespace Fifthweek.Api.Blogs
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;

    public interface IGetBlogChannelsAndQueuesDbStatement
    {
        Task<GetBlogChannelsAndQueuesDbStatement.GetBlogChannelsAndQueuesDbResult> ExecuteAsync(UserId userId);
    }
}