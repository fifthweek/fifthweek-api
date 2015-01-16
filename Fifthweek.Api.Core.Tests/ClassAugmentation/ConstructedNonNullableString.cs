namespace Fifthweek.Api.Core.Tests.ClassAugmentation
{
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoEqualityMembers, AutoConstructor]
    public partial class ConstructedNonNullableString
    {
        public string Value { get; private set; }
    }
}