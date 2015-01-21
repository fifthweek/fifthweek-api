namespace Fifthweek.Api.Accounts.Queries
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.CodeGeneration;

    [AutoConstructor]
    public partial class GetAccountSettingsQueryHandler : IQueryHandler<GetAccountSettingsQuery, GetAccountSettingsResult>
    {
        private readonly IAccountRepository accountRepository;

        public Task<GetAccountSettingsResult> HandleAsync(GetAccountSettingsQuery query)
        {
            query.AssertNotNull("query");

            UserId authenticatedUserId;
            query.Requester.AssertAuthenticated(out authenticatedUserId);
            query.Requester.AssertAuthenticatedAs(query.RequestedUserId);

            return this.accountRepository.GetAccountSettingsAsync(query.RequestedUserId);
        }
    }
}