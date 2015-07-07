namespace Fifthweek.Api.Payments.Commands
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Payments.Taxamo;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Services;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class CommitTestUserCreditToDatabase : ICommitTestUserCreditToDatabase
    {
        private readonly ITimestampCreator timestampCreator;
        private readonly ISetTestUserAccountBalanceDbStatement setTestUserAccountBalance;

        public async Task HandleAsync(
            UserId userId,
            PositiveInt amount)
        {
            userId.AssertNotNull("userId");
            amount.AssertNotNull("amount");

            var timestamp = this.timestampCreator.Now();

            // Just update account balance directly.
            await this.setTestUserAccountBalance.ExecuteAsync(userId, timestamp, amount);
        }
    }
}