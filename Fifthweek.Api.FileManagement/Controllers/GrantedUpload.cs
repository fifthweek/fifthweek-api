namespace Fifthweek.Api.FileManagement.Controllers
{
    using System;

    public class GrantedUpload
    {
        public GrantedUpload(Guid fileId, string uploadUri)
        {
            this.FileId = fileId;
            this.UploadUri = uploadUri;
        }

        public Guid FileId { get; private set; }

        public string UploadUri { get; private set; }
    }
}