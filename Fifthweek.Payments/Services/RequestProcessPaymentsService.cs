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
        public static readonly TimeSpan ProcessingDelay = TimeSpan.FromSeconds(30);

        private readonly IQueueService queueService;

        public Task ExecuteAsync()
        {
            return this.queueService.AddMessageToQueueAsync(
                Constants.RequestProcessPaymentsQueueName,
                new ProcessPaymentsMessage(),
                null,
                ProcessingDelay);
        }
    }
}