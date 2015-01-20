using System;
using System.Linq;



namespace Fifthweek.WebJobs.Deletions.Shared
{
    using System;
    using Fifthweek.CodeGeneration;
    public partial class DeletePostMessage 
    {
        public DeletePostMessage(
            System.Guid postId, 
            System.Guid fileId, 
            System.Guid imageId)
        {
            if (postId == null)
            {
                throw new ArgumentNullException("postId");
            }

            if (fileId == null)
            {
                throw new ArgumentNullException("fileId");
            }

            if (imageId == null)
            {
                throw new ArgumentNullException("imageId");
            }

            this.PostId = postId;
            this.FileId = fileId;
            this.ImageId = imageId;
        }
    }

}


