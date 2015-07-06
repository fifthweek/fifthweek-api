namespace Fifthweek.Api.Payments
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;

    public interface ISetUserPaymentOriginOriginalTaxamoTransactionKeyDbStatement
    {
        Task ExecuteAsync(
            UserId userId,
            string originalTaxamoTransactionKey);
    }
}