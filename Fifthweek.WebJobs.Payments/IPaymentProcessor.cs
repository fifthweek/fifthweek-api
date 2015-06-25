namespace Fifthweek.WebJobs.Payments
{
    using System.Threading;
    using System.Threading.Tasks;

    using Fifthweek.Payments.Shared;
    using Fifthweek.WebJobs.Shared;

    public interface IPaymentProcessor
    {
        Task ProcessPaymentsAsync(
            ProcessPaymentsMessage message,
            ILogger logger,
            CancellationToken cancellationToken);

        Task HandlePoisonMessageAsync(
            string message,
            ILogger logger,
            CancellationToken cancellationToken);
    }
}