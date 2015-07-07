namespace Fifthweek.Payments.Services.Credit.Stripe
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;

    public class PerformStripeCharge : IPerformStripeCharge
    {
        //https://stripe.com/docs/tutorials/charges

        public async Task<string> ExecuteAsync(string stripeCustomerId, AmountInUsCents amount, UserId userId, Guid transactionReference, string taxamoTransactionKey)
        {
            throw new NotImplementedException();
        }
    }
}