﻿namespace Fifthweek.Api.FileManagement.Controllers
{
    using Fifthweek.Api.Core;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoEqualityMembers]
    [AutoConstructor]
    public partial class UploadRequest
    {
        public UploadRequest()
        {
        }

        [Optional]
        public string ChannelId { get; set; }

        public string FilePath { get; set; }

        public string Purpose { get; set; }
    }
}