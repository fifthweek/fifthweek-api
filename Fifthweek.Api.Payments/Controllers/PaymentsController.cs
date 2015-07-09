namespace Fifthweek.Api.Payments.Controllers
{
    using System.Threading.Tasks;
    using System.Web.Http;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Payments.Commands;
    using Fifthweek.Api.Payments.Queries;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    [RoutePrefix("payment")]
    public partial class PaymentsController : ApiController
    {
        private readonly IRequesterContext requesterContext;

        private readonly IQueryHandler<GetCreditRequestSummaryQuery, CreditRequestSummary> getCreditRequestSummary;
        private readonly ICommandHandler<UpdatePaymentOriginCommand> updatePaymentsOrigin;
        private readonly ICommandHandler<ApplyCreditRequestCommand> applyCreditRequest;

        [Route("origins/{userId}")]
        public Task PutPaymentOriginAsync(string userId, [FromBody]PaymentOriginData data)
        {
            userId.AssertUrlParameterProvided("userId");
            data.AssertBodyProvided("data");

            var parsedData = data.Parse();
            var userIdObject = new UserId(userId.DecodeGuid());
            var requester = this.requesterContext.GetRequester();

            return this.updatePaymentsOrigin.HandleAsync(new UpdatePaymentOriginCommand(
                requester, 
                userIdObject, 
                parsedData.StripeToken,
                parsedData.BillingCountryCode,
                parsedData.CreditCardPrefix,
                parsedData.IpAddress));
        }

        [Route("creditRequests/{userId}")]
        public Task PostCreditRequestAsync(string userId, [FromBody]CreditRequestData data)
        {
            userId.AssertUrlParameterProvided("userId");
            data.AssertBodyProvided("data");

            var parsedData = data.Parse();
            var userIdObject = new UserId(userId.DecodeGuid());
            var requester = this.requesterContext.GetRequester();

            return this.applyCreditRequest.HandleAsync(new ApplyCreditRequestCommand(
                requester, 
                userIdObject, 
                parsedData.Amount,
                parsedData.ExpectedTotalAmount));
        }

        [Route("creditRequestSummaries/{userId}")]
        public Task<CreditRequestSummary> GetCreditRequestSummaryAsync(string userId)
        {
            userId.AssertUrlParameterProvided("userId");

            var userIdObject = new UserId(userId.DecodeGuid());
            var requester = this.requesterContext.GetRequester();

            return this.getCreditRequestSummary.HandleAsync(new GetCreditRequestSummaryQuery(
                requester, 
                userIdObject));
        }
    }
}