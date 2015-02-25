namespace Fifthweek.Api.Subscriptions
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Subscriptions.Shared;

    public interface IGetSubscriptionDbStatement
    {
        Task<GetSubscriptionDbStatement.GetSubscriptionDataDbResult> ExecuteAsync(SubscriptionId subscriptionid);
    }
}