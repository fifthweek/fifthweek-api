namespace Fifthweek.Payments.Services.Credit
{
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoPrimitive]
    public partial class AmountInMinorDenomination
    {
        public int Value { get; private set; }

        public decimal ToMajorDenomination()
        {
            return this.Value / 100.0m;
        }

        public PositiveInt ToPositiveInt()
        {
            return PositiveInt.Parse(this.Value);
        }

        public NonNegativeInt ToNonNegativeInt()
        {
            return NonNegativeInt.Parse(this.Value);
        }

        public static AmountInMinorDenomination FromMajorDenomination(decimal amount)
        {
            return new AmountInMinorDenomination((int)(amount * 100));
        }

        public static AmountInMinorDenomination Create(PositiveInt value)
        {
            return new AmountInMinorDenomination(value.Value);
        }

        public static AmountInMinorDenomination Create(NonNegativeInt value)
        {
            return new AmountInMinorDenomination(value.Value);
        }
    }
}