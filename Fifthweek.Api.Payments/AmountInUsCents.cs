namespace Fifthweek.Api.Payments
{
    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers]
    public partial class AmountInUsCents
    {
        public int Value { get; private set; }
    }
}