namespace Fifthweek.Api.FileManagement.Shared
{
    using Fifthweek.CodeGeneration;

    [AutoConstructor]
    [AutoEqualityMembers]
    public partial class FileInformation
    {
        public FileId FileId { get; private set; }

        public string ContainerName { get; private set; }
    }
}