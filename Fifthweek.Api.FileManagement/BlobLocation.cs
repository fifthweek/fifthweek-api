namespace Fifthweek.Api.FileManagement
{
    using Fifthweek.Api.Core;

    [AutoConstructor]
    [AutoEqualityMembers]
    public partial class BlobLocation
    {
        public string ContainerName { get; private set; }

        public string BlobName { get; private set; }
    }
}