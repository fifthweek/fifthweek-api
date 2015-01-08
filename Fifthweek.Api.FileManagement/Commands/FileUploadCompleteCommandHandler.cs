namespace Fifthweek.Api.FileManagement.Commands
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;

    public class FileUploadCompleteCommandHandler : ICommandHandler<FileUploadCompleteCommand>
    {
        public Task HandleAsync(FileUploadCompleteCommand command)
        {
            return Task.FromResult(0);
        }
    }
}