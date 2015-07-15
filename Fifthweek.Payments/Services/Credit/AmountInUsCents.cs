namespace Fifthweek.Payments.Services.Credit
{
    using Fifthweek.CodeGeneration;

    [AutoPrimitive]
    public partial class AmountInUsCents
    {
        public int Value { get; private set; }

        public decimal ToUsDollars()
        {
            return this.Value / 100.0m;
        }

        public static AmountInUsCents FromAmountInUsDollars(decimal amount)
        {
            return new AmountInUsCents((int)(amount * 100));
        }
    }
}