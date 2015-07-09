namespace Fifthweek.Api.Identity.Membership.Queries
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Azure;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
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

            if (result.ProfileImageFileId == null)
            {
                return new GetAccountSettingsResult(result.Name, result.Username, result.Email, null, (int)result.AccountBalance, result.BillingStatus, result.HasCreditCardDetails);
            }

            var fileInformation = await this.fileInformationAggregator.GetFileInformationAsync(
                null,
                result.ProfileImageFileId,
                FilePurposes.ProfileImage);

            return new GetAccountSettingsResult(result.Name, result.Username, result.Email, fileInformation, (int)result.AccountBalance, result.BillingStatus, result.HasCreditCardDetails);
        }
    }
}