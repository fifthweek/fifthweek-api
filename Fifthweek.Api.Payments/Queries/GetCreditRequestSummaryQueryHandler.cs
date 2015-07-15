namespace Fifthweek.Api.Payments.Queries
{
    using System;
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
            var userType = await this.requesterSecurity.GetUserTypeAsync(query.Requester);

            var subscriptionsAmount = await this.getUserWeeklySubscriptionsCost.ExecuteAsync(query.UserId);
            var amountToCharge = Math.Max(TopUpUserAccountsWithCredit.MinimumPaymentAmount, subscriptionsAmount);

            UserPaymentOriginResult origin;
            if (query.LocationDataOverride == null)
            {
                origin = await this.getUserPaymentOrigin.ExecuteAsync(query.UserId);
            }
            else
            {
                origin = new UserPaymentOriginResult(
                    query.LocationDataOverride.CountryCode == null ? null : query.LocationDataOverride.CountryCode.Value,
                    query.LocationDataOverride.CreditCardPrefix == null ? null : query.LocationDataOverride.CreditCardPrefix.Value,
                    query.LocationDataOverride.IpAddress == null ? null : query.LocationDataOverride.IpAddress.Value);
            }

            var taxInformation = await this.getTaxInformation.ExecuteAsync(
                PositiveInt.Parse(amountToCharge), 
                origin.CountryCode, 
                origin.CreditCardPrefix, 
                origin.IpAddress, 
                origin.OriginalTaxamoTransactionKey,
                userType);

            return new CreditRequestSummary(
                subscriptionsAmount,
                taxInformation);
        }
    }
}