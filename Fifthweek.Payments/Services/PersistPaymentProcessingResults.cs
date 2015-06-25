namespace Fifthweek.Payments.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class PersistPaymentProcessingResults : IPersistPaymentProcessingResults
    {
        private readonly IGuidCreator guidCreator;
        private readonly IPersistPaymentProcessingDataStatement persistPaymentProcessingData;
        private readonly IPersistCommittedAndUncommittedRecordsDbStatement persistCommittedAndUncommittedRecords;

        public async Task ExecuteAsync(PaymentProcessingData data, PaymentProcessingResults results)
        {
            data.AssertNotNull("data");
            results.AssertNotNull("results");

            var committedResults = results.Items.Where(v => v.IsComitted).ToList();
            var uncommittedResult = results.Items.SingleOrDefault(v => !v.IsComitted);

            var dataId = this.guidCreator.Create();

            var committedRecords = new List<AppendOnlyLedgerRecord>();
            foreach (var result in committedResults)
            {
                var transactionId = this.guidCreator.Create();

                committedRecords.Add(new AppendOnlyLedgerRecord(
                    this.guidCreator.CreateSqlSequential(),
                    data.SubscriberId.Value,
                    data.CreatorId.Value,
                    result.EndTimeExclusive,
                    -result.SubscriptionCost.Cost,
                    LedgerAccountType.Fifthweek,
                    transactionId,
                    dataId, 
                    null, 
                    null, 
                    null));

                committedRecords.Add(new AppendOnlyLedgerRecord(
                    this.guidCreator.CreateSqlSequential(),
                    Guid.Empty,
                    data.CreatorId.Value,
                    result.EndTimeExclusive,
                    result.SubscriptionCost.Cost,
                    LedgerAccountType.Fifthweek,
                    transactionId,
                    dataId, 
                    null,
                    null, 
                    null));

                var creatorPercentage = result.CreatorPercentageOverride == null
                                            ? Payments.Constants.DefaultCreatorPercentage
                                            : result.CreatorPercentageOverride.Percentage;

                var creatorPayment = result.SubscriptionCost.Cost * creatorPercentage;

                committedRecords.Add(new AppendOnlyLedgerRecord(
                    this.guidCreator.CreateSqlSequential(),
                    Guid.Empty,
                    data.CreatorId.Value,
                    result.EndTimeExclusive,
                    -creatorPayment,
                    LedgerAccountType.Fifthweek,
                    transactionId,
                    dataId,
                    null,
                    null,
                    null));

                committedRecords.Add(new AppendOnlyLedgerRecord(
                    this.guidCreator.CreateSqlSequential(),
                    data.CreatorId.Value,
                    data.CreatorId.Value,
                    result.EndTimeExclusive,
                    creatorPayment,
                    LedgerAccountType.Fifthweek,
                    transactionId,
                    dataId,
                    null, 
                    null, 
                    null));
            }

            UncommittedSubscriptionPayment uncommittedRecord = null;
            if (uncommittedResult != null)
            {
                uncommittedRecord = new UncommittedSubscriptionPayment(
                    data.SubscriberId.Value,
                    data.CreatorId.Value,
                    uncommittedResult.StartTimeInclusive,
                    uncommittedResult.EndTimeExclusive,
                    uncommittedResult.SubscriptionCost.Cost,
                    dataId);
            }

            await this.persistPaymentProcessingData.ExecuteAsync(new PersistedPaymentProcessingData(dataId, data, results));
            await this.persistCommittedAndUncommittedRecords.ExecuteAsync(committedRecords, uncommittedRecord);
        }
    }
}