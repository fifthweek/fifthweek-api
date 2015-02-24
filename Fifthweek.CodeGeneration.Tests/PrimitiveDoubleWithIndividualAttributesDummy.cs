namespace Fifthweek.CodeGeneration.Tests
{
    [AutoConstructor, AutoEqualityMembers, AutoJson]
    public partial class PrimitiveDoubleWithIndividualAttributesDummy
    {
        public double Value { get; private set; }
    }
}