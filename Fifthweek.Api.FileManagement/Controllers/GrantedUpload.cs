namespace Fifthweek.Api.FileManagement.Controllers
{
    using System;

    using Fifthweek.Api.Core;

    [AutoConstructor]
    [AutoEqualityMembers]
    public partial class GrantedUpload
    {
        public string FileId { get; private set; }

        public string UploadUri { get; private set; }
    }
}