namespace Fifthweek.Api.Accounts.Controllers
{
    using Fifthweek.Api.Core;

    [AutoConstructor]
    [AutoEqualityMembers]
    public partial class AccountSettingsResult
    {
        public string Email { get; private set; }

        [Optional]
        public string ProfileImageFileId { get; private set; }
    }
}