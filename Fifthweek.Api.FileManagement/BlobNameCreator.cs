namespace Fifthweek.Api.FileManagement
{
    using System;
    using System.Text;

    using Fifthweek.Api.Identity.Membership;

    public class BlobNameCreator : IBlobNameCreator
    {
        public string CreateFileName(FileId fileId)
        {
            return fileId.Value.ToString().ToLower();
        }
    }
}