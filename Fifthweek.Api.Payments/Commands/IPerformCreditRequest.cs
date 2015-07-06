namespace Fifthweek.Api.Payments.Commands
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Payments.Taxamo;

    public interface IPerformCreditRequest
    {
        Task<StripeTransactionResult> HandleAsync(
            ApplyCreditRequestCommand command,
            TaxamoTransactionResult taxamoTransaction,
            UserPaymentOriginResult origin);
    }
}