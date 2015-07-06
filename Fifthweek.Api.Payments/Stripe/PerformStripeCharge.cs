namespace Fifthweek.Api.Payments.Stripe
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Shared;

    public class PerformStripeCharge : IPerformStripeCharge
    {
        //https://stripe.com/docs/tutorials/charges

        public async Task<string> ExecuteAsync(string stripeCustomerId, AmountInUsCents amount, UserId userId, Guid transactionReference, string taxamoTransactionKey)
        {
            throw new NotImplementedException();
        }
    }
}