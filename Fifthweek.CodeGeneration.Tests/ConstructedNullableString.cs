namespace Fifthweek.CodeGeneration.Tests
{
    [AutoEqualityMembers, AutoConstructor]
    public partial class ConstructedNullableString
    {
        [Optional]
        public string Value { get; private set; }
    }
}