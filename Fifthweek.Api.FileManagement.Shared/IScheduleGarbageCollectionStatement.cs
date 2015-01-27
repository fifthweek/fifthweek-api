namespace Fifthweek.Api.FileManagement.Shared
{
    using System.Threading.Tasks;

    public interface IScheduleGarbageCollectionStatement
    {
        Task ExecuteAsync();
    }
}