namespace Fifthweek.Api.Payments.Commands
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Services.Credit;
    using Fifthweek.Shared;

    [AutoConstructor]
    [Decorator(OmitDefaultDecorators = true)]  // Turn off default retry policy so we don't repeatedly charge card if transient failure in Sql Azure.
    public partial class ApplyCreditRequestCommandHandler : ICommandHandler<ApplyCreditRequestCommand>
    {
        private readonly IRequesterSecurity requesterSecurity;
        private readonly IFifthweekRetryOnTransientErrorHandler retryOnTransientFailure;
        private readonly IApplyStandardUserCredit applyStandardUserCredit;
        private readonly ICommitTestUserCreditToDatabase commitTestUserCreditToDatabase;

        public async Task HandleAsync(ApplyCreditRequestCommand command)
        {
            command.AssertNotNull("command");
            await this.requesterSecurity.AuthenticateAsAsync(command.Requester, command.UserId);

            var isTestUser = await this.requesterSecurity.IsInRoleAsync(command.Requester, FifthweekRole.TestUser);
            if (isTestUser)
            {
                // For a test user we just update their account balance directly, 
                // as we don't want the credit to affect the Fifthweek accounts
                // or VAT related accounts.
                await this.retryOnTransientFailure.HandleAsync(() =>
                    this.commitTestUserCreditToDatabase.HandleAsync(
                        command.UserId,
                        command.Amount));

                return;
            }

            await this.applyStandardUserCredit.ExecuteAsync(command.UserId, command.Amount, command.ExpectedTotalAmount);
        }
    }
}