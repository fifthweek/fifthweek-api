namespace Fifthweek.Api.Payments.Taxamo
{
    using System.Threading.Tasks;

    public interface IDeleteTaxamoTransaction
    {
        Task ExecuteAsync(string transactionKey);
    }
}