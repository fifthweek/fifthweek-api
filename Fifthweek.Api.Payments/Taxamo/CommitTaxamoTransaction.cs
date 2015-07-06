namespace Fifthweek.Api.Payments.Taxamo
{
    using System.Threading.Tasks;

    public class CommitTaxamoTransaction : ICommitTaxamoTransaction
    {
        public async Task ExecuteAsync(string transactionKey)
        {
        }
    }
}