namespace Fifthweek.Payments.Services
{
    using System;

    using Fifthweek.Api.Identity.Shared.Membership;

    public class PaymentProcessingException : Exception
    {
        public PaymentProcessingException(Exception innerException, UserId subscriberId, UserId creatorId)
            : base(string.Format("Failed to process payments for subscriber '{0}' and creator '{1}'.", subscriberId, creatorId), innerException)
        {
            this.SubscriberId = subscriberId;
            this.CreatorId = creatorId;
        }

        public PaymentProcessingException(string message, UserId subscriberId, UserId creatorId)
            : base(message)
        {
            this.SubscriberId = subscriberId;
            this.CreatorId = creatorId;
        }

        public UserId SubscriberId { get; private set; }

        public UserId CreatorId { get; private set; }
    }
}