namespace Fifthweek.Api.Payments.Controllers
{
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor, AutoEqualityMembers]
    public partial class CreditRequestData
    {
        public CreditRequestData()
        {
        }

        [Parsed(typeof(PositiveInt))]
        public int Amount { get; set; }

        [Parsed(typeof(PositiveInt))]
        public int ExpectedTotalAmount { get; set; }
    }
}