namespace Fifthweek.Api.Payments.Commands
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Services;
    using Fifthweek.Payments.Services.Refunds;
    using Fifthweek.Shared;

    [AutoConstructor]
    [Decorator(OmitDefaultDecorators = true)]  // Turn off default retry policy so we don't repeatedly refund taxamo if transient failure.
    public partial class CreateCreditRefundCommandHandler : ICommandHandler<CreateCreditRefundCommand>
    {
        private readonly IRequesterSecurity requesterSecurity;
        private readonly ICreateCreditRefund createCreditRefund;
        private readonly IUpdateAccountBalancesDbStatement updateAccountBalances;

        public async Task HandleAsync(CreateCreditRefundCommand command)
        {
            command.AssertNotNull("command");
            var enactingUserId = await this.requesterSecurity.AuthenticateAsync(command.Requester);

            await this.requesterSecurity.AssertInRoleAsync(command.Requester, FifthweekRole.Administrator);

            var userId = await this.createCreditRefund.ExecuteAsync(
                enactingUserId,
                command.TransactionReference,
                command.Timestamp,
                command.RefundCreditAmount,
                command.Reason,
                command.Comment);

            await this.updateAccountBalances.ExecuteAsync(userId, command.Timestamp);
        }
    }
}