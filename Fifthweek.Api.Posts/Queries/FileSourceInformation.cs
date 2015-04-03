namespace Fifthweek.Api.Posts.Queries
{
    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers]
    public partial class FileSourceInformation
    {
        public string FileName { get; private set; }

        public string FileExtension { get; private set; }

        public string ContentType { get; private set; }

        public long Size { get; private set; }

        [Optional]
        public RenderSize RenderSize { get; private set; }
    }
}