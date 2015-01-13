namespace Fifthweek.Api.Accounts.Commands
{
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.Identity.Membership;

    [AutoConstructor]
    [AutoEqualityMembers]
    public partial class UpdateAccountSettingsCommand
    {
        public UserId AuthenticatedUserId { get; private set; }

        public UserId RequestedUserId { get; private set; }

        public Username NewUsername { get; private set; }

        public Email NewEmail { get; private set; }

        public Password NewPassword { get; private set; }

        public FileId NewProfileImageId { get; private set; }
    }
}