using System;
using System.Linq;

//// Generated on 13/02/2015 19:33:19 (UTC)
//// Mapped solution in 3.8s


namespace Fifthweek.Api.Persistence
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Fifthweek.CodeGeneration;
    using System.Linq;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Core;
    using Fifthweek.Shared;
    using Dapper;

    public partial class Channel 
    {
        public Channel(
            System.Guid id,
            System.Guid subscriptionId,
            Fifthweek.Api.Persistence.Subscription subscription,
            System.String name,
            System.String description,
            System.Int32 priceInUsCentsPerWeek,
            System.Boolean isVisibleToNonSubscribers,
            System.DateTime creationDate)
        {
            if (id == null)
            {
                throw new ArgumentNullException("id");
            }

            if (subscriptionId == null)
            {
                throw new ArgumentNullException("subscriptionId");
            }

            if (priceInUsCentsPerWeek == null)
            {
                throw new ArgumentNullException("priceInUsCentsPerWeek");
            }

            if (isVisibleToNonSubscribers == null)
            {
                throw new ArgumentNullException("isVisibleToNonSubscribers");
            }

            if (creationDate == null)
            {
                throw new ArgumentNullException("creationDate");
            }

            this.Id = id;
            this.SubscriptionId = subscriptionId;
            this.Subscription = subscription;
            this.Name = name;
            this.Description = description;
            this.PriceInUsCentsPerWeek = priceInUsCentsPerWeek;
            this.IsVisibleToNonSubscribers = isVisibleToNonSubscribers;
            this.CreationDate = creationDate;
        }
    }
}
namespace Fifthweek.Api.Persistence
{
    using System;
    using System.Linq;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Core;
    using Fifthweek.Shared;
    using Dapper;

    public partial class Collection 
    {
        public Collection(
            System.Guid id,
            System.Guid channelId,
            Fifthweek.Api.Persistence.Channel channel,
            System.String name,
            System.DateTime queueExclusiveLowerBound,
            System.DateTime creationDate)
        {
            if (id == null)
            {
                throw new ArgumentNullException("id");
            }

            if (channelId == null)
            {
                throw new ArgumentNullException("channelId");
            }

            if (name == null)
            {
                throw new ArgumentNullException("name");
            }

            if (queueExclusiveLowerBound == null)
            {
                throw new ArgumentNullException("queueExclusiveLowerBound");
            }

            if (creationDate == null)
            {
                throw new ArgumentNullException("creationDate");
            }

            this.Id = id;
            this.ChannelId = channelId;
            this.Channel = channel;
            this.Name = name;
            this.QueueExclusiveLowerBound = queueExclusiveLowerBound;
            this.CreationDate = creationDate;
        }
    }
}
namespace Fifthweek.Api.Persistence
{
    using System;
    using System.Linq;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Core;
    using Fifthweek.Shared;
    using Dapper;

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
    using System.Linq;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Core;
    using Fifthweek.Shared;
    using Dapper;

    public partial class Post 
    {
        public Post(
            System.Guid id,
            System.Guid channelId,
            Fifthweek.Api.Persistence.Channel channel,
            System.Nullable<System.Guid> collectionId,
            Fifthweek.Api.Persistence.Collection collection,
            System.Nullable<System.Guid> fileId,
            Fifthweek.Api.Persistence.File file,
            System.Nullable<System.Guid> imageId,
            Fifthweek.Api.Persistence.File image,
            System.String comment,
            System.Boolean scheduledByQueue,
            System.DateTime liveDate,
            System.DateTime creationDate)
        {
            if (id == null)
            {
                throw new ArgumentNullException("id");
            }

            if (channelId == null)
            {
                throw new ArgumentNullException("channelId");
            }

            if (scheduledByQueue == null)
            {
                throw new ArgumentNullException("scheduledByQueue");
            }

            if (liveDate == null)
            {
                throw new ArgumentNullException("liveDate");
            }

            if (creationDate == null)
            {
                throw new ArgumentNullException("creationDate");
            }

            this.Id = id;
            this.ChannelId = channelId;
            this.Channel = channel;
            this.CollectionId = collectionId;
            this.Collection = collection;
            this.FileId = fileId;
            this.File = file;
            this.ImageId = imageId;
            this.Image = image;
            this.Comment = comment;
            this.ScheduledByQueue = scheduledByQueue;
            this.LiveDate = liveDate;
            this.CreationDate = creationDate;
        }
    }
}
namespace Fifthweek.Api.Persistence
{
    using System;
    using System.Linq;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Core;
    using Fifthweek.Shared;
    using Dapper;

    public partial class RefreshToken 
    {
        public RefreshToken(
            System.String hashedId,
            System.String username,
            System.String clientId,
            System.DateTime issuedDate,
            System.DateTime expiresDate,
            System.String protectedTicket)
        {
            if (hashedId == null)
            {
                throw new ArgumentNullException("hashedId");
            }

            if (username == null)
            {
                throw new ArgumentNullException("username");
            }

            if (clientId == null)
            {
                throw new ArgumentNullException("clientId");
            }

            if (issuedDate == null)
            {
                throw new ArgumentNullException("issuedDate");
            }

            if (expiresDate == null)
            {
                throw new ArgumentNullException("expiresDate");
            }

            if (protectedTicket == null)
            {
                throw new ArgumentNullException("protectedTicket");
            }

            this.HashedId = hashedId;
            this.Username = username;
            this.ClientId = clientId;
            this.IssuedDate = issuedDate;
            this.ExpiresDate = expiresDate;
            this.ProtectedTicket = protectedTicket;
        }
    }
}
namespace Fifthweek.Api.Persistence
{
    using System;
    using System.Linq;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Core;
    using Fifthweek.Shared;
    using Dapper;

    public partial class Subscription 
    {
        public Subscription(
            System.Guid id,
            System.Guid creatorId,
            Fifthweek.Api.Persistence.Identity.FifthweekUser creator,
            System.String name,
            System.String tagline,
            System.String introduction,
            System.String description,
            System.String externalVideoUrl,
            System.Nullable<System.Guid> headerImageFileId,
            Fifthweek.Api.Persistence.File headerImageFile,
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

            if (introduction == null)
            {
                throw new ArgumentNullException("introduction");
            }

            if (creationDate == null)
            {
                throw new ArgumentNullException("creationDate");
            }

            this.Id = id;
            this.CreatorId = creatorId;
            this.Creator = creator;
            this.Name = name;
            this.Tagline = tagline;
            this.Introduction = introduction;
            this.Description = description;
            this.ExternalVideoUrl = externalVideoUrl;
            this.HeaderImageFileId = headerImageFileId;
            this.HeaderImageFile = headerImageFile;
            this.CreationDate = creationDate;
        }
    }
}
namespace Fifthweek.Api.Persistence
{
    using System;
    using System.Linq;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Core;
    using Fifthweek.Shared;
    using Dapper;

    public partial class WeeklyReleaseTime 
    {
        public WeeklyReleaseTime(
            System.Guid collectionId,
            Fifthweek.Api.Persistence.Collection collection,
            System.Byte hourOfWeek)
        {
            if (collectionId == null)
            {
                throw new ArgumentNullException("collectionId");
            }

            if (hourOfWeek == null)
            {
                throw new ArgumentNullException("hourOfWeek");
            }

            this.CollectionId = collectionId;
            this.Collection = collection;
            this.HourOfWeek = hourOfWeek;
        }
    }
}
namespace Fifthweek.Api.Persistence
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Fifthweek.CodeGeneration;

    public partial class EndToEndTestEmail 
    {
        public EndToEndTestEmail(
            System.String mailbox,
            System.String subject,
            System.String body,
            System.DateTime dateReceived)
        {
            if (mailbox == null)
            {
                throw new ArgumentNullException("mailbox");
            }

            if (subject == null)
            {
                throw new ArgumentNullException("subject");
            }

            if (body == null)
            {
                throw new ArgumentNullException("body");
            }

            if (dateReceived == null)
            {
                throw new ArgumentNullException("dateReceived");
            }

            this.Mailbox = mailbox;
            this.Subject = subject;
            this.Body = body;
            this.DateReceived = dateReceived;
        }
    }
}

