namespace Fifthweek.Payments.Services
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Azure;
    using Fifthweek.Azure;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Shared;

    [AutoConstructor]
    public partial class RequestProcessPaymentsService : IRequestProcessPaymentsService
    {
        private readonly IQueueService queueService;

        public Task ExecuteAsync()
        {
            return this.queueService.AddMessageToQueueAsync(
                Constants.RequestProcessPaymentsQueueName,
                ProcessPaymentsMessage.Default,
                null,
                Constants.PaymentProcessingDefaultMessageDelay);
        }

        public Task ExecuteImmediatelyAsync()
        {
            return this.queueService.AddMessageToQueueAsync(
                Constants.RequestProcessPaymentsQueueName,
                ProcessPaymentsMessage.Default,
                null,
                TimeSpan.Zero);
        }
    }
}