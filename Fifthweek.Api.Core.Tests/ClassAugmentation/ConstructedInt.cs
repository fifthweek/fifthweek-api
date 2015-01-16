namespace Fifthweek.Api.Core.Tests.ClassAugmentation
{
    using Fifthweek.Shared;

    [AutoEqualityMembers, AutoConstructor]
    public partial class ConstructedInt
    {
        public int Value { get; private set; }
    }
}