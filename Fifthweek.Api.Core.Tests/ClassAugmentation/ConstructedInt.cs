namespace Fifthweek.Api.Core.Tests.ClassAugmentation
{
    [AutoEqualityMembers, AutoConstructor]
    public partial class ConstructedInt
    {
        public int Value { get; private set; }
    }
}