
using System;





namespace Fifthweek.Api.Persistence
{
	using System;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	using Fifthweek.Api.Core;
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
namespace Fifthweek.Api.Persistence
{
	using System;
	using System.ComponentModel.DataAnnotations;
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


