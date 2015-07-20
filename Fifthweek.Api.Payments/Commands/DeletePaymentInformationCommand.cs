namespace Fifthweek.Api.Payments.Commands
{
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers]
    public partial class DeletePaymentInformationCommand
    {
        public Requester Requester { get; private set; }

        public UserId UserId { get; private set; }
    }
}