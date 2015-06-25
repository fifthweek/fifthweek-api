namespace Fifthweek.Payments.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;

    public interface IProcessPaymentsForSubscriber
    {
        Task ExecuteAsync(UserId subscriberId, DateTime endTimeExclusive, List<PaymentProcessingException> errors);
    }
}