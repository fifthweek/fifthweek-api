namespace Fifthweek.Api.Payments.Commands
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Azure;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    using Microsoft.WindowsAzure.Storage;

    [AutoConstructor]
    public partial class BlockPaymentProcessingCommandHandler : ICommandHandler<BlockPaymentProcessingCommand>
    {
        public static readonly TimeSpan LeaseLength = TimeSpan.FromMinutes(1);

        private readonly IRequesterSecurity requesterSecurity;
        private readonly IBlobLeaseHelper blobLeaseHelper;

        public async Task HandleAsync(BlockPaymentProcessingCommand command)
        {
            command.AssertNotNull("command");

            await this.requesterSecurity.AuthenticateAsync(command.Requester);
            await this.requesterSecurity.AssertInRoleAsync(command.Requester, FifthweekRole.Administrator);

            if (!string.IsNullOrWhiteSpace(command.ProposedLeaseId))
            {
                try
                {
                    var blob = this.blobLeaseHelper.GetLeaseBlob(Fifthweek.Payments.Shared.Constants.ProcessPaymentsLeaseObjectName);
                    await blob.AcquireLeaseAsync(LeaseLength, command.ProposedLeaseId, CancellationToken.None);
                }
                catch (Exception t)
                {
                    if (this.blobLeaseHelper.IsLeaseConflictException(t))
                    {
                        throw new LeaseConflictException(t);
                    }
                     
                    throw;
                }
            }
            else if (!string.IsNullOrWhiteSpace(command.LeaseId))
            {
                var blob = this.blobLeaseHelper.GetLeaseBlob(Fifthweek.Payments.Shared.Constants.ProcessPaymentsLeaseObjectName);
                await blob.RenewLeaseAsync(new AccessCondition { LeaseId = command.LeaseId }, CancellationToken.None);
            }
            else
            {
                throw new BadRequestException("Either leaseId or proposedLeaseId must be specified.");
            }
        }
    }
}