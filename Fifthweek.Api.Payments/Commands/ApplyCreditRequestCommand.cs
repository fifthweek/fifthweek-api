namespace Fifthweek.Api.Payments.Commands
{
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor, AutoEqualityMembers]
    public partial class ApplyCreditRequestCommand
    {
        public Requester Requester { get; private set; }

        public UserId UserId { get; private set; }

        public PositiveInt Amount { get; private set; }

        public PositiveInt ExpectedTotalAmount { get; private set; }
    }
}