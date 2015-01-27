﻿namespace Fifthweek.Api.Accounts.Commands
{
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;

    using UserId = Fifthweek.Api.Identity.Shared.Membership.UserId;

    [AutoConstructor]
    [AutoEqualityMembers]
    public partial class UpdateAccountSettingsCommand
    {
        public Requester Requester { get; private set; }

        public UserId RequestedUserId { get; private set; }

        public ValidUsername NewUsername { get; private set; }

        public ValidEmail NewEmail { get; private set; }

        public ValidPassword NewPassword { get; private set; }

        public FileId NewProfileImageId { get; private set; }
    }
}