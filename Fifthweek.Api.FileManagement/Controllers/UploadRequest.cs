namespace Fifthweek.Api.FileManagement.Controllers
{
    using Fifthweek.Api.Core;
    using Fifthweek.Shared;

    [AutoEqualityMembers]
    [AutoConstructor]
    public partial class UploadRequest
    {
        public UploadRequest()
        {
        }

        public string FilePath { get; set; }

        public string Purpose { get; set; }
    }
}