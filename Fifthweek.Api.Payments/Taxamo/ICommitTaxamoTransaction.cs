namespace Fifthweek.Api.Payments.Taxamo
{
    using System.Threading.Tasks;

    public interface ICommitTaxamoTransaction
    {
        Task ExecuteAsync(string transactionKey);
    }
}