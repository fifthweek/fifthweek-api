namespace Fifthweek.Api.Identity.Membership
{
    using Fifthweek.Api.Azure;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;

    [AutoConstructor]
    [AutoEqualityMembers]
    public partial class GetAccountSettingsResult
    {
        public Email Email { get; private set; }

        [Optional]
        public FileInformation ProfileImage { get; private set; }
    }
}