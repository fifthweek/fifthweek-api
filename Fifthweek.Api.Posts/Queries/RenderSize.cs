namespace Fifthweek.Api.Posts.Queries
{
    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers]
    public partial class RenderSize
    {
        public int Width { get; private set; }

        public int Height { get; private set; }
    }
}