using System.Threading.Tasks;
using Fifthweek.Api.Identity.Membership;

namespace Fifthweek.Api.Subscriptions
{
    public class SubscriptionSecurity : ISubscriptionSecurity
    {
        public Task<bool> CanUpdateAsync(UserId userId, SubscriptionId subscriptionId)
        {
            throw new System.NotImplementedException();
        }
    }
}