namespace Fifthweek.Api.Accounts
{
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.CodeGeneration;

    using Email = Fifthweek.Api.Identity.Shared.Membership.Email;
    using FileId = Fifthweek.Api.FileManagement.Shared.FileId;

    [AutoConstructor]
    [AutoEqualityMembers]
    public partial class GetAccountSettingsResult
    {
        public Email Email { get; private set; }

        public FileId ProfileImageFileId { get; private set; }
    }
}