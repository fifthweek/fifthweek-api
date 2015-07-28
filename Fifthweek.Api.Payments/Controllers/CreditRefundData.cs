namespace Fifthweek.Api.Payments.Controllers
{
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Services.Refunds;
    using Fifthweek.Shared;

    [AutoConstructor, AutoEqualityMembers]
    public partial class CreditRefundData
    {
        public CreditRefundData()
        {
        }

        [Parsed(typeof(PositiveInt))]
        public int RefundCreditAmount { get; set; }

        public RefundCreditReason Reason { get; set; }

        public string Comment { get; set; }
    }
}