namespace Fifthweek.Api.Core.Tests.ClassAugmentation
{
    [AutoEqualityMembers, AutoConstructor]
    public partial class ConstructedNullableString
    {
        [Optional]
        public string Value { get; private set; }
    }
}