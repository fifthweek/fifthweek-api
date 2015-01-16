namespace Fifthweek.Shared
{
    using Fifthweek.CodeGeneration;

    [AutoConstructor]
    [AutoEqualityMembers]
    public partial class FilePurpose
    {
        public string Name { get; private set; }

        public bool IsPublic { get; private set; }
    }
}