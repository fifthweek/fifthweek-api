namespace Fifthweek.Payments.Services.Refunds.Taxamo
{
    using System.Threading.Tasks;

    using Fifthweek.Shared;

    public interface ICreateTaxamoRefund
    {
        Task<CreateTaxamoRefund.TaxamoRefundResult> ExecuteAsync(
            string taxamoTransactionKey, 
            PositiveInt refundCreditAmount,
            UserType userType);
    }
}