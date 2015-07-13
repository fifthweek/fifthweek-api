namespace Fifthweek.Payments.Stripe
{
    public interface IStripeApiKeyRepository
    {
        string GetTestApiKey();

        string GetLiveApiKey();

        string GetApiKey(UserType userType);
    }
}