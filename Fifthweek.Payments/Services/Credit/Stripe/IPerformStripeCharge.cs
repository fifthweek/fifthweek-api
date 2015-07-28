namespace Fifthweek.Payments.Services.Credit.Stripe
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Payments.Stripe;

    public interface IPerformStripeCharge
    {
        Task<string> ExecuteAsync(
            string stripeCustomerId, 
            AmountInMinorDenomination amount, 
            UserId userId,
            TransactionReference transactionReference,
            string taxamoTransactionKey, 
            UserType userType);
    }
}