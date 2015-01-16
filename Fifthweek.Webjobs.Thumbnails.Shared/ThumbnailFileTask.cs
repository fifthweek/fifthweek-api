namespace Fifthweek.Webjobs.Thumbnails.Shared
{
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using Fifthweek.Webjobs.Files.Shared;

    [AutoConstructor]
    [AutoEqualityMembers]
    public partial class ThumbnailFileTask : IFileTask
    {
        public int Width { get; private set; }

        public int Height { get; private set; }
    }
}