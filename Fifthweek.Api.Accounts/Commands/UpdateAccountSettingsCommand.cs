namespace Fifthweek.Api.Accounts.Commands
{
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.Identity.Membership;

    [AutoConstructor]
    [AutoEqualityMembers]
    public partial class UpdateAccountSettingsCommand
    {
        public UserId Requester { get; private set; }

        public UserId RequestedAccount { get; private set; }

        [Optional]
        public string NewUsername { get; private set; }

        [Optional]
        public string NewEmail { get; private set; }

        [Optional]
        public FileId NewProfileImageId { get; private set; }
    }
}