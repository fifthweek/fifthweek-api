namespace Fifthweek.Api.Accounts.Controllers
{
    using Fifthweek.Api.Core;

    [AutoConstructor]
    [AutoEqualityMembers]
    public partial class AccountSettingsData
    {
        public string Email { get; private set; }

        [Optional]
        public string ProfileImageId { get; private set; }
    }
}