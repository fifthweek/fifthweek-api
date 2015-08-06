namespace Fifthweek.Api.Payments.Controllers
{
    using System;
    using System.Threading.Tasks;
    using System.Web.Http;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Payments.Commands;
    using Fifthweek.Api.Payments.Queries;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Services.Administration;
    using Fifthweek.Payments.Services.Credit.Taxamo;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Shared;

    [AutoConstructor]
    [RoutePrefix("payment")]
    public partial class PaymentsController : ApiController
    {
        private readonly IRequesterContext requesterContext;

        private readonly IQueryHandler<GetCreditRequestSummaryQuery, CreditRequestSummary> getCreditRequestSummary;
        private readonly ICommandHandler<UpdatePaymentOriginCommand> updatePaymentsOrigin;
        private readonly ICommandHandler<ApplyCreditRequestCommand> applyCreditRequest;
        private readonly ICommandHandler<DeletePaymentInformationCommand> deletePaymentInformation;
        private readonly ICommandHandler<CreateCreditRefundCommand> createCreditRefund;
        private readonly ICommandHandler<CreateTransactionRefundCommand> createTransactionRefund;
        private readonly IQueryHandler<GetTransactionsQuery, GetTransactionsResult> getTransactions;
        private readonly ICommandHandler<BlockPaymentProcessingCommand> blockPaymentProcessing;
        private readonly ITimestampCreator timestampCreator;
        private readonly IGuidCreator guidCreator;

        [Route("origins/{userId}")]
        public async Task PutPaymentOriginAsync(string userId, [FromBody]PaymentOriginData data)
        {
            userId.AssertUrlParameterProvided("userId");
            data.AssertBodyProvided("data");

            var parsedData = data.Parse();
            var userIdObject = new UserId(userId.DecodeGuid());
            var requester = await this.requesterContext.GetRequesterAsync();

            await this.updatePaymentsOrigin.HandleAsync(new UpdatePaymentOriginCommand(
                requester, 
                userIdObject, 
                parsedData.StripeToken,
                parsedData.CountryCode,
                parsedData.CreditCardPrefix,
                parsedData.IpAddress));
        }

        [Route("creditRequests/{userId}")]
        public async Task PostCreditRequestAsync(string userId, [FromBody]CreditRequestData data)
        {
            userId.AssertUrlParameterProvided("userId");
            data.AssertBodyProvided("data");

            var parsedData = data.Parse();
            var userIdObject = new UserId(userId.DecodeGuid());
            var requester = await this.requesterContext.GetRequesterAsync();

            var timestamp = this.timestampCreator.Now();
            var transactionReference = new TransactionReference(this.guidCreator.CreateSqlSequential());

            await this.applyCreditRequest.HandleAsync(new ApplyCreditRequestCommand(
                requester, 
                userIdObject, 
                timestamp,
                transactionReference,
                parsedData.Amount,
                parsedData.ExpectedTotalAmount));
        }

        [Route("creditRequestSummaries/{userId}")]
        public async Task<CreditRequestSummary> GetCreditRequestSummaryAsync(
            string userId,
            [FromUri]string countryCode = null,
            [FromUri]string creditCardPrefix = null,
            [FromUri]string ipAddress = null)
        {
            userId.AssertUrlParameterProvided("userId");

            var parsedData = creditCardPrefix == null && ipAddress == null && countryCode == null 
                ? null 
                : new PaymentLocationData(countryCode, creditCardPrefix, ipAddress).Parse();

            var locationData = parsedData == null 
                ? null
                : new GetCreditRequestSummaryQuery.LocationData(parsedData.CountryCode, parsedData.CreditCardPrefix, parsedData.IpAddress);

            var userIdObject = new UserId(userId.DecodeGuid());
            var requester = await this.requesterContext.GetRequesterAsync();

            return await this.getCreditRequestSummary.HandleAsync(new GetCreditRequestSummaryQuery(
                requester, 
                userIdObject,
                locationData));
        }

        [Route("paymentInformation/{userId}")]
        public async Task DeletePaymentInformationAsync(string userId)
        {
            userId.AssertUrlParameterProvided("userId");

            var userIdObject = new UserId(userId.DecodeGuid());
            var requester = await this.requesterContext.GetRequesterAsync();

            await this.deletePaymentInformation.HandleAsync(new DeletePaymentInformationCommand(requester, userIdObject));
        }

        [Route("transactionRefunds/{transactionReference}")]
        public async Task PostTransactionRefundAsync(string transactionReference, [FromBody]TransactionRefundData data)
        {
            transactionReference.AssertUrlParameterProvided("transactionReference");
            data.AssertBodyProvided("data");

            var transactionReferenceObject = new TransactionReference(transactionReference.DecodeGuid());
            var requester = await this.requesterContext.GetRequesterAsync();
            var timestamp = this.timestampCreator.Now();

            await this.createTransactionRefund.HandleAsync(new CreateTransactionRefundCommand(
                requester, 
                transactionReferenceObject,
                timestamp,
                data.Comment));
        }

        [Route("creditRefunds/{transactionReference}")]
        public async Task PostCreditRefundAsync(string transactionReference, [FromBody]CreditRefundData data)
        {
            transactionReference.AssertUrlParameterProvided("transactionReference");
            data.AssertBodyProvided("data");

            var transactionReferenceObject = new TransactionReference(transactionReference.DecodeGuid());
            var requester = await this.requesterContext.GetRequesterAsync();
            var timestamp = this.timestampCreator.Now();
            var parsedData = data.Parse();

            await this.createCreditRefund.HandleAsync(new CreateCreditRefundCommand(
                requester, 
                transactionReferenceObject,
                timestamp,
                parsedData.RefundCreditAmount,
                parsedData.Reason,
                parsedData.Comment));
        }

        [Route("transactions")]
        public async Task<GetTransactionsResult> GetTransactionsAsync(
            [FromUri]string userId = null,
            [FromUri]DateTime? startTimeInclusive = null,
            [FromUri]DateTime? endTimeExclusive = null)
        {
            var userIdObject = string.IsNullOrWhiteSpace(userId) ? null : new UserId(userId.DecodeGuid());
            var requester = await this.requesterContext.GetRequesterAsync();

            return await this.getTransactions.HandleAsync(new GetTransactionsQuery(
                requester,
                userIdObject,
                startTimeInclusive,
                endTimeExclusive));
        }

        [Route("lease")]
        public async Task<BlockPaymentProcessingResult> GetPaymentProcessingLeaseAsync(
            [FromUri]string leaseId = null)
        {
            var requester = await this.requesterContext.GetRequesterAsync();

            string proposedLeaseId = null;
            if (string.IsNullOrWhiteSpace(leaseId))
            {
                leaseId = null;
                proposedLeaseId = this.guidCreator.Create().ToString();
            }

            var command = new BlockPaymentProcessingCommand(requester, leaseId, proposedLeaseId);

            try
            {
                await this.blockPaymentProcessing.HandleAsync(command);
                return new BlockPaymentProcessingResult(
                    (int)BlockPaymentProcessingCommandHandler.LeaseLength.TotalSeconds,
                    command.LeaseId ?? command.ProposedLeaseId);
            }
            catch (LeaseConflictException)
            {
                return null;
            }
        }
    }
}