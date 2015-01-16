namespace Fifthweek.Api.Accounts
{
    using System;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Shared;

    public partial class GetAccountSettingsDapperResult
    {
        [Constructed(typeof(Email))]
        public string Email { get; set; }

        [Constructed(typeof(FileId))]
        public Guid ProfileImageFileId { get; set; }
    }
}