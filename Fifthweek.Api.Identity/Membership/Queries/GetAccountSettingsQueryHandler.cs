namespace Fifthweek.Api.Identity.Membership.Queries
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Azure;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Services;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class GetAccountSettingsQueryHandler : IQueryHandler<GetAccountSettingsQuery, GetAccountSettingsResult>
    {
        private readonly IRequesterSecurity requesterSecurity;
        private readonly IGetAccountSettingsDbStatement getAccountSettings;
        private readonly IFileInformationAggregator fileInformationAggregator;

        public async Task<GetAccountSettingsResult> HandleAsync(GetAccountSettingsQuery query)
        {
            query.AssertNotNull("query");

            await this.requesterSecurity.AuthenticateAsAsync(query.Requester, query.RequestedUserId);

            var result = await this.getAccountSettings.ExecuteAsync(query.RequestedUserId);

            var creatorPercentage = GetCreatorPercentage(result.CreatorPercentageOverride, query.Now);

            if (result.ProfileImageFileId == null)
            {
                return new GetAccountSettingsResult(
                    result.Username, 
                    result.Email, 
                    null, 
                    (int)result.AccountBalance, 
                    result.PaymentStatus,
                    result.HasPaymentInformation,
                    creatorPercentage.Percentage,
                    GetWeeksRemaining(creatorPercentage.ExpiryDate, query.Now));
            }

            var fileInformation = await this.fileInformationAggregator.GetFileInformationAsync(
                null,
                result.ProfileImageFileId,
                FilePurposes.ProfileImage);

            return new GetAccountSettingsResult(
                result.Username, 
                result.Email,
                fileInformation,
                (int)result.AccountBalance,
                result.PaymentStatus,
                result.HasPaymentInformation,
                creatorPercentage.Percentage,
                GetWeeksRemaining(creatorPercentage.ExpiryDate, query.Now));
        }

        internal static CreatorPercentageOverrideData GetCreatorPercentage(CreatorPercentageOverrideData creatorPercentageOverride, DateTime now)
        {
            var paymentProcessingStartDateInclusive = PaymentProcessingUtilities.GetPaymentProcessingStartDate(now);
            var paymentProcessingEndDateExclusive = paymentProcessingStartDateInclusive.AddDays(7);

            var creatorPercentageOverrideResult = PaymentProcessingUtilities.GetCreatorPercentageOverride(
                creatorPercentageOverride, paymentProcessingEndDateExclusive);

            return creatorPercentageOverrideResult
                ?? new CreatorPercentageOverrideData(
                    Fifthweek.Payments.Constants.DefaultCreatorPercentage,
                    null);
        }

        internal static int? GetWeeksRemaining(DateTime? expiryDate, DateTime now)
        {
            if (expiryDate == null)
            {
                return null;
            }

            var expiryDateWeekStartDateInclusive = PaymentProcessingUtilities.GetPaymentProcessingStartDate(expiryDate.Value);
            var finalWeekEndDateInclusive = expiryDateWeekStartDateInclusive.AddTicks(-1);
            return (int)((finalWeekEndDateInclusive - now).TotalDays / 7);
        }
    }
}