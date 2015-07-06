﻿namespace Fifthweek.Api.Payments.Commands
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Payments.Stripe;
    using Fifthweek.CodeGeneration;
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
            
            var origin = await this.getUserPaymentOrigin.ExecuteAsync(command.UserId);

            var stripeCustomerId = origin == null ? null : origin.StripeCustomerId;
            
            if (stripeCustomerId != null)
            {
                // If exists, update customer in stripe.
                await this.updateStripeCustomerCreditCard.ExecuteAsync(stripeCustomerId, command.StripeToken);
            }
            else
            {
                // If not exists, create customer in stripe.
                stripeCustomerId = await this.createStripeCustomer.ExecuteAsync(command.UserId, command.StripeToken);
            }

            // Update origin.
            await this.setUserPaymentOrigin.ExecuteAsync(
                command.UserId,
                stripeCustomerId,
                command.BillingCountryCode,
                command.CreditCardPrefix,
                command.IpAddress);
        }
    }
}