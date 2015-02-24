namespace Fifthweek.Api.Azure
{
    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers]
    public partial class BlobInformation
    {
        public string ContainerName { get; private set; }

        public string BlobName { get; private set; }

        public string Uri { get; private set; }
    }
}