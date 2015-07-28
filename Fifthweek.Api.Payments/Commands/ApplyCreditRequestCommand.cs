namespace Fifthweek.Api.Payments.Commands
{
    using System;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Shared;

    [AutoConstructor, AutoEqualityMembers]
    public partial class ApplyCreditRequestCommand
    {
        public Requester Requester { get; private set; }

        public UserId UserId { get; private set; }

        public DateTime Timestamp { get; private set; }

        public TransactionReference TransactionReference { get; private set; }

        public PositiveInt Amount { get; private set; }

        public PositiveInt ExpectedTotalAmount { get; private set; }
    }
}