namespace Fifthweek.Api.Persistence
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Fifthweek.CodeGeneration;
    using System.Linq;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Core;
    using Fifthweek.Shared;
    using Dapper;

    public partial class Channel 
    {
        public override string ToString()
        {
            return string.Format("Channel({0}, {1}, \"{2}\", \"{3}\", {4}, {5}, {6})", this.Id == null ? "null" : this.Id.ToString(), this.SubscriptionId == null ? "null" : this.SubscriptionId.ToString(), this.Name == null ? "null" : this.Name.ToString(), this.Description == null ? "null" : this.Description.ToString(), this.PriceInUsCentsPerWeek == null ? "null" : this.PriceInUsCentsPerWeek.ToString(), this.IsVisibleToNonSubscribers == null ? "null" : this.IsVisibleToNonSubscribers.ToString(), this.CreationDate == null ? "null" : this.CreationDate.ToString());
        }
        
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
        
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
        
            if (obj.GetType() != this.GetType())
            {
                return false;
            }
        
            return this.Equals((Channel)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Id != null ? this.Id.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.SubscriptionId != null ? this.SubscriptionId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Name != null ? this.Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Description != null ? this.Description.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.PriceInUsCentsPerWeek != null ? this.PriceInUsCentsPerWeek.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.IsVisibleToNonSubscribers != null ? this.IsVisibleToNonSubscribers.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.CreationDate != null ? this.CreationDate.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(Channel other)
        {
            if (!object.Equals(this.Id, other.Id))
            {
                return false;
            }
        
            if (!object.Equals(this.SubscriptionId, other.SubscriptionId))
            {
                return false;
            }
        
            if (!object.Equals(this.Name, other.Name))
            {
                return false;
            }
        
            if (!object.Equals(this.Description, other.Description))
            {
                return false;
            }
        
            if (!object.Equals(this.PriceInUsCentsPerWeek, other.PriceInUsCentsPerWeek))
            {
                return false;
            }
        
            if (!object.Equals(this.IsVisibleToNonSubscribers, other.IsVisibleToNonSubscribers))
            {
                return false;
            }
        
            if (!object.Equals(this.CreationDate, other.CreationDate))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Api.Persistence
{
    using System;
    using System.Linq;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Core;
    using Fifthweek.Shared;
    using Dapper;

    public partial class Collection 
    {
        public override string ToString()
        {
            return string.Format("Collection({0}, {1}, \"{2}\", {3}, {4})", this.Id == null ? "null" : this.Id.ToString(), this.ChannelId == null ? "null" : this.ChannelId.ToString(), this.Name == null ? "null" : this.Name.ToString(), this.QueueExclusiveLowerBound == null ? "null" : this.QueueExclusiveLowerBound.ToString(), this.CreationDate == null ? "null" : this.CreationDate.ToString());
        }
        
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
        
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
        
            if (obj.GetType() != this.GetType())
            {
                return false;
            }
        
            return this.Equals((Collection)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Id != null ? this.Id.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.ChannelId != null ? this.ChannelId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Name != null ? this.Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.QueueExclusiveLowerBound != null ? this.QueueExclusiveLowerBound.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.CreationDate != null ? this.CreationDate.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(Collection other)
        {
            if (!object.Equals(this.Id, other.Id))
            {
                return false;
            }
        
            if (!object.Equals(this.ChannelId, other.ChannelId))
            {
                return false;
            }
        
            if (!object.Equals(this.Name, other.Name))
            {
                return false;
            }
        
            if (!object.Equals(this.QueueExclusiveLowerBound, other.QueueExclusiveLowerBound))
            {
                return false;
            }
        
            if (!object.Equals(this.CreationDate, other.CreationDate))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Api.Persistence
{
    using System;
    using System.Linq;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Core;
    using Fifthweek.Shared;
    using Dapper;

    public partial class File 
    {
        public override string ToString()
        {
            return string.Format("File({0}, {1}, {2}, {3}, {4}, {5}, {6}, \"{7}\", \"{8}\", {9}, \"{10}\")", this.Id == null ? "null" : this.Id.ToString(), this.UserId == null ? "null" : this.UserId.ToString(), this.State == null ? "null" : this.State.ToString(), this.UploadStartedDate == null ? "null" : this.UploadStartedDate.ToString(), this.UploadCompletedDate == null ? "null" : this.UploadCompletedDate.ToString(), this.ProcessingStartedDate == null ? "null" : this.ProcessingStartedDate.ToString(), this.ProcessingCompletedDate == null ? "null" : this.ProcessingCompletedDate.ToString(), this.FileNameWithoutExtension == null ? "null" : this.FileNameWithoutExtension.ToString(), this.FileExtension == null ? "null" : this.FileExtension.ToString(), this.BlobSizeBytes == null ? "null" : this.BlobSizeBytes.ToString(), this.Purpose == null ? "null" : this.Purpose.ToString());
        }
        
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
        
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
        
            if (obj.GetType() != this.GetType())
            {
                return false;
            }
        
            return this.Equals((File)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Id != null ? this.Id.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.UserId != null ? this.UserId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.State != null ? this.State.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.UploadStartedDate != null ? this.UploadStartedDate.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.UploadCompletedDate != null ? this.UploadCompletedDate.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.ProcessingStartedDate != null ? this.ProcessingStartedDate.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.ProcessingCompletedDate != null ? this.ProcessingCompletedDate.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.FileNameWithoutExtension != null ? this.FileNameWithoutExtension.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.FileExtension != null ? this.FileExtension.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.BlobSizeBytes != null ? this.BlobSizeBytes.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Purpose != null ? this.Purpose.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(File other)
        {
            if (!object.Equals(this.Id, other.Id))
            {
                return false;
            }
        
            if (!object.Equals(this.UserId, other.UserId))
            {
                return false;
            }
        
            if (!object.Equals(this.State, other.State))
            {
                return false;
            }
        
            if (!object.Equals(this.UploadStartedDate, other.UploadStartedDate))
            {
                return false;
            }
        
            if (!object.Equals(this.UploadCompletedDate, other.UploadCompletedDate))
            {
                return false;
            }
        
            if (!object.Equals(this.ProcessingStartedDate, other.ProcessingStartedDate))
            {
                return false;
            }
        
            if (!object.Equals(this.ProcessingCompletedDate, other.ProcessingCompletedDate))
            {
                return false;
            }
        
            if (!object.Equals(this.FileNameWithoutExtension, other.FileNameWithoutExtension))
            {
                return false;
            }
        
            if (!object.Equals(this.FileExtension, other.FileExtension))
            {
                return false;
            }
        
            if (!object.Equals(this.BlobSizeBytes, other.BlobSizeBytes))
            {
                return false;
            }
        
            if (!object.Equals(this.Purpose, other.Purpose))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Api.Persistence
{
    using System;
    using System.Linq;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Core;
    using Fifthweek.Shared;
    using Dapper;

    public partial class Post 
    {
        public override string ToString()
        {
            return string.Format("Post({0}, {1}, {2}, {3}, {4}, \"{5}\", {6}, {7}, {8})", this.Id == null ? "null" : this.Id.ToString(), this.ChannelId == null ? "null" : this.ChannelId.ToString(), this.CollectionId == null ? "null" : this.CollectionId.ToString(), this.FileId == null ? "null" : this.FileId.ToString(), this.ImageId == null ? "null" : this.ImageId.ToString(), this.Comment == null ? "null" : this.Comment.ToString(), this.ScheduledByQueue == null ? "null" : this.ScheduledByQueue.ToString(), this.LiveDate == null ? "null" : this.LiveDate.ToString(), this.CreationDate == null ? "null" : this.CreationDate.ToString());
        }
        
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
        
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
        
            if (obj.GetType() != this.GetType())
            {
                return false;
            }
        
            return this.Equals((Post)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Id != null ? this.Id.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.ChannelId != null ? this.ChannelId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.CollectionId != null ? this.CollectionId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.FileId != null ? this.FileId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.ImageId != null ? this.ImageId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Comment != null ? this.Comment.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.ScheduledByQueue != null ? this.ScheduledByQueue.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.LiveDate != null ? this.LiveDate.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.CreationDate != null ? this.CreationDate.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(Post other)
        {
            if (!object.Equals(this.Id, other.Id))
            {
                return false;
            }
        
            if (!object.Equals(this.ChannelId, other.ChannelId))
            {
                return false;
            }
        
            if (!object.Equals(this.CollectionId, other.CollectionId))
            {
                return false;
            }
        
            if (!object.Equals(this.FileId, other.FileId))
            {
                return false;
            }
        
            if (!object.Equals(this.ImageId, other.ImageId))
            {
                return false;
            }
        
            if (!object.Equals(this.Comment, other.Comment))
            {
                return false;
            }
        
            if (!object.Equals(this.ScheduledByQueue, other.ScheduledByQueue))
            {
                return false;
            }
        
            if (!object.Equals(this.LiveDate, other.LiveDate))
            {
                return false;
            }
        
            if (!object.Equals(this.CreationDate, other.CreationDate))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Api.Persistence
{
    using System;
    using System.Linq;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Core;
    using Fifthweek.Shared;
    using Dapper;

    public partial class RefreshToken 
    {
        public override string ToString()
        {
            return string.Format("RefreshToken(\"{0}\", \"{1}\", \"{2}\", {3}, {4}, \"{5}\")", this.HashedId == null ? "null" : this.HashedId.ToString(), this.Username == null ? "null" : this.Username.ToString(), this.ClientId == null ? "null" : this.ClientId.ToString(), this.IssuedDate == null ? "null" : this.IssuedDate.ToString(), this.ExpiresDate == null ? "null" : this.ExpiresDate.ToString(), this.ProtectedTicket == null ? "null" : this.ProtectedTicket.ToString());
        }
        
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
        
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
        
            if (obj.GetType() != this.GetType())
            {
                return false;
            }
        
            return this.Equals((RefreshToken)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.HashedId != null ? this.HashedId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Username != null ? this.Username.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.ClientId != null ? this.ClientId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.IssuedDate != null ? this.IssuedDate.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.ExpiresDate != null ? this.ExpiresDate.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.ProtectedTicket != null ? this.ProtectedTicket.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(RefreshToken other)
        {
            if (!object.Equals(this.HashedId, other.HashedId))
            {
                return false;
            }
        
            if (!object.Equals(this.Username, other.Username))
            {
                return false;
            }
        
            if (!object.Equals(this.ClientId, other.ClientId))
            {
                return false;
            }
        
            if (!object.Equals(this.IssuedDate, other.IssuedDate))
            {
                return false;
            }
        
            if (!object.Equals(this.ExpiresDate, other.ExpiresDate))
            {
                return false;
            }
        
            if (!object.Equals(this.ProtectedTicket, other.ProtectedTicket))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Api.Persistence
{
    using System;
    using System.Linq;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Core;
    using Fifthweek.Shared;
    using Dapper;

    public partial class Subscription 
    {
        public override string ToString()
        {
            return string.Format("Subscription({0}, {1}, \"{2}\", \"{3}\", \"{4}\", \"{5}\", \"{6}\", {7}, {8})", this.Id == null ? "null" : this.Id.ToString(), this.CreatorId == null ? "null" : this.CreatorId.ToString(), this.Name == null ? "null" : this.Name.ToString(), this.Tagline == null ? "null" : this.Tagline.ToString(), this.Introduction == null ? "null" : this.Introduction.ToString(), this.Description == null ? "null" : this.Description.ToString(), this.ExternalVideoUrl == null ? "null" : this.ExternalVideoUrl.ToString(), this.HeaderImageFileId == null ? "null" : this.HeaderImageFileId.ToString(), this.CreationDate == null ? "null" : this.CreationDate.ToString());
        }
        
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
        
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
        
            if (obj.GetType() != this.GetType())
            {
                return false;
            }
        
            return this.Equals((Subscription)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Id != null ? this.Id.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.CreatorId != null ? this.CreatorId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Name != null ? this.Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Tagline != null ? this.Tagline.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Introduction != null ? this.Introduction.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Description != null ? this.Description.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.ExternalVideoUrl != null ? this.ExternalVideoUrl.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.HeaderImageFileId != null ? this.HeaderImageFileId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.CreationDate != null ? this.CreationDate.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(Subscription other)
        {
            if (!object.Equals(this.Id, other.Id))
            {
                return false;
            }
        
            if (!object.Equals(this.CreatorId, other.CreatorId))
            {
                return false;
            }
        
            if (!object.Equals(this.Name, other.Name))
            {
                return false;
            }
        
            if (!object.Equals(this.Tagline, other.Tagline))
            {
                return false;
            }
        
            if (!object.Equals(this.Introduction, other.Introduction))
            {
                return false;
            }
        
            if (!object.Equals(this.Description, other.Description))
            {
                return false;
            }
        
            if (!object.Equals(this.ExternalVideoUrl, other.ExternalVideoUrl))
            {
                return false;
            }
        
            if (!object.Equals(this.HeaderImageFileId, other.HeaderImageFileId))
            {
                return false;
            }
        
            if (!object.Equals(this.CreationDate, other.CreationDate))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Api.Persistence
{
    using System;
    using System.Linq;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Core;
    using Fifthweek.Shared;
    using Dapper;

    public partial class WeeklyReleaseTime 
    {
        public override string ToString()
        {
            return string.Format("WeeklyReleaseTime({0}, {1})", this.CollectionId == null ? "null" : this.CollectionId.ToString(), this.HourOfWeek == null ? "null" : this.HourOfWeek.ToString());
        }
        
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
        
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
        
            if (obj.GetType() != this.GetType())
            {
                return false;
            }
        
            return this.Equals((WeeklyReleaseTime)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.CollectionId != null ? this.CollectionId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.HourOfWeek != null ? this.HourOfWeek.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(WeeklyReleaseTime other)
        {
            if (!object.Equals(this.CollectionId, other.CollectionId))
            {
                return false;
            }
        
            if (!object.Equals(this.HourOfWeek, other.HourOfWeek))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Api.Persistence.Identity
{
    using System;
    using System.Linq;
    using System.ComponentModel.DataAnnotations;
    using Fifthweek.CodeGeneration;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Collections.Generic;

    public partial class FifthweekRole 
    {
        public override string ToString()
        {
            return string.Format("FifthweekRole({0}, \"{1}\")", this.Id == null ? "null" : this.Id.ToString(), this.Name == null ? "null" : this.Name.ToString());
        }
        
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
        
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
        
            if (obj.GetType() != this.GetType())
            {
                return false;
            }
        
            return this.Equals((FifthweekRole)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Id != null ? this.Id.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Name != null ? this.Name.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(FifthweekRole other)
        {
            if (!object.Equals(this.Id, other.Id))
            {
                return false;
            }
        
            if (!object.Equals(this.Name, other.Name))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Api.Persistence.Identity
{
    using System;
    using System.Linq;
    using System.ComponentModel.DataAnnotations;
    using Fifthweek.CodeGeneration;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Collections.Generic;

    public partial class FifthweekUser 
    {
        public override string ToString()
        {
            return string.Format("FifthweekUser(\"{0}\", {1}, {2}, {3}, {4}, {5}, {6}, \"{7}\", {8}, {9}, {10}, \"{11}\", \"{12}\", {13}, \"{14}\", {15}, \"{16}\")", this.ExampleWork == null ? "null" : this.ExampleWork.ToString(), this.RegistrationDate == null ? "null" : this.RegistrationDate.ToString(), this.LastSignInDate == null ? "null" : this.LastSignInDate.ToString(), this.LastAccessTokenDate == null ? "null" : this.LastAccessTokenDate.ToString(), this.ProfileImageFileId == null ? "null" : this.ProfileImageFileId.ToString(), this.Id == null ? "null" : this.Id.ToString(), this.AccessFailedCount == null ? "null" : this.AccessFailedCount.ToString(), this.Email == null ? "null" : this.Email.ToString(), this.EmailConfirmed == null ? "null" : this.EmailConfirmed.ToString(), this.LockoutEnabled == null ? "null" : this.LockoutEnabled.ToString(), this.LockoutEndDateUtc == null ? "null" : this.LockoutEndDateUtc.ToString(), this.PasswordHash == null ? "null" : this.PasswordHash.ToString(), this.PhoneNumber == null ? "null" : this.PhoneNumber.ToString(), this.PhoneNumberConfirmed == null ? "null" : this.PhoneNumberConfirmed.ToString(), this.SecurityStamp == null ? "null" : this.SecurityStamp.ToString(), this.TwoFactorEnabled == null ? "null" : this.TwoFactorEnabled.ToString(), this.UserName == null ? "null" : this.UserName.ToString());
        }
        
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
        
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
        
            if (obj.GetType() != this.GetType())
            {
                return false;
            }
        
            return this.Equals((FifthweekUser)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.ExampleWork != null ? this.ExampleWork.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.RegistrationDate != null ? this.RegistrationDate.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.LastSignInDate != null ? this.LastSignInDate.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.LastAccessTokenDate != null ? this.LastAccessTokenDate.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.ProfileImageFileId != null ? this.ProfileImageFileId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Id != null ? this.Id.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.AccessFailedCount != null ? this.AccessFailedCount.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Email != null ? this.Email.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.EmailConfirmed != null ? this.EmailConfirmed.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.LockoutEnabled != null ? this.LockoutEnabled.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.LockoutEndDateUtc != null ? this.LockoutEndDateUtc.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.PasswordHash != null ? this.PasswordHash.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.PhoneNumber != null ? this.PhoneNumber.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.PhoneNumberConfirmed != null ? this.PhoneNumberConfirmed.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.SecurityStamp != null ? this.SecurityStamp.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.TwoFactorEnabled != null ? this.TwoFactorEnabled.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.UserName != null ? this.UserName.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(FifthweekUser other)
        {
            if (!object.Equals(this.ExampleWork, other.ExampleWork))
            {
                return false;
            }
        
            if (!object.Equals(this.RegistrationDate, other.RegistrationDate))
            {
                return false;
            }
        
            if (!object.Equals(this.LastSignInDate, other.LastSignInDate))
            {
                return false;
            }
        
            if (!object.Equals(this.LastAccessTokenDate, other.LastAccessTokenDate))
            {
                return false;
            }
        
            if (!object.Equals(this.ProfileImageFileId, other.ProfileImageFileId))
            {
                return false;
            }
        
            if (!object.Equals(this.Id, other.Id))
            {
                return false;
            }
        
            if (!object.Equals(this.AccessFailedCount, other.AccessFailedCount))
            {
                return false;
            }
        
            if (!object.Equals(this.Email, other.Email))
            {
                return false;
            }
        
            if (!object.Equals(this.EmailConfirmed, other.EmailConfirmed))
            {
                return false;
            }
        
            if (!object.Equals(this.LockoutEnabled, other.LockoutEnabled))
            {
                return false;
            }
        
            if (!object.Equals(this.LockoutEndDateUtc, other.LockoutEndDateUtc))
            {
                return false;
            }
        
            if (!object.Equals(this.PasswordHash, other.PasswordHash))
            {
                return false;
            }
        
            if (!object.Equals(this.PhoneNumber, other.PhoneNumber))
            {
                return false;
            }
        
            if (!object.Equals(this.PhoneNumberConfirmed, other.PhoneNumberConfirmed))
            {
                return false;
            }
        
            if (!object.Equals(this.SecurityStamp, other.SecurityStamp))
            {
                return false;
            }
        
            if (!object.Equals(this.TwoFactorEnabled, other.TwoFactorEnabled))
            {
                return false;
            }
        
            if (!object.Equals(this.UserName, other.UserName))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Api.Persistence.Identity
{
    using System;
    using System.Linq;
    using System.ComponentModel.DataAnnotations;
    using Fifthweek.CodeGeneration;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Collections.Generic;

    public partial class FifthweekUserRole 
    {
        public override string ToString()
        {
            return string.Format("FifthweekUserRole({0}, {1})", this.RoleId == null ? "null" : this.RoleId.ToString(), this.UserId == null ? "null" : this.UserId.ToString());
        }
        
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
        
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
        
            if (obj.GetType() != this.GetType())
            {
                return false;
            }
        
            return this.Equals((FifthweekUserRole)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.RoleId != null ? this.RoleId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.UserId != null ? this.UserId.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(FifthweekUserRole other)
        {
            if (!object.Equals(this.RoleId, other.RoleId))
            {
                return false;
            }
        
            if (!object.Equals(this.UserId, other.UserId))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Api.Persistence
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Fifthweek.CodeGeneration;

    public partial class EndToEndTestEmail 
    {
        public override string ToString()
        {
            return string.Format("EndToEndTestEmail(\"{0}\", \"{1}\", \"{2}\", {3})", this.Mailbox == null ? "null" : this.Mailbox.ToString(), this.Subject == null ? "null" : this.Subject.ToString(), this.Body == null ? "null" : this.Body.ToString(), this.DateReceived == null ? "null" : this.DateReceived.ToString());
        }
        
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
        
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
        
            if (obj.GetType() != this.GetType())
            {
                return false;
            }
        
            return this.Equals((EndToEndTestEmail)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Mailbox != null ? this.Mailbox.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Subject != null ? this.Subject.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Body != null ? this.Body.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.DateReceived != null ? this.DateReceived.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(EndToEndTestEmail other)
        {
            if (!object.Equals(this.Mailbox, other.Mailbox))
            {
                return false;
            }
        
            if (!object.Equals(this.Subject, other.Subject))
            {
                return false;
            }
        
            if (!object.Equals(this.Body, other.Body))
            {
                return false;
            }
        
            if (!object.Equals(this.DateReceived, other.DateReceived))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Api.Persistence
{
    using System;
    using System.Linq;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Core;
    using Fifthweek.Shared;
    using Dapper;

    public partial class Collection 
    {
        public Collection Copy()
        {
            var copy = new Collection();
            copy.Id = this.Id;
            copy.ChannelId = this.ChannelId;
            copy.Channel = this.Channel;
            copy.Name = this.Name;
            copy.QueueExclusiveLowerBound = this.QueueExclusiveLowerBound;
            copy.CreationDate = this.CreationDate;
            return copy;
        }
        
        public Collection Copy(Action<Collection> applyDelta)
        {
            var copy = this.Copy();
            applyDelta(copy);
            return copy;
        }
    }
}
namespace Fifthweek.Api.Persistence
{
    using System;
    using System.Linq;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Core;
    using Fifthweek.Shared;
    using Dapper;

    public partial class Post 
    {
        public Post Copy()
        {
            var copy = new Post();
            copy.Id = this.Id;
            copy.ChannelId = this.ChannelId;
            copy.Channel = this.Channel;
            copy.CollectionId = this.CollectionId;
            copy.Collection = this.Collection;
            copy.FileId = this.FileId;
            copy.File = this.File;
            copy.ImageId = this.ImageId;
            copy.Image = this.Image;
            copy.Comment = this.Comment;
            copy.ScheduledByQueue = this.ScheduledByQueue;
            copy.LiveDate = this.LiveDate;
            copy.CreationDate = this.CreationDate;
            return copy;
        }
        
        public Post Copy(Action<Post> applyDelta)
        {
            var copy = this.Copy();
            applyDelta(copy);
            return copy;
        }
    }
}

namespace Fifthweek.Api.Persistence
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Fifthweek.CodeGeneration;
    using System.Linq;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Core;
    using Fifthweek.Shared;
    using Dapper;

    public partial class Channel  : IIdentityEquatable
    {
        public const string Table = "Channels";
        
        public Channel(
            System.Guid id)
        {
            if (id == null)
            {
                throw new ArgumentNullException("id");
            }

            this.Id = id;
        }
        
        public bool IdentityEquals(object other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }
        
            if (ReferenceEquals(this, other))
            {
                return true;
            }
        
            if (other.GetType() != this.GetType())
            {
                return false;
            }
        
            return this.IdentityEquals((Channel)other);
        }
        
        protected bool IdentityEquals(Channel other)
        {
            if (!object.Equals(this.Id, other.Id))
            {
                return false;
            }
        
            return true;
        }
        
        [Flags]
        public enum Fields
        {
            Empty = 0,
            Id = 1, 
            SubscriptionId = 2, 
            Name = 4, 
            Description = 8, 
            PriceInUsCentsPerWeek = 16, 
            IsVisibleToNonSubscribers = 32, 
            CreationDate = 64
        }
    }

    public static partial class ChannelExtensions
    {
        public static System.Threading.Tasks.Task InsertAsync(
            this System.Data.Common.DbConnection connection, 
            System.Collections.Generic.IEnumerable<Channel> entities, 
            bool idempotent = true, 
            System.Data.IDbTransaction transaction = null)
        {
            return Dapper.SqlMapper.ExecuteAsync(
                connection, 
                InsertStatement(idempotent), 
                entities.Select(entity => new 
                {
                    entity.Id, entity.SubscriptionId, entity.Name, entity.Description, entity.PriceInUsCentsPerWeek, entity.IsVisibleToNonSubscribers, entity.CreationDate
                }).ToArray(),
                transaction);
        }
        
        public static System.Threading.Tasks.Task InsertAsync(
            this System.Data.Common.DbConnection connection, 
            Channel entity,
            bool idempotent = true, 
            System.Data.IDbTransaction transaction = null)
        {
            return Dapper.SqlMapper.ExecuteAsync(
                connection, 
                InsertStatement(idempotent), 
                new 
                {
                    entity.Id, entity.SubscriptionId, entity.Name, entity.Description, entity.PriceInUsCentsPerWeek, entity.IsVisibleToNonSubscribers, entity.CreationDate
                },
                transaction);
        }
        
        public static System.Threading.Tasks.Task<int> InsertAsync(
            this System.Data.Common.DbConnection connection,
            SqlGenerationParameters<Channel, Channel.Fields> parameters)
        {
            var sql = new System.Text.StringBuilder();
        
            if (parameters.Declarations != null)
            {
                sql.AppendLine(parameters.Declarations);
            }
            
            int currentIndex = 0;
            if (parameters.Conditions != null)
            {
                foreach (var condition in parameters.Conditions)
                {
                    sql.Append("IF ");
                    sql.AppendLine(condition); // Remember to use `WITH (UPDLOCK, HOLDLOCK)` in your conditions! See: http://samsaffron.com/blog/archive/2007/04/04/14.aspx
                    sql.AppendLine("BEGIN");
                    ++currentIndex;
                }
            }
        
            sql.AppendLine(InsertStatement(parameters.IdempotentInsert));
        
            if (parameters.Conditions != null)
            {
                sql.AppendLine("SELECT -1 AS FailedConditionIndex"); // Indicates all conditions passed and operation was attempted.
                    
                foreach (var condition in parameters.Conditions)
                {
                    sql.AppendLine("END");
                    sql.AppendLine("ELSE");
                    sql.Append("SELECT ").Append(--currentIndex).AppendLine(" AS FailedConditionIndex");
                }
            }
        
            var entity = parameters.Entity;
            var parameterObject = parameters.ExcludedFromInput != null
                ? AllExceptSpecifiedParameters(entity, parameters.ExcludedFromInput.Value)
                : new Dapper.DynamicParameters(new { entity.Id, entity.SubscriptionId, entity.Name, entity.Description, entity.PriceInUsCentsPerWeek, entity.IsVisibleToNonSubscribers, entity.CreationDate });
        
            if (parameters.AdditionalParameters != null)
            {
                parameterObject.AddDynamicParams(parameters.AdditionalParameters);
            }
        
            return Dapper.SqlMapper.ExecuteScalarAsync<int>(
                connection,
                sql.ToString(),
                parameterObject);
        }
        
        public static System.Threading.Tasks.Task UpsertAsync(
            this System.Data.Common.DbConnection connection, 
            Channel entity, 
            Channel.Fields fields,
            System.Data.IDbTransaction transaction = null)
        {
            return Dapper.SqlMapper.ExecuteAsync(
                connection, 
                UpsertStatement(Channel.Fields.Empty, fields), 
                new 
                {
                    entity.Id, entity.SubscriptionId, entity.Name, entity.Description, entity.PriceInUsCentsPerWeek, entity.IsVisibleToNonSubscribers, entity.CreationDate
                },
                transaction);
        }
        
        public static System.Threading.Tasks.Task UpsertAsync(
            this System.Data.Common.DbConnection connection, 
            Channel entity, 
            Channel.Fields mergeOnFields,
            Channel.Fields updateFields,
            System.Data.IDbTransaction transaction = null)
        {
            return Dapper.SqlMapper.ExecuteAsync(
                connection, 
                UpsertStatement(mergeOnFields, updateFields), 
                new 
                {
                    entity.Id, entity.SubscriptionId, entity.Name, entity.Description, entity.PriceInUsCentsPerWeek, entity.IsVisibleToNonSubscribers, entity.CreationDate
                },
                transaction);
        }
        
        public static System.Threading.Tasks.Task UpdateAsync(
            this System.Data.Common.DbConnection connection, 
            Channel entity, 
            Channel.Fields fields,
            System.Data.IDbTransaction transaction = null)
        {
            return Dapper.SqlMapper.ExecuteAsync(connection, UpdateStatement(fields), OnlySpecifiedParameters(entity, fields), transaction);
        }
        
        public static System.Threading.Tasks.Task<int> UpdateAsync(
            this System.Data.Common.DbConnection connection,
            SqlGenerationParameters<Channel, Channel.Fields> parameters)
        {
            if (parameters.UpdateMask == null)
            {
                throw new ArgumentException("Must contain update mask", "parameters");
            }
        
            var sql = new System.Text.StringBuilder();
        
            if (parameters.Declarations != null)
            {
                sql.AppendLine(parameters.Declarations);
            }
            
            int currentIndex = 0;
            if (parameters.Conditions != null)
            {
                foreach (var condition in parameters.Conditions)
                {
                    sql.Append("IF ");
                    sql.AppendLine(condition); // Remember to use `WITH (UPDLOCK, HOLDLOCK)` in your conditions! See: http://samsaffron.com/blog/archive/2007/04/04/14.aspx
                    sql.AppendLine("BEGIN");
                    ++currentIndex;
                }
            }
        
            sql.AppendLine(UpdateStatement(parameters.UpdateMask.Value));
        
            if (parameters.Conditions != null)
            {
                sql.AppendLine("SELECT -1 AS FailedConditionIndex"); // Indicates all conditions passed and operation was attempted.
                    
                foreach (var condition in parameters.Conditions)
                {
                    sql.AppendLine("END");
                    sql.AppendLine("ELSE");
                    sql.Append("SELECT ").Append(--currentIndex).AppendLine(" AS FailedConditionIndex");
                }
            }
        
            var parameterObject = OnlySpecifiedParameters(parameters.Entity, parameters.UpdateMask.Value, parameters.ExcludedFromInput);
        
            if (parameters.AdditionalParameters != null)
            {
                parameterObject.AddDynamicParams(parameters.AdditionalParameters);
            }
        
            return Dapper.SqlMapper.ExecuteScalarAsync<int>(
                connection,
                sql.ToString(),
                parameterObject);
        }
        
        public static string InsertStatement(bool idempotent = true)
        {
            const string insert = "INSERT INTO Channels(Id, SubscriptionId, Name, Description, PriceInUsCentsPerWeek, IsVisibleToNonSubscribers, CreationDate) VALUES(@Id, @SubscriptionId, @Name, @Description, @PriceInUsCentsPerWeek, @IsVisibleToNonSubscribers, @CreationDate)";
            return idempotent ? SqlStatementTemplates.IdempotentInsert(insert) : insert;
        }
        
        public static string UpsertStatement(Channel.Fields mergeOnFields, Channel.Fields updateFields)
        {
            // HOLDLOCK ensures operation is concurrent by not releasing the U lock on the row after determining
            // it does not exist. See: http://weblogs.sqlteam.com/dang/archive/2009/01/31/UPSERT-Race-Condition-With-MERGE.aspx
            var sql = new System.Text.StringBuilder();
            sql.Append(
                @"MERGE Channels WITH (HOLDLOCK) as Target
                USING (VALUES (@Id, @SubscriptionId, @Name, @Description, @PriceInUsCentsPerWeek, @IsVisibleToNonSubscribers, @CreationDate)) AS Source (Id, SubscriptionId, Name, Description, PriceInUsCentsPerWeek, IsVisibleToNonSubscribers, CreationDate)
                ON    (");
                
            if (mergeOnFields == Channel.Fields.Empty)
            {
                sql.Append(@"Target.Id = Source.Id");
            }
            else
            {
                sql.AppendMergeOnParameters(GetFieldNames(mergeOnFields, false));
            }
                
            sql.Append(@")
                WHEN MATCHED THEN
                    UPDATE
                    SET        ");
            sql.AppendUpdateParameters(GetFieldNames(updateFields));
            sql.Append(
                @" WHEN NOT MATCHED THEN
                    INSERT  (Id, SubscriptionId, Name, Description, PriceInUsCentsPerWeek, IsVisibleToNonSubscribers, CreationDate)
                    VALUES  (Source.Id, Source.SubscriptionId, Source.Name, Source.Description, Source.PriceInUsCentsPerWeek, Source.IsVisibleToNonSubscribers, Source.CreationDate);");
            return sql.ToString();
        }
        
        public static string UpdateStatement(Channel.Fields fields)
        {
            var sql = new System.Text.StringBuilder();
            sql.Append("UPDATE Channels SET ");
            sql.AppendUpdateParameters(GetFieldNames(fields));
            sql.Append(" WHERE Id = @Id");
            return sql.ToString();
        }
        
        private static System.Collections.Generic.IReadOnlyList<string> GetFieldNames(Channel.Fields fields, bool autoIncludePrimaryKeys = true)
        {
            var fieldNames = new System.Collections.Generic.List<string>();
            if (autoIncludePrimaryKeys)
            {
                fieldNames.Add("Id");
            }
        
            if (fields.HasFlag(Channel.Fields.SubscriptionId))
            {
                fieldNames.Add("SubscriptionId");
            }
        
            if (fields.HasFlag(Channel.Fields.Name))
            {
                fieldNames.Add("Name");
            }
        
            if (fields.HasFlag(Channel.Fields.Description))
            {
                fieldNames.Add("Description");
            }
        
            if (fields.HasFlag(Channel.Fields.PriceInUsCentsPerWeek))
            {
                fieldNames.Add("PriceInUsCentsPerWeek");
            }
        
            if (fields.HasFlag(Channel.Fields.IsVisibleToNonSubscribers))
            {
                fieldNames.Add("IsVisibleToNonSubscribers");
            }
        
            if (fields.HasFlag(Channel.Fields.CreationDate))
            {
                fieldNames.Add("CreationDate");
            }
        
            return fieldNames;
        }
        
        private static Dapper.DynamicParameters OnlySpecifiedParameters(
            Channel entity, 
            Channel.Fields fields,
            Channel.Fields? excludedFields = null)
        {
            var parameters = new Dapper.DynamicParameters();
        
            // Assume we never want to exclude primary key field(s) from our input.
            parameters.Add("Id", entity.Id);
            if (fields.HasFlag(Channel.Fields.SubscriptionId) && (excludedFields == null || !excludedFields.Value.HasFlag(Channel.Fields.SubscriptionId)))
            {
                parameters.Add("SubscriptionId", entity.SubscriptionId);
            }
        
            if (fields.HasFlag(Channel.Fields.Name) && (excludedFields == null || !excludedFields.Value.HasFlag(Channel.Fields.Name)))
            {
                parameters.Add("Name", entity.Name);
            }
        
            if (fields.HasFlag(Channel.Fields.Description) && (excludedFields == null || !excludedFields.Value.HasFlag(Channel.Fields.Description)))
            {
                parameters.Add("Description", entity.Description);
            }
        
            if (fields.HasFlag(Channel.Fields.PriceInUsCentsPerWeek) && (excludedFields == null || !excludedFields.Value.HasFlag(Channel.Fields.PriceInUsCentsPerWeek)))
            {
                parameters.Add("PriceInUsCentsPerWeek", entity.PriceInUsCentsPerWeek);
            }
        
            if (fields.HasFlag(Channel.Fields.IsVisibleToNonSubscribers) && (excludedFields == null || !excludedFields.Value.HasFlag(Channel.Fields.IsVisibleToNonSubscribers)))
            {
                parameters.Add("IsVisibleToNonSubscribers", entity.IsVisibleToNonSubscribers);
            }
        
            if (fields.HasFlag(Channel.Fields.CreationDate) && (excludedFields == null || !excludedFields.Value.HasFlag(Channel.Fields.CreationDate)))
            {
                parameters.Add("CreationDate", entity.CreationDate);
            }
        
            return parameters;
        }
        
        private static Dapper.DynamicParameters AllExceptSpecifiedParameters(
            Channel entity, 
            Channel.Fields fields)
        {
            var parameters = new Dapper.DynamicParameters();
        
            // Assume we never want to exclude primary key field(s) from our input.
            parameters.Add("Id", entity.Id);
            if (!fields.HasFlag(Channel.Fields.SubscriptionId))
            {
                parameters.Add("SubscriptionId", entity.SubscriptionId);
            }
        
            if (!fields.HasFlag(Channel.Fields.Name))
            {
                parameters.Add("Name", entity.Name);
            }
        
            if (!fields.HasFlag(Channel.Fields.Description))
            {
                parameters.Add("Description", entity.Description);
            }
        
            if (!fields.HasFlag(Channel.Fields.PriceInUsCentsPerWeek))
            {
                parameters.Add("PriceInUsCentsPerWeek", entity.PriceInUsCentsPerWeek);
            }
        
            if (!fields.HasFlag(Channel.Fields.IsVisibleToNonSubscribers))
            {
                parameters.Add("IsVisibleToNonSubscribers", entity.IsVisibleToNonSubscribers);
            }
        
            if (!fields.HasFlag(Channel.Fields.CreationDate))
            {
                parameters.Add("CreationDate", entity.CreationDate);
            }
        
            return parameters;
        }
        
    }
}
namespace Fifthweek.Api.Persistence
{
    using System;
    using System.Linq;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Core;
    using Fifthweek.Shared;
    using Dapper;

    public partial class Collection  : IIdentityEquatable
    {
        public const string Table = "Collections";
        
        public Collection(
            System.Guid id)
        {
            if (id == null)
            {
                throw new ArgumentNullException("id");
            }

            this.Id = id;
        }
        
        public bool IdentityEquals(object other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }
        
            if (ReferenceEquals(this, other))
            {
                return true;
            }
        
            if (other.GetType() != this.GetType())
            {
                return false;
            }
        
            return this.IdentityEquals((Collection)other);
        }
        
        protected bool IdentityEquals(Collection other)
        {
            if (!object.Equals(this.Id, other.Id))
            {
                return false;
            }
        
            return true;
        }
        
        [Flags]
        public enum Fields
        {
            Empty = 0,
            Id = 1, 
            ChannelId = 2, 
            Name = 4, 
            QueueExclusiveLowerBound = 8, 
            CreationDate = 16
        }
    }

    public static partial class CollectionExtensions
    {
        public static System.Threading.Tasks.Task InsertAsync(
            this System.Data.Common.DbConnection connection, 
            System.Collections.Generic.IEnumerable<Collection> entities, 
            bool idempotent = true, 
            System.Data.IDbTransaction transaction = null)
        {
            return Dapper.SqlMapper.ExecuteAsync(
                connection, 
                InsertStatement(idempotent), 
                entities.Select(entity => new 
                {
                    entity.Id, entity.ChannelId, entity.Name, entity.QueueExclusiveLowerBound, entity.CreationDate
                }).ToArray(),
                transaction);
        }
        
        public static System.Threading.Tasks.Task InsertAsync(
            this System.Data.Common.DbConnection connection, 
            Collection entity,
            bool idempotent = true, 
            System.Data.IDbTransaction transaction = null)
        {
            return Dapper.SqlMapper.ExecuteAsync(
                connection, 
                InsertStatement(idempotent), 
                new 
                {
                    entity.Id, entity.ChannelId, entity.Name, entity.QueueExclusiveLowerBound, entity.CreationDate
                },
                transaction);
        }
        
        public static System.Threading.Tasks.Task<int> InsertAsync(
            this System.Data.Common.DbConnection connection,
            SqlGenerationParameters<Collection, Collection.Fields> parameters)
        {
            var sql = new System.Text.StringBuilder();
        
            if (parameters.Declarations != null)
            {
                sql.AppendLine(parameters.Declarations);
            }
            
            int currentIndex = 0;
            if (parameters.Conditions != null)
            {
                foreach (var condition in parameters.Conditions)
                {
                    sql.Append("IF ");
                    sql.AppendLine(condition); // Remember to use `WITH (UPDLOCK, HOLDLOCK)` in your conditions! See: http://samsaffron.com/blog/archive/2007/04/04/14.aspx
                    sql.AppendLine("BEGIN");
                    ++currentIndex;
                }
            }
        
            sql.AppendLine(InsertStatement(parameters.IdempotentInsert));
        
            if (parameters.Conditions != null)
            {
                sql.AppendLine("SELECT -1 AS FailedConditionIndex"); // Indicates all conditions passed and operation was attempted.
                    
                foreach (var condition in parameters.Conditions)
                {
                    sql.AppendLine("END");
                    sql.AppendLine("ELSE");
                    sql.Append("SELECT ").Append(--currentIndex).AppendLine(" AS FailedConditionIndex");
                }
            }
        
            var entity = parameters.Entity;
            var parameterObject = parameters.ExcludedFromInput != null
                ? AllExceptSpecifiedParameters(entity, parameters.ExcludedFromInput.Value)
                : new Dapper.DynamicParameters(new { entity.Id, entity.ChannelId, entity.Name, entity.QueueExclusiveLowerBound, entity.CreationDate });
        
            if (parameters.AdditionalParameters != null)
            {
                parameterObject.AddDynamicParams(parameters.AdditionalParameters);
            }
        
            return Dapper.SqlMapper.ExecuteScalarAsync<int>(
                connection,
                sql.ToString(),
                parameterObject);
        }
        
        public static System.Threading.Tasks.Task UpsertAsync(
            this System.Data.Common.DbConnection connection, 
            Collection entity, 
            Collection.Fields fields,
            System.Data.IDbTransaction transaction = null)
        {
            return Dapper.SqlMapper.ExecuteAsync(
                connection, 
                UpsertStatement(Collection.Fields.Empty, fields), 
                new 
                {
                    entity.Id, entity.ChannelId, entity.Name, entity.QueueExclusiveLowerBound, entity.CreationDate
                },
                transaction);
        }
        
        public static System.Threading.Tasks.Task UpsertAsync(
            this System.Data.Common.DbConnection connection, 
            Collection entity, 
            Collection.Fields mergeOnFields,
            Collection.Fields updateFields,
            System.Data.IDbTransaction transaction = null)
        {
            return Dapper.SqlMapper.ExecuteAsync(
                connection, 
                UpsertStatement(mergeOnFields, updateFields), 
                new 
                {
                    entity.Id, entity.ChannelId, entity.Name, entity.QueueExclusiveLowerBound, entity.CreationDate
                },
                transaction);
        }
        
        public static System.Threading.Tasks.Task UpdateAsync(
            this System.Data.Common.DbConnection connection, 
            Collection entity, 
            Collection.Fields fields,
            System.Data.IDbTransaction transaction = null)
        {
            return Dapper.SqlMapper.ExecuteAsync(connection, UpdateStatement(fields), OnlySpecifiedParameters(entity, fields), transaction);
        }
        
        public static System.Threading.Tasks.Task<int> UpdateAsync(
            this System.Data.Common.DbConnection connection,
            SqlGenerationParameters<Collection, Collection.Fields> parameters)
        {
            if (parameters.UpdateMask == null)
            {
                throw new ArgumentException("Must contain update mask", "parameters");
            }
        
            var sql = new System.Text.StringBuilder();
        
            if (parameters.Declarations != null)
            {
                sql.AppendLine(parameters.Declarations);
            }
            
            int currentIndex = 0;
            if (parameters.Conditions != null)
            {
                foreach (var condition in parameters.Conditions)
                {
                    sql.Append("IF ");
                    sql.AppendLine(condition); // Remember to use `WITH (UPDLOCK, HOLDLOCK)` in your conditions! See: http://samsaffron.com/blog/archive/2007/04/04/14.aspx
                    sql.AppendLine("BEGIN");
                    ++currentIndex;
                }
            }
        
            sql.AppendLine(UpdateStatement(parameters.UpdateMask.Value));
        
            if (parameters.Conditions != null)
            {
                sql.AppendLine("SELECT -1 AS FailedConditionIndex"); // Indicates all conditions passed and operation was attempted.
                    
                foreach (var condition in parameters.Conditions)
                {
                    sql.AppendLine("END");
                    sql.AppendLine("ELSE");
                    sql.Append("SELECT ").Append(--currentIndex).AppendLine(" AS FailedConditionIndex");
                }
            }
        
            var parameterObject = OnlySpecifiedParameters(parameters.Entity, parameters.UpdateMask.Value, parameters.ExcludedFromInput);
        
            if (parameters.AdditionalParameters != null)
            {
                parameterObject.AddDynamicParams(parameters.AdditionalParameters);
            }
        
            return Dapper.SqlMapper.ExecuteScalarAsync<int>(
                connection,
                sql.ToString(),
                parameterObject);
        }
        
        public static string InsertStatement(bool idempotent = true)
        {
            const string insert = "INSERT INTO Collections(Id, ChannelId, Name, QueueExclusiveLowerBound, CreationDate) VALUES(@Id, @ChannelId, @Name, @QueueExclusiveLowerBound, @CreationDate)";
            return idempotent ? SqlStatementTemplates.IdempotentInsert(insert) : insert;
        }
        
        public static string UpsertStatement(Collection.Fields mergeOnFields, Collection.Fields updateFields)
        {
            // HOLDLOCK ensures operation is concurrent by not releasing the U lock on the row after determining
            // it does not exist. See: http://weblogs.sqlteam.com/dang/archive/2009/01/31/UPSERT-Race-Condition-With-MERGE.aspx
            var sql = new System.Text.StringBuilder();
            sql.Append(
                @"MERGE Collections WITH (HOLDLOCK) as Target
                USING (VALUES (@Id, @ChannelId, @Name, @QueueExclusiveLowerBound, @CreationDate)) AS Source (Id, ChannelId, Name, QueueExclusiveLowerBound, CreationDate)
                ON    (");
                
            if (mergeOnFields == Collection.Fields.Empty)
            {
                sql.Append(@"Target.Id = Source.Id");
            }
            else
            {
                sql.AppendMergeOnParameters(GetFieldNames(mergeOnFields, false));
            }
                
            sql.Append(@")
                WHEN MATCHED THEN
                    UPDATE
                    SET        ");
            sql.AppendUpdateParameters(GetFieldNames(updateFields));
            sql.Append(
                @" WHEN NOT MATCHED THEN
                    INSERT  (Id, ChannelId, Name, QueueExclusiveLowerBound, CreationDate)
                    VALUES  (Source.Id, Source.ChannelId, Source.Name, Source.QueueExclusiveLowerBound, Source.CreationDate);");
            return sql.ToString();
        }
        
        public static string UpdateStatement(Collection.Fields fields)
        {
            var sql = new System.Text.StringBuilder();
            sql.Append("UPDATE Collections SET ");
            sql.AppendUpdateParameters(GetFieldNames(fields));
            sql.Append(" WHERE Id = @Id");
            return sql.ToString();
        }
        
        private static System.Collections.Generic.IReadOnlyList<string> GetFieldNames(Collection.Fields fields, bool autoIncludePrimaryKeys = true)
        {
            var fieldNames = new System.Collections.Generic.List<string>();
            if (autoIncludePrimaryKeys)
            {
                fieldNames.Add("Id");
            }
        
            if (fields.HasFlag(Collection.Fields.ChannelId))
            {
                fieldNames.Add("ChannelId");
            }
        
            if (fields.HasFlag(Collection.Fields.Name))
            {
                fieldNames.Add("Name");
            }
        
            if (fields.HasFlag(Collection.Fields.QueueExclusiveLowerBound))
            {
                fieldNames.Add("QueueExclusiveLowerBound");
            }
        
            if (fields.HasFlag(Collection.Fields.CreationDate))
            {
                fieldNames.Add("CreationDate");
            }
        
            return fieldNames;
        }
        
        private static Dapper.DynamicParameters OnlySpecifiedParameters(
            Collection entity, 
            Collection.Fields fields,
            Collection.Fields? excludedFields = null)
        {
            var parameters = new Dapper.DynamicParameters();
        
            // Assume we never want to exclude primary key field(s) from our input.
            parameters.Add("Id", entity.Id);
            if (fields.HasFlag(Collection.Fields.ChannelId) && (excludedFields == null || !excludedFields.Value.HasFlag(Collection.Fields.ChannelId)))
            {
                parameters.Add("ChannelId", entity.ChannelId);
            }
        
            if (fields.HasFlag(Collection.Fields.Name) && (excludedFields == null || !excludedFields.Value.HasFlag(Collection.Fields.Name)))
            {
                parameters.Add("Name", entity.Name);
            }
        
            if (fields.HasFlag(Collection.Fields.QueueExclusiveLowerBound) && (excludedFields == null || !excludedFields.Value.HasFlag(Collection.Fields.QueueExclusiveLowerBound)))
            {
                parameters.Add("QueueExclusiveLowerBound", entity.QueueExclusiveLowerBound);
            }
        
            if (fields.HasFlag(Collection.Fields.CreationDate) && (excludedFields == null || !excludedFields.Value.HasFlag(Collection.Fields.CreationDate)))
            {
                parameters.Add("CreationDate", entity.CreationDate);
            }
        
            return parameters;
        }
        
        private static Dapper.DynamicParameters AllExceptSpecifiedParameters(
            Collection entity, 
            Collection.Fields fields)
        {
            var parameters = new Dapper.DynamicParameters();
        
            // Assume we never want to exclude primary key field(s) from our input.
            parameters.Add("Id", entity.Id);
            if (!fields.HasFlag(Collection.Fields.ChannelId))
            {
                parameters.Add("ChannelId", entity.ChannelId);
            }
        
            if (!fields.HasFlag(Collection.Fields.Name))
            {
                parameters.Add("Name", entity.Name);
            }
        
            if (!fields.HasFlag(Collection.Fields.QueueExclusiveLowerBound))
            {
                parameters.Add("QueueExclusiveLowerBound", entity.QueueExclusiveLowerBound);
            }
        
            if (!fields.HasFlag(Collection.Fields.CreationDate))
            {
                parameters.Add("CreationDate", entity.CreationDate);
            }
        
            return parameters;
        }
        
    }
}
namespace Fifthweek.Api.Persistence
{
    using System;
    using System.Linq;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Core;
    using Fifthweek.Shared;
    using Dapper;

    public partial class File  : IIdentityEquatable
    {
        public const string Table = "Files";
        
        public File(
            System.Guid id)
        {
            if (id == null)
            {
                throw new ArgumentNullException("id");
            }

            this.Id = id;
        }
        
        public bool IdentityEquals(object other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }
        
            if (ReferenceEquals(this, other))
            {
                return true;
            }
        
            if (other.GetType() != this.GetType())
            {
                return false;
            }
        
            return this.IdentityEquals((File)other);
        }
        
        protected bool IdentityEquals(File other)
        {
            if (!object.Equals(this.Id, other.Id))
            {
                return false;
            }
        
            return true;
        }
        
        [Flags]
        public enum Fields
        {
            Empty = 0,
            Id = 1, 
            UserId = 2, 
            State = 4, 
            UploadStartedDate = 8, 
            UploadCompletedDate = 16, 
            ProcessingStartedDate = 32, 
            ProcessingCompletedDate = 64, 
            FileNameWithoutExtension = 128, 
            FileExtension = 256, 
            BlobSizeBytes = 512, 
            Purpose = 1024
        }
    }

    public static partial class FileExtensions
    {
        public static System.Threading.Tasks.Task InsertAsync(
            this System.Data.Common.DbConnection connection, 
            System.Collections.Generic.IEnumerable<File> entities, 
            bool idempotent = true, 
            System.Data.IDbTransaction transaction = null)
        {
            return Dapper.SqlMapper.ExecuteAsync(
                connection, 
                InsertStatement(idempotent), 
                entities.Select(entity => new 
                {
                    entity.Id, entity.UserId, entity.State, entity.UploadStartedDate, entity.UploadCompletedDate, entity.ProcessingStartedDate, entity.ProcessingCompletedDate, entity.FileNameWithoutExtension, entity.FileExtension, entity.BlobSizeBytes, entity.Purpose
                }).ToArray(),
                transaction);
        }
        
        public static System.Threading.Tasks.Task InsertAsync(
            this System.Data.Common.DbConnection connection, 
            File entity,
            bool idempotent = true, 
            System.Data.IDbTransaction transaction = null)
        {
            return Dapper.SqlMapper.ExecuteAsync(
                connection, 
                InsertStatement(idempotent), 
                new 
                {
                    entity.Id, entity.UserId, entity.State, entity.UploadStartedDate, entity.UploadCompletedDate, entity.ProcessingStartedDate, entity.ProcessingCompletedDate, entity.FileNameWithoutExtension, entity.FileExtension, entity.BlobSizeBytes, entity.Purpose
                },
                transaction);
        }
        
        public static System.Threading.Tasks.Task<int> InsertAsync(
            this System.Data.Common.DbConnection connection,
            SqlGenerationParameters<File, File.Fields> parameters)
        {
            var sql = new System.Text.StringBuilder();
        
            if (parameters.Declarations != null)
            {
                sql.AppendLine(parameters.Declarations);
            }
            
            int currentIndex = 0;
            if (parameters.Conditions != null)
            {
                foreach (var condition in parameters.Conditions)
                {
                    sql.Append("IF ");
                    sql.AppendLine(condition); // Remember to use `WITH (UPDLOCK, HOLDLOCK)` in your conditions! See: http://samsaffron.com/blog/archive/2007/04/04/14.aspx
                    sql.AppendLine("BEGIN");
                    ++currentIndex;
                }
            }
        
            sql.AppendLine(InsertStatement(parameters.IdempotentInsert));
        
            if (parameters.Conditions != null)
            {
                sql.AppendLine("SELECT -1 AS FailedConditionIndex"); // Indicates all conditions passed and operation was attempted.
                    
                foreach (var condition in parameters.Conditions)
                {
                    sql.AppendLine("END");
                    sql.AppendLine("ELSE");
                    sql.Append("SELECT ").Append(--currentIndex).AppendLine(" AS FailedConditionIndex");
                }
            }
        
            var entity = parameters.Entity;
            var parameterObject = parameters.ExcludedFromInput != null
                ? AllExceptSpecifiedParameters(entity, parameters.ExcludedFromInput.Value)
                : new Dapper.DynamicParameters(new { entity.Id, entity.UserId, entity.State, entity.UploadStartedDate, entity.UploadCompletedDate, entity.ProcessingStartedDate, entity.ProcessingCompletedDate, entity.FileNameWithoutExtension, entity.FileExtension, entity.BlobSizeBytes, entity.Purpose });
        
            if (parameters.AdditionalParameters != null)
            {
                parameterObject.AddDynamicParams(parameters.AdditionalParameters);
            }
        
            return Dapper.SqlMapper.ExecuteScalarAsync<int>(
                connection,
                sql.ToString(),
                parameterObject);
        }
        
        public static System.Threading.Tasks.Task UpsertAsync(
            this System.Data.Common.DbConnection connection, 
            File entity, 
            File.Fields fields,
            System.Data.IDbTransaction transaction = null)
        {
            return Dapper.SqlMapper.ExecuteAsync(
                connection, 
                UpsertStatement(File.Fields.Empty, fields), 
                new 
                {
                    entity.Id, entity.UserId, entity.State, entity.UploadStartedDate, entity.UploadCompletedDate, entity.ProcessingStartedDate, entity.ProcessingCompletedDate, entity.FileNameWithoutExtension, entity.FileExtension, entity.BlobSizeBytes, entity.Purpose
                },
                transaction);
        }
        
        public static System.Threading.Tasks.Task UpsertAsync(
            this System.Data.Common.DbConnection connection, 
            File entity, 
            File.Fields mergeOnFields,
            File.Fields updateFields,
            System.Data.IDbTransaction transaction = null)
        {
            return Dapper.SqlMapper.ExecuteAsync(
                connection, 
                UpsertStatement(mergeOnFields, updateFields), 
                new 
                {
                    entity.Id, entity.UserId, entity.State, entity.UploadStartedDate, entity.UploadCompletedDate, entity.ProcessingStartedDate, entity.ProcessingCompletedDate, entity.FileNameWithoutExtension, entity.FileExtension, entity.BlobSizeBytes, entity.Purpose
                },
                transaction);
        }
        
        public static System.Threading.Tasks.Task UpdateAsync(
            this System.Data.Common.DbConnection connection, 
            File entity, 
            File.Fields fields,
            System.Data.IDbTransaction transaction = null)
        {
            return Dapper.SqlMapper.ExecuteAsync(connection, UpdateStatement(fields), OnlySpecifiedParameters(entity, fields), transaction);
        }
        
        public static System.Threading.Tasks.Task<int> UpdateAsync(
            this System.Data.Common.DbConnection connection,
            SqlGenerationParameters<File, File.Fields> parameters)
        {
            if (parameters.UpdateMask == null)
            {
                throw new ArgumentException("Must contain update mask", "parameters");
            }
        
            var sql = new System.Text.StringBuilder();
        
            if (parameters.Declarations != null)
            {
                sql.AppendLine(parameters.Declarations);
            }
            
            int currentIndex = 0;
            if (parameters.Conditions != null)
            {
                foreach (var condition in parameters.Conditions)
                {
                    sql.Append("IF ");
                    sql.AppendLine(condition); // Remember to use `WITH (UPDLOCK, HOLDLOCK)` in your conditions! See: http://samsaffron.com/blog/archive/2007/04/04/14.aspx
                    sql.AppendLine("BEGIN");
                    ++currentIndex;
                }
            }
        
            sql.AppendLine(UpdateStatement(parameters.UpdateMask.Value));
        
            if (parameters.Conditions != null)
            {
                sql.AppendLine("SELECT -1 AS FailedConditionIndex"); // Indicates all conditions passed and operation was attempted.
                    
                foreach (var condition in parameters.Conditions)
                {
                    sql.AppendLine("END");
                    sql.AppendLine("ELSE");
                    sql.Append("SELECT ").Append(--currentIndex).AppendLine(" AS FailedConditionIndex");
                }
            }
        
            var parameterObject = OnlySpecifiedParameters(parameters.Entity, parameters.UpdateMask.Value, parameters.ExcludedFromInput);
        
            if (parameters.AdditionalParameters != null)
            {
                parameterObject.AddDynamicParams(parameters.AdditionalParameters);
            }
        
            return Dapper.SqlMapper.ExecuteScalarAsync<int>(
                connection,
                sql.ToString(),
                parameterObject);
        }
        
        public static string InsertStatement(bool idempotent = true)
        {
            const string insert = "INSERT INTO Files(Id, UserId, State, UploadStartedDate, UploadCompletedDate, ProcessingStartedDate, ProcessingCompletedDate, FileNameWithoutExtension, FileExtension, BlobSizeBytes, Purpose) VALUES(@Id, @UserId, @State, @UploadStartedDate, @UploadCompletedDate, @ProcessingStartedDate, @ProcessingCompletedDate, @FileNameWithoutExtension, @FileExtension, @BlobSizeBytes, @Purpose)";
            return idempotent ? SqlStatementTemplates.IdempotentInsert(insert) : insert;
        }
        
        public static string UpsertStatement(File.Fields mergeOnFields, File.Fields updateFields)
        {
            // HOLDLOCK ensures operation is concurrent by not releasing the U lock on the row after determining
            // it does not exist. See: http://weblogs.sqlteam.com/dang/archive/2009/01/31/UPSERT-Race-Condition-With-MERGE.aspx
            var sql = new System.Text.StringBuilder();
            sql.Append(
                @"MERGE Files WITH (HOLDLOCK) as Target
                USING (VALUES (@Id, @UserId, @State, @UploadStartedDate, @UploadCompletedDate, @ProcessingStartedDate, @ProcessingCompletedDate, @FileNameWithoutExtension, @FileExtension, @BlobSizeBytes, @Purpose)) AS Source (Id, UserId, State, UploadStartedDate, UploadCompletedDate, ProcessingStartedDate, ProcessingCompletedDate, FileNameWithoutExtension, FileExtension, BlobSizeBytes, Purpose)
                ON    (");
                
            if (mergeOnFields == File.Fields.Empty)
            {
                sql.Append(@"Target.Id = Source.Id");
            }
            else
            {
                sql.AppendMergeOnParameters(GetFieldNames(mergeOnFields, false));
            }
                
            sql.Append(@")
                WHEN MATCHED THEN
                    UPDATE
                    SET        ");
            sql.AppendUpdateParameters(GetFieldNames(updateFields));
            sql.Append(
                @" WHEN NOT MATCHED THEN
                    INSERT  (Id, UserId, State, UploadStartedDate, UploadCompletedDate, ProcessingStartedDate, ProcessingCompletedDate, FileNameWithoutExtension, FileExtension, BlobSizeBytes, Purpose)
                    VALUES  (Source.Id, Source.UserId, Source.State, Source.UploadStartedDate, Source.UploadCompletedDate, Source.ProcessingStartedDate, Source.ProcessingCompletedDate, Source.FileNameWithoutExtension, Source.FileExtension, Source.BlobSizeBytes, Source.Purpose);");
            return sql.ToString();
        }
        
        public static string UpdateStatement(File.Fields fields)
        {
            var sql = new System.Text.StringBuilder();
            sql.Append("UPDATE Files SET ");
            sql.AppendUpdateParameters(GetFieldNames(fields));
            sql.Append(" WHERE Id = @Id");
            return sql.ToString();
        }
        
        private static System.Collections.Generic.IReadOnlyList<string> GetFieldNames(File.Fields fields, bool autoIncludePrimaryKeys = true)
        {
            var fieldNames = new System.Collections.Generic.List<string>();
            if (autoIncludePrimaryKeys)
            {
                fieldNames.Add("Id");
            }
        
            if (fields.HasFlag(File.Fields.UserId))
            {
                fieldNames.Add("UserId");
            }
        
            if (fields.HasFlag(File.Fields.State))
            {
                fieldNames.Add("State");
            }
        
            if (fields.HasFlag(File.Fields.UploadStartedDate))
            {
                fieldNames.Add("UploadStartedDate");
            }
        
            if (fields.HasFlag(File.Fields.UploadCompletedDate))
            {
                fieldNames.Add("UploadCompletedDate");
            }
        
            if (fields.HasFlag(File.Fields.ProcessingStartedDate))
            {
                fieldNames.Add("ProcessingStartedDate");
            }
        
            if (fields.HasFlag(File.Fields.ProcessingCompletedDate))
            {
                fieldNames.Add("ProcessingCompletedDate");
            }
        
            if (fields.HasFlag(File.Fields.FileNameWithoutExtension))
            {
                fieldNames.Add("FileNameWithoutExtension");
            }
        
            if (fields.HasFlag(File.Fields.FileExtension))
            {
                fieldNames.Add("FileExtension");
            }
        
            if (fields.HasFlag(File.Fields.BlobSizeBytes))
            {
                fieldNames.Add("BlobSizeBytes");
            }
        
            if (fields.HasFlag(File.Fields.Purpose))
            {
                fieldNames.Add("Purpose");
            }
        
            return fieldNames;
        }
        
        private static Dapper.DynamicParameters OnlySpecifiedParameters(
            File entity, 
            File.Fields fields,
            File.Fields? excludedFields = null)
        {
            var parameters = new Dapper.DynamicParameters();
        
            // Assume we never want to exclude primary key field(s) from our input.
            parameters.Add("Id", entity.Id);
            if (fields.HasFlag(File.Fields.UserId) && (excludedFields == null || !excludedFields.Value.HasFlag(File.Fields.UserId)))
            {
                parameters.Add("UserId", entity.UserId);
            }
        
            if (fields.HasFlag(File.Fields.State) && (excludedFields == null || !excludedFields.Value.HasFlag(File.Fields.State)))
            {
                parameters.Add("State", entity.State);
            }
        
            if (fields.HasFlag(File.Fields.UploadStartedDate) && (excludedFields == null || !excludedFields.Value.HasFlag(File.Fields.UploadStartedDate)))
            {
                parameters.Add("UploadStartedDate", entity.UploadStartedDate);
            }
        
            if (fields.HasFlag(File.Fields.UploadCompletedDate) && (excludedFields == null || !excludedFields.Value.HasFlag(File.Fields.UploadCompletedDate)))
            {
                parameters.Add("UploadCompletedDate", entity.UploadCompletedDate);
            }
        
            if (fields.HasFlag(File.Fields.ProcessingStartedDate) && (excludedFields == null || !excludedFields.Value.HasFlag(File.Fields.ProcessingStartedDate)))
            {
                parameters.Add("ProcessingStartedDate", entity.ProcessingStartedDate);
            }
        
            if (fields.HasFlag(File.Fields.ProcessingCompletedDate) && (excludedFields == null || !excludedFields.Value.HasFlag(File.Fields.ProcessingCompletedDate)))
            {
                parameters.Add("ProcessingCompletedDate", entity.ProcessingCompletedDate);
            }
        
            if (fields.HasFlag(File.Fields.FileNameWithoutExtension) && (excludedFields == null || !excludedFields.Value.HasFlag(File.Fields.FileNameWithoutExtension)))
            {
                parameters.Add("FileNameWithoutExtension", entity.FileNameWithoutExtension);
            }
        
            if (fields.HasFlag(File.Fields.FileExtension) && (excludedFields == null || !excludedFields.Value.HasFlag(File.Fields.FileExtension)))
            {
                parameters.Add("FileExtension", entity.FileExtension);
            }
        
            if (fields.HasFlag(File.Fields.BlobSizeBytes) && (excludedFields == null || !excludedFields.Value.HasFlag(File.Fields.BlobSizeBytes)))
            {
                parameters.Add("BlobSizeBytes", entity.BlobSizeBytes);
            }
        
            if (fields.HasFlag(File.Fields.Purpose) && (excludedFields == null || !excludedFields.Value.HasFlag(File.Fields.Purpose)))
            {
                parameters.Add("Purpose", entity.Purpose);
            }
        
            return parameters;
        }
        
        private static Dapper.DynamicParameters AllExceptSpecifiedParameters(
            File entity, 
            File.Fields fields)
        {
            var parameters = new Dapper.DynamicParameters();
        
            // Assume we never want to exclude primary key field(s) from our input.
            parameters.Add("Id", entity.Id);
            if (!fields.HasFlag(File.Fields.UserId))
            {
                parameters.Add("UserId", entity.UserId);
            }
        
            if (!fields.HasFlag(File.Fields.State))
            {
                parameters.Add("State", entity.State);
            }
        
            if (!fields.HasFlag(File.Fields.UploadStartedDate))
            {
                parameters.Add("UploadStartedDate", entity.UploadStartedDate);
            }
        
            if (!fields.HasFlag(File.Fields.UploadCompletedDate))
            {
                parameters.Add("UploadCompletedDate", entity.UploadCompletedDate);
            }
        
            if (!fields.HasFlag(File.Fields.ProcessingStartedDate))
            {
                parameters.Add("ProcessingStartedDate", entity.ProcessingStartedDate);
            }
        
            if (!fields.HasFlag(File.Fields.ProcessingCompletedDate))
            {
                parameters.Add("ProcessingCompletedDate", entity.ProcessingCompletedDate);
            }
        
            if (!fields.HasFlag(File.Fields.FileNameWithoutExtension))
            {
                parameters.Add("FileNameWithoutExtension", entity.FileNameWithoutExtension);
            }
        
            if (!fields.HasFlag(File.Fields.FileExtension))
            {
                parameters.Add("FileExtension", entity.FileExtension);
            }
        
            if (!fields.HasFlag(File.Fields.BlobSizeBytes))
            {
                parameters.Add("BlobSizeBytes", entity.BlobSizeBytes);
            }
        
            if (!fields.HasFlag(File.Fields.Purpose))
            {
                parameters.Add("Purpose", entity.Purpose);
            }
        
            return parameters;
        }
        
    }
}
namespace Fifthweek.Api.Persistence
{
    using System;
    using System.Linq;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Core;
    using Fifthweek.Shared;
    using Dapper;

    public partial class Post  : IIdentityEquatable
    {
        public const string Table = "Posts";
        
        public Post(
            System.Guid id)
        {
            if (id == null)
            {
                throw new ArgumentNullException("id");
            }

            this.Id = id;
        }
        
        public bool IdentityEquals(object other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }
        
            if (ReferenceEquals(this, other))
            {
                return true;
            }
        
            if (other.GetType() != this.GetType())
            {
                return false;
            }
        
            return this.IdentityEquals((Post)other);
        }
        
        protected bool IdentityEquals(Post other)
        {
            if (!object.Equals(this.Id, other.Id))
            {
                return false;
            }
        
            return true;
        }
        
        [Flags]
        public enum Fields
        {
            Empty = 0,
            Id = 1, 
            ChannelId = 2, 
            CollectionId = 4, 
            FileId = 8, 
            ImageId = 16, 
            Comment = 32, 
            ScheduledByQueue = 64, 
            LiveDate = 128, 
            CreationDate = 256
        }
    }

    public static partial class PostExtensions
    {
        public static System.Threading.Tasks.Task InsertAsync(
            this System.Data.Common.DbConnection connection, 
            System.Collections.Generic.IEnumerable<Post> entities, 
            bool idempotent = true, 
            System.Data.IDbTransaction transaction = null)
        {
            return Dapper.SqlMapper.ExecuteAsync(
                connection, 
                InsertStatement(idempotent), 
                entities.Select(entity => new 
                {
                    entity.Id, entity.ChannelId, entity.CollectionId, entity.FileId, entity.ImageId, entity.Comment, entity.ScheduledByQueue, entity.LiveDate, entity.CreationDate
                }).ToArray(),
                transaction);
        }
        
        public static System.Threading.Tasks.Task InsertAsync(
            this System.Data.Common.DbConnection connection, 
            Post entity,
            bool idempotent = true, 
            System.Data.IDbTransaction transaction = null)
        {
            return Dapper.SqlMapper.ExecuteAsync(
                connection, 
                InsertStatement(idempotent), 
                new 
                {
                    entity.Id, entity.ChannelId, entity.CollectionId, entity.FileId, entity.ImageId, entity.Comment, entity.ScheduledByQueue, entity.LiveDate, entity.CreationDate
                },
                transaction);
        }
        
        public static System.Threading.Tasks.Task<int> InsertAsync(
            this System.Data.Common.DbConnection connection,
            SqlGenerationParameters<Post, Post.Fields> parameters)
        {
            var sql = new System.Text.StringBuilder();
        
            if (parameters.Declarations != null)
            {
                sql.AppendLine(parameters.Declarations);
            }
            
            int currentIndex = 0;
            if (parameters.Conditions != null)
            {
                foreach (var condition in parameters.Conditions)
                {
                    sql.Append("IF ");
                    sql.AppendLine(condition); // Remember to use `WITH (UPDLOCK, HOLDLOCK)` in your conditions! See: http://samsaffron.com/blog/archive/2007/04/04/14.aspx
                    sql.AppendLine("BEGIN");
                    ++currentIndex;
                }
            }
        
            sql.AppendLine(InsertStatement(parameters.IdempotentInsert));
        
            if (parameters.Conditions != null)
            {
                sql.AppendLine("SELECT -1 AS FailedConditionIndex"); // Indicates all conditions passed and operation was attempted.
                    
                foreach (var condition in parameters.Conditions)
                {
                    sql.AppendLine("END");
                    sql.AppendLine("ELSE");
                    sql.Append("SELECT ").Append(--currentIndex).AppendLine(" AS FailedConditionIndex");
                }
            }
        
            var entity = parameters.Entity;
            var parameterObject = parameters.ExcludedFromInput != null
                ? AllExceptSpecifiedParameters(entity, parameters.ExcludedFromInput.Value)
                : new Dapper.DynamicParameters(new { entity.Id, entity.ChannelId, entity.CollectionId, entity.FileId, entity.ImageId, entity.Comment, entity.ScheduledByQueue, entity.LiveDate, entity.CreationDate });
        
            if (parameters.AdditionalParameters != null)
            {
                parameterObject.AddDynamicParams(parameters.AdditionalParameters);
            }
        
            return Dapper.SqlMapper.ExecuteScalarAsync<int>(
                connection,
                sql.ToString(),
                parameterObject);
        }
        
        public static System.Threading.Tasks.Task UpsertAsync(
            this System.Data.Common.DbConnection connection, 
            Post entity, 
            Post.Fields fields,
            System.Data.IDbTransaction transaction = null)
        {
            return Dapper.SqlMapper.ExecuteAsync(
                connection, 
                UpsertStatement(Post.Fields.Empty, fields), 
                new 
                {
                    entity.Id, entity.ChannelId, entity.CollectionId, entity.FileId, entity.ImageId, entity.Comment, entity.ScheduledByQueue, entity.LiveDate, entity.CreationDate
                },
                transaction);
        }
        
        public static System.Threading.Tasks.Task UpsertAsync(
            this System.Data.Common.DbConnection connection, 
            Post entity, 
            Post.Fields mergeOnFields,
            Post.Fields updateFields,
            System.Data.IDbTransaction transaction = null)
        {
            return Dapper.SqlMapper.ExecuteAsync(
                connection, 
                UpsertStatement(mergeOnFields, updateFields), 
                new 
                {
                    entity.Id, entity.ChannelId, entity.CollectionId, entity.FileId, entity.ImageId, entity.Comment, entity.ScheduledByQueue, entity.LiveDate, entity.CreationDate
                },
                transaction);
        }
        
        public static System.Threading.Tasks.Task UpdateAsync(
            this System.Data.Common.DbConnection connection, 
            Post entity, 
            Post.Fields fields,
            System.Data.IDbTransaction transaction = null)
        {
            return Dapper.SqlMapper.ExecuteAsync(connection, UpdateStatement(fields), OnlySpecifiedParameters(entity, fields), transaction);
        }
        
        public static System.Threading.Tasks.Task<int> UpdateAsync(
            this System.Data.Common.DbConnection connection,
            SqlGenerationParameters<Post, Post.Fields> parameters)
        {
            if (parameters.UpdateMask == null)
            {
                throw new ArgumentException("Must contain update mask", "parameters");
            }
        
            var sql = new System.Text.StringBuilder();
        
            if (parameters.Declarations != null)
            {
                sql.AppendLine(parameters.Declarations);
            }
            
            int currentIndex = 0;
            if (parameters.Conditions != null)
            {
                foreach (var condition in parameters.Conditions)
                {
                    sql.Append("IF ");
                    sql.AppendLine(condition); // Remember to use `WITH (UPDLOCK, HOLDLOCK)` in your conditions! See: http://samsaffron.com/blog/archive/2007/04/04/14.aspx
                    sql.AppendLine("BEGIN");
                    ++currentIndex;
                }
            }
        
            sql.AppendLine(UpdateStatement(parameters.UpdateMask.Value));
        
            if (parameters.Conditions != null)
            {
                sql.AppendLine("SELECT -1 AS FailedConditionIndex"); // Indicates all conditions passed and operation was attempted.
                    
                foreach (var condition in parameters.Conditions)
                {
                    sql.AppendLine("END");
                    sql.AppendLine("ELSE");
                    sql.Append("SELECT ").Append(--currentIndex).AppendLine(" AS FailedConditionIndex");
                }
            }
        
            var parameterObject = OnlySpecifiedParameters(parameters.Entity, parameters.UpdateMask.Value, parameters.ExcludedFromInput);
        
            if (parameters.AdditionalParameters != null)
            {
                parameterObject.AddDynamicParams(parameters.AdditionalParameters);
            }
        
            return Dapper.SqlMapper.ExecuteScalarAsync<int>(
                connection,
                sql.ToString(),
                parameterObject);
        }
        
        public static string InsertStatement(bool idempotent = true)
        {
            const string insert = "INSERT INTO Posts(Id, ChannelId, CollectionId, FileId, ImageId, Comment, ScheduledByQueue, LiveDate, CreationDate) VALUES(@Id, @ChannelId, @CollectionId, @FileId, @ImageId, @Comment, @ScheduledByQueue, @LiveDate, @CreationDate)";
            return idempotent ? SqlStatementTemplates.IdempotentInsert(insert) : insert;
        }
        
        public static string UpsertStatement(Post.Fields mergeOnFields, Post.Fields updateFields)
        {
            // HOLDLOCK ensures operation is concurrent by not releasing the U lock on the row after determining
            // it does not exist. See: http://weblogs.sqlteam.com/dang/archive/2009/01/31/UPSERT-Race-Condition-With-MERGE.aspx
            var sql = new System.Text.StringBuilder();
            sql.Append(
                @"MERGE Posts WITH (HOLDLOCK) as Target
                USING (VALUES (@Id, @ChannelId, @CollectionId, @FileId, @ImageId, @Comment, @ScheduledByQueue, @LiveDate, @CreationDate)) AS Source (Id, ChannelId, CollectionId, FileId, ImageId, Comment, ScheduledByQueue, LiveDate, CreationDate)
                ON    (");
                
            if (mergeOnFields == Post.Fields.Empty)
            {
                sql.Append(@"Target.Id = Source.Id");
            }
            else
            {
                sql.AppendMergeOnParameters(GetFieldNames(mergeOnFields, false));
            }
                
            sql.Append(@")
                WHEN MATCHED THEN
                    UPDATE
                    SET        ");
            sql.AppendUpdateParameters(GetFieldNames(updateFields));
            sql.Append(
                @" WHEN NOT MATCHED THEN
                    INSERT  (Id, ChannelId, CollectionId, FileId, ImageId, Comment, ScheduledByQueue, LiveDate, CreationDate)
                    VALUES  (Source.Id, Source.ChannelId, Source.CollectionId, Source.FileId, Source.ImageId, Source.Comment, Source.ScheduledByQueue, Source.LiveDate, Source.CreationDate);");
            return sql.ToString();
        }
        
        public static string UpdateStatement(Post.Fields fields)
        {
            var sql = new System.Text.StringBuilder();
            sql.Append("UPDATE Posts SET ");
            sql.AppendUpdateParameters(GetFieldNames(fields));
            sql.Append(" WHERE Id = @Id");
            return sql.ToString();
        }
        
        private static System.Collections.Generic.IReadOnlyList<string> GetFieldNames(Post.Fields fields, bool autoIncludePrimaryKeys = true)
        {
            var fieldNames = new System.Collections.Generic.List<string>();
            if (autoIncludePrimaryKeys)
            {
                fieldNames.Add("Id");
            }
        
            if (fields.HasFlag(Post.Fields.ChannelId))
            {
                fieldNames.Add("ChannelId");
            }
        
            if (fields.HasFlag(Post.Fields.CollectionId))
            {
                fieldNames.Add("CollectionId");
            }
        
            if (fields.HasFlag(Post.Fields.FileId))
            {
                fieldNames.Add("FileId");
            }
        
            if (fields.HasFlag(Post.Fields.ImageId))
            {
                fieldNames.Add("ImageId");
            }
        
            if (fields.HasFlag(Post.Fields.Comment))
            {
                fieldNames.Add("Comment");
            }
        
            if (fields.HasFlag(Post.Fields.ScheduledByQueue))
            {
                fieldNames.Add("ScheduledByQueue");
            }
        
            if (fields.HasFlag(Post.Fields.LiveDate))
            {
                fieldNames.Add("LiveDate");
            }
        
            if (fields.HasFlag(Post.Fields.CreationDate))
            {
                fieldNames.Add("CreationDate");
            }
        
            return fieldNames;
        }
        
        private static Dapper.DynamicParameters OnlySpecifiedParameters(
            Post entity, 
            Post.Fields fields,
            Post.Fields? excludedFields = null)
        {
            var parameters = new Dapper.DynamicParameters();
        
            // Assume we never want to exclude primary key field(s) from our input.
            parameters.Add("Id", entity.Id);
            if (fields.HasFlag(Post.Fields.ChannelId) && (excludedFields == null || !excludedFields.Value.HasFlag(Post.Fields.ChannelId)))
            {
                parameters.Add("ChannelId", entity.ChannelId);
            }
        
            if (fields.HasFlag(Post.Fields.CollectionId) && (excludedFields == null || !excludedFields.Value.HasFlag(Post.Fields.CollectionId)))
            {
                parameters.Add("CollectionId", entity.CollectionId);
            }
        
            if (fields.HasFlag(Post.Fields.FileId) && (excludedFields == null || !excludedFields.Value.HasFlag(Post.Fields.FileId)))
            {
                parameters.Add("FileId", entity.FileId);
            }
        
            if (fields.HasFlag(Post.Fields.ImageId) && (excludedFields == null || !excludedFields.Value.HasFlag(Post.Fields.ImageId)))
            {
                parameters.Add("ImageId", entity.ImageId);
            }
        
            if (fields.HasFlag(Post.Fields.Comment) && (excludedFields == null || !excludedFields.Value.HasFlag(Post.Fields.Comment)))
            {
                parameters.Add("Comment", entity.Comment);
            }
        
            if (fields.HasFlag(Post.Fields.ScheduledByQueue) && (excludedFields == null || !excludedFields.Value.HasFlag(Post.Fields.ScheduledByQueue)))
            {
                parameters.Add("ScheduledByQueue", entity.ScheduledByQueue);
            }
        
            if (fields.HasFlag(Post.Fields.LiveDate) && (excludedFields == null || !excludedFields.Value.HasFlag(Post.Fields.LiveDate)))
            {
                parameters.Add("LiveDate", entity.LiveDate);
            }
        
            if (fields.HasFlag(Post.Fields.CreationDate) && (excludedFields == null || !excludedFields.Value.HasFlag(Post.Fields.CreationDate)))
            {
                parameters.Add("CreationDate", entity.CreationDate);
            }
        
            return parameters;
        }
        
        private static Dapper.DynamicParameters AllExceptSpecifiedParameters(
            Post entity, 
            Post.Fields fields)
        {
            var parameters = new Dapper.DynamicParameters();
        
            // Assume we never want to exclude primary key field(s) from our input.
            parameters.Add("Id", entity.Id);
            if (!fields.HasFlag(Post.Fields.ChannelId))
            {
                parameters.Add("ChannelId", entity.ChannelId);
            }
        
            if (!fields.HasFlag(Post.Fields.CollectionId))
            {
                parameters.Add("CollectionId", entity.CollectionId);
            }
        
            if (!fields.HasFlag(Post.Fields.FileId))
            {
                parameters.Add("FileId", entity.FileId);
            }
        
            if (!fields.HasFlag(Post.Fields.ImageId))
            {
                parameters.Add("ImageId", entity.ImageId);
            }
        
            if (!fields.HasFlag(Post.Fields.Comment))
            {
                parameters.Add("Comment", entity.Comment);
            }
        
            if (!fields.HasFlag(Post.Fields.ScheduledByQueue))
            {
                parameters.Add("ScheduledByQueue", entity.ScheduledByQueue);
            }
        
            if (!fields.HasFlag(Post.Fields.LiveDate))
            {
                parameters.Add("LiveDate", entity.LiveDate);
            }
        
            if (!fields.HasFlag(Post.Fields.CreationDate))
            {
                parameters.Add("CreationDate", entity.CreationDate);
            }
        
            return parameters;
        }
        
    }
}
namespace Fifthweek.Api.Persistence
{
    using System;
    using System.Linq;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Core;
    using Fifthweek.Shared;
    using Dapper;

    public partial class RefreshToken  : IIdentityEquatable
    {
        public const string Table = "RefreshTokens";
        
        public RefreshToken(
            System.String hashedId)
        {
            if (hashedId == null)
            {
                throw new ArgumentNullException("hashedId");
            }

            this.HashedId = hashedId;
        }
        
        public bool IdentityEquals(object other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }
        
            if (ReferenceEquals(this, other))
            {
                return true;
            }
        
            if (other.GetType() != this.GetType())
            {
                return false;
            }
        
            return this.IdentityEquals((RefreshToken)other);
        }
        
        protected bool IdentityEquals(RefreshToken other)
        {
            if (!object.Equals(this.HashedId, other.HashedId))
            {
                return false;
            }
        
            return true;
        }
        
        [Flags]
        public enum Fields
        {
            Empty = 0,
            HashedId = 1, 
            Username = 2, 
            ClientId = 4, 
            IssuedDate = 8, 
            ExpiresDate = 16, 
            ProtectedTicket = 32
        }
    }

    public static partial class RefreshTokenExtensions
    {
        public static System.Threading.Tasks.Task InsertAsync(
            this System.Data.Common.DbConnection connection, 
            System.Collections.Generic.IEnumerable<RefreshToken> entities, 
            bool idempotent = true, 
            System.Data.IDbTransaction transaction = null)
        {
            return Dapper.SqlMapper.ExecuteAsync(
                connection, 
                InsertStatement(idempotent), 
                entities.Select(entity => new 
                {
                    entity.HashedId, entity.Username, entity.ClientId, entity.IssuedDate, entity.ExpiresDate, entity.ProtectedTicket
                }).ToArray(),
                transaction);
        }
        
        public static System.Threading.Tasks.Task InsertAsync(
            this System.Data.Common.DbConnection connection, 
            RefreshToken entity,
            bool idempotent = true, 
            System.Data.IDbTransaction transaction = null)
        {
            return Dapper.SqlMapper.ExecuteAsync(
                connection, 
                InsertStatement(idempotent), 
                new 
                {
                    entity.HashedId, entity.Username, entity.ClientId, entity.IssuedDate, entity.ExpiresDate, entity.ProtectedTicket
                },
                transaction);
        }
        
        public static System.Threading.Tasks.Task<int> InsertAsync(
            this System.Data.Common.DbConnection connection,
            SqlGenerationParameters<RefreshToken, RefreshToken.Fields> parameters)
        {
            var sql = new System.Text.StringBuilder();
        
            if (parameters.Declarations != null)
            {
                sql.AppendLine(parameters.Declarations);
            }
            
            int currentIndex = 0;
            if (parameters.Conditions != null)
            {
                foreach (var condition in parameters.Conditions)
                {
                    sql.Append("IF ");
                    sql.AppendLine(condition); // Remember to use `WITH (UPDLOCK, HOLDLOCK)` in your conditions! See: http://samsaffron.com/blog/archive/2007/04/04/14.aspx
                    sql.AppendLine("BEGIN");
                    ++currentIndex;
                }
            }
        
            sql.AppendLine(InsertStatement(parameters.IdempotentInsert));
        
            if (parameters.Conditions != null)
            {
                sql.AppendLine("SELECT -1 AS FailedConditionIndex"); // Indicates all conditions passed and operation was attempted.
                    
                foreach (var condition in parameters.Conditions)
                {
                    sql.AppendLine("END");
                    sql.AppendLine("ELSE");
                    sql.Append("SELECT ").Append(--currentIndex).AppendLine(" AS FailedConditionIndex");
                }
            }
        
            var entity = parameters.Entity;
            var parameterObject = parameters.ExcludedFromInput != null
                ? AllExceptSpecifiedParameters(entity, parameters.ExcludedFromInput.Value)
                : new Dapper.DynamicParameters(new { entity.HashedId, entity.Username, entity.ClientId, entity.IssuedDate, entity.ExpiresDate, entity.ProtectedTicket });
        
            if (parameters.AdditionalParameters != null)
            {
                parameterObject.AddDynamicParams(parameters.AdditionalParameters);
            }
        
            return Dapper.SqlMapper.ExecuteScalarAsync<int>(
                connection,
                sql.ToString(),
                parameterObject);
        }
        
        public static System.Threading.Tasks.Task UpsertAsync(
            this System.Data.Common.DbConnection connection, 
            RefreshToken entity, 
            RefreshToken.Fields fields,
            System.Data.IDbTransaction transaction = null)
        {
            return Dapper.SqlMapper.ExecuteAsync(
                connection, 
                UpsertStatement(RefreshToken.Fields.Empty, fields), 
                new 
                {
                    entity.HashedId, entity.Username, entity.ClientId, entity.IssuedDate, entity.ExpiresDate, entity.ProtectedTicket
                },
                transaction);
        }
        
        public static System.Threading.Tasks.Task UpsertAsync(
            this System.Data.Common.DbConnection connection, 
            RefreshToken entity, 
            RefreshToken.Fields mergeOnFields,
            RefreshToken.Fields updateFields,
            System.Data.IDbTransaction transaction = null)
        {
            return Dapper.SqlMapper.ExecuteAsync(
                connection, 
                UpsertStatement(mergeOnFields, updateFields), 
                new 
                {
                    entity.HashedId, entity.Username, entity.ClientId, entity.IssuedDate, entity.ExpiresDate, entity.ProtectedTicket
                },
                transaction);
        }
        
        public static System.Threading.Tasks.Task UpdateAsync(
            this System.Data.Common.DbConnection connection, 
            RefreshToken entity, 
            RefreshToken.Fields fields,
            System.Data.IDbTransaction transaction = null)
        {
            return Dapper.SqlMapper.ExecuteAsync(connection, UpdateStatement(fields), OnlySpecifiedParameters(entity, fields), transaction);
        }
        
        public static System.Threading.Tasks.Task<int> UpdateAsync(
            this System.Data.Common.DbConnection connection,
            SqlGenerationParameters<RefreshToken, RefreshToken.Fields> parameters)
        {
            if (parameters.UpdateMask == null)
            {
                throw new ArgumentException("Must contain update mask", "parameters");
            }
        
            var sql = new System.Text.StringBuilder();
        
            if (parameters.Declarations != null)
            {
                sql.AppendLine(parameters.Declarations);
            }
            
            int currentIndex = 0;
            if (parameters.Conditions != null)
            {
                foreach (var condition in parameters.Conditions)
                {
                    sql.Append("IF ");
                    sql.AppendLine(condition); // Remember to use `WITH (UPDLOCK, HOLDLOCK)` in your conditions! See: http://samsaffron.com/blog/archive/2007/04/04/14.aspx
                    sql.AppendLine("BEGIN");
                    ++currentIndex;
                }
            }
        
            sql.AppendLine(UpdateStatement(parameters.UpdateMask.Value));
        
            if (parameters.Conditions != null)
            {
                sql.AppendLine("SELECT -1 AS FailedConditionIndex"); // Indicates all conditions passed and operation was attempted.
                    
                foreach (var condition in parameters.Conditions)
                {
                    sql.AppendLine("END");
                    sql.AppendLine("ELSE");
                    sql.Append("SELECT ").Append(--currentIndex).AppendLine(" AS FailedConditionIndex");
                }
            }
        
            var parameterObject = OnlySpecifiedParameters(parameters.Entity, parameters.UpdateMask.Value, parameters.ExcludedFromInput);
        
            if (parameters.AdditionalParameters != null)
            {
                parameterObject.AddDynamicParams(parameters.AdditionalParameters);
            }
        
            return Dapper.SqlMapper.ExecuteScalarAsync<int>(
                connection,
                sql.ToString(),
                parameterObject);
        }
        
        public static string InsertStatement(bool idempotent = true)
        {
            const string insert = "INSERT INTO RefreshTokens(HashedId, Username, ClientId, IssuedDate, ExpiresDate, ProtectedTicket) VALUES(@HashedId, @Username, @ClientId, @IssuedDate, @ExpiresDate, @ProtectedTicket)";
            return idempotent ? SqlStatementTemplates.IdempotentInsert(insert) : insert;
        }
        
        public static string UpsertStatement(RefreshToken.Fields mergeOnFields, RefreshToken.Fields updateFields)
        {
            // HOLDLOCK ensures operation is concurrent by not releasing the U lock on the row after determining
            // it does not exist. See: http://weblogs.sqlteam.com/dang/archive/2009/01/31/UPSERT-Race-Condition-With-MERGE.aspx
            var sql = new System.Text.StringBuilder();
            sql.Append(
                @"MERGE RefreshTokens WITH (HOLDLOCK) as Target
                USING (VALUES (@HashedId, @Username, @ClientId, @IssuedDate, @ExpiresDate, @ProtectedTicket)) AS Source (HashedId, Username, ClientId, IssuedDate, ExpiresDate, ProtectedTicket)
                ON    (");
                
            if (mergeOnFields == RefreshToken.Fields.Empty)
            {
                sql.Append(@"Target.HashedId = Source.HashedId");
            }
            else
            {
                sql.AppendMergeOnParameters(GetFieldNames(mergeOnFields, false));
            }
                
            sql.Append(@")
                WHEN MATCHED THEN
                    UPDATE
                    SET        ");
            sql.AppendUpdateParameters(GetFieldNames(updateFields));
            sql.Append(
                @" WHEN NOT MATCHED THEN
                    INSERT  (HashedId, Username, ClientId, IssuedDate, ExpiresDate, ProtectedTicket)
                    VALUES  (Source.HashedId, Source.Username, Source.ClientId, Source.IssuedDate, Source.ExpiresDate, Source.ProtectedTicket);");
            return sql.ToString();
        }
        
        public static string UpdateStatement(RefreshToken.Fields fields)
        {
            var sql = new System.Text.StringBuilder();
            sql.Append("UPDATE RefreshTokens SET ");
            sql.AppendUpdateParameters(GetFieldNames(fields));
            sql.Append(" WHERE HashedId = @HashedId");
            return sql.ToString();
        }
        
        private static System.Collections.Generic.IReadOnlyList<string> GetFieldNames(RefreshToken.Fields fields, bool autoIncludePrimaryKeys = true)
        {
            var fieldNames = new System.Collections.Generic.List<string>();
            if (autoIncludePrimaryKeys)
            {
                fieldNames.Add("HashedId");
            }
        
            if (fields.HasFlag(RefreshToken.Fields.Username))
            {
                fieldNames.Add("Username");
            }
        
            if (fields.HasFlag(RefreshToken.Fields.ClientId))
            {
                fieldNames.Add("ClientId");
            }
        
            if (fields.HasFlag(RefreshToken.Fields.IssuedDate))
            {
                fieldNames.Add("IssuedDate");
            }
        
            if (fields.HasFlag(RefreshToken.Fields.ExpiresDate))
            {
                fieldNames.Add("ExpiresDate");
            }
        
            if (fields.HasFlag(RefreshToken.Fields.ProtectedTicket))
            {
                fieldNames.Add("ProtectedTicket");
            }
        
            return fieldNames;
        }
        
        private static Dapper.DynamicParameters OnlySpecifiedParameters(
            RefreshToken entity, 
            RefreshToken.Fields fields,
            RefreshToken.Fields? excludedFields = null)
        {
            var parameters = new Dapper.DynamicParameters();
        
            // Assume we never want to exclude primary key field(s) from our input.
            parameters.Add("HashedId", entity.HashedId);
            if (fields.HasFlag(RefreshToken.Fields.Username) && (excludedFields == null || !excludedFields.Value.HasFlag(RefreshToken.Fields.Username)))
            {
                parameters.Add("Username", entity.Username);
            }
        
            if (fields.HasFlag(RefreshToken.Fields.ClientId) && (excludedFields == null || !excludedFields.Value.HasFlag(RefreshToken.Fields.ClientId)))
            {
                parameters.Add("ClientId", entity.ClientId);
            }
        
            if (fields.HasFlag(RefreshToken.Fields.IssuedDate) && (excludedFields == null || !excludedFields.Value.HasFlag(RefreshToken.Fields.IssuedDate)))
            {
                parameters.Add("IssuedDate", entity.IssuedDate);
            }
        
            if (fields.HasFlag(RefreshToken.Fields.ExpiresDate) && (excludedFields == null || !excludedFields.Value.HasFlag(RefreshToken.Fields.ExpiresDate)))
            {
                parameters.Add("ExpiresDate", entity.ExpiresDate);
            }
        
            if (fields.HasFlag(RefreshToken.Fields.ProtectedTicket) && (excludedFields == null || !excludedFields.Value.HasFlag(RefreshToken.Fields.ProtectedTicket)))
            {
                parameters.Add("ProtectedTicket", entity.ProtectedTicket);
            }
        
            return parameters;
        }
        
        private static Dapper.DynamicParameters AllExceptSpecifiedParameters(
            RefreshToken entity, 
            RefreshToken.Fields fields)
        {
            var parameters = new Dapper.DynamicParameters();
        
            // Assume we never want to exclude primary key field(s) from our input.
            parameters.Add("HashedId", entity.HashedId);
            if (!fields.HasFlag(RefreshToken.Fields.Username))
            {
                parameters.Add("Username", entity.Username);
            }
        
            if (!fields.HasFlag(RefreshToken.Fields.ClientId))
            {
                parameters.Add("ClientId", entity.ClientId);
            }
        
            if (!fields.HasFlag(RefreshToken.Fields.IssuedDate))
            {
                parameters.Add("IssuedDate", entity.IssuedDate);
            }
        
            if (!fields.HasFlag(RefreshToken.Fields.ExpiresDate))
            {
                parameters.Add("ExpiresDate", entity.ExpiresDate);
            }
        
            if (!fields.HasFlag(RefreshToken.Fields.ProtectedTicket))
            {
                parameters.Add("ProtectedTicket", entity.ProtectedTicket);
            }
        
            return parameters;
        }
        
    }
}
namespace Fifthweek.Api.Persistence
{
    using System;
    using System.Linq;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Core;
    using Fifthweek.Shared;
    using Dapper;

    public partial class Subscription  : IIdentityEquatable
    {
        public const string Table = "Subscriptions";
        
        public Subscription(
            System.Guid id)
        {
            if (id == null)
            {
                throw new ArgumentNullException("id");
            }

            this.Id = id;
        }
        
        public bool IdentityEquals(object other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }
        
            if (ReferenceEquals(this, other))
            {
                return true;
            }
        
            if (other.GetType() != this.GetType())
            {
                return false;
            }
        
            return this.IdentityEquals((Subscription)other);
        }
        
        protected bool IdentityEquals(Subscription other)
        {
            if (!object.Equals(this.Id, other.Id))
            {
                return false;
            }
        
            return true;
        }
        
        [Flags]
        public enum Fields
        {
            Empty = 0,
            Id = 1, 
            CreatorId = 2, 
            Name = 4, 
            Tagline = 8, 
            Introduction = 16, 
            Description = 32, 
            ExternalVideoUrl = 64, 
            HeaderImageFileId = 128, 
            CreationDate = 256
        }
    }

    public static partial class SubscriptionExtensions
    {
        public static System.Threading.Tasks.Task InsertAsync(
            this System.Data.Common.DbConnection connection, 
            System.Collections.Generic.IEnumerable<Subscription> entities, 
            bool idempotent = true, 
            System.Data.IDbTransaction transaction = null)
        {
            return Dapper.SqlMapper.ExecuteAsync(
                connection, 
                InsertStatement(idempotent), 
                entities.Select(entity => new 
                {
                    entity.Id, entity.CreatorId, entity.Name, entity.Tagline, entity.Introduction, entity.Description, entity.ExternalVideoUrl, entity.HeaderImageFileId, entity.CreationDate
                }).ToArray(),
                transaction);
        }
        
        public static System.Threading.Tasks.Task InsertAsync(
            this System.Data.Common.DbConnection connection, 
            Subscription entity,
            bool idempotent = true, 
            System.Data.IDbTransaction transaction = null)
        {
            return Dapper.SqlMapper.ExecuteAsync(
                connection, 
                InsertStatement(idempotent), 
                new 
                {
                    entity.Id, entity.CreatorId, entity.Name, entity.Tagline, entity.Introduction, entity.Description, entity.ExternalVideoUrl, entity.HeaderImageFileId, entity.CreationDate
                },
                transaction);
        }
        
        public static System.Threading.Tasks.Task<int> InsertAsync(
            this System.Data.Common.DbConnection connection,
            SqlGenerationParameters<Subscription, Subscription.Fields> parameters)
        {
            var sql = new System.Text.StringBuilder();
        
            if (parameters.Declarations != null)
            {
                sql.AppendLine(parameters.Declarations);
            }
            
            int currentIndex = 0;
            if (parameters.Conditions != null)
            {
                foreach (var condition in parameters.Conditions)
                {
                    sql.Append("IF ");
                    sql.AppendLine(condition); // Remember to use `WITH (UPDLOCK, HOLDLOCK)` in your conditions! See: http://samsaffron.com/blog/archive/2007/04/04/14.aspx
                    sql.AppendLine("BEGIN");
                    ++currentIndex;
                }
            }
        
            sql.AppendLine(InsertStatement(parameters.IdempotentInsert));
        
            if (parameters.Conditions != null)
            {
                sql.AppendLine("SELECT -1 AS FailedConditionIndex"); // Indicates all conditions passed and operation was attempted.
                    
                foreach (var condition in parameters.Conditions)
                {
                    sql.AppendLine("END");
                    sql.AppendLine("ELSE");
                    sql.Append("SELECT ").Append(--currentIndex).AppendLine(" AS FailedConditionIndex");
                }
            }
        
            var entity = parameters.Entity;
            var parameterObject = parameters.ExcludedFromInput != null
                ? AllExceptSpecifiedParameters(entity, parameters.ExcludedFromInput.Value)
                : new Dapper.DynamicParameters(new { entity.Id, entity.CreatorId, entity.Name, entity.Tagline, entity.Introduction, entity.Description, entity.ExternalVideoUrl, entity.HeaderImageFileId, entity.CreationDate });
        
            if (parameters.AdditionalParameters != null)
            {
                parameterObject.AddDynamicParams(parameters.AdditionalParameters);
            }
        
            return Dapper.SqlMapper.ExecuteScalarAsync<int>(
                connection,
                sql.ToString(),
                parameterObject);
        }
        
        public static System.Threading.Tasks.Task UpsertAsync(
            this System.Data.Common.DbConnection connection, 
            Subscription entity, 
            Subscription.Fields fields,
            System.Data.IDbTransaction transaction = null)
        {
            return Dapper.SqlMapper.ExecuteAsync(
                connection, 
                UpsertStatement(Subscription.Fields.Empty, fields), 
                new 
                {
                    entity.Id, entity.CreatorId, entity.Name, entity.Tagline, entity.Introduction, entity.Description, entity.ExternalVideoUrl, entity.HeaderImageFileId, entity.CreationDate
                },
                transaction);
        }
        
        public static System.Threading.Tasks.Task UpsertAsync(
            this System.Data.Common.DbConnection connection, 
            Subscription entity, 
            Subscription.Fields mergeOnFields,
            Subscription.Fields updateFields,
            System.Data.IDbTransaction transaction = null)
        {
            return Dapper.SqlMapper.ExecuteAsync(
                connection, 
                UpsertStatement(mergeOnFields, updateFields), 
                new 
                {
                    entity.Id, entity.CreatorId, entity.Name, entity.Tagline, entity.Introduction, entity.Description, entity.ExternalVideoUrl, entity.HeaderImageFileId, entity.CreationDate
                },
                transaction);
        }
        
        public static System.Threading.Tasks.Task UpdateAsync(
            this System.Data.Common.DbConnection connection, 
            Subscription entity, 
            Subscription.Fields fields,
            System.Data.IDbTransaction transaction = null)
        {
            return Dapper.SqlMapper.ExecuteAsync(connection, UpdateStatement(fields), OnlySpecifiedParameters(entity, fields), transaction);
        }
        
        public static System.Threading.Tasks.Task<int> UpdateAsync(
            this System.Data.Common.DbConnection connection,
            SqlGenerationParameters<Subscription, Subscription.Fields> parameters)
        {
            if (parameters.UpdateMask == null)
            {
                throw new ArgumentException("Must contain update mask", "parameters");
            }
        
            var sql = new System.Text.StringBuilder();
        
            if (parameters.Declarations != null)
            {
                sql.AppendLine(parameters.Declarations);
            }
            
            int currentIndex = 0;
            if (parameters.Conditions != null)
            {
                foreach (var condition in parameters.Conditions)
                {
                    sql.Append("IF ");
                    sql.AppendLine(condition); // Remember to use `WITH (UPDLOCK, HOLDLOCK)` in your conditions! See: http://samsaffron.com/blog/archive/2007/04/04/14.aspx
                    sql.AppendLine("BEGIN");
                    ++currentIndex;
                }
            }
        
            sql.AppendLine(UpdateStatement(parameters.UpdateMask.Value));
        
            if (parameters.Conditions != null)
            {
                sql.AppendLine("SELECT -1 AS FailedConditionIndex"); // Indicates all conditions passed and operation was attempted.
                    
                foreach (var condition in parameters.Conditions)
                {
                    sql.AppendLine("END");
                    sql.AppendLine("ELSE");
                    sql.Append("SELECT ").Append(--currentIndex).AppendLine(" AS FailedConditionIndex");
                }
            }
        
            var parameterObject = OnlySpecifiedParameters(parameters.Entity, parameters.UpdateMask.Value, parameters.ExcludedFromInput);
        
            if (parameters.AdditionalParameters != null)
            {
                parameterObject.AddDynamicParams(parameters.AdditionalParameters);
            }
        
            return Dapper.SqlMapper.ExecuteScalarAsync<int>(
                connection,
                sql.ToString(),
                parameterObject);
        }
        
        public static string InsertStatement(bool idempotent = true)
        {
            const string insert = "INSERT INTO Subscriptions(Id, CreatorId, Name, Tagline, Introduction, Description, ExternalVideoUrl, HeaderImageFileId, CreationDate) VALUES(@Id, @CreatorId, @Name, @Tagline, @Introduction, @Description, @ExternalVideoUrl, @HeaderImageFileId, @CreationDate)";
            return idempotent ? SqlStatementTemplates.IdempotentInsert(insert) : insert;
        }
        
        public static string UpsertStatement(Subscription.Fields mergeOnFields, Subscription.Fields updateFields)
        {
            // HOLDLOCK ensures operation is concurrent by not releasing the U lock on the row after determining
            // it does not exist. See: http://weblogs.sqlteam.com/dang/archive/2009/01/31/UPSERT-Race-Condition-With-MERGE.aspx
            var sql = new System.Text.StringBuilder();
            sql.Append(
                @"MERGE Subscriptions WITH (HOLDLOCK) as Target
                USING (VALUES (@Id, @CreatorId, @Name, @Tagline, @Introduction, @Description, @ExternalVideoUrl, @HeaderImageFileId, @CreationDate)) AS Source (Id, CreatorId, Name, Tagline, Introduction, Description, ExternalVideoUrl, HeaderImageFileId, CreationDate)
                ON    (");
                
            if (mergeOnFields == Subscription.Fields.Empty)
            {
                sql.Append(@"Target.Id = Source.Id");
            }
            else
            {
                sql.AppendMergeOnParameters(GetFieldNames(mergeOnFields, false));
            }
                
            sql.Append(@")
                WHEN MATCHED THEN
                    UPDATE
                    SET        ");
            sql.AppendUpdateParameters(GetFieldNames(updateFields));
            sql.Append(
                @" WHEN NOT MATCHED THEN
                    INSERT  (Id, CreatorId, Name, Tagline, Introduction, Description, ExternalVideoUrl, HeaderImageFileId, CreationDate)
                    VALUES  (Source.Id, Source.CreatorId, Source.Name, Source.Tagline, Source.Introduction, Source.Description, Source.ExternalVideoUrl, Source.HeaderImageFileId, Source.CreationDate);");
            return sql.ToString();
        }
        
        public static string UpdateStatement(Subscription.Fields fields)
        {
            var sql = new System.Text.StringBuilder();
            sql.Append("UPDATE Subscriptions SET ");
            sql.AppendUpdateParameters(GetFieldNames(fields));
            sql.Append(" WHERE Id = @Id");
            return sql.ToString();
        }
        
        private static System.Collections.Generic.IReadOnlyList<string> GetFieldNames(Subscription.Fields fields, bool autoIncludePrimaryKeys = true)
        {
            var fieldNames = new System.Collections.Generic.List<string>();
            if (autoIncludePrimaryKeys)
            {
                fieldNames.Add("Id");
            }
        
            if (fields.HasFlag(Subscription.Fields.CreatorId))
            {
                fieldNames.Add("CreatorId");
            }
        
            if (fields.HasFlag(Subscription.Fields.Name))
            {
                fieldNames.Add("Name");
            }
        
            if (fields.HasFlag(Subscription.Fields.Tagline))
            {
                fieldNames.Add("Tagline");
            }
        
            if (fields.HasFlag(Subscription.Fields.Introduction))
            {
                fieldNames.Add("Introduction");
            }
        
            if (fields.HasFlag(Subscription.Fields.Description))
            {
                fieldNames.Add("Description");
            }
        
            if (fields.HasFlag(Subscription.Fields.ExternalVideoUrl))
            {
                fieldNames.Add("ExternalVideoUrl");
            }
        
            if (fields.HasFlag(Subscription.Fields.HeaderImageFileId))
            {
                fieldNames.Add("HeaderImageFileId");
            }
        
            if (fields.HasFlag(Subscription.Fields.CreationDate))
            {
                fieldNames.Add("CreationDate");
            }
        
            return fieldNames;
        }
        
        private static Dapper.DynamicParameters OnlySpecifiedParameters(
            Subscription entity, 
            Subscription.Fields fields,
            Subscription.Fields? excludedFields = null)
        {
            var parameters = new Dapper.DynamicParameters();
        
            // Assume we never want to exclude primary key field(s) from our input.
            parameters.Add("Id", entity.Id);
            if (fields.HasFlag(Subscription.Fields.CreatorId) && (excludedFields == null || !excludedFields.Value.HasFlag(Subscription.Fields.CreatorId)))
            {
                parameters.Add("CreatorId", entity.CreatorId);
            }
        
            if (fields.HasFlag(Subscription.Fields.Name) && (excludedFields == null || !excludedFields.Value.HasFlag(Subscription.Fields.Name)))
            {
                parameters.Add("Name", entity.Name);
            }
        
            if (fields.HasFlag(Subscription.Fields.Tagline) && (excludedFields == null || !excludedFields.Value.HasFlag(Subscription.Fields.Tagline)))
            {
                parameters.Add("Tagline", entity.Tagline);
            }
        
            if (fields.HasFlag(Subscription.Fields.Introduction) && (excludedFields == null || !excludedFields.Value.HasFlag(Subscription.Fields.Introduction)))
            {
                parameters.Add("Introduction", entity.Introduction);
            }
        
            if (fields.HasFlag(Subscription.Fields.Description) && (excludedFields == null || !excludedFields.Value.HasFlag(Subscription.Fields.Description)))
            {
                parameters.Add("Description", entity.Description);
            }
        
            if (fields.HasFlag(Subscription.Fields.ExternalVideoUrl) && (excludedFields == null || !excludedFields.Value.HasFlag(Subscription.Fields.ExternalVideoUrl)))
            {
                parameters.Add("ExternalVideoUrl", entity.ExternalVideoUrl);
            }
        
            if (fields.HasFlag(Subscription.Fields.HeaderImageFileId) && (excludedFields == null || !excludedFields.Value.HasFlag(Subscription.Fields.HeaderImageFileId)))
            {
                parameters.Add("HeaderImageFileId", entity.HeaderImageFileId);
            }
        
            if (fields.HasFlag(Subscription.Fields.CreationDate) && (excludedFields == null || !excludedFields.Value.HasFlag(Subscription.Fields.CreationDate)))
            {
                parameters.Add("CreationDate", entity.CreationDate);
            }
        
            return parameters;
        }
        
        private static Dapper.DynamicParameters AllExceptSpecifiedParameters(
            Subscription entity, 
            Subscription.Fields fields)
        {
            var parameters = new Dapper.DynamicParameters();
        
            // Assume we never want to exclude primary key field(s) from our input.
            parameters.Add("Id", entity.Id);
            if (!fields.HasFlag(Subscription.Fields.CreatorId))
            {
                parameters.Add("CreatorId", entity.CreatorId);
            }
        
            if (!fields.HasFlag(Subscription.Fields.Name))
            {
                parameters.Add("Name", entity.Name);
            }
        
            if (!fields.HasFlag(Subscription.Fields.Tagline))
            {
                parameters.Add("Tagline", entity.Tagline);
            }
        
            if (!fields.HasFlag(Subscription.Fields.Introduction))
            {
                parameters.Add("Introduction", entity.Introduction);
            }
        
            if (!fields.HasFlag(Subscription.Fields.Description))
            {
                parameters.Add("Description", entity.Description);
            }
        
            if (!fields.HasFlag(Subscription.Fields.ExternalVideoUrl))
            {
                parameters.Add("ExternalVideoUrl", entity.ExternalVideoUrl);
            }
        
            if (!fields.HasFlag(Subscription.Fields.HeaderImageFileId))
            {
                parameters.Add("HeaderImageFileId", entity.HeaderImageFileId);
            }
        
            if (!fields.HasFlag(Subscription.Fields.CreationDate))
            {
                parameters.Add("CreationDate", entity.CreationDate);
            }
        
            return parameters;
        }
        
    }
}
namespace Fifthweek.Api.Persistence
{
    using System;
    using System.Linq;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Core;
    using Fifthweek.Shared;
    using Dapper;

    public partial class WeeklyReleaseTime  : IIdentityEquatable
    {
        public const string Table = "WeeklyReleaseTimes";
        
        public WeeklyReleaseTime(
            System.Guid collectionId,
            System.Byte hourOfWeek)
        {
            if (collectionId == null)
            {
                throw new ArgumentNullException("collectionId");
            }

            if (hourOfWeek == null)
            {
                throw new ArgumentNullException("hourOfWeek");
            }

            this.CollectionId = collectionId;
            this.HourOfWeek = hourOfWeek;
        }
        
        public bool IdentityEquals(object other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }
        
            if (ReferenceEquals(this, other))
            {
                return true;
            }
        
            if (other.GetType() != this.GetType())
            {
                return false;
            }
        
            return this.IdentityEquals((WeeklyReleaseTime)other);
        }
        
        protected bool IdentityEquals(WeeklyReleaseTime other)
        {
            if (!object.Equals(this.CollectionId, other.CollectionId))
            {
                return false;
            }
        
            if (!object.Equals(this.HourOfWeek, other.HourOfWeek))
            {
                return false;
            }
        
            return true;
        }
        
        [Flags]
        public enum Fields
        {
            Empty = 0,
            CollectionId = 1, 
            HourOfWeek = 2
        }
    }

    public static partial class WeeklyReleaseTimeExtensions
    {
        public static System.Threading.Tasks.Task InsertAsync(
            this System.Data.Common.DbConnection connection, 
            System.Collections.Generic.IEnumerable<WeeklyReleaseTime> entities, 
            bool idempotent = true, 
            System.Data.IDbTransaction transaction = null)
        {
            return Dapper.SqlMapper.ExecuteAsync(
                connection, 
                InsertStatement(idempotent), 
                entities.Select(entity => new 
                {
                    entity.CollectionId, entity.HourOfWeek
                }).ToArray(),
                transaction);
        }
        
        public static System.Threading.Tasks.Task InsertAsync(
            this System.Data.Common.DbConnection connection, 
            WeeklyReleaseTime entity,
            bool idempotent = true, 
            System.Data.IDbTransaction transaction = null)
        {
            return Dapper.SqlMapper.ExecuteAsync(
                connection, 
                InsertStatement(idempotent), 
                new 
                {
                    entity.CollectionId, entity.HourOfWeek
                },
                transaction);
        }
        
        public static System.Threading.Tasks.Task<int> InsertAsync(
            this System.Data.Common.DbConnection connection,
            SqlGenerationParameters<WeeklyReleaseTime, WeeklyReleaseTime.Fields> parameters)
        {
            var sql = new System.Text.StringBuilder();
        
            if (parameters.Declarations != null)
            {
                sql.AppendLine(parameters.Declarations);
            }
            
            int currentIndex = 0;
            if (parameters.Conditions != null)
            {
                foreach (var condition in parameters.Conditions)
                {
                    sql.Append("IF ");
                    sql.AppendLine(condition); // Remember to use `WITH (UPDLOCK, HOLDLOCK)` in your conditions! See: http://samsaffron.com/blog/archive/2007/04/04/14.aspx
                    sql.AppendLine("BEGIN");
                    ++currentIndex;
                }
            }
        
            sql.AppendLine(InsertStatement(parameters.IdempotentInsert));
        
            if (parameters.Conditions != null)
            {
                sql.AppendLine("SELECT -1 AS FailedConditionIndex"); // Indicates all conditions passed and operation was attempted.
                    
                foreach (var condition in parameters.Conditions)
                {
                    sql.AppendLine("END");
                    sql.AppendLine("ELSE");
                    sql.Append("SELECT ").Append(--currentIndex).AppendLine(" AS FailedConditionIndex");
                }
            }
        
            var entity = parameters.Entity;
            var parameterObject = parameters.ExcludedFromInput != null
                ? AllExceptSpecifiedParameters(entity, parameters.ExcludedFromInput.Value)
                : new Dapper.DynamicParameters(new { entity.CollectionId, entity.HourOfWeek });
        
            if (parameters.AdditionalParameters != null)
            {
                parameterObject.AddDynamicParams(parameters.AdditionalParameters);
            }
        
            return Dapper.SqlMapper.ExecuteScalarAsync<int>(
                connection,
                sql.ToString(),
                parameterObject);
        }
        
        public static System.Threading.Tasks.Task UpsertAsync(
            this System.Data.Common.DbConnection connection, 
            WeeklyReleaseTime entity, 
            WeeklyReleaseTime.Fields fields,
            System.Data.IDbTransaction transaction = null)
        {
            return Dapper.SqlMapper.ExecuteAsync(
                connection, 
                UpsertStatement(WeeklyReleaseTime.Fields.Empty, fields), 
                new 
                {
                    entity.CollectionId, entity.HourOfWeek
                },
                transaction);
        }
        
        public static System.Threading.Tasks.Task UpsertAsync(
            this System.Data.Common.DbConnection connection, 
            WeeklyReleaseTime entity, 
            WeeklyReleaseTime.Fields mergeOnFields,
            WeeklyReleaseTime.Fields updateFields,
            System.Data.IDbTransaction transaction = null)
        {
            return Dapper.SqlMapper.ExecuteAsync(
                connection, 
                UpsertStatement(mergeOnFields, updateFields), 
                new 
                {
                    entity.CollectionId, entity.HourOfWeek
                },
                transaction);
        }
        
        public static System.Threading.Tasks.Task UpdateAsync(
            this System.Data.Common.DbConnection connection, 
            WeeklyReleaseTime entity, 
            WeeklyReleaseTime.Fields fields,
            System.Data.IDbTransaction transaction = null)
        {
            return Dapper.SqlMapper.ExecuteAsync(connection, UpdateStatement(fields), OnlySpecifiedParameters(entity, fields), transaction);
        }
        
        public static System.Threading.Tasks.Task<int> UpdateAsync(
            this System.Data.Common.DbConnection connection,
            SqlGenerationParameters<WeeklyReleaseTime, WeeklyReleaseTime.Fields> parameters)
        {
            if (parameters.UpdateMask == null)
            {
                throw new ArgumentException("Must contain update mask", "parameters");
            }
        
            var sql = new System.Text.StringBuilder();
        
            if (parameters.Declarations != null)
            {
                sql.AppendLine(parameters.Declarations);
            }
            
            int currentIndex = 0;
            if (parameters.Conditions != null)
            {
                foreach (var condition in parameters.Conditions)
                {
                    sql.Append("IF ");
                    sql.AppendLine(condition); // Remember to use `WITH (UPDLOCK, HOLDLOCK)` in your conditions! See: http://samsaffron.com/blog/archive/2007/04/04/14.aspx
                    sql.AppendLine("BEGIN");
                    ++currentIndex;
                }
            }
        
            sql.AppendLine(UpdateStatement(parameters.UpdateMask.Value));
        
            if (parameters.Conditions != null)
            {
                sql.AppendLine("SELECT -1 AS FailedConditionIndex"); // Indicates all conditions passed and operation was attempted.
                    
                foreach (var condition in parameters.Conditions)
                {
                    sql.AppendLine("END");
                    sql.AppendLine("ELSE");
                    sql.Append("SELECT ").Append(--currentIndex).AppendLine(" AS FailedConditionIndex");
                }
            }
        
            var parameterObject = OnlySpecifiedParameters(parameters.Entity, parameters.UpdateMask.Value, parameters.ExcludedFromInput);
        
            if (parameters.AdditionalParameters != null)
            {
                parameterObject.AddDynamicParams(parameters.AdditionalParameters);
            }
        
            return Dapper.SqlMapper.ExecuteScalarAsync<int>(
                connection,
                sql.ToString(),
                parameterObject);
        }
        
        public static string InsertStatement(bool idempotent = true)
        {
            const string insert = "INSERT INTO WeeklyReleaseTimes(CollectionId, HourOfWeek) VALUES(@CollectionId, @HourOfWeek)";
            return idempotent ? SqlStatementTemplates.IdempotentInsert(insert) : insert;
        }
        
        public static string UpsertStatement(WeeklyReleaseTime.Fields mergeOnFields, WeeklyReleaseTime.Fields updateFields)
        {
            // HOLDLOCK ensures operation is concurrent by not releasing the U lock on the row after determining
            // it does not exist. See: http://weblogs.sqlteam.com/dang/archive/2009/01/31/UPSERT-Race-Condition-With-MERGE.aspx
            var sql = new System.Text.StringBuilder();
            sql.Append(
                @"MERGE WeeklyReleaseTimes WITH (HOLDLOCK) as Target
                USING (VALUES (@CollectionId, @HourOfWeek)) AS Source (CollectionId, HourOfWeek)
                ON    (");
                
            if (mergeOnFields == WeeklyReleaseTime.Fields.Empty)
            {
                sql.Append(@"Target.CollectionId = Source.CollectionId AND Target.HourOfWeek = Source.HourOfWeek");
            }
            else
            {
                sql.AppendMergeOnParameters(GetFieldNames(mergeOnFields, false));
            }
                
            sql.Append(@")
                WHEN MATCHED THEN
                    UPDATE
                    SET        ");
            sql.AppendUpdateParameters(GetFieldNames(updateFields));
            sql.Append(
                @" WHEN NOT MATCHED THEN
                    INSERT  (CollectionId, HourOfWeek)
                    VALUES  (Source.CollectionId, Source.HourOfWeek);");
            return sql.ToString();
        }
        
        public static string UpdateStatement(WeeklyReleaseTime.Fields fields)
        {
            var sql = new System.Text.StringBuilder();
            sql.Append("UPDATE WeeklyReleaseTimes SET ");
            sql.AppendUpdateParameters(GetFieldNames(fields));
            sql.Append(" WHERE CollectionId = @CollectionId AND HourOfWeek = @HourOfWeek");
            return sql.ToString();
        }
        
        private static System.Collections.Generic.IReadOnlyList<string> GetFieldNames(WeeklyReleaseTime.Fields fields, bool autoIncludePrimaryKeys = true)
        {
            var fieldNames = new System.Collections.Generic.List<string>();
            if (autoIncludePrimaryKeys)
            {
                fieldNames.Add("CollectionId");
            }
        
            if (autoIncludePrimaryKeys)
            {
                fieldNames.Add("HourOfWeek");
            }
        
            return fieldNames;
        }
        
        private static Dapper.DynamicParameters OnlySpecifiedParameters(
            WeeklyReleaseTime entity, 
            WeeklyReleaseTime.Fields fields,
            WeeklyReleaseTime.Fields? excludedFields = null)
        {
            var parameters = new Dapper.DynamicParameters();
        
            // Assume we never want to exclude primary key field(s) from our input.
            parameters.Add("CollectionId", entity.CollectionId);
            parameters.Add("HourOfWeek", entity.HourOfWeek);
            return parameters;
        }
        
        private static Dapper.DynamicParameters AllExceptSpecifiedParameters(
            WeeklyReleaseTime entity, 
            WeeklyReleaseTime.Fields fields)
        {
            var parameters = new Dapper.DynamicParameters();
        
            // Assume we never want to exclude primary key field(s) from our input.
            parameters.Add("CollectionId", entity.CollectionId);
            parameters.Add("HourOfWeek", entity.HourOfWeek);
            return parameters;
        }
        
    }
}
namespace Fifthweek.Api.Persistence.Identity
{
    using System;
    using System.Linq;
    using System.ComponentModel.DataAnnotations;
    using Fifthweek.CodeGeneration;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Collections.Generic;

    public partial class FifthweekRole  : IIdentityEquatable
    {
        public const string Table = "AspNetRoles";
        
        public FifthweekRole(
            System.Guid id)
        {
            if (id == null)
            {
                throw new ArgumentNullException("id");
            }

            this.Id = id;
        }
        
        public bool IdentityEquals(object other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }
        
            if (ReferenceEquals(this, other))
            {
                return true;
            }
        
            if (other.GetType() != this.GetType())
            {
                return false;
            }
        
            return this.IdentityEquals((FifthweekRole)other);
        }
        
        protected bool IdentityEquals(FifthweekRole other)
        {
            if (!object.Equals(this.Id, other.Id))
            {
                return false;
            }
        
            return true;
        }
        
        [Flags]
        public enum Fields
        {
            Empty = 0,
            Id = 1, 
            Name = 2
        }
    }

    public static partial class FifthweekRoleExtensions
    {
        public static System.Threading.Tasks.Task InsertAsync(
            this System.Data.Common.DbConnection connection, 
            System.Collections.Generic.IEnumerable<FifthweekRole> entities, 
            bool idempotent = true, 
            System.Data.IDbTransaction transaction = null)
        {
            return Dapper.SqlMapper.ExecuteAsync(
                connection, 
                InsertStatement(idempotent), 
                entities.Select(entity => new 
                {
                    entity.Id, entity.Name
                }).ToArray(),
                transaction);
        }
        
        public static System.Threading.Tasks.Task InsertAsync(
            this System.Data.Common.DbConnection connection, 
            FifthweekRole entity,
            bool idempotent = true, 
            System.Data.IDbTransaction transaction = null)
        {
            return Dapper.SqlMapper.ExecuteAsync(
                connection, 
                InsertStatement(idempotent), 
                new 
                {
                    entity.Id, entity.Name
                },
                transaction);
        }
        
        public static System.Threading.Tasks.Task<int> InsertAsync(
            this System.Data.Common.DbConnection connection,
            SqlGenerationParameters<FifthweekRole, FifthweekRole.Fields> parameters)
        {
            var sql = new System.Text.StringBuilder();
        
            if (parameters.Declarations != null)
            {
                sql.AppendLine(parameters.Declarations);
            }
            
            int currentIndex = 0;
            if (parameters.Conditions != null)
            {
                foreach (var condition in parameters.Conditions)
                {
                    sql.Append("IF ");
                    sql.AppendLine(condition); // Remember to use `WITH (UPDLOCK, HOLDLOCK)` in your conditions! See: http://samsaffron.com/blog/archive/2007/04/04/14.aspx
                    sql.AppendLine("BEGIN");
                    ++currentIndex;
                }
            }
        
            sql.AppendLine(InsertStatement(parameters.IdempotentInsert));
        
            if (parameters.Conditions != null)
            {
                sql.AppendLine("SELECT -1 AS FailedConditionIndex"); // Indicates all conditions passed and operation was attempted.
                    
                foreach (var condition in parameters.Conditions)
                {
                    sql.AppendLine("END");
                    sql.AppendLine("ELSE");
                    sql.Append("SELECT ").Append(--currentIndex).AppendLine(" AS FailedConditionIndex");
                }
            }
        
            var entity = parameters.Entity;
            var parameterObject = parameters.ExcludedFromInput != null
                ? AllExceptSpecifiedParameters(entity, parameters.ExcludedFromInput.Value)
                : new Dapper.DynamicParameters(new { entity.Id, entity.Name });
        
            if (parameters.AdditionalParameters != null)
            {
                parameterObject.AddDynamicParams(parameters.AdditionalParameters);
            }
        
            return Dapper.SqlMapper.ExecuteScalarAsync<int>(
                connection,
                sql.ToString(),
                parameterObject);
        }
        
        public static System.Threading.Tasks.Task UpsertAsync(
            this System.Data.Common.DbConnection connection, 
            FifthweekRole entity, 
            FifthweekRole.Fields fields,
            System.Data.IDbTransaction transaction = null)
        {
            return Dapper.SqlMapper.ExecuteAsync(
                connection, 
                UpsertStatement(FifthweekRole.Fields.Empty, fields), 
                new 
                {
                    entity.Id, entity.Name
                },
                transaction);
        }
        
        public static System.Threading.Tasks.Task UpsertAsync(
            this System.Data.Common.DbConnection connection, 
            FifthweekRole entity, 
            FifthweekRole.Fields mergeOnFields,
            FifthweekRole.Fields updateFields,
            System.Data.IDbTransaction transaction = null)
        {
            return Dapper.SqlMapper.ExecuteAsync(
                connection, 
                UpsertStatement(mergeOnFields, updateFields), 
                new 
                {
                    entity.Id, entity.Name
                },
                transaction);
        }
        
        public static System.Threading.Tasks.Task UpdateAsync(
            this System.Data.Common.DbConnection connection, 
            FifthweekRole entity, 
            FifthweekRole.Fields fields,
            System.Data.IDbTransaction transaction = null)
        {
            return Dapper.SqlMapper.ExecuteAsync(connection, UpdateStatement(fields), OnlySpecifiedParameters(entity, fields), transaction);
        }
        
        public static System.Threading.Tasks.Task<int> UpdateAsync(
            this System.Data.Common.DbConnection connection,
            SqlGenerationParameters<FifthweekRole, FifthweekRole.Fields> parameters)
        {
            if (parameters.UpdateMask == null)
            {
                throw new ArgumentException("Must contain update mask", "parameters");
            }
        
            var sql = new System.Text.StringBuilder();
        
            if (parameters.Declarations != null)
            {
                sql.AppendLine(parameters.Declarations);
            }
            
            int currentIndex = 0;
            if (parameters.Conditions != null)
            {
                foreach (var condition in parameters.Conditions)
                {
                    sql.Append("IF ");
                    sql.AppendLine(condition); // Remember to use `WITH (UPDLOCK, HOLDLOCK)` in your conditions! See: http://samsaffron.com/blog/archive/2007/04/04/14.aspx
                    sql.AppendLine("BEGIN");
                    ++currentIndex;
                }
            }
        
            sql.AppendLine(UpdateStatement(parameters.UpdateMask.Value));
        
            if (parameters.Conditions != null)
            {
                sql.AppendLine("SELECT -1 AS FailedConditionIndex"); // Indicates all conditions passed and operation was attempted.
                    
                foreach (var condition in parameters.Conditions)
                {
                    sql.AppendLine("END");
                    sql.AppendLine("ELSE");
                    sql.Append("SELECT ").Append(--currentIndex).AppendLine(" AS FailedConditionIndex");
                }
            }
        
            var parameterObject = OnlySpecifiedParameters(parameters.Entity, parameters.UpdateMask.Value, parameters.ExcludedFromInput);
        
            if (parameters.AdditionalParameters != null)
            {
                parameterObject.AddDynamicParams(parameters.AdditionalParameters);
            }
        
            return Dapper.SqlMapper.ExecuteScalarAsync<int>(
                connection,
                sql.ToString(),
                parameterObject);
        }
        
        public static string InsertStatement(bool idempotent = true)
        {
            const string insert = "INSERT INTO AspNetRoles(Id, Name) VALUES(@Id, @Name)";
            return idempotent ? SqlStatementTemplates.IdempotentInsert(insert) : insert;
        }
        
        public static string UpsertStatement(FifthweekRole.Fields mergeOnFields, FifthweekRole.Fields updateFields)
        {
            // HOLDLOCK ensures operation is concurrent by not releasing the U lock on the row after determining
            // it does not exist. See: http://weblogs.sqlteam.com/dang/archive/2009/01/31/UPSERT-Race-Condition-With-MERGE.aspx
            var sql = new System.Text.StringBuilder();
            sql.Append(
                @"MERGE AspNetRoles WITH (HOLDLOCK) as Target
                USING (VALUES (@Id, @Name)) AS Source (Id, Name)
                ON    (");
                
            if (mergeOnFields == FifthweekRole.Fields.Empty)
            {
                sql.Append(@"Target.Id = Source.Id");
            }
            else
            {
                sql.AppendMergeOnParameters(GetFieldNames(mergeOnFields, false));
            }
                
            sql.Append(@")
                WHEN MATCHED THEN
                    UPDATE
                    SET        ");
            sql.AppendUpdateParameters(GetFieldNames(updateFields));
            sql.Append(
                @" WHEN NOT MATCHED THEN
                    INSERT  (Id, Name)
                    VALUES  (Source.Id, Source.Name);");
            return sql.ToString();
        }
        
        public static string UpdateStatement(FifthweekRole.Fields fields)
        {
            var sql = new System.Text.StringBuilder();
            sql.Append("UPDATE AspNetRoles SET ");
            sql.AppendUpdateParameters(GetFieldNames(fields));
            sql.Append(" WHERE Id = @Id");
            return sql.ToString();
        }
        
        private static System.Collections.Generic.IReadOnlyList<string> GetFieldNames(FifthweekRole.Fields fields, bool autoIncludePrimaryKeys = true)
        {
            var fieldNames = new System.Collections.Generic.List<string>();
            if (autoIncludePrimaryKeys)
            {
                fieldNames.Add("Id");
            }
        
            if (fields.HasFlag(FifthweekRole.Fields.Name))
            {
                fieldNames.Add("Name");
            }
        
            return fieldNames;
        }
        
        private static Dapper.DynamicParameters OnlySpecifiedParameters(
            FifthweekRole entity, 
            FifthweekRole.Fields fields,
            FifthweekRole.Fields? excludedFields = null)
        {
            var parameters = new Dapper.DynamicParameters();
        
            // Assume we never want to exclude primary key field(s) from our input.
            parameters.Add("Id", entity.Id);
            if (fields.HasFlag(FifthweekRole.Fields.Name) && (excludedFields == null || !excludedFields.Value.HasFlag(FifthweekRole.Fields.Name)))
            {
                parameters.Add("Name", entity.Name);
            }
        
            return parameters;
        }
        
        private static Dapper.DynamicParameters AllExceptSpecifiedParameters(
            FifthweekRole entity, 
            FifthweekRole.Fields fields)
        {
            var parameters = new Dapper.DynamicParameters();
        
            // Assume we never want to exclude primary key field(s) from our input.
            parameters.Add("Id", entity.Id);
            if (!fields.HasFlag(FifthweekRole.Fields.Name))
            {
                parameters.Add("Name", entity.Name);
            }
        
            return parameters;
        }
        
    }
}
namespace Fifthweek.Api.Persistence.Identity
{
    using System;
    using System.Linq;
    using System.ComponentModel.DataAnnotations;
    using Fifthweek.CodeGeneration;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Collections.Generic;

    public partial class FifthweekUser  : IIdentityEquatable
    {
        public const string Table = "AspNetUsers";
        
        public FifthweekUser(
            System.Guid id)
        {
            if (id == null)
            {
                throw new ArgumentNullException("id");
            }

            this.Id = id;
        }
        
        public bool IdentityEquals(object other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }
        
            if (ReferenceEquals(this, other))
            {
                return true;
            }
        
            if (other.GetType() != this.GetType())
            {
                return false;
            }
        
            return this.IdentityEquals((FifthweekUser)other);
        }
        
        protected bool IdentityEquals(FifthweekUser other)
        {
            if (!object.Equals(this.Id, other.Id))
            {
                return false;
            }
        
            return true;
        }
        
        [Flags]
        public enum Fields
        {
            Empty = 0,
            ExampleWork = 1, 
            RegistrationDate = 2, 
            LastSignInDate = 4, 
            LastAccessTokenDate = 8, 
            ProfileImageFileId = 16, 
            Id = 32, 
            AccessFailedCount = 64, 
            Email = 128, 
            EmailConfirmed = 256, 
            LockoutEnabled = 512, 
            LockoutEndDateUtc = 1024, 
            PasswordHash = 2048, 
            PhoneNumber = 4096, 
            PhoneNumberConfirmed = 8192, 
            SecurityStamp = 16384, 
            TwoFactorEnabled = 32768, 
            UserName = 65536
        }
    }

    public static partial class FifthweekUserExtensions
    {
        public static System.Threading.Tasks.Task InsertAsync(
            this System.Data.Common.DbConnection connection, 
            System.Collections.Generic.IEnumerable<FifthweekUser> entities, 
            bool idempotent = true, 
            System.Data.IDbTransaction transaction = null)
        {
            return Dapper.SqlMapper.ExecuteAsync(
                connection, 
                InsertStatement(idempotent), 
                entities.Select(entity => new 
                {
                    entity.ExampleWork, entity.RegistrationDate, entity.LastSignInDate, entity.LastAccessTokenDate, entity.ProfileImageFileId, entity.Id, entity.AccessFailedCount, entity.Email, entity.EmailConfirmed, entity.LockoutEnabled, entity.LockoutEndDateUtc, entity.PasswordHash, entity.PhoneNumber, entity.PhoneNumberConfirmed, entity.SecurityStamp, entity.TwoFactorEnabled, entity.UserName
                }).ToArray(),
                transaction);
        }
        
        public static System.Threading.Tasks.Task InsertAsync(
            this System.Data.Common.DbConnection connection, 
            FifthweekUser entity,
            bool idempotent = true, 
            System.Data.IDbTransaction transaction = null)
        {
            return Dapper.SqlMapper.ExecuteAsync(
                connection, 
                InsertStatement(idempotent), 
                new 
                {
                    entity.ExampleWork, entity.RegistrationDate, entity.LastSignInDate, entity.LastAccessTokenDate, entity.ProfileImageFileId, entity.Id, entity.AccessFailedCount, entity.Email, entity.EmailConfirmed, entity.LockoutEnabled, entity.LockoutEndDateUtc, entity.PasswordHash, entity.PhoneNumber, entity.PhoneNumberConfirmed, entity.SecurityStamp, entity.TwoFactorEnabled, entity.UserName
                },
                transaction);
        }
        
        public static System.Threading.Tasks.Task<int> InsertAsync(
            this System.Data.Common.DbConnection connection,
            SqlGenerationParameters<FifthweekUser, FifthweekUser.Fields> parameters)
        {
            var sql = new System.Text.StringBuilder();
        
            if (parameters.Declarations != null)
            {
                sql.AppendLine(parameters.Declarations);
            }
            
            int currentIndex = 0;
            if (parameters.Conditions != null)
            {
                foreach (var condition in parameters.Conditions)
                {
                    sql.Append("IF ");
                    sql.AppendLine(condition); // Remember to use `WITH (UPDLOCK, HOLDLOCK)` in your conditions! See: http://samsaffron.com/blog/archive/2007/04/04/14.aspx
                    sql.AppendLine("BEGIN");
                    ++currentIndex;
                }
            }
        
            sql.AppendLine(InsertStatement(parameters.IdempotentInsert));
        
            if (parameters.Conditions != null)
            {
                sql.AppendLine("SELECT -1 AS FailedConditionIndex"); // Indicates all conditions passed and operation was attempted.
                    
                foreach (var condition in parameters.Conditions)
                {
                    sql.AppendLine("END");
                    sql.AppendLine("ELSE");
                    sql.Append("SELECT ").Append(--currentIndex).AppendLine(" AS FailedConditionIndex");
                }
            }
        
            var entity = parameters.Entity;
            var parameterObject = parameters.ExcludedFromInput != null
                ? AllExceptSpecifiedParameters(entity, parameters.ExcludedFromInput.Value)
                : new Dapper.DynamicParameters(new { entity.ExampleWork, entity.RegistrationDate, entity.LastSignInDate, entity.LastAccessTokenDate, entity.ProfileImageFileId, entity.Id, entity.AccessFailedCount, entity.Email, entity.EmailConfirmed, entity.LockoutEnabled, entity.LockoutEndDateUtc, entity.PasswordHash, entity.PhoneNumber, entity.PhoneNumberConfirmed, entity.SecurityStamp, entity.TwoFactorEnabled, entity.UserName });
        
            if (parameters.AdditionalParameters != null)
            {
                parameterObject.AddDynamicParams(parameters.AdditionalParameters);
            }
        
            return Dapper.SqlMapper.ExecuteScalarAsync<int>(
                connection,
                sql.ToString(),
                parameterObject);
        }
        
        public static System.Threading.Tasks.Task UpsertAsync(
            this System.Data.Common.DbConnection connection, 
            FifthweekUser entity, 
            FifthweekUser.Fields fields,
            System.Data.IDbTransaction transaction = null)
        {
            return Dapper.SqlMapper.ExecuteAsync(
                connection, 
                UpsertStatement(FifthweekUser.Fields.Empty, fields), 
                new 
                {
                    entity.ExampleWork, entity.RegistrationDate, entity.LastSignInDate, entity.LastAccessTokenDate, entity.ProfileImageFileId, entity.Id, entity.AccessFailedCount, entity.Email, entity.EmailConfirmed, entity.LockoutEnabled, entity.LockoutEndDateUtc, entity.PasswordHash, entity.PhoneNumber, entity.PhoneNumberConfirmed, entity.SecurityStamp, entity.TwoFactorEnabled, entity.UserName
                },
                transaction);
        }
        
        public static System.Threading.Tasks.Task UpsertAsync(
            this System.Data.Common.DbConnection connection, 
            FifthweekUser entity, 
            FifthweekUser.Fields mergeOnFields,
            FifthweekUser.Fields updateFields,
            System.Data.IDbTransaction transaction = null)
        {
            return Dapper.SqlMapper.ExecuteAsync(
                connection, 
                UpsertStatement(mergeOnFields, updateFields), 
                new 
                {
                    entity.ExampleWork, entity.RegistrationDate, entity.LastSignInDate, entity.LastAccessTokenDate, entity.ProfileImageFileId, entity.Id, entity.AccessFailedCount, entity.Email, entity.EmailConfirmed, entity.LockoutEnabled, entity.LockoutEndDateUtc, entity.PasswordHash, entity.PhoneNumber, entity.PhoneNumberConfirmed, entity.SecurityStamp, entity.TwoFactorEnabled, entity.UserName
                },
                transaction);
        }
        
        public static System.Threading.Tasks.Task UpdateAsync(
            this System.Data.Common.DbConnection connection, 
            FifthweekUser entity, 
            FifthweekUser.Fields fields,
            System.Data.IDbTransaction transaction = null)
        {
            return Dapper.SqlMapper.ExecuteAsync(connection, UpdateStatement(fields), OnlySpecifiedParameters(entity, fields), transaction);
        }
        
        public static System.Threading.Tasks.Task<int> UpdateAsync(
            this System.Data.Common.DbConnection connection,
            SqlGenerationParameters<FifthweekUser, FifthweekUser.Fields> parameters)
        {
            if (parameters.UpdateMask == null)
            {
                throw new ArgumentException("Must contain update mask", "parameters");
            }
        
            var sql = new System.Text.StringBuilder();
        
            if (parameters.Declarations != null)
            {
                sql.AppendLine(parameters.Declarations);
            }
            
            int currentIndex = 0;
            if (parameters.Conditions != null)
            {
                foreach (var condition in parameters.Conditions)
                {
                    sql.Append("IF ");
                    sql.AppendLine(condition); // Remember to use `WITH (UPDLOCK, HOLDLOCK)` in your conditions! See: http://samsaffron.com/blog/archive/2007/04/04/14.aspx
                    sql.AppendLine("BEGIN");
                    ++currentIndex;
                }
            }
        
            sql.AppendLine(UpdateStatement(parameters.UpdateMask.Value));
        
            if (parameters.Conditions != null)
            {
                sql.AppendLine("SELECT -1 AS FailedConditionIndex"); // Indicates all conditions passed and operation was attempted.
                    
                foreach (var condition in parameters.Conditions)
                {
                    sql.AppendLine("END");
                    sql.AppendLine("ELSE");
                    sql.Append("SELECT ").Append(--currentIndex).AppendLine(" AS FailedConditionIndex");
                }
            }
        
            var parameterObject = OnlySpecifiedParameters(parameters.Entity, parameters.UpdateMask.Value, parameters.ExcludedFromInput);
        
            if (parameters.AdditionalParameters != null)
            {
                parameterObject.AddDynamicParams(parameters.AdditionalParameters);
            }
        
            return Dapper.SqlMapper.ExecuteScalarAsync<int>(
                connection,
                sql.ToString(),
                parameterObject);
        }
        
        public static string InsertStatement(bool idempotent = true)
        {
            const string insert = "INSERT INTO AspNetUsers(ExampleWork, RegistrationDate, LastSignInDate, LastAccessTokenDate, ProfileImageFileId, Id, AccessFailedCount, Email, EmailConfirmed, LockoutEnabled, LockoutEndDateUtc, PasswordHash, PhoneNumber, PhoneNumberConfirmed, SecurityStamp, TwoFactorEnabled, UserName) VALUES(@ExampleWork, @RegistrationDate, @LastSignInDate, @LastAccessTokenDate, @ProfileImageFileId, @Id, @AccessFailedCount, @Email, @EmailConfirmed, @LockoutEnabled, @LockoutEndDateUtc, @PasswordHash, @PhoneNumber, @PhoneNumberConfirmed, @SecurityStamp, @TwoFactorEnabled, @UserName)";
            return idempotent ? SqlStatementTemplates.IdempotentInsert(insert) : insert;
        }
        
        public static string UpsertStatement(FifthweekUser.Fields mergeOnFields, FifthweekUser.Fields updateFields)
        {
            // HOLDLOCK ensures operation is concurrent by not releasing the U lock on the row after determining
            // it does not exist. See: http://weblogs.sqlteam.com/dang/archive/2009/01/31/UPSERT-Race-Condition-With-MERGE.aspx
            var sql = new System.Text.StringBuilder();
            sql.Append(
                @"MERGE AspNetUsers WITH (HOLDLOCK) as Target
                USING (VALUES (@ExampleWork, @RegistrationDate, @LastSignInDate, @LastAccessTokenDate, @ProfileImageFileId, @Id, @AccessFailedCount, @Email, @EmailConfirmed, @LockoutEnabled, @LockoutEndDateUtc, @PasswordHash, @PhoneNumber, @PhoneNumberConfirmed, @SecurityStamp, @TwoFactorEnabled, @UserName)) AS Source (ExampleWork, RegistrationDate, LastSignInDate, LastAccessTokenDate, ProfileImageFileId, Id, AccessFailedCount, Email, EmailConfirmed, LockoutEnabled, LockoutEndDateUtc, PasswordHash, PhoneNumber, PhoneNumberConfirmed, SecurityStamp, TwoFactorEnabled, UserName)
                ON    (");
                
            if (mergeOnFields == FifthweekUser.Fields.Empty)
            {
                sql.Append(@"Target.Id = Source.Id");
            }
            else
            {
                sql.AppendMergeOnParameters(GetFieldNames(mergeOnFields, false));
            }
                
            sql.Append(@")
                WHEN MATCHED THEN
                    UPDATE
                    SET        ");
            sql.AppendUpdateParameters(GetFieldNames(updateFields));
            sql.Append(
                @" WHEN NOT MATCHED THEN
                    INSERT  (ExampleWork, RegistrationDate, LastSignInDate, LastAccessTokenDate, ProfileImageFileId, Id, AccessFailedCount, Email, EmailConfirmed, LockoutEnabled, LockoutEndDateUtc, PasswordHash, PhoneNumber, PhoneNumberConfirmed, SecurityStamp, TwoFactorEnabled, UserName)
                    VALUES  (Source.ExampleWork, Source.RegistrationDate, Source.LastSignInDate, Source.LastAccessTokenDate, Source.ProfileImageFileId, Source.Id, Source.AccessFailedCount, Source.Email, Source.EmailConfirmed, Source.LockoutEnabled, Source.LockoutEndDateUtc, Source.PasswordHash, Source.PhoneNumber, Source.PhoneNumberConfirmed, Source.SecurityStamp, Source.TwoFactorEnabled, Source.UserName);");
            return sql.ToString();
        }
        
        public static string UpdateStatement(FifthweekUser.Fields fields)
        {
            var sql = new System.Text.StringBuilder();
            sql.Append("UPDATE AspNetUsers SET ");
            sql.AppendUpdateParameters(GetFieldNames(fields));
            sql.Append(" WHERE Id = @Id");
            return sql.ToString();
        }
        
        private static System.Collections.Generic.IReadOnlyList<string> GetFieldNames(FifthweekUser.Fields fields, bool autoIncludePrimaryKeys = true)
        {
            var fieldNames = new System.Collections.Generic.List<string>();
            if (fields.HasFlag(FifthweekUser.Fields.ExampleWork))
            {
                fieldNames.Add("ExampleWork");
            }
        
            if (fields.HasFlag(FifthweekUser.Fields.RegistrationDate))
            {
                fieldNames.Add("RegistrationDate");
            }
        
            if (fields.HasFlag(FifthweekUser.Fields.LastSignInDate))
            {
                fieldNames.Add("LastSignInDate");
            }
        
            if (fields.HasFlag(FifthweekUser.Fields.LastAccessTokenDate))
            {
                fieldNames.Add("LastAccessTokenDate");
            }
        
            if (fields.HasFlag(FifthweekUser.Fields.ProfileImageFileId))
            {
                fieldNames.Add("ProfileImageFileId");
            }
        
            if (autoIncludePrimaryKeys)
            {
                fieldNames.Add("Id");
            }
        
            if (fields.HasFlag(FifthweekUser.Fields.AccessFailedCount))
            {
                fieldNames.Add("AccessFailedCount");
            }
        
            if (fields.HasFlag(FifthweekUser.Fields.Email))
            {
                fieldNames.Add("Email");
            }
        
            if (fields.HasFlag(FifthweekUser.Fields.EmailConfirmed))
            {
                fieldNames.Add("EmailConfirmed");
            }
        
            if (fields.HasFlag(FifthweekUser.Fields.LockoutEnabled))
            {
                fieldNames.Add("LockoutEnabled");
            }
        
            if (fields.HasFlag(FifthweekUser.Fields.LockoutEndDateUtc))
            {
                fieldNames.Add("LockoutEndDateUtc");
            }
        
            if (fields.HasFlag(FifthweekUser.Fields.PasswordHash))
            {
                fieldNames.Add("PasswordHash");
            }
        
            if (fields.HasFlag(FifthweekUser.Fields.PhoneNumber))
            {
                fieldNames.Add("PhoneNumber");
            }
        
            if (fields.HasFlag(FifthweekUser.Fields.PhoneNumberConfirmed))
            {
                fieldNames.Add("PhoneNumberConfirmed");
            }
        
            if (fields.HasFlag(FifthweekUser.Fields.SecurityStamp))
            {
                fieldNames.Add("SecurityStamp");
            }
        
            if (fields.HasFlag(FifthweekUser.Fields.TwoFactorEnabled))
            {
                fieldNames.Add("TwoFactorEnabled");
            }
        
            if (fields.HasFlag(FifthweekUser.Fields.UserName))
            {
                fieldNames.Add("UserName");
            }
        
            return fieldNames;
        }
        
        private static Dapper.DynamicParameters OnlySpecifiedParameters(
            FifthweekUser entity, 
            FifthweekUser.Fields fields,
            FifthweekUser.Fields? excludedFields = null)
        {
            var parameters = new Dapper.DynamicParameters();
        
            // Assume we never want to exclude primary key field(s) from our input.
            if (fields.HasFlag(FifthweekUser.Fields.ExampleWork) && (excludedFields == null || !excludedFields.Value.HasFlag(FifthweekUser.Fields.ExampleWork)))
            {
                parameters.Add("ExampleWork", entity.ExampleWork);
            }
        
            if (fields.HasFlag(FifthweekUser.Fields.RegistrationDate) && (excludedFields == null || !excludedFields.Value.HasFlag(FifthweekUser.Fields.RegistrationDate)))
            {
                parameters.Add("RegistrationDate", entity.RegistrationDate);
            }
        
            if (fields.HasFlag(FifthweekUser.Fields.LastSignInDate) && (excludedFields == null || !excludedFields.Value.HasFlag(FifthweekUser.Fields.LastSignInDate)))
            {
                parameters.Add("LastSignInDate", entity.LastSignInDate);
            }
        
            if (fields.HasFlag(FifthweekUser.Fields.LastAccessTokenDate) && (excludedFields == null || !excludedFields.Value.HasFlag(FifthweekUser.Fields.LastAccessTokenDate)))
            {
                parameters.Add("LastAccessTokenDate", entity.LastAccessTokenDate);
            }
        
            if (fields.HasFlag(FifthweekUser.Fields.ProfileImageFileId) && (excludedFields == null || !excludedFields.Value.HasFlag(FifthweekUser.Fields.ProfileImageFileId)))
            {
                parameters.Add("ProfileImageFileId", entity.ProfileImageFileId);
            }
        
            parameters.Add("Id", entity.Id);
            if (fields.HasFlag(FifthweekUser.Fields.AccessFailedCount) && (excludedFields == null || !excludedFields.Value.HasFlag(FifthweekUser.Fields.AccessFailedCount)))
            {
                parameters.Add("AccessFailedCount", entity.AccessFailedCount);
            }
        
            if (fields.HasFlag(FifthweekUser.Fields.Email) && (excludedFields == null || !excludedFields.Value.HasFlag(FifthweekUser.Fields.Email)))
            {
                parameters.Add("Email", entity.Email);
            }
        
            if (fields.HasFlag(FifthweekUser.Fields.EmailConfirmed) && (excludedFields == null || !excludedFields.Value.HasFlag(FifthweekUser.Fields.EmailConfirmed)))
            {
                parameters.Add("EmailConfirmed", entity.EmailConfirmed);
            }
        
            if (fields.HasFlag(FifthweekUser.Fields.LockoutEnabled) && (excludedFields == null || !excludedFields.Value.HasFlag(FifthweekUser.Fields.LockoutEnabled)))
            {
                parameters.Add("LockoutEnabled", entity.LockoutEnabled);
            }
        
            if (fields.HasFlag(FifthweekUser.Fields.LockoutEndDateUtc) && (excludedFields == null || !excludedFields.Value.HasFlag(FifthweekUser.Fields.LockoutEndDateUtc)))
            {
                parameters.Add("LockoutEndDateUtc", entity.LockoutEndDateUtc);
            }
        
            if (fields.HasFlag(FifthweekUser.Fields.PasswordHash) && (excludedFields == null || !excludedFields.Value.HasFlag(FifthweekUser.Fields.PasswordHash)))
            {
                parameters.Add("PasswordHash", entity.PasswordHash);
            }
        
            if (fields.HasFlag(FifthweekUser.Fields.PhoneNumber) && (excludedFields == null || !excludedFields.Value.HasFlag(FifthweekUser.Fields.PhoneNumber)))
            {
                parameters.Add("PhoneNumber", entity.PhoneNumber);
            }
        
            if (fields.HasFlag(FifthweekUser.Fields.PhoneNumberConfirmed) && (excludedFields == null || !excludedFields.Value.HasFlag(FifthweekUser.Fields.PhoneNumberConfirmed)))
            {
                parameters.Add("PhoneNumberConfirmed", entity.PhoneNumberConfirmed);
            }
        
            if (fields.HasFlag(FifthweekUser.Fields.SecurityStamp) && (excludedFields == null || !excludedFields.Value.HasFlag(FifthweekUser.Fields.SecurityStamp)))
            {
                parameters.Add("SecurityStamp", entity.SecurityStamp);
            }
        
            if (fields.HasFlag(FifthweekUser.Fields.TwoFactorEnabled) && (excludedFields == null || !excludedFields.Value.HasFlag(FifthweekUser.Fields.TwoFactorEnabled)))
            {
                parameters.Add("TwoFactorEnabled", entity.TwoFactorEnabled);
            }
        
            if (fields.HasFlag(FifthweekUser.Fields.UserName) && (excludedFields == null || !excludedFields.Value.HasFlag(FifthweekUser.Fields.UserName)))
            {
                parameters.Add("UserName", entity.UserName);
            }
        
            return parameters;
        }
        
        private static Dapper.DynamicParameters AllExceptSpecifiedParameters(
            FifthweekUser entity, 
            FifthweekUser.Fields fields)
        {
            var parameters = new Dapper.DynamicParameters();
        
            // Assume we never want to exclude primary key field(s) from our input.
            if (!fields.HasFlag(FifthweekUser.Fields.ExampleWork))
            {
                parameters.Add("ExampleWork", entity.ExampleWork);
            }
        
            if (!fields.HasFlag(FifthweekUser.Fields.RegistrationDate))
            {
                parameters.Add("RegistrationDate", entity.RegistrationDate);
            }
        
            if (!fields.HasFlag(FifthweekUser.Fields.LastSignInDate))
            {
                parameters.Add("LastSignInDate", entity.LastSignInDate);
            }
        
            if (!fields.HasFlag(FifthweekUser.Fields.LastAccessTokenDate))
            {
                parameters.Add("LastAccessTokenDate", entity.LastAccessTokenDate);
            }
        
            if (!fields.HasFlag(FifthweekUser.Fields.ProfileImageFileId))
            {
                parameters.Add("ProfileImageFileId", entity.ProfileImageFileId);
            }
        
            parameters.Add("Id", entity.Id);
            if (!fields.HasFlag(FifthweekUser.Fields.AccessFailedCount))
            {
                parameters.Add("AccessFailedCount", entity.AccessFailedCount);
            }
        
            if (!fields.HasFlag(FifthweekUser.Fields.Email))
            {
                parameters.Add("Email", entity.Email);
            }
        
            if (!fields.HasFlag(FifthweekUser.Fields.EmailConfirmed))
            {
                parameters.Add("EmailConfirmed", entity.EmailConfirmed);
            }
        
            if (!fields.HasFlag(FifthweekUser.Fields.LockoutEnabled))
            {
                parameters.Add("LockoutEnabled", entity.LockoutEnabled);
            }
        
            if (!fields.HasFlag(FifthweekUser.Fields.LockoutEndDateUtc))
            {
                parameters.Add("LockoutEndDateUtc", entity.LockoutEndDateUtc);
            }
        
            if (!fields.HasFlag(FifthweekUser.Fields.PasswordHash))
            {
                parameters.Add("PasswordHash", entity.PasswordHash);
            }
        
            if (!fields.HasFlag(FifthweekUser.Fields.PhoneNumber))
            {
                parameters.Add("PhoneNumber", entity.PhoneNumber);
            }
        
            if (!fields.HasFlag(FifthweekUser.Fields.PhoneNumberConfirmed))
            {
                parameters.Add("PhoneNumberConfirmed", entity.PhoneNumberConfirmed);
            }
        
            if (!fields.HasFlag(FifthweekUser.Fields.SecurityStamp))
            {
                parameters.Add("SecurityStamp", entity.SecurityStamp);
            }
        
            if (!fields.HasFlag(FifthweekUser.Fields.TwoFactorEnabled))
            {
                parameters.Add("TwoFactorEnabled", entity.TwoFactorEnabled);
            }
        
            if (!fields.HasFlag(FifthweekUser.Fields.UserName))
            {
                parameters.Add("UserName", entity.UserName);
            }
        
            return parameters;
        }
        
    }
}
namespace Fifthweek.Api.Persistence.Identity
{
    using System;
    using System.Linq;
    using System.ComponentModel.DataAnnotations;
    using Fifthweek.CodeGeneration;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Collections.Generic;

    public partial class FifthweekUserRole  : IIdentityEquatable
    {
        public const string Table = "AspNetUserRoles";
        
        public FifthweekUserRole(
            System.Guid roleId,
            System.Guid userId)
        {
            if (roleId == null)
            {
                throw new ArgumentNullException("roleId");
            }

            if (userId == null)
            {
                throw new ArgumentNullException("userId");
            }

            this.RoleId = roleId;
            this.UserId = userId;
        }
        
        public bool IdentityEquals(object other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }
        
            if (ReferenceEquals(this, other))
            {
                return true;
            }
        
            if (other.GetType() != this.GetType())
            {
                return false;
            }
        
            return this.IdentityEquals((FifthweekUserRole)other);
        }
        
        protected bool IdentityEquals(FifthweekUserRole other)
        {
            if (!object.Equals(this.RoleId, other.RoleId))
            {
                return false;
            }
        
            if (!object.Equals(this.UserId, other.UserId))
            {
                return false;
            }
        
            return true;
        }
        
        [Flags]
        public enum Fields
        {
            Empty = 0,
            RoleId = 1, 
            UserId = 2
        }
    }

    public static partial class FifthweekUserRoleExtensions
    {
        public static System.Threading.Tasks.Task InsertAsync(
            this System.Data.Common.DbConnection connection, 
            System.Collections.Generic.IEnumerable<FifthweekUserRole> entities, 
            bool idempotent = true, 
            System.Data.IDbTransaction transaction = null)
        {
            return Dapper.SqlMapper.ExecuteAsync(
                connection, 
                InsertStatement(idempotent), 
                entities.Select(entity => new 
                {
                    entity.RoleId, entity.UserId
                }).ToArray(),
                transaction);
        }
        
        public static System.Threading.Tasks.Task InsertAsync(
            this System.Data.Common.DbConnection connection, 
            FifthweekUserRole entity,
            bool idempotent = true, 
            System.Data.IDbTransaction transaction = null)
        {
            return Dapper.SqlMapper.ExecuteAsync(
                connection, 
                InsertStatement(idempotent), 
                new 
                {
                    entity.RoleId, entity.UserId
                },
                transaction);
        }
        
        public static System.Threading.Tasks.Task<int> InsertAsync(
            this System.Data.Common.DbConnection connection,
            SqlGenerationParameters<FifthweekUserRole, FifthweekUserRole.Fields> parameters)
        {
            var sql = new System.Text.StringBuilder();
        
            if (parameters.Declarations != null)
            {
                sql.AppendLine(parameters.Declarations);
            }
            
            int currentIndex = 0;
            if (parameters.Conditions != null)
            {
                foreach (var condition in parameters.Conditions)
                {
                    sql.Append("IF ");
                    sql.AppendLine(condition); // Remember to use `WITH (UPDLOCK, HOLDLOCK)` in your conditions! See: http://samsaffron.com/blog/archive/2007/04/04/14.aspx
                    sql.AppendLine("BEGIN");
                    ++currentIndex;
                }
            }
        
            sql.AppendLine(InsertStatement(parameters.IdempotentInsert));
        
            if (parameters.Conditions != null)
            {
                sql.AppendLine("SELECT -1 AS FailedConditionIndex"); // Indicates all conditions passed and operation was attempted.
                    
                foreach (var condition in parameters.Conditions)
                {
                    sql.AppendLine("END");
                    sql.AppendLine("ELSE");
                    sql.Append("SELECT ").Append(--currentIndex).AppendLine(" AS FailedConditionIndex");
                }
            }
        
            var entity = parameters.Entity;
            var parameterObject = parameters.ExcludedFromInput != null
                ? AllExceptSpecifiedParameters(entity, parameters.ExcludedFromInput.Value)
                : new Dapper.DynamicParameters(new { entity.RoleId, entity.UserId });
        
            if (parameters.AdditionalParameters != null)
            {
                parameterObject.AddDynamicParams(parameters.AdditionalParameters);
            }
        
            return Dapper.SqlMapper.ExecuteScalarAsync<int>(
                connection,
                sql.ToString(),
                parameterObject);
        }
        
        public static System.Threading.Tasks.Task UpsertAsync(
            this System.Data.Common.DbConnection connection, 
            FifthweekUserRole entity, 
            FifthweekUserRole.Fields fields,
            System.Data.IDbTransaction transaction = null)
        {
            return Dapper.SqlMapper.ExecuteAsync(
                connection, 
                UpsertStatement(FifthweekUserRole.Fields.Empty, fields), 
                new 
                {
                    entity.RoleId, entity.UserId
                },
                transaction);
        }
        
        public static System.Threading.Tasks.Task UpsertAsync(
            this System.Data.Common.DbConnection connection, 
            FifthweekUserRole entity, 
            FifthweekUserRole.Fields mergeOnFields,
            FifthweekUserRole.Fields updateFields,
            System.Data.IDbTransaction transaction = null)
        {
            return Dapper.SqlMapper.ExecuteAsync(
                connection, 
                UpsertStatement(mergeOnFields, updateFields), 
                new 
                {
                    entity.RoleId, entity.UserId
                },
                transaction);
        }
        
        public static System.Threading.Tasks.Task UpdateAsync(
            this System.Data.Common.DbConnection connection, 
            FifthweekUserRole entity, 
            FifthweekUserRole.Fields fields,
            System.Data.IDbTransaction transaction = null)
        {
            return Dapper.SqlMapper.ExecuteAsync(connection, UpdateStatement(fields), OnlySpecifiedParameters(entity, fields), transaction);
        }
        
        public static System.Threading.Tasks.Task<int> UpdateAsync(
            this System.Data.Common.DbConnection connection,
            SqlGenerationParameters<FifthweekUserRole, FifthweekUserRole.Fields> parameters)
        {
            if (parameters.UpdateMask == null)
            {
                throw new ArgumentException("Must contain update mask", "parameters");
            }
        
            var sql = new System.Text.StringBuilder();
        
            if (parameters.Declarations != null)
            {
                sql.AppendLine(parameters.Declarations);
            }
            
            int currentIndex = 0;
            if (parameters.Conditions != null)
            {
                foreach (var condition in parameters.Conditions)
                {
                    sql.Append("IF ");
                    sql.AppendLine(condition); // Remember to use `WITH (UPDLOCK, HOLDLOCK)` in your conditions! See: http://samsaffron.com/blog/archive/2007/04/04/14.aspx
                    sql.AppendLine("BEGIN");
                    ++currentIndex;
                }
            }
        
            sql.AppendLine(UpdateStatement(parameters.UpdateMask.Value));
        
            if (parameters.Conditions != null)
            {
                sql.AppendLine("SELECT -1 AS FailedConditionIndex"); // Indicates all conditions passed and operation was attempted.
                    
                foreach (var condition in parameters.Conditions)
                {
                    sql.AppendLine("END");
                    sql.AppendLine("ELSE");
                    sql.Append("SELECT ").Append(--currentIndex).AppendLine(" AS FailedConditionIndex");
                }
            }
        
            var parameterObject = OnlySpecifiedParameters(parameters.Entity, parameters.UpdateMask.Value, parameters.ExcludedFromInput);
        
            if (parameters.AdditionalParameters != null)
            {
                parameterObject.AddDynamicParams(parameters.AdditionalParameters);
            }
        
            return Dapper.SqlMapper.ExecuteScalarAsync<int>(
                connection,
                sql.ToString(),
                parameterObject);
        }
        
        public static string InsertStatement(bool idempotent = true)
        {
            const string insert = "INSERT INTO AspNetUserRoles(RoleId, UserId) VALUES(@RoleId, @UserId)";
            return idempotent ? SqlStatementTemplates.IdempotentInsert(insert) : insert;
        }
        
        public static string UpsertStatement(FifthweekUserRole.Fields mergeOnFields, FifthweekUserRole.Fields updateFields)
        {
            // HOLDLOCK ensures operation is concurrent by not releasing the U lock on the row after determining
            // it does not exist. See: http://weblogs.sqlteam.com/dang/archive/2009/01/31/UPSERT-Race-Condition-With-MERGE.aspx
            var sql = new System.Text.StringBuilder();
            sql.Append(
                @"MERGE AspNetUserRoles WITH (HOLDLOCK) as Target
                USING (VALUES (@RoleId, @UserId)) AS Source (RoleId, UserId)
                ON    (");
                
            if (mergeOnFields == FifthweekUserRole.Fields.Empty)
            {
                sql.Append(@"Target.RoleId = Source.RoleId AND Target.UserId = Source.UserId");
            }
            else
            {
                sql.AppendMergeOnParameters(GetFieldNames(mergeOnFields, false));
            }
                
            sql.Append(@")
                WHEN MATCHED THEN
                    UPDATE
                    SET        ");
            sql.AppendUpdateParameters(GetFieldNames(updateFields));
            sql.Append(
                @" WHEN NOT MATCHED THEN
                    INSERT  (RoleId, UserId)
                    VALUES  (Source.RoleId, Source.UserId);");
            return sql.ToString();
        }
        
        public static string UpdateStatement(FifthweekUserRole.Fields fields)
        {
            var sql = new System.Text.StringBuilder();
            sql.Append("UPDATE AspNetUserRoles SET ");
            sql.AppendUpdateParameters(GetFieldNames(fields));
            sql.Append(" WHERE RoleId = @RoleId AND UserId = @UserId");
            return sql.ToString();
        }
        
        private static System.Collections.Generic.IReadOnlyList<string> GetFieldNames(FifthweekUserRole.Fields fields, bool autoIncludePrimaryKeys = true)
        {
            var fieldNames = new System.Collections.Generic.List<string>();
            if (autoIncludePrimaryKeys)
            {
                fieldNames.Add("RoleId");
            }
        
            if (autoIncludePrimaryKeys)
            {
                fieldNames.Add("UserId");
            }
        
            return fieldNames;
        }
        
        private static Dapper.DynamicParameters OnlySpecifiedParameters(
            FifthweekUserRole entity, 
            FifthweekUserRole.Fields fields,
            FifthweekUserRole.Fields? excludedFields = null)
        {
            var parameters = new Dapper.DynamicParameters();
        
            // Assume we never want to exclude primary key field(s) from our input.
            parameters.Add("RoleId", entity.RoleId);
            parameters.Add("UserId", entity.UserId);
            return parameters;
        }
        
        private static Dapper.DynamicParameters AllExceptSpecifiedParameters(
            FifthweekUserRole entity, 
            FifthweekUserRole.Fields fields)
        {
            var parameters = new Dapper.DynamicParameters();
        
            // Assume we never want to exclude primary key field(s) from our input.
            parameters.Add("RoleId", entity.RoleId);
            parameters.Add("UserId", entity.UserId);
            return parameters;
        }
        
    }
}
namespace Fifthweek.Api.Persistence
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Fifthweek.CodeGeneration;

    public partial class EndToEndTestEmail  : IIdentityEquatable
    {
        public const string Table = "EndToEndTestEmails";
        
        public EndToEndTestEmail(
            System.String mailbox)
        {
            if (mailbox == null)
            {
                throw new ArgumentNullException("mailbox");
            }

            this.Mailbox = mailbox;
        }
        
        public bool IdentityEquals(object other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }
        
            if (ReferenceEquals(this, other))
            {
                return true;
            }
        
            if (other.GetType() != this.GetType())
            {
                return false;
            }
        
            return this.IdentityEquals((EndToEndTestEmail)other);
        }
        
        protected bool IdentityEquals(EndToEndTestEmail other)
        {
            if (!object.Equals(this.Mailbox, other.Mailbox))
            {
                return false;
            }
        
            return true;
        }
        
        [Flags]
        public enum Fields
        {
            Empty = 0,
            Mailbox = 1, 
            Subject = 2, 
            Body = 4, 
            DateReceived = 8
        }
    }

    public static partial class EndToEndTestEmailExtensions
    {
        public static System.Threading.Tasks.Task InsertAsync(
            this System.Data.Common.DbConnection connection, 
            System.Collections.Generic.IEnumerable<EndToEndTestEmail> entities, 
            bool idempotent = true, 
            System.Data.IDbTransaction transaction = null)
        {
            return Dapper.SqlMapper.ExecuteAsync(
                connection, 
                InsertStatement(idempotent), 
                entities.Select(entity => new 
                {
                    entity.Mailbox, entity.Subject, entity.Body, entity.DateReceived
                }).ToArray(),
                transaction);
        }
        
        public static System.Threading.Tasks.Task InsertAsync(
            this System.Data.Common.DbConnection connection, 
            EndToEndTestEmail entity,
            bool idempotent = true, 
            System.Data.IDbTransaction transaction = null)
        {
            return Dapper.SqlMapper.ExecuteAsync(
                connection, 
                InsertStatement(idempotent), 
                new 
                {
                    entity.Mailbox, entity.Subject, entity.Body, entity.DateReceived
                },
                transaction);
        }
        
        public static System.Threading.Tasks.Task<int> InsertAsync(
            this System.Data.Common.DbConnection connection,
            SqlGenerationParameters<EndToEndTestEmail, EndToEndTestEmail.Fields> parameters)
        {
            var sql = new System.Text.StringBuilder();
        
            if (parameters.Declarations != null)
            {
                sql.AppendLine(parameters.Declarations);
            }
            
            int currentIndex = 0;
            if (parameters.Conditions != null)
            {
                foreach (var condition in parameters.Conditions)
                {
                    sql.Append("IF ");
                    sql.AppendLine(condition); // Remember to use `WITH (UPDLOCK, HOLDLOCK)` in your conditions! See: http://samsaffron.com/blog/archive/2007/04/04/14.aspx
                    sql.AppendLine("BEGIN");
                    ++currentIndex;
                }
            }
        
            sql.AppendLine(InsertStatement(parameters.IdempotentInsert));
        
            if (parameters.Conditions != null)
            {
                sql.AppendLine("SELECT -1 AS FailedConditionIndex"); // Indicates all conditions passed and operation was attempted.
                    
                foreach (var condition in parameters.Conditions)
                {
                    sql.AppendLine("END");
                    sql.AppendLine("ELSE");
                    sql.Append("SELECT ").Append(--currentIndex).AppendLine(" AS FailedConditionIndex");
                }
            }
        
            var entity = parameters.Entity;
            var parameterObject = parameters.ExcludedFromInput != null
                ? AllExceptSpecifiedParameters(entity, parameters.ExcludedFromInput.Value)
                : new Dapper.DynamicParameters(new { entity.Mailbox, entity.Subject, entity.Body, entity.DateReceived });
        
            if (parameters.AdditionalParameters != null)
            {
                parameterObject.AddDynamicParams(parameters.AdditionalParameters);
            }
        
            return Dapper.SqlMapper.ExecuteScalarAsync<int>(
                connection,
                sql.ToString(),
                parameterObject);
        }
        
        public static System.Threading.Tasks.Task UpsertAsync(
            this System.Data.Common.DbConnection connection, 
            EndToEndTestEmail entity, 
            EndToEndTestEmail.Fields fields,
            System.Data.IDbTransaction transaction = null)
        {
            return Dapper.SqlMapper.ExecuteAsync(
                connection, 
                UpsertStatement(EndToEndTestEmail.Fields.Empty, fields), 
                new 
                {
                    entity.Mailbox, entity.Subject, entity.Body, entity.DateReceived
                },
                transaction);
        }
        
        public static System.Threading.Tasks.Task UpsertAsync(
            this System.Data.Common.DbConnection connection, 
            EndToEndTestEmail entity, 
            EndToEndTestEmail.Fields mergeOnFields,
            EndToEndTestEmail.Fields updateFields,
            System.Data.IDbTransaction transaction = null)
        {
            return Dapper.SqlMapper.ExecuteAsync(
                connection, 
                UpsertStatement(mergeOnFields, updateFields), 
                new 
                {
                    entity.Mailbox, entity.Subject, entity.Body, entity.DateReceived
                },
                transaction);
        }
        
        public static System.Threading.Tasks.Task UpdateAsync(
            this System.Data.Common.DbConnection connection, 
            EndToEndTestEmail entity, 
            EndToEndTestEmail.Fields fields,
            System.Data.IDbTransaction transaction = null)
        {
            return Dapper.SqlMapper.ExecuteAsync(connection, UpdateStatement(fields), OnlySpecifiedParameters(entity, fields), transaction);
        }
        
        public static System.Threading.Tasks.Task<int> UpdateAsync(
            this System.Data.Common.DbConnection connection,
            SqlGenerationParameters<EndToEndTestEmail, EndToEndTestEmail.Fields> parameters)
        {
            if (parameters.UpdateMask == null)
            {
                throw new ArgumentException("Must contain update mask", "parameters");
            }
        
            var sql = new System.Text.StringBuilder();
        
            if (parameters.Declarations != null)
            {
                sql.AppendLine(parameters.Declarations);
            }
            
            int currentIndex = 0;
            if (parameters.Conditions != null)
            {
                foreach (var condition in parameters.Conditions)
                {
                    sql.Append("IF ");
                    sql.AppendLine(condition); // Remember to use `WITH (UPDLOCK, HOLDLOCK)` in your conditions! See: http://samsaffron.com/blog/archive/2007/04/04/14.aspx
                    sql.AppendLine("BEGIN");
                    ++currentIndex;
                }
            }
        
            sql.AppendLine(UpdateStatement(parameters.UpdateMask.Value));
        
            if (parameters.Conditions != null)
            {
                sql.AppendLine("SELECT -1 AS FailedConditionIndex"); // Indicates all conditions passed and operation was attempted.
                    
                foreach (var condition in parameters.Conditions)
                {
                    sql.AppendLine("END");
                    sql.AppendLine("ELSE");
                    sql.Append("SELECT ").Append(--currentIndex).AppendLine(" AS FailedConditionIndex");
                }
            }
        
            var parameterObject = OnlySpecifiedParameters(parameters.Entity, parameters.UpdateMask.Value, parameters.ExcludedFromInput);
        
            if (parameters.AdditionalParameters != null)
            {
                parameterObject.AddDynamicParams(parameters.AdditionalParameters);
            }
        
            return Dapper.SqlMapper.ExecuteScalarAsync<int>(
                connection,
                sql.ToString(),
                parameterObject);
        }
        
        public static string InsertStatement(bool idempotent = true)
        {
            const string insert = "INSERT INTO EndToEndTestEmails(Mailbox, Subject, Body, DateReceived) VALUES(@Mailbox, @Subject, @Body, @DateReceived)";
            return idempotent ? SqlStatementTemplates.IdempotentInsert(insert) : insert;
        }
        
        public static string UpsertStatement(EndToEndTestEmail.Fields mergeOnFields, EndToEndTestEmail.Fields updateFields)
        {
            // HOLDLOCK ensures operation is concurrent by not releasing the U lock on the row after determining
            // it does not exist. See: http://weblogs.sqlteam.com/dang/archive/2009/01/31/UPSERT-Race-Condition-With-MERGE.aspx
            var sql = new System.Text.StringBuilder();
            sql.Append(
                @"MERGE EndToEndTestEmails WITH (HOLDLOCK) as Target
                USING (VALUES (@Mailbox, @Subject, @Body, @DateReceived)) AS Source (Mailbox, Subject, Body, DateReceived)
                ON    (");
                
            if (mergeOnFields == EndToEndTestEmail.Fields.Empty)
            {
                sql.Append(@"Target.Mailbox = Source.Mailbox");
            }
            else
            {
                sql.AppendMergeOnParameters(GetFieldNames(mergeOnFields, false));
            }
                
            sql.Append(@")
                WHEN MATCHED THEN
                    UPDATE
                    SET        ");
            sql.AppendUpdateParameters(GetFieldNames(updateFields));
            sql.Append(
                @" WHEN NOT MATCHED THEN
                    INSERT  (Mailbox, Subject, Body, DateReceived)
                    VALUES  (Source.Mailbox, Source.Subject, Source.Body, Source.DateReceived);");
            return sql.ToString();
        }
        
        public static string UpdateStatement(EndToEndTestEmail.Fields fields)
        {
            var sql = new System.Text.StringBuilder();
            sql.Append("UPDATE EndToEndTestEmails SET ");
            sql.AppendUpdateParameters(GetFieldNames(fields));
            sql.Append(" WHERE Mailbox = @Mailbox");
            return sql.ToString();
        }
        
        private static System.Collections.Generic.IReadOnlyList<string> GetFieldNames(EndToEndTestEmail.Fields fields, bool autoIncludePrimaryKeys = true)
        {
            var fieldNames = new System.Collections.Generic.List<string>();
            if (autoIncludePrimaryKeys)
            {
                fieldNames.Add("Mailbox");
            }
        
            if (fields.HasFlag(EndToEndTestEmail.Fields.Subject))
            {
                fieldNames.Add("Subject");
            }
        
            if (fields.HasFlag(EndToEndTestEmail.Fields.Body))
            {
                fieldNames.Add("Body");
            }
        
            if (fields.HasFlag(EndToEndTestEmail.Fields.DateReceived))
            {
                fieldNames.Add("DateReceived");
            }
        
            return fieldNames;
        }
        
        private static Dapper.DynamicParameters OnlySpecifiedParameters(
            EndToEndTestEmail entity, 
            EndToEndTestEmail.Fields fields,
            EndToEndTestEmail.Fields? excludedFields = null)
        {
            var parameters = new Dapper.DynamicParameters();
        
            // Assume we never want to exclude primary key field(s) from our input.
            parameters.Add("Mailbox", entity.Mailbox);
            if (fields.HasFlag(EndToEndTestEmail.Fields.Subject) && (excludedFields == null || !excludedFields.Value.HasFlag(EndToEndTestEmail.Fields.Subject)))
            {
                parameters.Add("Subject", entity.Subject);
            }
        
            if (fields.HasFlag(EndToEndTestEmail.Fields.Body) && (excludedFields == null || !excludedFields.Value.HasFlag(EndToEndTestEmail.Fields.Body)))
            {
                parameters.Add("Body", entity.Body);
            }
        
            if (fields.HasFlag(EndToEndTestEmail.Fields.DateReceived) && (excludedFields == null || !excludedFields.Value.HasFlag(EndToEndTestEmail.Fields.DateReceived)))
            {
                parameters.Add("DateReceived", entity.DateReceived);
            }
        
            return parameters;
        }
        
        private static Dapper.DynamicParameters AllExceptSpecifiedParameters(
            EndToEndTestEmail entity, 
            EndToEndTestEmail.Fields fields)
        {
            var parameters = new Dapper.DynamicParameters();
        
            // Assume we never want to exclude primary key field(s) from our input.
            parameters.Add("Mailbox", entity.Mailbox);
            if (!fields.HasFlag(EndToEndTestEmail.Fields.Subject))
            {
                parameters.Add("Subject", entity.Subject);
            }
        
            if (!fields.HasFlag(EndToEndTestEmail.Fields.Body))
            {
                parameters.Add("Body", entity.Body);
            }
        
            if (!fields.HasFlag(EndToEndTestEmail.Fields.DateReceived))
            {
                parameters.Add("DateReceived", entity.DateReceived);
            }
        
            return parameters;
        }
        
    }
}

