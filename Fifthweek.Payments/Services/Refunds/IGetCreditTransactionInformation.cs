namespace Fifthweek.Payments.Services.Refunds
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Payments.Shared;

    public interface IGetCreditTransactionInformation
    {
        Task<GetCreditTransactionInformation.GetCreditTransactionResult> ExecuteAsync(TransactionReference transactionReference);
    }
}