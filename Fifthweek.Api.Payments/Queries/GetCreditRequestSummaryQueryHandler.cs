namespace Fifthweek.Api.Payments.Queries
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Payments.Controllers;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Services.Credit;
    using Fifthweek.Payments.Services.Credit.Taxamo;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class GetCreditRequestSummaryQueryHandler : IQueryHandler<GetCreditRequestSummaryQuery, CreditRequestSummary>
    {
        private readonly IRequesterSecurity requesterSecurity;
        private readonly IGetUserPaymentOriginDbStatement getUserPaymentOrigin;
        private readonly IGetTaxInformation getTaxInformation;
        private readonly IGetUserWeeklySubscriptionsCost getUserWeeklySubscriptionsCost;
        
        public async Task<CreditRequestSummary> HandleAsync(GetCreditRequestSummaryQuery query)
        {
            query.AssertNotNull("query");

            await this.requesterSecurity.AuthenticateAsAsync(query.Requester, query.UserId);

            var amountToCharge = await this.getUserWeeklySubscriptionsCost.ExecuteAsync(query.UserId);

            var origin = await this.getUserPaymentOrigin.ExecuteAsync(query.UserId);

            var taxInformation = await this.getTaxInformation.ExecuteAsync(
                PositiveInt.Parse(amountToCharge), 
                origin.CountryCode, 
                origin.CreditCardPrefix, 
                origin.IpAddress, 
                origin.OriginalTaxamoTransactionKey);

            return new CreditRequestSummary(
                taxInformation.Amount.Value,
                taxInformation.TotalAmount.Value,
                taxInformation.TaxAmount.Value,
                taxInformation.TaxRate,
                taxInformation.TaxName,
                taxInformation.TaxEntityName,
                taxInformation.CountryName);
        }
    }
}