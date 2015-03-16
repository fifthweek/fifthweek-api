namespace Fifthweek.Api.Posts.Queries
{
    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers]
    public partial class FileSourceInformation
    {
        [Optional]
        public string FileName { get; private set; }

        [Optional]
        public string FileExtension { get; private set; }

        [Optional]
        public long Size { get; private set; }
    }
}