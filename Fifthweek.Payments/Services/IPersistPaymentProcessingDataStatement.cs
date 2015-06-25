namespace Fifthweek.Payments.Services
{
    using System.Threading.Tasks;

    public interface IPersistPaymentProcessingDataStatement
    {
        Task ExecuteAsync(PersistedPaymentProcessingData data);
    }
}