namespace Fifthweek.Api.Accounts.Commands
{
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Shared;

    [AutoConstructor]
    [AutoEqualityMembers]
    public partial class UpdateAccountSettingsCommand
    {
        public UserId AuthenticatedUserId { get; private set; }

        public UserId RequestedUserId { get; private set; }

        public ValidUsername NewUsername { get; private set; }

        public ValidEmail NewEmail { get; private set; }

        public ValidPassword NewPassword { get; private set; }

        public FileId NewProfileImageId { get; private set; }
    }
}