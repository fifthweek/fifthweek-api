namespace Fifthweek.Payments.Services.Credit.Taxamo
{
    using System.Threading.Tasks;

    public interface IDeleteTaxamoTransaction
    {
        Task ExecuteAsync(string transactionKey);
    }
}