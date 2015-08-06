namespace Fifthweek.Api.Payments.Commands
{
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers]
    public partial class BlockPaymentProcessingCommand
    {
        public Requester Requester { get; private set; }

        [Optional]
        public string LeaseId { get; private set; }

        [Optional]
        public string ProposedLeaseId { get; private set; }
    }
}