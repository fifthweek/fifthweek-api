namespace Fifthweek.Payments.Services.Refunds.Stripe
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Shared;

    public interface ICreateStripeRefund
    {
        Task ExecuteAsync(
            UserId enactingUserId,
            string stripeChargeId, 
            PositiveInt totalRefundAmount, 
            RefundCreditReason reason, 
            UserType userType);
    }
}