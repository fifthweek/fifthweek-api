namespace Fifthweek.Api.Availability
{
    using System;
    using System.Globalization;
    using System.Threading.Tasks;

    using Fifthweek.Api.Availability.Queries;
    using Fifthweek.Api.Core;
    using Fifthweek.Azure;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Shared;

    using Microsoft.Practices.EnterpriseLibrary.TransientFaultHandling;

    using Constants = Fifthweek.Azure.Constants;

    [AutoConstructor]
    public partial class TestPaymentsAvailabilityStatement : ITestPaymentsAvailabilityStatement
    {
        public static readonly TimeSpan PaymentsUnavailableTimeSpan = TimeSpan.FromTicks(Payments.Shared.Constants.PaymentProcessingDefaultMessageDelay.Ticks * 2);
        public static readonly TimeSpan RepeatRestartAttemptTimeSpan = TimeSpan.FromMinutes(30);
        public static readonly TimeSpan ProcessingTimePerSubscriberWarningTimeSpan = TimeSpan.FromSeconds(1);

        private readonly IExceptionHandler exceptionHandler;
        private readonly ITransientErrorDetectionStrategy transientErrorDetectionStrategy;
        private readonly ICloudStorageAccount cloudStorageAccount;
        private readonly ITimestampCreator timestampCreator;
        private readonly IRequestProcessPaymentsService requestProcessPayments;
        private readonly ILastPaymentsRestartTimeContainer lastPaymentsRestartTimeContainer;

        public async Task<bool> ExecuteAsync()
        {
            bool result = false;
            try
            {
                var now = this.timestampCreator.Now();

                var blobClient = this.cloudStorageAccount.CreateCloudBlobClient();
                var leaseContainer = blobClient.GetContainerReference(Constants.AzureLeaseObjectsContainerName);
                var blob = leaseContainer.GetBlockBlobReference(Fifthweek.Payments.Shared.Constants.ProcessPaymentsLeaseObjectName);
                await blob.FetchAttributesAsync();

                string startTimestampString, endTimestampString, renewCountString;

                if (blob.Metadata.TryGetValue(Constants.LeaseStartTimestampMetadataKey, out startTimestampString)
                    && blob.Metadata.TryGetValue(Constants.LeaseEndTimestampMetadataKey, out endTimestampString)
                    && blob.Metadata.TryGetValue(Constants.LeaseRenewCountMetadataKey, out renewCountString))
                {
                    var startTime = startTimestampString.FromIso8601String();
                    var endTime = endTimestampString.FromIso8601String();
                    var renewCount = int.Parse(renewCountString);

                    if ((now - startTime) >= PaymentsUnavailableTimeSpan)
                    {
                        await this.AttemptPaymentProcessingRestartIfRequired(now, "Attempted to restart payments processing.");
                    }
                    else
                    {
                        result = true;
                    }
                }
                else
                {
                    await this.AttemptPaymentProcessingRestartIfRequired(now, null);
                }
            }
            catch (Exception t)
            {
                if (this.transientErrorDetectionStrategy.IsTransient(t))
                {
                    this.exceptionHandler.ReportExceptionAsync(
                        new TransientErrorException("A transient error occurred while checking payments processing.", t));
                }
                else
                {
                    this.exceptionHandler.ReportExceptionAsync(t);
                }
            }

            return result;
        }


        private async Task AttemptPaymentProcessingRestartIfRequired(DateTime now, string warningMessage)
        {
            var localLastRestartTime = this.lastPaymentsRestartTimeContainer.LastRestartTime;
            if ((now - localLastRestartTime) >= RepeatRestartAttemptTimeSpan)
            {
                this.lastPaymentsRestartTimeContainer.LastRestartTime = now;

                // Enqueue process payments message to try and re-start the service.
                await this.requestProcessPayments.ExecuteImmediatelyAsync();

                if (warningMessage != null)
                {
                    this.exceptionHandler.ReportExceptionAsync(new WarningException(warningMessage));
                }
            }
        }
    }
}