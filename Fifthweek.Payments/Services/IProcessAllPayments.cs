namespace Fifthweek.Payments.Services
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    using Fifthweek.Azure;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Shared;
    using Fifthweek.WebJobs.Shared;

    public interface IProcessAllPayments
    {
        Task ExecuteAsync(IKeepAliveHandler keepAliveHandler, List<PaymentProcessingException> errors, CancellationToken cancellationToken);
    }
}