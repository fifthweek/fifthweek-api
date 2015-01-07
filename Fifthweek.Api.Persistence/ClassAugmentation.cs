
using System;






namespace Fifthweek.Api.Persistence
{
	public partial class File
	{
        public File(
            System.Guid id, 
            System.Guid userId, 
            Fifthweek.Api.Persistence.FileState state, 
            System.String blobReference, 
            System.DateTime creationDate, 
            System.Nullable<System.DateTime> completionDate, 
            System.Nullable<System.DateTime> processedDate, 
            System.String fileName, 
            System.String mimeType)
        {
            if (id == null)
            {
                throw new ArgumentNullException("id");
            }

            if (userId == null)
            {
                throw new ArgumentNullException("userId");
            }

            if (state == null)
            {
                throw new ArgumentNullException("state");
            }

            if (blobReference == null)
            {
                throw new ArgumentNullException("blobReference");
            }

            if (creationDate == null)
            {
                throw new ArgumentNullException("creationDate");
            }

            if (fileName == null)
            {
                throw new ArgumentNullException("fileName");
            }

            this.Id = id;
            this.UserId = userId;
            this.State = state;
            this.BlobReference = blobReference;
            this.CreationDate = creationDate;
            this.CompletionDate = completionDate;
            this.ProcessedDate = processedDate;
            this.FileName = fileName;
            this.MimeType = mimeType;
        }
	}
}


