namespace Fifthweek.WebJobs.Payments
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.ExceptionServices;
    using System.Threading;
    using System.Threading.Tasks;

    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Services;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Shared;
    using Fifthweek.WebJobs.Shared;

    using Microsoft.WindowsAzure.Storage.Blob;

    [AutoConstructor]
    public partial class PaymentProcessor : IPaymentProcessor
    {
        public static readonly TimeSpan MinimumTimeBetweenPaymentProcessing =
            Fifthweek.Payments.Shared.Constants.PaymentProcessingDefaultMessageDelay.Subtract(TimeSpan.FromMinutes(5));

        private readonly IProcessAllPayments processAllPayments;
        private readonly IPaymentProcessingLeaseFactory paymentProcessingLeaseFactory;
        private readonly IRequestProcessPaymentsService requestProcessPayments;

        public async Task ProcessPaymentsAsync(
            ProcessPaymentsMessage message,
            ILogger logger,
            CancellationToken cancellationToken)
        {
            ExceptionDispatchInfo exceptionDispatchInfo = null;
            var lease = this.paymentProcessingLeaseFactory.Create(cancellationToken);
            try
            {
                if (await lease.TryAcquireLeaseAsync())
                {
                    var timeSinceLastLease = await lease.GetTimeSinceLastLeaseAsync();

                    if (timeSinceLastLease > MinimumTimeBetweenPaymentProcessing)
                    {
                        var errors = new List<PaymentProcessingException>();
                        await this.processAllPayments.ExecuteAsync(lease, errors);

                        if (errors.Count > 0)
                        {
                            foreach (var error in errors)
                            {
                                logger.Error(error);
                            }
                        }

                        await this.requestProcessPayments.ExecuteAsync();
                    }
                    else
                    {
                        logger.Warn("Skipping processing payments as last processed {0}s ago.", (int)timeSinceLastLease.TotalSeconds);
                        await lease.ReleaseLeaseAsync();
                    }
                }
                else
                {
                    logger.Warn("Failed to acquire lease to process payments due to conflict.");
                }
            }
            catch (Exception t)
            {
                exceptionDispatchInfo = ExceptionDispatchInfo.Capture(t);
                logger.Error(t);
            }

            if (lease.GetIsAcquired())
            {
                await lease.UpdateTimestampsAsync();
                await lease.ReleaseLeaseAsync();
            }

            if (exceptionDispatchInfo != null)
            {
                exceptionDispatchInfo.Throw();
            }
        }

        public async Task HandlePoisonMessageAsync(
            string message,
            ILogger logger,
            CancellationToken cancellationToken)
        {
            logger.Warn("Failed to handle process payments message.");

            // Try again after an interval.
            await this.requestProcessPayments.ExecuteAsync();
        }
    }
}