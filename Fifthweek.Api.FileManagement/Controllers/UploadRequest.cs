namespace Fifthweek.Api.FileManagement.Controllers
{
    using Fifthweek.Api.Core;

    [AutoEqualityMembers]
    public partial class UploadRequest
    {
        public string FilePath { get; set; }

        public string Purpose { get; set; }
    }
}