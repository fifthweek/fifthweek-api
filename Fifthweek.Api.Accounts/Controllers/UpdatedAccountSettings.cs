namespace Fifthweek.Api.Accounts.Controllers
{
    using System;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.Identity.Membership;

    [AutoEqualityMembers]
    public partial class UpdatedAccountSettings
    {
        [Parsed(typeof(ValidatedUsername))]
        public string NewUsername { get; set; }

        [Parsed(typeof(ValidatedEmail))]
        public string NewEmail { get; set; }

        [Optional]
        [Parsed(typeof(ValidatedPassword))]
        public string NewPassword { get; set; }

        public string NewProfileImageId { get; set; }
    }
}