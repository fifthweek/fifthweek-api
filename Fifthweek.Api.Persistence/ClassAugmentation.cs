
using System;





namespace Fifthweek.Api.Persistence
{
	using System;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	using Fifthweek.Api.Core;
	using Fifthweek.Api.Persistence.Identity;
	public partial class File
	{
        public File(
            System.Guid id, 
            Fifthweek.Api.Persistence.Identity.FifthweekUser user, 
            System.Guid userId, 
            Fifthweek.Api.Persistence.FileState state, 
            System.DateTime uploadStartedDate, 
            System.Nullable<System.DateTime> uploadCompletedDate, 
            System.Nullable<System.DateTime> processingStartedDate, 
            System.Nullable<System.DateTime> processingCompletedDate, 
            System.String fileNameWithoutExtension, 
            System.String fileExtension, 
            System.Int64 blobSizeBytes, 
            System.String purpose)
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

            if (uploadStartedDate == null)
            {
                throw new ArgumentNullException("uploadStartedDate");
            }

            if (fileNameWithoutExtension == null)
            {
                throw new ArgumentNullException("fileNameWithoutExtension");
            }

            if (fileExtension == null)
            {
                throw new ArgumentNullException("fileExtension");
            }

            if (blobSizeBytes == null)
            {
                throw new ArgumentNullException("blobSizeBytes");
            }

            if (purpose == null)
            {
                throw new ArgumentNullException("purpose");
            }

            this.Id = id;
            this.User = user;
            this.UserId = userId;
            this.State = state;
            this.UploadStartedDate = uploadStartedDate;
            this.UploadCompletedDate = uploadCompletedDate;
            this.ProcessingStartedDate = processingStartedDate;
            this.ProcessingCompletedDate = processingCompletedDate;
            this.FileNameWithoutExtension = fileNameWithoutExtension;
            this.FileExtension = fileExtension;
            this.BlobSizeBytes = blobSizeBytes;
            this.Purpose = purpose;
        }
	}

}
namespace Fifthweek.Api.Persistence
{
	using System;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	using Fifthweek.Api.Core;
	using Fifthweek.Api.Persistence.Identity;
	public partial class Subscription
	{
        public Subscription(
            System.Guid id, 
            Fifthweek.Api.Persistence.Identity.FifthweekUser creator, 
            System.Guid creatorId, 
            System.String name, 
            System.String tagline, 
            System.DateTime creationDate)
        {
            if (id == null)
            {
                throw new ArgumentNullException("id");
            }

            if (creatorId == null)
            {
                throw new ArgumentNullException("creatorId");
            }

            if (name == null)
            {
                throw new ArgumentNullException("name");
            }

            if (tagline == null)
            {
                throw new ArgumentNullException("tagline");
            }

            if (creationDate == null)
            {
                throw new ArgumentNullException("creationDate");
            }

            this.Id = id;
            this.Creator = creator;
            this.CreatorId = creatorId;
            this.Name = name;
            this.Tagline = tagline;
            this.CreationDate = creationDate;
        }
	}

}


