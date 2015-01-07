namespace Fifthweek.Api.FileManagement.Commands
{
    using Fifthweek.Api.Core;

    [AutoConstructor]
    [AutoEqualityMembers]
    public partial class FileUploadCompleteCommand
    {
        public FileId FileId { get; private set; }
    }
}