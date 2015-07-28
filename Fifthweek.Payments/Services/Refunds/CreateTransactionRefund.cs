namespace Fifthweek.Payments.Services.Refunds
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class CreateTransactionRefund : ICreateTransactionRefund
    {
        private readonly IGuidCreator guidCreator;
        private readonly IGetRecordsForTransactionDbStatement getRecordsForTransaction;
        private readonly IPersistCommittedRecordsDbStatement persistCommittedRecords;

        public async Task<CreateTransactionRefundResult> ExecuteAsync(
            UserId enactingUserId,
            TransactionReference transactionReference,
            DateTime timestamp,
            string comment)
        {
            enactingUserId.AssertNotNull("enactingUserId");
            transactionReference.AssertNotNull("transactionReference");

            var formattedComment = string.IsNullOrWhiteSpace(comment)
                ? string.Format("Performed By {0}", enactingUserId)
                : string.Format("Performed By {0} - {1}", enactingUserId, comment);

            var records = await this.getRecordsForTransaction.ExecuteAsync(transactionReference);


            if (records.Count == 0)
            {
                throw new InvalidOperationException("No records found for transaction reference: " + transactionReference);
            }

            UserId subscriberId = null;
            UserId creatorId = null;

            foreach (var record in records)
            {
                LedgerTransactionType refundType;
                switch (record.TransactionType)
                {
                    case LedgerTransactionType.SubscriptionPayment:
                        refundType = LedgerTransactionType.SubscriptionRefund;
                        break;

                    default:
                        // This is a safety against both refunding other transaction types,
                        // and refunding transactions which has already been refunded.
                        throw new InvalidOperationException(
                            "Cannot refund transaction of type " + record.TransactionType);
                }

                if (record.AccountType == LedgerAccountType.FifthweekCredit)
                {
                    subscriberId = new UserId(record.AccountOwnerId);
                    creatorId = new UserId(record.CounterpartyId.Value);
                }

                record.Id = this.guidCreator.CreateSqlSequential();
                record.Timestamp = timestamp;
                record.TransactionType = refundType;
                record.Amount = -record.Amount;
                record.Comment = formattedComment;
            }

            await this.persistCommittedRecords.ExecuteAsync(records);
            
            return new CreateTransactionRefundResult(subscriberId, creatorId);
        }

        [AutoConstructor, AutoEqualityMembers]
        public partial class CreateTransactionRefundResult
        {
            public UserId SubscriberId { get; private set; }

            public UserId CreatorId { get; private set; }
        }
    }
}