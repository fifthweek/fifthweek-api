namespace Fifthweek.Api.FileManagement.Commands
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;

    public class CompleteFileUploadCommandHandler : ICommandHandler<CompleteFileUploadCommand>
    {
        public Task HandleAsync(CompleteFileUploadCommand command)
        {
            // Check the requester has access to this file.

            return Task.FromResult(0);
        }
    }
}