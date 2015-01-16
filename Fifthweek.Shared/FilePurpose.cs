namespace Fifthweek.Shared
{
    [AutoConstructor]
    [AutoEqualityMembers]
    public partial class FilePurpose
    {
        public string Name { get; private set; }

        public bool IsPublic { get; private set; }
    }
}