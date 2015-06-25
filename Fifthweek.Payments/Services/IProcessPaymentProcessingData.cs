namespace Fifthweek.Payments.Services
{
    using System.Threading.Tasks;

    public interface IProcessPaymentProcessingData
    {
        Task<PaymentProcessingResults> ExecuteAsync(PaymentProcessingData data);
    }
}