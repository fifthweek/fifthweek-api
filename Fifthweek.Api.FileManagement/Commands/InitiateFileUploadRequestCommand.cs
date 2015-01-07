namespace Fifthweek.Api.FileManagement.Commands
{
    using Fifthweek.Api.Core;

    [AutoConstructor]
    [AutoEqualityMembers]
    public partial class InitiateFileUploadRequestCommand
    {
        public FileId FileId { get; private set; }
    }
}