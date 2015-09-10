namespace Fifthweek.Api.Identity.Membership
{
    using Fifthweek.Api.Azure;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.CodeGeneration;

    [AutoConstructor]
    [AutoEqualityMembers]
    public partial class GetAccountSettingsResult
    {
        public Username Username { get; private set; }

        public Email Email { get; private set; }

        [Optional]
        public FileInformation ProfileImage { get; private set; }

        public int AccountBalance { get; set; }

        public PaymentStatus PaymentStatus { get; private set; }

        public bool IsRetryingPayment
        {
            get
            {
                return this.PaymentStatus > PaymentStatus.None && this.PaymentStatus < PaymentStatus.Failed;
            }
        }

        public bool HasPaymentInformation { get; private set; }

        public decimal CreatorPercentage { get; private set; }

        [Optional]
        public int? CreatorPercentageWeeksRemaining { get; private set; }
    }
}