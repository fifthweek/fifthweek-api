namespace Fifthweek.WebJobs.Payments
{
    using System.Threading.Tasks;

    public interface ICreateAllSnapshotsProcessor
    {
        Task ExecuteAsync();
    }
}