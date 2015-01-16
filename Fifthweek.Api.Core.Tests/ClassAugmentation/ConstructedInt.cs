namespace Fifthweek.Api.Core.Tests.ClassAugmentation
{
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoEqualityMembers, AutoConstructor]
    public partial class ConstructedInt
    {
        public int Value { get; private set; }
    }
}