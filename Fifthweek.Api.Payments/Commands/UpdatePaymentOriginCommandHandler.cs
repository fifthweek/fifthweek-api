namespace Fifthweek.Api.Payments.Commands
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments;
    using Fifthweek.Payments.Services.Credit;
    using Fifthweek.Payments.Services.Credit.Stripe;
    using Fifthweek.Payments.Stripe;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class UpdatePaymentOriginCommandHandler : ICommandHandler<UpdatePaymentOriginCommand>
    {
        private readonly IRequesterSecurity requesterSecurity;

        private readonly ISetUserPaymentOriginDbStatement setUserPaymentOrigin;
        private readonly IGetUserPaymentOriginDbStatement getUserPaymentOrigin;
        private readonly ICreateStripeCustomer createStripeCustomer;
        private readonly IUpdateStripeCustomerCreditCard updateStripeCustomerCreditCard;

        public async Task HandleAsync(UpdatePaymentOriginCommand command)
        {
            command.AssertNotNull("command");

            await this.requesterSecurity.AuthenticateAsAsync(command.Requester, command.UserId);

            var userType = await this.requesterSecurity.GetUserTypeAsync(command.Requester);

            var origin = await this.getUserPaymentOrigin.ExecuteAsync(command.UserId);

            var stripeCustomerId = origin == null ? null : origin.StripeCustomerId;

            if (command.StripeToken != null)
            {
                if (stripeCustomerId != null)
                {
                    // If exists, update customer in stripe.
                    await this.updateStripeCustomerCreditCard.ExecuteAsync(command.UserId, stripeCustomerId, command.StripeToken.Value, userType);
                }
                else
                {
                    // If not exists, create customer in stripe.
                    stripeCustomerId = await this.createStripeCustomer.ExecuteAsync(command.UserId, command.StripeToken.Value, userType);
                }
            }

            // Update origin.
            await this.setUserPaymentOrigin.ExecuteAsync(
                command.UserId,
                stripeCustomerId,
                command.CountryCode,
                command.CreditCardPrefix,
                command.IpAddress);
        }
    }
}