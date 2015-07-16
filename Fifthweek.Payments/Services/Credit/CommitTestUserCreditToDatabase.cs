namespace Fifthweek.Payments.Services.Credit
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
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