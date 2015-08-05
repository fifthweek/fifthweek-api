namespace Fifthweek.Payments.Services.Credit
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public interface ITopUpUserAccountsWithCredit
    {
        Task<bool> ExecuteAsync(
            IReadOnlyList<CalculatedAccountBalanceResult> updatedAccountBalances, 
            List<PaymentProcessingException> errors,
            CancellationToken cancellationToken);
    }
}