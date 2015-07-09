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
        [Optional]
        public CreatorName Name { get; private set; }

        public Username Username { get; private set; }

        public Email Email { get; private set; }

        [Optional]
        public FileInformation ProfileImage { get; private set; }

        public int AccountBalance { get; set; }

        public BillingStatus BillingStatus { get; private set; }

        public bool HasCreditCardDetails { get; private set; }
    }
}