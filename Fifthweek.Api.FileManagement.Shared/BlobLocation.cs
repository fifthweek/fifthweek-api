namespace Fifthweek.Api.FileManagement.Shared
{
    using Fifthweek.CodeGeneration;

    [AutoConstructor]
    [AutoEqualityMembers]
    public partial class BlobLocation
    {
        public string ContainerName { get; private set; }

        public string BlobName { get; private set; }
    }
}