namespace Fifthweek.Api.Payments.Controllers
{
    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers]
    public partial class TransactionRefundData
    {
        public TransactionRefundData()
        {
        }

        public string Comment { get; set; }
    }
}