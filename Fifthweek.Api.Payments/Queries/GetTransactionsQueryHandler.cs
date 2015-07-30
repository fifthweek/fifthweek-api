namespace Fifthweek.Api.Payments.Queries
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Services.Administration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class GetTransactionsQueryHandler : IQueryHandler<GetTransactionsQuery, GetTransactionsResult>
    {
        private readonly IRequesterSecurity requesterSecurity;
        private readonly ITimestampCreator timestampCreator;
        private readonly IGetTransactionsDbStatement getTransactions;

        public async Task<GetTransactionsResult> HandleAsync(GetTransactionsQuery query)
        {
            query.AssertNotNull("query");

            DateTime startTimeInclusive, endTimeExclusive;

            await this.requesterSecurity.AuthenticateAsync(query.Requester);
            await this.requesterSecurity.AssertInRoleAsync(query.Requester, FifthweekRole.Administrator);

            if (query.StartTimeInclusive == null && query.EndTimeExclusive == null)
            {
                endTimeExclusive = this.timestampCreator.Now().AddMinutes(15);
                startTimeInclusive = endTimeExclusive.AddDays(-4 * 7);
            }
            else if (query.StartTimeInclusive == null)
            {
                endTimeExclusive = query.EndTimeExclusive.Value;
                startTimeInclusive = endTimeExclusive.AddDays(-4 * 7);
            }
            else if (query.EndTimeExclusive == null)
            {
                startTimeInclusive = query.StartTimeInclusive.Value;
                endTimeExclusive = startTimeInclusive.AddDays(4 * 7);
            }
            else
            {
                endTimeExclusive = query.EndTimeExclusive.Value;
                startTimeInclusive = query.StartTimeInclusive.Value;
            }

            return await this.getTransactions.ExecuteAsync(query.UserId, startTimeInclusive, endTimeExclusive);
        }
    }
}