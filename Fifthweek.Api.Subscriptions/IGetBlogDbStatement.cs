namespace Fifthweek.Api.Subscriptions
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Subscriptions.Shared;

    public interface IGetBlogDbStatement
    {
        Task<GetBlogDbStatement.GetSubscriptionDataDbResult> ExecuteAsync(BlogId blogId);
    }
}