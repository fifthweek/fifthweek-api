namespace Fifthweek.Api.Payments.Commands
{
    using System;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Shared;

    [AutoConstructor, AutoEqualityMembers]
    public partial class CreateTransactionRefundCommand
    {
        public Requester Requester { get; private set; }

        public TransactionReference TransactionReference { get; private set; }

        public DateTime Timestamp { get; private set; }

        public string Comment { get; private set; }
    }
}