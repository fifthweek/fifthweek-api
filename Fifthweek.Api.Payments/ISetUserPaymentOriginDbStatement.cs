namespace Fifthweek.Api.Payments
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;

    public interface ISetUserPaymentOriginDbStatement
    {
        Task ExecuteAsync(
            UserId userId,
            string stripeCustomerId,
            ValidCountryCode billingCountryCode,
            ValidCreditCardPrefix creditCardPrefix,
            ValidIpAddress ipAddress);
    }
}