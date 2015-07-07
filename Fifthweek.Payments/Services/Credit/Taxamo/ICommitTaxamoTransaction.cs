namespace Fifthweek.Payments.Services.Credit.Taxamo
{
    using System.Threading.Tasks;

    public interface ICommitTaxamoTransaction
    {
        Task ExecuteAsync(string transactionKey);
    }
}