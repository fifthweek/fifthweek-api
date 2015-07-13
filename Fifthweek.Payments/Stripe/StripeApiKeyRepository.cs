namespace Fifthweek.Payments.Stripe
{
    public class StripeApiKeyRepository : IStripeApiKeyRepository
    {
        public string GetTestApiKey()
        {
            return StripeConfiguration.GetTestApiKey();
        }

        public string GetLiveApiKey()
        {
            return StripeConfiguration.GetLiveApiKey();
        }

        public string GetApiKey(UserType userType)
        {
            return userType == UserType.StandardUser ? this.GetLiveApiKey() : this.GetTestApiKey();
        }
    }
}