namespace Fifthweek.Api.Core.Tests.ClassAugmentation
{
    using Fifthweek.Shared;

    [AutoEqualityMembers, AutoConstructor]
    public partial class ConstructedNullableString
    {
        [Optional]
        public string Value { get; private set; }
    }
}