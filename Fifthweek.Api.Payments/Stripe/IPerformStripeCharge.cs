namespace Fifthweek.Api.Payments.Stripe
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Shared;

    public interface IPerformStripeCharge
    {
        Task<string> ExecuteAsync(string stripeCustomerId, AmountInUsCents amount, UserId userId, Guid transactionReference, string taxamoTransactionKey);
    }
}