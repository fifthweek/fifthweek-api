namespace Fifthweek.Api.FileManagement.Controllers
{
    using Fifthweek.CodeGeneration;

    [AutoEqualityMembers]
    [AutoConstructor]
    public partial class UploadCompleteNotification
    {
        public UploadCompleteNotification()
        {
        }

        [Optional]
        public string ChannelId { get; set; }

        public string FileId { get; set; }
    }
}