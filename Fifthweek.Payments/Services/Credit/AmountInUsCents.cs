namespace Fifthweek.Payments.Services.Credit
{
    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers]
    public partial class AmountInUsCents
    {
        public int Value { get; private set; }
    }
}