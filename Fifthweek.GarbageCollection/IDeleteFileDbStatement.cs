namespace Fifthweek.GarbageCollection
{
    using System.Threading.Tasks;

    using Fifthweek.Api.FileManagement.Shared;

    public interface IDeleteFileDbStatement
    {
        Task ExecuteAsync(FileId fileId);
    }
}