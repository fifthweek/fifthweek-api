namespace Fifthweek.Api.Accounts.Controllers
{
    using System;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

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