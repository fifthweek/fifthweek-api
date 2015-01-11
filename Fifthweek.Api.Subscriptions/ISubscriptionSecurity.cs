using System.Threading.Tasks;
using Fifthweek.Api.Identity.Membership;

namespace Fifthweek.Api.Subscriptions
{
    public interface ISubscriptionSecurity
    {
        Task<bool> IsUpdateAllowedAsync(UserId requester, SubscriptionId subscriptionId);
    }
}