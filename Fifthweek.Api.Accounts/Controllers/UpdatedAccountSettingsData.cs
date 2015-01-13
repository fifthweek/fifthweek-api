namespace Fifthweek.Api.Accounts.Controllers
{
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement;

    [AutoEqualityMembers]
    public partial class UpdatedAccountSettingsData
    {
        public string NewUsername { get; set; }

        public string NewEmail { get; set; }

        public FileId NewProfileImageId { get; set; }
    }
}