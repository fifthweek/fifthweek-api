namespace Fifthweek.Api.Payments
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Payments;

    public interface ISetUserPaymentOriginDbStatement
    {
        Task ExecuteAsync(
            UserId userId,
            string paymentOriginKey,
            PaymentOriginKeyType paymentOriginKeyType,
            ValidCountryCode billingCountryCode,
            ValidCreditCardPrefix creditCardPrefix,
            ValidIpAddress ipAddress);
    }
}