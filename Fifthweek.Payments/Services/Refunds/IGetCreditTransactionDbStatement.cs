namespace Fifthweek.Payments.Services.Refunds
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Payments.Shared;

    public interface IGetCreditTransactionDbStatement
    {
        Task<GetCreditTransactionDbStatement.GetCreditTransactionResult> ExecuteAsync(TransactionReference transactionReference);
    }
}