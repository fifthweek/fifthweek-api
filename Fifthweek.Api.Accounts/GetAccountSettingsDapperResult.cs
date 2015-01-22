namespace Fifthweek.Api.Accounts
{
    using System;

    public partial class GetAccountSettingsDapperResult
    {
        public string Email { get; set; }

        public Guid ProfileImageFileId { get; set; }
    }
}