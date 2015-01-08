namespace Fifthweek.Api.Core.Tests.ClassAugmentation
{
    [AutoEqualityMembers, AutoConstructor]
    public partial class ConstructedNonNullableString
    {
        public string Value { get; private set; }
    }
}