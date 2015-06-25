namespace Fifthweek.Payments.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Fifthweek.Payments.Shared;

    public interface IProcessAllPayments
    {
        Task ExecuteAsync(IKeepAliveHandler keepAliveHandler, List<PaymentProcessingException> errors);
    }
}