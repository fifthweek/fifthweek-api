namespace Fifthweek.Payments.Services.Administration
{
    using System;
    using System.Collections.Generic;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Shared;

    [AutoConstructor]
    [AutoEqualityMembers]
    public partial class GetTransactionsResult
    {
        public IReadOnlyList<Item> Records { get; private set; }

        [AutoConstructor]
        [AutoEqualityMembers]
        public partial class Item
        {
            public Guid Id { get; set; }

            public UserId AccountOwnerId { get; set; }

            [Optional]
            public Username AccountOwnerUsername { get; set; }

            [Optional]
            public UserId CounterpartyId { get; set; }

            [Optional]
            public Username CounterpartyUsername { get; set; }

            public DateTime Timestamp { get; set; }

            public decimal Amount { get; set; }

            public LedgerAccountType AccountType { get; set; }

            public LedgerTransactionType TransactionType { get; set; }

            public TransactionReference TransactionReference { get; set; }

            [Optional]
            public Guid? InputDataReference { get; set; }

            [Optional]
            public string Comment { get; set; }

            [Optional]
            public string StripeChargeId { get; set; }

            [Optional]
            public string TaxamoTransactionKey { get; set; }
        }
    }
}