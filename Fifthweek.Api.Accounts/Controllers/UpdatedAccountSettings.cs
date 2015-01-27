namespace Fifthweek.Api.Accounts.Controllers
{
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;

    [AutoEqualityMembers]
    public partial class UpdatedAccountSettings
    {
        [Parsed(typeof(ValidUsername))]
        public string NewUsername { get; set; }

        [Parsed(typeof(ValidEmail))]
        public string NewEmail { get; set; }

        [Optional]
        [Parsed(typeof(ValidPassword))]
        public string NewPassword { get; set; }

        public string NewProfileImageId { get; set; }
    }
}