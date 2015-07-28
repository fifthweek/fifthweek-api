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
    public partial class CreateTransactionRefundCommandHandler : ICommandHandler<CreateTransactionRefundCommand>
    {
        private readonly IRequesterSecurity requesterSecurity;
        private readonly ICreateTransactionRefund createTransactionRefund;
        private readonly IUpdateAccountBalancesDbStatement updateAccountBalances;

        public async Task HandleAsync(CreateTransactionRefundCommand command)
        {
            command.AssertNotNull("command");
            var enactingUserId = await this.requesterSecurity.AuthenticateAsync(command.Requester);

            await this.requesterSecurity.AssertInRoleAsync(command.Requester, FifthweekRole.Administrator);

            var result = await this.createTransactionRefund.ExecuteAsync(
                enactingUserId,
                command.TransactionReference,
                command.Timestamp,
                command.Comment);

            await this.updateAccountBalances.ExecuteAsync(result.SubscriberId, command.Timestamp);
            await this.updateAccountBalances.ExecuteAsync(result.CreatorId, command.Timestamp);
        }
    }
}