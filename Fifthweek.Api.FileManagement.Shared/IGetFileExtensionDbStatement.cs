namespace Fifthweek.Api.FileManagement.Shared
{
    using System.Threading.Tasks;

    public interface IGetFileExtensionDbStatement
    {
        Task<string> ExecuteAsync(Shared.FileId fileId);
    }
}