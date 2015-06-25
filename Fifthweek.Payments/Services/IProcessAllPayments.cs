namespace Fifthweek.Payments.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IProcessAllPayments
    {
        Task ExecuteAsync(List<PaymentProcessingException> errors);
    }
}