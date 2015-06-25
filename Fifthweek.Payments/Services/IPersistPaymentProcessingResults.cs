namespace Fifthweek.Payments.Services
{
    using System.Threading.Tasks;

    public interface IPersistPaymentProcessingResults
    {
        Task ExecuteAsync(PaymentProcessingData data, PaymentProcessingResults results);
    }
}