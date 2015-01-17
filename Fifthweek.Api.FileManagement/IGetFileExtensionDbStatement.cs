namespace Fifthweek.Api.FileManagement
{
    using System.Threading.Tasks;

    public interface IGetFileExtensionDbStatement
    {
        Task<string> ExecuteAsync(FileId fileId);
    }
}