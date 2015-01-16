namespace Fifthweek.Api.Accounts
{
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    [AutoEqualityMembers]
    public partial class GetAccountSettingsResult
    {
        public Email Email { get; private set; }

        public FileId ProfileImageFileId { get; private set; }
    }
}