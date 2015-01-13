namespace Fifthweek.Api.Accounts.Controllers
{
    using System;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.Identity.Membership;

    [AutoEqualityMembers]
    public partial class UpdatedAccountSettings
    {
        [Parsed(typeof(Username))]
        public string NewUsername { get; set; }

        [Parsed(typeof(Email))]
        public string NewEmail { get; set; }

        [Optional]
        [Parsed(typeof(Password))]
        public string NewPassword { get; set; }

        public string NewProfileImageId { get; set; }
    }
}