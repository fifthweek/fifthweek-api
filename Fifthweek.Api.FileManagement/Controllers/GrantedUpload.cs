namespace Fifthweek.Api.FileManagement.Controllers
{
    using System;

    using Fifthweek.Api.Azure;
    using Fifthweek.Api.Core;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    [AutoEqualityMembers]
    public partial class GrantedUpload
    {
        public FileId FileId { get; private set; }

        public BlobSharedAccessInformation AccessInformation { get; private set; }
    }
}