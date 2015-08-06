namespace Fifthweek.Payments.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Azure;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Shared;
    using Fifthweek.WebJobs.Shared;

    public interface IProcessPaymentsForSubscriber
    {
        Task ExecuteAsync(UserId subscriberId, DateTime endTimeExclusive, IKeepAliveHandler keepAliveHandler, List<PaymentProcessingException> errors);
    }
}