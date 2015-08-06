namespace Fifthweek.Api.Payments.Commands
{
    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers]
    public partial class BlockPaymentProcessingResult
    {
        public int LeaseLengthSeconds { get; private set; }

        public string LeaseId { get; private set; }
    }
}