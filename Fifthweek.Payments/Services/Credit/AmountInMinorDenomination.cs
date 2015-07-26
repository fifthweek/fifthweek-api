namespace Fifthweek.Payments.Services.Credit
{
    using Fifthweek.CodeGeneration;

    [AutoPrimitive]
    public partial class AmountInMinorDenomination
    {
        public int Value { get; private set; }

        public decimal ToMajorDenomination()
        {
            return this.Value / 100.0m;
        }

        public static AmountInMinorDenomination FromMajorDenomination(decimal amount)
        {
            return new AmountInMinorDenomination((int)(amount * 100));
        }
    }
}