namespace Fifthweek.GarbageCollection
{
    using System.Threading.Tasks;

    public interface IDeleteBlobsForFile
    {
        Task ExecuteAsync(OrphanedFileData file);
    }
}