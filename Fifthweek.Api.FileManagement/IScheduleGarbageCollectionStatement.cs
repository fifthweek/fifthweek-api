namespace Fifthweek.Api.FileManagement
{
    using System.Threading.Tasks;

    public interface IScheduleGarbageCollectionStatement
    {
        Task ExecuteAsync();
    }
}