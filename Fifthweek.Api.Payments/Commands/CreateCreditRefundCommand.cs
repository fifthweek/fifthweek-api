namespace Fifthweek.Api.Payments.Commands
{
    using System;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Services.Refunds;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Shared;

    [AutoConstructor, AutoEqualityMembers]
    public partial class CreateCreditRefundCommand
    {
        public Requester Requester { get; private set; }

        public TransactionReference TransactionReference { get; private set; }

        public DateTime Timestamp { get; private set; }

        public PositiveInt RefundCreditAmount { get; private set; }

        public RefundCreditReason Reason { get; private set; }

        public string Comment { get; private set; }
    }
}