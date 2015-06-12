using System;
using System.Linq;

//// Generated on 12/06/2015 11:03:07 (UTC)
//// Mapped solution in 10.57s


namespace Fifthweek.Api.Persistence
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.CodeGeneration;
    using System.Linq;

    public partial class Blog 
    {
        public Blog(
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
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Fifthweek.CodeGeneration;
    using System.Linq;
    using Fifthweek.Api.Persistence.Identity;

    public partial class Channel 
    {
        public Channel(
            System.Guid id,
            System.Guid blogId,
            Fifthweek.Api.Persistence.Blog blog,
            System.String name,
            System.String description,
            System.Int32 priceInUsCentsPerWeek,
            System.Boolean isVisibleToNonSubscribers,
            System.DateTime creationDate,
            System.DateTime priceLastSetDate)
        {
            if (id == null)
            {
                throw new ArgumentNullException("id");
            }

            if (blogId == null)
            {
                throw new ArgumentNullException("blogId");
            }

            if (name == null)
            {
                throw new ArgumentNullException("name");
            }

            if (description == null)
            {
                throw new ArgumentNullException("description");
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

            if (priceLastSetDate == null)
            {
                throw new ArgumentNullException("priceLastSetDate");
            }

            this.Id = id;
            this.BlogId = blogId;
            this.Blog = blog;
            this.Name = name;
            this.Description = description;
            this.PriceInUsCentsPerWeek = priceInUsCentsPerWeek;
            this.IsVisibleToNonSubscribers = isVisibleToNonSubscribers;
            this.CreationDate = creationDate;
            this.PriceLastSetDate = priceLastSetDate;
        }
    }
}
namespace Fifthweek.Api.Persistence
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.CodeGeneration;
    using System.Linq;

    public partial class ChannelSubscription 
    {
        public ChannelSubscription(
            System.Guid channelId,
            Fifthweek.Api.Persistence.Channel channel,
            System.Guid userId,
            Fifthweek.Api.Persistence.Identity.FifthweekUser user,
            System.Int32 acceptedPriceInUsCentsPerWeek,
            System.DateTime priceLastAcceptedDate,
            System.DateTime subscriptionStartDate)
        {
            if (channelId == null)
            {
                throw new ArgumentNullException("channelId");
            }

            if (userId == null)
            {
                throw new ArgumentNullException("userId");
            }

            if (acceptedPriceInUsCentsPerWeek == null)
            {
                throw new ArgumentNullException("acceptedPriceInUsCentsPerWeek");
            }

            if (priceLastAcceptedDate == null)
            {
                throw new ArgumentNullException("priceLastAcceptedDate");
            }

            if (subscriptionStartDate == null)
            {
                throw new ArgumentNullException("subscriptionStartDate");
            }

            this.ChannelId = channelId;
            this.Channel = channel;
            this.UserId = userId;
            this.User = user;
            this.AcceptedPriceInUsCentsPerWeek = acceptedPriceInUsCentsPerWeek;
            this.PriceLastAcceptedDate = priceLastAcceptedDate;
            this.SubscriptionStartDate = subscriptionStartDate;
        }
    }
}
namespace Fifthweek.Api.Persistence
{
    using System;
    using System.Linq;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.CodeGeneration;

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
    using Fifthweek.Api.Persistence.Identity;
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
    using System.Linq;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.CodeGeneration;

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
            System.Nullable<System.Int32> processingAttempts,
            System.String fileNameWithoutExtension,
            System.String fileExtension,
            System.Int64 blobSizeBytes,
            System.String purpose,
            System.Nullable<System.Int32> renderWidth,
            System.Nullable<System.Int32> renderHeight)
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
            this.ProcessingAttempts = processingAttempts;
            this.FileNameWithoutExtension = fileNameWithoutExtension;
            this.FileExtension = fileExtension;
            this.BlobSizeBytes = blobSizeBytes;
            this.Purpose = purpose;
            this.RenderWidth = renderWidth;
            this.RenderHeight = renderHeight;
        }
    }
}
namespace Fifthweek.Api.Persistence
{
    using System;
    using System.Linq;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.CodeGeneration;

    public partial class FreeAccessUser 
    {
        public FreeAccessUser(
            System.Guid blogId,
            Fifthweek.Api.Persistence.Blog blog,
            System.String email)
        {
            if (blogId == null)
            {
                throw new ArgumentNullException("blogId");
            }

            if (email == null)
            {
                throw new ArgumentNullException("email");
            }

            this.BlogId = blogId;
            this.Blog = blog;
            this.Email = email;
        }
    }
}
namespace Fifthweek.Api.Persistence
{
    using System;
    using System.Linq;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.CodeGeneration;

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
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.CodeGeneration;

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
namespace Fifthweek.Api.Persistence.Snapshots
{
    using System;
    using System.Linq;
    using System.ComponentModel.DataAnnotations;
    using Fifthweek.CodeGeneration;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class CreatorChannelsSnapshot 
    {
        public CreatorChannelsSnapshot(
            System.Guid id,
            System.DateTime timestamp,
            System.Guid creatorId)
        {
            if (id == null)
            {
                throw new ArgumentNullException("id");
            }

            if (timestamp == null)
            {
                throw new ArgumentNullException("timestamp");
            }

            if (creatorId == null)
            {
                throw new ArgumentNullException("creatorId");
            }

            this.Id = id;
            this.Timestamp = timestamp;
            this.CreatorId = creatorId;
        }
    }
}
namespace Fifthweek.Api.Persistence.Snapshots
{
    using System;
    using System.Linq;
    using System.ComponentModel.DataAnnotations;
    using Fifthweek.CodeGeneration;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class CreatorChannelsSnapshotItem 
    {
        public CreatorChannelsSnapshotItem(
            System.Guid creatorChannelsSnapshotId,
            Fifthweek.Api.Persistence.Snapshots.CreatorChannelsSnapshot creatorChannelsSnapshot,
            System.Guid channelId,
            System.Int32 priceInUsCentsPerWeek)
        {
            if (creatorChannelsSnapshotId == null)
            {
                throw new ArgumentNullException("creatorChannelsSnapshotId");
            }

            if (channelId == null)
            {
                throw new ArgumentNullException("channelId");
            }

            if (priceInUsCentsPerWeek == null)
            {
                throw new ArgumentNullException("priceInUsCentsPerWeek");
            }

            this.CreatorChannelsSnapshotId = creatorChannelsSnapshotId;
            this.CreatorChannelsSnapshot = creatorChannelsSnapshot;
            this.ChannelId = channelId;
            this.PriceInUsCentsPerWeek = priceInUsCentsPerWeek;
        }
    }
}
namespace Fifthweek.Api.Persistence.Snapshots
{
    using System;
    using System.Linq;
    using System.ComponentModel.DataAnnotations;
    using Fifthweek.CodeGeneration;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class CreatorFreeAccessUsersSnapshot 
    {
        public CreatorFreeAccessUsersSnapshot(
            System.Guid id,
            System.DateTime timestamp,
            System.Guid creatorId)
        {
            if (id == null)
            {
                throw new ArgumentNullException("id");
            }

            if (timestamp == null)
            {
                throw new ArgumentNullException("timestamp");
            }

            if (creatorId == null)
            {
                throw new ArgumentNullException("creatorId");
            }

            this.Id = id;
            this.Timestamp = timestamp;
            this.CreatorId = creatorId;
        }
    }
}
namespace Fifthweek.Api.Persistence.Snapshots
{
    using System;
    using System.Linq;
    using System.ComponentModel.DataAnnotations;
    using Fifthweek.CodeGeneration;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class CreatorFreeAccessUsersSnapshotItem 
    {
        public CreatorFreeAccessUsersSnapshotItem(
            System.Guid creatorFreeAccessUsersSnapshotId,
            Fifthweek.Api.Persistence.Snapshots.CreatorFreeAccessUsersSnapshot creatorFreeAccessUsersSnapshot,
            System.String email)
        {
            if (creatorFreeAccessUsersSnapshotId == null)
            {
                throw new ArgumentNullException("creatorFreeAccessUsersSnapshotId");
            }

            if (email == null)
            {
                throw new ArgumentNullException("email");
            }

            this.CreatorFreeAccessUsersSnapshotId = creatorFreeAccessUsersSnapshotId;
            this.CreatorFreeAccessUsersSnapshot = creatorFreeAccessUsersSnapshot;
            this.Email = email;
        }
    }
}
namespace Fifthweek.Api.Persistence.Snapshots
{
    using System;
    using System.Linq;
    using System.ComponentModel.DataAnnotations;
    using Fifthweek.CodeGeneration;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class SubscriberChannelsSnapshot 
    {
        public SubscriberChannelsSnapshot(
            System.Guid id,
            System.DateTime timestamp,
            System.Guid subscriberId)
        {
            if (id == null)
            {
                throw new ArgumentNullException("id");
            }

            if (timestamp == null)
            {
                throw new ArgumentNullException("timestamp");
            }

            if (subscriberId == null)
            {
                throw new ArgumentNullException("subscriberId");
            }

            this.Id = id;
            this.Timestamp = timestamp;
            this.SubscriberId = subscriberId;
        }
    }
}
namespace Fifthweek.Api.Persistence.Snapshots
{
    using System;
    using System.Linq;
    using System.ComponentModel.DataAnnotations;
    using Fifthweek.CodeGeneration;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class SubscriberChannelsSnapshotItem 
    {
        public SubscriberChannelsSnapshotItem(
            System.Guid subscriberChannelsSnapshotId,
            Fifthweek.Api.Persistence.Snapshots.SubscriberChannelsSnapshot subscriberChannelsSnapshot,
            System.Guid channelId,
            System.Int32 acceptedPriceInUsCentsPerWeek,
            System.DateTime subscriptionStartDate)
        {
            if (subscriberChannelsSnapshotId == null)
            {
                throw new ArgumentNullException("subscriberChannelsSnapshotId");
            }

            if (channelId == null)
            {
                throw new ArgumentNullException("channelId");
            }

            if (acceptedPriceInUsCentsPerWeek == null)
            {
                throw new ArgumentNullException("acceptedPriceInUsCentsPerWeek");
            }

            if (subscriptionStartDate == null)
            {
                throw new ArgumentNullException("subscriptionStartDate");
            }

            this.SubscriberChannelsSnapshotId = subscriberChannelsSnapshotId;
            this.SubscriberChannelsSnapshot = subscriberChannelsSnapshot;
            this.ChannelId = channelId;
            this.AcceptedPriceInUsCentsPerWeek = acceptedPriceInUsCentsPerWeek;
            this.SubscriptionStartDate = subscriptionStartDate;
        }
    }
}
namespace Fifthweek.Api.Persistence.Snapshots
{
    using System;
    using System.Linq;
    using System.ComponentModel.DataAnnotations;
    using Fifthweek.CodeGeneration;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class SubscriberSnapshot 
    {
        public SubscriberSnapshot(
            System.DateTime timestamp,
            System.Guid subscriberId,
            System.String email)
        {
            if (timestamp == null)
            {
                throw new ArgumentNullException("timestamp");
            }

            if (subscriberId == null)
            {
                throw new ArgumentNullException("subscriberId");
            }

            this.Timestamp = timestamp;
            this.SubscriberId = subscriberId;
            this.Email = email;
        }
    }
}
namespace Fifthweek.Api.Persistence
{
    using System;
    using System.Linq;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.CodeGeneration;

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
    using System.ComponentModel.DataAnnotations.Schema;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.CodeGeneration;
    using System.Linq;

    public partial class Blog 
    {
        public override string ToString()
        {
            return string.Format("Blog({0}, {1}, \"{2}\", \"{3}\", \"{4}\", \"{5}\", \"{6}\", {7}, {8})", this.Id == null ? "null" : this.Id.ToString(), this.CreatorId == null ? "null" : this.CreatorId.ToString(), this.Name == null ? "null" : this.Name.ToString(), this.Tagline == null ? "null" : this.Tagline.ToString(), this.Introduction == null ? "null" : this.Introduction.ToString(), this.Description == null ? "null" : this.Description.ToString(), this.ExternalVideoUrl == null ? "null" : this.ExternalVideoUrl.ToString(), this.HeaderImageFileId == null ? "null" : this.HeaderImageFileId.ToString(), this.CreationDate == null ? "null" : this.CreationDate.ToString());
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
        
            return this.Equals((Blog)obj);
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
        
        protected bool Equals(Blog other)
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
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Fifthweek.CodeGeneration;
    using System.Linq;
    using Fifthweek.Api.Persistence.Identity;

    public partial class Channel 
    {
        public override string ToString()
        {
            return string.Format("Channel({0}, {1}, \"{2}\", \"{3}\", {4}, {5}, {6}, {7})", this.Id == null ? "null" : this.Id.ToString(), this.BlogId == null ? "null" : this.BlogId.ToString(), this.Name == null ? "null" : this.Name.ToString(), this.Description == null ? "null" : this.Description.ToString(), this.PriceInUsCentsPerWeek == null ? "null" : this.PriceInUsCentsPerWeek.ToString(), this.IsVisibleToNonSubscribers == null ? "null" : this.IsVisibleToNonSubscribers.ToString(), this.CreationDate == null ? "null" : this.CreationDate.ToString(), this.PriceLastSetDate == null ? "null" : this.PriceLastSetDate.ToString());
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
                hashCode = (hashCode * 397) ^ (this.BlogId != null ? this.BlogId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Name != null ? this.Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Description != null ? this.Description.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.PriceInUsCentsPerWeek != null ? this.PriceInUsCentsPerWeek.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.IsVisibleToNonSubscribers != null ? this.IsVisibleToNonSubscribers.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.CreationDate != null ? this.CreationDate.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.PriceLastSetDate != null ? this.PriceLastSetDate.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(Channel other)
        {
            if (!object.Equals(this.Id, other.Id))
            {
                return false;
            }
        
            if (!object.Equals(this.BlogId, other.BlogId))
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
        
            if (!object.Equals(this.PriceLastSetDate, other.PriceLastSetDate))
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
    using System.ComponentModel.DataAnnotations.Schema;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.CodeGeneration;
    using System.Linq;

    public partial class ChannelSubscription 
    {
        public override string ToString()
        {
            return string.Format("ChannelSubscription({0}, {1}, {2}, {3}, {4})", this.ChannelId == null ? "null" : this.ChannelId.ToString(), this.UserId == null ? "null" : this.UserId.ToString(), this.AcceptedPriceInUsCentsPerWeek == null ? "null" : this.AcceptedPriceInUsCentsPerWeek.ToString(), this.PriceLastAcceptedDate == null ? "null" : this.PriceLastAcceptedDate.ToString(), this.SubscriptionStartDate == null ? "null" : this.SubscriptionStartDate.ToString());
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
        
            return this.Equals((ChannelSubscription)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.ChannelId != null ? this.ChannelId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.UserId != null ? this.UserId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.AcceptedPriceInUsCentsPerWeek != null ? this.AcceptedPriceInUsCentsPerWeek.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.PriceLastAcceptedDate != null ? this.PriceLastAcceptedDate.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.SubscriptionStartDate != null ? this.SubscriptionStartDate.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(ChannelSubscription other)
        {
            if (!object.Equals(this.ChannelId, other.ChannelId))
            {
                return false;
            }
        
            if (!object.Equals(this.UserId, other.UserId))
            {
                return false;
            }
        
            if (!object.Equals(this.AcceptedPriceInUsCentsPerWeek, other.AcceptedPriceInUsCentsPerWeek))
            {
                return false;
            }
        
            if (!object.Equals(this.PriceLastAcceptedDate, other.PriceLastAcceptedDate))
            {
                return false;
            }
        
            if (!object.Equals(this.SubscriptionStartDate, other.SubscriptionStartDate))
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
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.CodeGeneration;

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
    using Fifthweek.Api.Persistence.Identity;
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
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.CodeGeneration;

    public partial class File 
    {
        public override string ToString()
        {
            return string.Format("File({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, \"{8}\", \"{9}\", {10}, \"{11}\", {12}, {13})", this.Id == null ? "null" : this.Id.ToString(), this.UserId == null ? "null" : this.UserId.ToString(), this.State == null ? "null" : this.State.ToString(), this.UploadStartedDate == null ? "null" : this.UploadStartedDate.ToString(), this.UploadCompletedDate == null ? "null" : this.UploadCompletedDate.ToString(), this.ProcessingStartedDate == null ? "null" : this.ProcessingStartedDate.ToString(), this.ProcessingCompletedDate == null ? "null" : this.ProcessingCompletedDate.ToString(), this.ProcessingAttempts == null ? "null" : this.ProcessingAttempts.ToString(), this.FileNameWithoutExtension == null ? "null" : this.FileNameWithoutExtension.ToString(), this.FileExtension == null ? "null" : this.FileExtension.ToString(), this.BlobSizeBytes == null ? "null" : this.BlobSizeBytes.ToString(), this.Purpose == null ? "null" : this.Purpose.ToString(), this.RenderWidth == null ? "null" : this.RenderWidth.ToString(), this.RenderHeight == null ? "null" : this.RenderHeight.ToString());
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
                hashCode = (hashCode * 397) ^ (this.ProcessingAttempts != null ? this.ProcessingAttempts.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.FileNameWithoutExtension != null ? this.FileNameWithoutExtension.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.FileExtension != null ? this.FileExtension.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.BlobSizeBytes != null ? this.BlobSizeBytes.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Purpose != null ? this.Purpose.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.RenderWidth != null ? this.RenderWidth.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.RenderHeight != null ? this.RenderHeight.GetHashCode() : 0);
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
        
            if (!object.Equals(this.ProcessingAttempts, other.ProcessingAttempts))
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
        
            if (!object.Equals(this.RenderWidth, other.RenderWidth))
            {
                return false;
            }
        
            if (!object.Equals(this.RenderHeight, other.RenderHeight))
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
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.CodeGeneration;

    public partial class FreeAccessUser 
    {
        public override string ToString()
        {
            return string.Format("FreeAccessUser({0}, \"{1}\")", this.BlogId == null ? "null" : this.BlogId.ToString(), this.Email == null ? "null" : this.Email.ToString());
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
        
            return this.Equals((FreeAccessUser)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.BlogId != null ? this.BlogId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Email != null ? this.Email.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(FreeAccessUser other)
        {
            if (!object.Equals(this.BlogId, other.BlogId))
            {
                return false;
            }
        
            if (!object.Equals(this.Email, other.Email))
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
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.CodeGeneration;

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
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.CodeGeneration;

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
namespace Fifthweek.Api.Persistence.Snapshots
{
    using System;
    using System.Linq;
    using System.ComponentModel.DataAnnotations;
    using Fifthweek.CodeGeneration;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class CreatorChannelsSnapshot 
    {
        public override string ToString()
        {
            return string.Format("CreatorChannelsSnapshot({0}, {1}, {2})", this.Id == null ? "null" : this.Id.ToString(), this.Timestamp == null ? "null" : this.Timestamp.ToString(), this.CreatorId == null ? "null" : this.CreatorId.ToString());
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
        
            return this.Equals((CreatorChannelsSnapshot)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Id != null ? this.Id.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Timestamp != null ? this.Timestamp.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.CreatorId != null ? this.CreatorId.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(CreatorChannelsSnapshot other)
        {
            if (!object.Equals(this.Id, other.Id))
            {
                return false;
            }
        
            if (!object.Equals(this.Timestamp, other.Timestamp))
            {
                return false;
            }
        
            if (!object.Equals(this.CreatorId, other.CreatorId))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Api.Persistence.Snapshots
{
    using System;
    using System.Linq;
    using System.ComponentModel.DataAnnotations;
    using Fifthweek.CodeGeneration;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class CreatorChannelsSnapshotItem 
    {
        public override string ToString()
        {
            return string.Format("CreatorChannelsSnapshotItem({0}, {1}, {2})", this.CreatorChannelsSnapshotId == null ? "null" : this.CreatorChannelsSnapshotId.ToString(), this.ChannelId == null ? "null" : this.ChannelId.ToString(), this.PriceInUsCentsPerWeek == null ? "null" : this.PriceInUsCentsPerWeek.ToString());
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
        
            return this.Equals((CreatorChannelsSnapshotItem)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.CreatorChannelsSnapshotId != null ? this.CreatorChannelsSnapshotId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.ChannelId != null ? this.ChannelId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.PriceInUsCentsPerWeek != null ? this.PriceInUsCentsPerWeek.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(CreatorChannelsSnapshotItem other)
        {
            if (!object.Equals(this.CreatorChannelsSnapshotId, other.CreatorChannelsSnapshotId))
            {
                return false;
            }
        
            if (!object.Equals(this.ChannelId, other.ChannelId))
            {
                return false;
            }
        
            if (!object.Equals(this.PriceInUsCentsPerWeek, other.PriceInUsCentsPerWeek))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Api.Persistence.Snapshots
{
    using System;
    using System.Linq;
    using System.ComponentModel.DataAnnotations;
    using Fifthweek.CodeGeneration;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class CreatorFreeAccessUsersSnapshot 
    {
        public override string ToString()
        {
            return string.Format("CreatorFreeAccessUsersSnapshot({0}, {1}, {2})", this.Id == null ? "null" : this.Id.ToString(), this.Timestamp == null ? "null" : this.Timestamp.ToString(), this.CreatorId == null ? "null" : this.CreatorId.ToString());
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
        
            return this.Equals((CreatorFreeAccessUsersSnapshot)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Id != null ? this.Id.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Timestamp != null ? this.Timestamp.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.CreatorId != null ? this.CreatorId.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(CreatorFreeAccessUsersSnapshot other)
        {
            if (!object.Equals(this.Id, other.Id))
            {
                return false;
            }
        
            if (!object.Equals(this.Timestamp, other.Timestamp))
            {
                return false;
            }
        
            if (!object.Equals(this.CreatorId, other.CreatorId))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Api.Persistence.Snapshots
{
    using System;
    using System.Linq;
    using System.ComponentModel.DataAnnotations;
    using Fifthweek.CodeGeneration;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class CreatorFreeAccessUsersSnapshotItem 
    {
        public override string ToString()
        {
            return string.Format("CreatorFreeAccessUsersSnapshotItem({0}, \"{1}\")", this.CreatorFreeAccessUsersSnapshotId == null ? "null" : this.CreatorFreeAccessUsersSnapshotId.ToString(), this.Email == null ? "null" : this.Email.ToString());
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
        
            return this.Equals((CreatorFreeAccessUsersSnapshotItem)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.CreatorFreeAccessUsersSnapshotId != null ? this.CreatorFreeAccessUsersSnapshotId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Email != null ? this.Email.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(CreatorFreeAccessUsersSnapshotItem other)
        {
            if (!object.Equals(this.CreatorFreeAccessUsersSnapshotId, other.CreatorFreeAccessUsersSnapshotId))
            {
                return false;
            }
        
            if (!object.Equals(this.Email, other.Email))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Api.Persistence.Snapshots
{
    using System;
    using System.Linq;
    using System.ComponentModel.DataAnnotations;
    using Fifthweek.CodeGeneration;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class SubscriberChannelsSnapshot 
    {
        public override string ToString()
        {
            return string.Format("SubscriberChannelsSnapshot({0}, {1}, {2})", this.Id == null ? "null" : this.Id.ToString(), this.Timestamp == null ? "null" : this.Timestamp.ToString(), this.SubscriberId == null ? "null" : this.SubscriberId.ToString());
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
        
            return this.Equals((SubscriberChannelsSnapshot)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Id != null ? this.Id.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Timestamp != null ? this.Timestamp.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.SubscriberId != null ? this.SubscriberId.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(SubscriberChannelsSnapshot other)
        {
            if (!object.Equals(this.Id, other.Id))
            {
                return false;
            }
        
            if (!object.Equals(this.Timestamp, other.Timestamp))
            {
                return false;
            }
        
            if (!object.Equals(this.SubscriberId, other.SubscriberId))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Api.Persistence.Snapshots
{
    using System;
    using System.Linq;
    using System.ComponentModel.DataAnnotations;
    using Fifthweek.CodeGeneration;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class SubscriberChannelsSnapshotItem 
    {
        public override string ToString()
        {
            return string.Format("SubscriberChannelsSnapshotItem({0}, {1}, {2}, {3})", this.SubscriberChannelsSnapshotId == null ? "null" : this.SubscriberChannelsSnapshotId.ToString(), this.ChannelId == null ? "null" : this.ChannelId.ToString(), this.AcceptedPriceInUsCentsPerWeek == null ? "null" : this.AcceptedPriceInUsCentsPerWeek.ToString(), this.SubscriptionStartDate == null ? "null" : this.SubscriptionStartDate.ToString());
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
        
            return this.Equals((SubscriberChannelsSnapshotItem)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.SubscriberChannelsSnapshotId != null ? this.SubscriberChannelsSnapshotId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.ChannelId != null ? this.ChannelId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.AcceptedPriceInUsCentsPerWeek != null ? this.AcceptedPriceInUsCentsPerWeek.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.SubscriptionStartDate != null ? this.SubscriptionStartDate.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(SubscriberChannelsSnapshotItem other)
        {
            if (!object.Equals(this.SubscriberChannelsSnapshotId, other.SubscriberChannelsSnapshotId))
            {
                return false;
            }
        
            if (!object.Equals(this.ChannelId, other.ChannelId))
            {
                return false;
            }
        
            if (!object.Equals(this.AcceptedPriceInUsCentsPerWeek, other.AcceptedPriceInUsCentsPerWeek))
            {
                return false;
            }
        
            if (!object.Equals(this.SubscriptionStartDate, other.SubscriptionStartDate))
            {
                return false;
            }
        
            return true;
        }
    }
}
namespace Fifthweek.Api.Persistence.Snapshots
{
    using System;
    using System.Linq;
    using System.ComponentModel.DataAnnotations;
    using Fifthweek.CodeGeneration;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class SubscriberSnapshot 
    {
        public override string ToString()
        {
            return string.Format("SubscriberSnapshot({0}, {1}, \"{2}\")", this.Timestamp == null ? "null" : this.Timestamp.ToString(), this.SubscriberId == null ? "null" : this.SubscriberId.ToString(), this.Email == null ? "null" : this.Email.ToString());
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
        
            return this.Equals((SubscriberSnapshot)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Timestamp != null ? this.Timestamp.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.SubscriberId != null ? this.SubscriberId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Email != null ? this.Email.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(SubscriberSnapshot other)
        {
            if (!object.Equals(this.Timestamp, other.Timestamp))
            {
                return false;
            }
        
            if (!object.Equals(this.SubscriberId, other.SubscriberId))
            {
                return false;
            }
        
            if (!object.Equals(this.Email, other.Email))
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
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.CodeGeneration;

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
    using System.ComponentModel.DataAnnotations.Schema;

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
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class FifthweekUser 
    {
        public override string ToString()
        {
            return string.Format("FifthweekUser(\"{0}\", {1}, {2}, {3}, {4}, \"{5}\", \"{6}\", {7}, {8}, {9}, {10}, {11}, \"{12}\", \"{13}\", {14}, \"{15}\", {16}, \"{17}\")", this.ExampleWork == null ? "null" : this.ExampleWork.ToString(), this.RegistrationDate == null ? "null" : this.RegistrationDate.ToString(), this.LastSignInDate == null ? "null" : this.LastSignInDate.ToString(), this.LastAccessTokenDate == null ? "null" : this.LastAccessTokenDate.ToString(), this.ProfileImageFileId == null ? "null" : this.ProfileImageFileId.ToString(), this.Email == null ? "null" : this.Email.ToString(), this.Name == null ? "null" : this.Name.ToString(), this.Id == null ? "null" : this.Id.ToString(), this.AccessFailedCount == null ? "null" : this.AccessFailedCount.ToString(), this.EmailConfirmed == null ? "null" : this.EmailConfirmed.ToString(), this.LockoutEnabled == null ? "null" : this.LockoutEnabled.ToString(), this.LockoutEndDateUtc == null ? "null" : this.LockoutEndDateUtc.ToString(), this.PasswordHash == null ? "null" : this.PasswordHash.ToString(), this.PhoneNumber == null ? "null" : this.PhoneNumber.ToString(), this.PhoneNumberConfirmed == null ? "null" : this.PhoneNumberConfirmed.ToString(), this.SecurityStamp == null ? "null" : this.SecurityStamp.ToString(), this.TwoFactorEnabled == null ? "null" : this.TwoFactorEnabled.ToString(), this.UserName == null ? "null" : this.UserName.ToString());
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
                hashCode = (hashCode * 397) ^ (this.Email != null ? this.Email.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Name != null ? this.Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Id != null ? this.Id.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.AccessFailedCount != null ? this.AccessFailedCount.GetHashCode() : 0);
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
        
            if (!object.Equals(this.Email, other.Email))
            {
                return false;
            }
        
            if (!object.Equals(this.Name, other.Name))
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
    using System.ComponentModel.DataAnnotations.Schema;

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
    using System.Linq;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.CodeGeneration;

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
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.CodeGeneration;

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
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.CodeGeneration;
    using System.Linq;

    public partial class Blog  : IIdentityEquatable
    {
        public const string Table = "Blogs";
        
        public Blog(
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
        
            return this.IdentityEquals((Blog)other);
        }
        
        protected bool IdentityEquals(Blog other)
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

    public static partial class BlogExtensions
    {
        public static System.Threading.Tasks.Task InsertAsync(
            this System.Data.Common.DbConnection connection, 
            System.Collections.Generic.IEnumerable<Blog> entities, 
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
            Blog entity,
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
            SqlGenerationParameters<Blog, Blog.Fields> parameters)
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
            Blog entity, 
            Blog.Fields fields,
            System.Data.IDbTransaction transaction = null)
        {
            return Dapper.SqlMapper.ExecuteAsync(
                connection, 
                UpsertStatement(Blog.Fields.Empty, fields), 
                new 
                {
                    entity.Id, entity.CreatorId, entity.Name, entity.Tagline, entity.Introduction, entity.Description, entity.ExternalVideoUrl, entity.HeaderImageFileId, entity.CreationDate
                },
                transaction);
        }
        
        public static System.Threading.Tasks.Task UpsertAsync(
            this System.Data.Common.DbConnection connection, 
            Blog entity, 
            Blog.Fields mergeOnFields,
            Blog.Fields updateFields,
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
            Blog entity, 
            Blog.Fields fields,
            System.Data.IDbTransaction transaction = null)
        {
            return Dapper.SqlMapper.ExecuteAsync(connection, UpdateStatement(fields), OnlySpecifiedParameters(entity, fields), transaction);
        }
        
        public static System.Threading.Tasks.Task<int> UpdateAsync(
            this System.Data.Common.DbConnection connection,
            SqlGenerationParameters<Blog, Blog.Fields> parameters)
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
            const string insert = "INSERT INTO Blogs(Id, CreatorId, Name, Tagline, Introduction, Description, ExternalVideoUrl, HeaderImageFileId, CreationDate) VALUES(@Id, @CreatorId, @Name, @Tagline, @Introduction, @Description, @ExternalVideoUrl, @HeaderImageFileId, @CreationDate)";
            return idempotent ? SqlStatementTemplates.IdempotentInsert(insert) : insert;
        }
        
        public static string UpsertStatement(Blog.Fields mergeOnFields, Blog.Fields updateFields)
        {
            // HOLDLOCK ensures operation is concurrent by not releasing the U lock on the row after determining
            // it does not exist. See: http://weblogs.sqlteam.com/dang/archive/2009/01/31/UPSERT-Race-Condition-With-MERGE.aspx
            var sql = new System.Text.StringBuilder();
            sql.Append(
                @"MERGE Blogs WITH (HOLDLOCK) as Target
                USING (VALUES (@Id, @CreatorId, @Name, @Tagline, @Introduction, @Description, @ExternalVideoUrl, @HeaderImageFileId, @CreationDate)) AS Source (Id, CreatorId, Name, Tagline, Introduction, Description, ExternalVideoUrl, HeaderImageFileId, CreationDate)
                ON    (");
                
            if (mergeOnFields == Blog.Fields.Empty)
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
        
        public static string UpdateStatement(Blog.Fields fields)
        {
            var sql = new System.Text.StringBuilder();
            sql.Append("UPDATE Blogs SET ");
            sql.AppendUpdateParameters(GetFieldNames(fields));
            sql.Append(" WHERE Id = @Id");
            return sql.ToString();
        }
        
        private static System.Collections.Generic.IReadOnlyList<string> GetFieldNames(Blog.Fields fields, bool autoIncludePrimaryKeys = true)
        {
            var fieldNames = new System.Collections.Generic.List<string>();
            if (autoIncludePrimaryKeys)
            {
                fieldNames.Add("Id");
            }
        
            if (fields.HasFlag(Blog.Fields.CreatorId))
            {
                fieldNames.Add("CreatorId");
            }
        
            if (fields.HasFlag(Blog.Fields.Name))
            {
                fieldNames.Add("Name");
            }
        
            if (fields.HasFlag(Blog.Fields.Tagline))
            {
                fieldNames.Add("Tagline");
            }
        
            if (fields.HasFlag(Blog.Fields.Introduction))
            {
                fieldNames.Add("Introduction");
            }
        
            if (fields.HasFlag(Blog.Fields.Description))
            {
                fieldNames.Add("Description");
            }
        
            if (fields.HasFlag(Blog.Fields.ExternalVideoUrl))
            {
                fieldNames.Add("ExternalVideoUrl");
            }
        
            if (fields.HasFlag(Blog.Fields.HeaderImageFileId))
            {
                fieldNames.Add("HeaderImageFileId");
            }
        
            if (fields.HasFlag(Blog.Fields.CreationDate))
            {
                fieldNames.Add("CreationDate");
            }
        
            return fieldNames;
        }
        
        private static Dapper.DynamicParameters OnlySpecifiedParameters(
            Blog entity, 
            Blog.Fields fields,
            Blog.Fields? excludedFields = null)
        {
            var parameters = new Dapper.DynamicParameters();
        
            // Assume we never want to exclude primary key field(s) from our input.
            parameters.Add("Id", entity.Id);
            if (fields.HasFlag(Blog.Fields.CreatorId) && (excludedFields == null || !excludedFields.Value.HasFlag(Blog.Fields.CreatorId)))
            {
                parameters.Add("CreatorId", entity.CreatorId);
            }
        
            if (fields.HasFlag(Blog.Fields.Name) && (excludedFields == null || !excludedFields.Value.HasFlag(Blog.Fields.Name)))
            {
                parameters.Add("Name", entity.Name);
            }
        
            if (fields.HasFlag(Blog.Fields.Tagline) && (excludedFields == null || !excludedFields.Value.HasFlag(Blog.Fields.Tagline)))
            {
                parameters.Add("Tagline", entity.Tagline);
            }
        
            if (fields.HasFlag(Blog.Fields.Introduction) && (excludedFields == null || !excludedFields.Value.HasFlag(Blog.Fields.Introduction)))
            {
                parameters.Add("Introduction", entity.Introduction);
            }
        
            if (fields.HasFlag(Blog.Fields.Description) && (excludedFields == null || !excludedFields.Value.HasFlag(Blog.Fields.Description)))
            {
                parameters.Add("Description", entity.Description);
            }
        
            if (fields.HasFlag(Blog.Fields.ExternalVideoUrl) && (excludedFields == null || !excludedFields.Value.HasFlag(Blog.Fields.ExternalVideoUrl)))
            {
                parameters.Add("ExternalVideoUrl", entity.ExternalVideoUrl);
            }
        
            if (fields.HasFlag(Blog.Fields.HeaderImageFileId) && (excludedFields == null || !excludedFields.Value.HasFlag(Blog.Fields.HeaderImageFileId)))
            {
                parameters.Add("HeaderImageFileId", entity.HeaderImageFileId);
            }
        
            if (fields.HasFlag(Blog.Fields.CreationDate) && (excludedFields == null || !excludedFields.Value.HasFlag(Blog.Fields.CreationDate)))
            {
                parameters.Add("CreationDate", entity.CreationDate);
            }
        
            return parameters;
        }
        
        private static Dapper.DynamicParameters AllExceptSpecifiedParameters(
            Blog entity, 
            Blog.Fields fields)
        {
            var parameters = new Dapper.DynamicParameters();
        
            // Assume we never want to exclude primary key field(s) from our input.
            parameters.Add("Id", entity.Id);
            if (!fields.HasFlag(Blog.Fields.CreatorId))
            {
                parameters.Add("CreatorId", entity.CreatorId);
            }
        
            if (!fields.HasFlag(Blog.Fields.Name))
            {
                parameters.Add("Name", entity.Name);
            }
        
            if (!fields.HasFlag(Blog.Fields.Tagline))
            {
                parameters.Add("Tagline", entity.Tagline);
            }
        
            if (!fields.HasFlag(Blog.Fields.Introduction))
            {
                parameters.Add("Introduction", entity.Introduction);
            }
        
            if (!fields.HasFlag(Blog.Fields.Description))
            {
                parameters.Add("Description", entity.Description);
            }
        
            if (!fields.HasFlag(Blog.Fields.ExternalVideoUrl))
            {
                parameters.Add("ExternalVideoUrl", entity.ExternalVideoUrl);
            }
        
            if (!fields.HasFlag(Blog.Fields.HeaderImageFileId))
            {
                parameters.Add("HeaderImageFileId", entity.HeaderImageFileId);
            }
        
            if (!fields.HasFlag(Blog.Fields.CreationDate))
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
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Fifthweek.CodeGeneration;
    using System.Linq;
    using Fifthweek.Api.Persistence.Identity;

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
            BlogId = 2, 
            Name = 4, 
            Description = 8, 
            PriceInUsCentsPerWeek = 16, 
            IsVisibleToNonSubscribers = 32, 
            CreationDate = 64, 
            PriceLastSetDate = 128
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
                    entity.Id, entity.BlogId, entity.Name, entity.Description, entity.PriceInUsCentsPerWeek, entity.IsVisibleToNonSubscribers, entity.CreationDate, entity.PriceLastSetDate
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
                    entity.Id, entity.BlogId, entity.Name, entity.Description, entity.PriceInUsCentsPerWeek, entity.IsVisibleToNonSubscribers, entity.CreationDate, entity.PriceLastSetDate
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
                : new Dapper.DynamicParameters(new { entity.Id, entity.BlogId, entity.Name, entity.Description, entity.PriceInUsCentsPerWeek, entity.IsVisibleToNonSubscribers, entity.CreationDate, entity.PriceLastSetDate });
        
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
                    entity.Id, entity.BlogId, entity.Name, entity.Description, entity.PriceInUsCentsPerWeek, entity.IsVisibleToNonSubscribers, entity.CreationDate, entity.PriceLastSetDate
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
                    entity.Id, entity.BlogId, entity.Name, entity.Description, entity.PriceInUsCentsPerWeek, entity.IsVisibleToNonSubscribers, entity.CreationDate, entity.PriceLastSetDate
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
            const string insert = "INSERT INTO Channels(Id, BlogId, Name, Description, PriceInUsCentsPerWeek, IsVisibleToNonSubscribers, CreationDate, PriceLastSetDate) VALUES(@Id, @BlogId, @Name, @Description, @PriceInUsCentsPerWeek, @IsVisibleToNonSubscribers, @CreationDate, @PriceLastSetDate)";
            return idempotent ? SqlStatementTemplates.IdempotentInsert(insert) : insert;
        }
        
        public static string UpsertStatement(Channel.Fields mergeOnFields, Channel.Fields updateFields)
        {
            // HOLDLOCK ensures operation is concurrent by not releasing the U lock on the row after determining
            // it does not exist. See: http://weblogs.sqlteam.com/dang/archive/2009/01/31/UPSERT-Race-Condition-With-MERGE.aspx
            var sql = new System.Text.StringBuilder();
            sql.Append(
                @"MERGE Channels WITH (HOLDLOCK) as Target
                USING (VALUES (@Id, @BlogId, @Name, @Description, @PriceInUsCentsPerWeek, @IsVisibleToNonSubscribers, @CreationDate, @PriceLastSetDate)) AS Source (Id, BlogId, Name, Description, PriceInUsCentsPerWeek, IsVisibleToNonSubscribers, CreationDate, PriceLastSetDate)
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
                    INSERT  (Id, BlogId, Name, Description, PriceInUsCentsPerWeek, IsVisibleToNonSubscribers, CreationDate, PriceLastSetDate)
                    VALUES  (Source.Id, Source.BlogId, Source.Name, Source.Description, Source.PriceInUsCentsPerWeek, Source.IsVisibleToNonSubscribers, Source.CreationDate, Source.PriceLastSetDate);");
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
        
            if (fields.HasFlag(Channel.Fields.BlogId))
            {
                fieldNames.Add("BlogId");
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
        
            if (fields.HasFlag(Channel.Fields.PriceLastSetDate))
            {
                fieldNames.Add("PriceLastSetDate");
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
            if (fields.HasFlag(Channel.Fields.BlogId) && (excludedFields == null || !excludedFields.Value.HasFlag(Channel.Fields.BlogId)))
            {
                parameters.Add("BlogId", entity.BlogId);
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
        
            if (fields.HasFlag(Channel.Fields.PriceLastSetDate) && (excludedFields == null || !excludedFields.Value.HasFlag(Channel.Fields.PriceLastSetDate)))
            {
                parameters.Add("PriceLastSetDate", entity.PriceLastSetDate);
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
            if (!fields.HasFlag(Channel.Fields.BlogId))
            {
                parameters.Add("BlogId", entity.BlogId);
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
        
            if (!fields.HasFlag(Channel.Fields.PriceLastSetDate))
            {
                parameters.Add("PriceLastSetDate", entity.PriceLastSetDate);
            }
        
            return parameters;
        }
        
    }
}
namespace Fifthweek.Api.Persistence
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.CodeGeneration;
    using System.Linq;

    public partial class ChannelSubscription  : IIdentityEquatable
    {
        public const string Table = "ChannelSubscriptions";
        
        public ChannelSubscription(
            System.Guid channelId,
            System.Guid userId)
        {
            if (channelId == null)
            {
                throw new ArgumentNullException("channelId");
            }

            if (userId == null)
            {
                throw new ArgumentNullException("userId");
            }

            this.ChannelId = channelId;
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
        
            return this.IdentityEquals((ChannelSubscription)other);
        }
        
        protected bool IdentityEquals(ChannelSubscription other)
        {
            if (!object.Equals(this.ChannelId, other.ChannelId))
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
            ChannelId = 1, 
            UserId = 2, 
            AcceptedPriceInUsCentsPerWeek = 4, 
            PriceLastAcceptedDate = 8, 
            SubscriptionStartDate = 16
        }
    }

    public static partial class ChannelSubscriptionExtensions
    {
        public static System.Threading.Tasks.Task InsertAsync(
            this System.Data.Common.DbConnection connection, 
            System.Collections.Generic.IEnumerable<ChannelSubscription> entities, 
            bool idempotent = true, 
            System.Data.IDbTransaction transaction = null)
        {
            return Dapper.SqlMapper.ExecuteAsync(
                connection, 
                InsertStatement(idempotent), 
                entities.Select(entity => new 
                {
                    entity.ChannelId, entity.UserId, entity.AcceptedPriceInUsCentsPerWeek, entity.PriceLastAcceptedDate, entity.SubscriptionStartDate
                }).ToArray(),
                transaction);
        }
        
        public static System.Threading.Tasks.Task InsertAsync(
            this System.Data.Common.DbConnection connection, 
            ChannelSubscription entity,
            bool idempotent = true, 
            System.Data.IDbTransaction transaction = null)
        {
            return Dapper.SqlMapper.ExecuteAsync(
                connection, 
                InsertStatement(idempotent), 
                new 
                {
                    entity.ChannelId, entity.UserId, entity.AcceptedPriceInUsCentsPerWeek, entity.PriceLastAcceptedDate, entity.SubscriptionStartDate
                },
                transaction);
        }
        
        public static System.Threading.Tasks.Task<int> InsertAsync(
            this System.Data.Common.DbConnection connection,
            SqlGenerationParameters<ChannelSubscription, ChannelSubscription.Fields> parameters)
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
                : new Dapper.DynamicParameters(new { entity.ChannelId, entity.UserId, entity.AcceptedPriceInUsCentsPerWeek, entity.PriceLastAcceptedDate, entity.SubscriptionStartDate });
        
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
            ChannelSubscription entity, 
            ChannelSubscription.Fields fields,
            System.Data.IDbTransaction transaction = null)
        {
            return Dapper.SqlMapper.ExecuteAsync(
                connection, 
                UpsertStatement(ChannelSubscription.Fields.Empty, fields), 
                new 
                {
                    entity.ChannelId, entity.UserId, entity.AcceptedPriceInUsCentsPerWeek, entity.PriceLastAcceptedDate, entity.SubscriptionStartDate
                },
                transaction);
        }
        
        public static System.Threading.Tasks.Task UpsertAsync(
            this System.Data.Common.DbConnection connection, 
            ChannelSubscription entity, 
            ChannelSubscription.Fields mergeOnFields,
            ChannelSubscription.Fields updateFields,
            System.Data.IDbTransaction transaction = null)
        {
            return Dapper.SqlMapper.ExecuteAsync(
                connection, 
                UpsertStatement(mergeOnFields, updateFields), 
                new 
                {
                    entity.ChannelId, entity.UserId, entity.AcceptedPriceInUsCentsPerWeek, entity.PriceLastAcceptedDate, entity.SubscriptionStartDate
                },
                transaction);
        }
        
        public static System.Threading.Tasks.Task UpdateAsync(
            this System.Data.Common.DbConnection connection, 
            ChannelSubscription entity, 
            ChannelSubscription.Fields fields,
            System.Data.IDbTransaction transaction = null)
        {
            return Dapper.SqlMapper.ExecuteAsync(connection, UpdateStatement(fields), OnlySpecifiedParameters(entity, fields), transaction);
        }
        
        public static System.Threading.Tasks.Task<int> UpdateAsync(
            this System.Data.Common.DbConnection connection,
            SqlGenerationParameters<ChannelSubscription, ChannelSubscription.Fields> parameters)
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
            const string insert = "INSERT INTO ChannelSubscriptions(ChannelId, UserId, AcceptedPriceInUsCentsPerWeek, PriceLastAcceptedDate, SubscriptionStartDate) VALUES(@ChannelId, @UserId, @AcceptedPriceInUsCentsPerWeek, @PriceLastAcceptedDate, @SubscriptionStartDate)";
            return idempotent ? SqlStatementTemplates.IdempotentInsert(insert) : insert;
        }
        
        public static string UpsertStatement(ChannelSubscription.Fields mergeOnFields, ChannelSubscription.Fields updateFields)
        {
            // HOLDLOCK ensures operation is concurrent by not releasing the U lock on the row after determining
            // it does not exist. See: http://weblogs.sqlteam.com/dang/archive/2009/01/31/UPSERT-Race-Condition-With-MERGE.aspx
            var sql = new System.Text.StringBuilder();
            sql.Append(
                @"MERGE ChannelSubscriptions WITH (HOLDLOCK) as Target
                USING (VALUES (@ChannelId, @UserId, @AcceptedPriceInUsCentsPerWeek, @PriceLastAcceptedDate, @SubscriptionStartDate)) AS Source (ChannelId, UserId, AcceptedPriceInUsCentsPerWeek, PriceLastAcceptedDate, SubscriptionStartDate)
                ON    (");
                
            if (mergeOnFields == ChannelSubscription.Fields.Empty)
            {
                sql.Append(@"Target.ChannelId = Source.ChannelId AND Target.UserId = Source.UserId");
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
                    INSERT  (ChannelId, UserId, AcceptedPriceInUsCentsPerWeek, PriceLastAcceptedDate, SubscriptionStartDate)
                    VALUES  (Source.ChannelId, Source.UserId, Source.AcceptedPriceInUsCentsPerWeek, Source.PriceLastAcceptedDate, Source.SubscriptionStartDate);");
            return sql.ToString();
        }
        
        public static string UpdateStatement(ChannelSubscription.Fields fields)
        {
            var sql = new System.Text.StringBuilder();
            sql.Append("UPDATE ChannelSubscriptions SET ");
            sql.AppendUpdateParameters(GetFieldNames(fields));
            sql.Append(" WHERE ChannelId = @ChannelId AND UserId = @UserId");
            return sql.ToString();
        }
        
        private static System.Collections.Generic.IReadOnlyList<string> GetFieldNames(ChannelSubscription.Fields fields, bool autoIncludePrimaryKeys = true)
        {
            var fieldNames = new System.Collections.Generic.List<string>();
            if (autoIncludePrimaryKeys)
            {
                fieldNames.Add("ChannelId");
            }
        
            if (autoIncludePrimaryKeys)
            {
                fieldNames.Add("UserId");
            }
        
            if (fields.HasFlag(ChannelSubscription.Fields.AcceptedPriceInUsCentsPerWeek))
            {
                fieldNames.Add("AcceptedPriceInUsCentsPerWeek");
            }
        
            if (fields.HasFlag(ChannelSubscription.Fields.PriceLastAcceptedDate))
            {
                fieldNames.Add("PriceLastAcceptedDate");
            }
        
            if (fields.HasFlag(ChannelSubscription.Fields.SubscriptionStartDate))
            {
                fieldNames.Add("SubscriptionStartDate");
            }
        
            return fieldNames;
        }
        
        private static Dapper.DynamicParameters OnlySpecifiedParameters(
            ChannelSubscription entity, 
            ChannelSubscription.Fields fields,
            ChannelSubscription.Fields? excludedFields = null)
        {
            var parameters = new Dapper.DynamicParameters();
        
            // Assume we never want to exclude primary key field(s) from our input.
            parameters.Add("ChannelId", entity.ChannelId);
            parameters.Add("UserId", entity.UserId);
            if (fields.HasFlag(ChannelSubscription.Fields.AcceptedPriceInUsCentsPerWeek) && (excludedFields == null || !excludedFields.Value.HasFlag(ChannelSubscription.Fields.AcceptedPriceInUsCentsPerWeek)))
            {
                parameters.Add("AcceptedPriceInUsCentsPerWeek", entity.AcceptedPriceInUsCentsPerWeek);
            }
        
            if (fields.HasFlag(ChannelSubscription.Fields.PriceLastAcceptedDate) && (excludedFields == null || !excludedFields.Value.HasFlag(ChannelSubscription.Fields.PriceLastAcceptedDate)))
            {
                parameters.Add("PriceLastAcceptedDate", entity.PriceLastAcceptedDate);
            }
        
            if (fields.HasFlag(ChannelSubscription.Fields.SubscriptionStartDate) && (excludedFields == null || !excludedFields.Value.HasFlag(ChannelSubscription.Fields.SubscriptionStartDate)))
            {
                parameters.Add("SubscriptionStartDate", entity.SubscriptionStartDate);
            }
        
            return parameters;
        }
        
        private static Dapper.DynamicParameters AllExceptSpecifiedParameters(
            ChannelSubscription entity, 
            ChannelSubscription.Fields fields)
        {
            var parameters = new Dapper.DynamicParameters();
        
            // Assume we never want to exclude primary key field(s) from our input.
            parameters.Add("ChannelId", entity.ChannelId);
            parameters.Add("UserId", entity.UserId);
            if (!fields.HasFlag(ChannelSubscription.Fields.AcceptedPriceInUsCentsPerWeek))
            {
                parameters.Add("AcceptedPriceInUsCentsPerWeek", entity.AcceptedPriceInUsCentsPerWeek);
            }
        
            if (!fields.HasFlag(ChannelSubscription.Fields.PriceLastAcceptedDate))
            {
                parameters.Add("PriceLastAcceptedDate", entity.PriceLastAcceptedDate);
            }
        
            if (!fields.HasFlag(ChannelSubscription.Fields.SubscriptionStartDate))
            {
                parameters.Add("SubscriptionStartDate", entity.SubscriptionStartDate);
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
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.CodeGeneration;

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
    using Fifthweek.Api.Persistence.Identity;
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
namespace Fifthweek.Api.Persistence
{
    using System;
    using System.Linq;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.CodeGeneration;

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
            ProcessingAttempts = 128, 
            FileNameWithoutExtension = 256, 
            FileExtension = 512, 
            BlobSizeBytes = 1024, 
            Purpose = 2048, 
            RenderWidth = 4096, 
            RenderHeight = 8192
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
                    entity.Id, entity.UserId, entity.State, entity.UploadStartedDate, entity.UploadCompletedDate, entity.ProcessingStartedDate, entity.ProcessingCompletedDate, entity.ProcessingAttempts, entity.FileNameWithoutExtension, entity.FileExtension, entity.BlobSizeBytes, entity.Purpose, entity.RenderWidth, entity.RenderHeight
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
                    entity.Id, entity.UserId, entity.State, entity.UploadStartedDate, entity.UploadCompletedDate, entity.ProcessingStartedDate, entity.ProcessingCompletedDate, entity.ProcessingAttempts, entity.FileNameWithoutExtension, entity.FileExtension, entity.BlobSizeBytes, entity.Purpose, entity.RenderWidth, entity.RenderHeight
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
                : new Dapper.DynamicParameters(new { entity.Id, entity.UserId, entity.State, entity.UploadStartedDate, entity.UploadCompletedDate, entity.ProcessingStartedDate, entity.ProcessingCompletedDate, entity.ProcessingAttempts, entity.FileNameWithoutExtension, entity.FileExtension, entity.BlobSizeBytes, entity.Purpose, entity.RenderWidth, entity.RenderHeight });
        
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
                    entity.Id, entity.UserId, entity.State, entity.UploadStartedDate, entity.UploadCompletedDate, entity.ProcessingStartedDate, entity.ProcessingCompletedDate, entity.ProcessingAttempts, entity.FileNameWithoutExtension, entity.FileExtension, entity.BlobSizeBytes, entity.Purpose, entity.RenderWidth, entity.RenderHeight
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
                    entity.Id, entity.UserId, entity.State, entity.UploadStartedDate, entity.UploadCompletedDate, entity.ProcessingStartedDate, entity.ProcessingCompletedDate, entity.ProcessingAttempts, entity.FileNameWithoutExtension, entity.FileExtension, entity.BlobSizeBytes, entity.Purpose, entity.RenderWidth, entity.RenderHeight
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
            const string insert = "INSERT INTO Files(Id, UserId, State, UploadStartedDate, UploadCompletedDate, ProcessingStartedDate, ProcessingCompletedDate, ProcessingAttempts, FileNameWithoutExtension, FileExtension, BlobSizeBytes, Purpose, RenderWidth, RenderHeight) VALUES(@Id, @UserId, @State, @UploadStartedDate, @UploadCompletedDate, @ProcessingStartedDate, @ProcessingCompletedDate, @ProcessingAttempts, @FileNameWithoutExtension, @FileExtension, @BlobSizeBytes, @Purpose, @RenderWidth, @RenderHeight)";
            return idempotent ? SqlStatementTemplates.IdempotentInsert(insert) : insert;
        }
        
        public static string UpsertStatement(File.Fields mergeOnFields, File.Fields updateFields)
        {
            // HOLDLOCK ensures operation is concurrent by not releasing the U lock on the row after determining
            // it does not exist. See: http://weblogs.sqlteam.com/dang/archive/2009/01/31/UPSERT-Race-Condition-With-MERGE.aspx
            var sql = new System.Text.StringBuilder();
            sql.Append(
                @"MERGE Files WITH (HOLDLOCK) as Target
                USING (VALUES (@Id, @UserId, @State, @UploadStartedDate, @UploadCompletedDate, @ProcessingStartedDate, @ProcessingCompletedDate, @ProcessingAttempts, @FileNameWithoutExtension, @FileExtension, @BlobSizeBytes, @Purpose, @RenderWidth, @RenderHeight)) AS Source (Id, UserId, State, UploadStartedDate, UploadCompletedDate, ProcessingStartedDate, ProcessingCompletedDate, ProcessingAttempts, FileNameWithoutExtension, FileExtension, BlobSizeBytes, Purpose, RenderWidth, RenderHeight)
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
                    INSERT  (Id, UserId, State, UploadStartedDate, UploadCompletedDate, ProcessingStartedDate, ProcessingCompletedDate, ProcessingAttempts, FileNameWithoutExtension, FileExtension, BlobSizeBytes, Purpose, RenderWidth, RenderHeight)
                    VALUES  (Source.Id, Source.UserId, Source.State, Source.UploadStartedDate, Source.UploadCompletedDate, Source.ProcessingStartedDate, Source.ProcessingCompletedDate, Source.ProcessingAttempts, Source.FileNameWithoutExtension, Source.FileExtension, Source.BlobSizeBytes, Source.Purpose, Source.RenderWidth, Source.RenderHeight);");
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
        
            if (fields.HasFlag(File.Fields.ProcessingAttempts))
            {
                fieldNames.Add("ProcessingAttempts");
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
        
            if (fields.HasFlag(File.Fields.RenderWidth))
            {
                fieldNames.Add("RenderWidth");
            }
        
            if (fields.HasFlag(File.Fields.RenderHeight))
            {
                fieldNames.Add("RenderHeight");
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
        
            if (fields.HasFlag(File.Fields.ProcessingAttempts) && (excludedFields == null || !excludedFields.Value.HasFlag(File.Fields.ProcessingAttempts)))
            {
                parameters.Add("ProcessingAttempts", entity.ProcessingAttempts);
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
        
            if (fields.HasFlag(File.Fields.RenderWidth) && (excludedFields == null || !excludedFields.Value.HasFlag(File.Fields.RenderWidth)))
            {
                parameters.Add("RenderWidth", entity.RenderWidth);
            }
        
            if (fields.HasFlag(File.Fields.RenderHeight) && (excludedFields == null || !excludedFields.Value.HasFlag(File.Fields.RenderHeight)))
            {
                parameters.Add("RenderHeight", entity.RenderHeight);
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
        
            if (!fields.HasFlag(File.Fields.ProcessingAttempts))
            {
                parameters.Add("ProcessingAttempts", entity.ProcessingAttempts);
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
        
            if (!fields.HasFlag(File.Fields.RenderWidth))
            {
                parameters.Add("RenderWidth", entity.RenderWidth);
            }
        
            if (!fields.HasFlag(File.Fields.RenderHeight))
            {
                parameters.Add("RenderHeight", entity.RenderHeight);
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
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.CodeGeneration;

    public partial class FreeAccessUser  : IIdentityEquatable
    {
        public const string Table = "FreeAccessUsers";
        
        public FreeAccessUser(
            System.Guid blogId,
            System.String email)
        {
            if (blogId == null)
            {
                throw new ArgumentNullException("blogId");
            }

            if (email == null)
            {
                throw new ArgumentNullException("email");
            }

            this.BlogId = blogId;
            this.Email = email;
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
        
            return this.IdentityEquals((FreeAccessUser)other);
        }
        
        protected bool IdentityEquals(FreeAccessUser other)
        {
            if (!object.Equals(this.BlogId, other.BlogId))
            {
                return false;
            }
        
            if (!object.Equals(this.Email, other.Email))
            {
                return false;
            }
        
            return true;
        }
        
        [Flags]
        public enum Fields
        {
            Empty = 0,
            BlogId = 1, 
            Email = 2
        }
    }

    public static partial class FreeAccessUserExtensions
    {
        public static System.Threading.Tasks.Task InsertAsync(
            this System.Data.Common.DbConnection connection, 
            System.Collections.Generic.IEnumerable<FreeAccessUser> entities, 
            bool idempotent = true, 
            System.Data.IDbTransaction transaction = null)
        {
            return Dapper.SqlMapper.ExecuteAsync(
                connection, 
                InsertStatement(idempotent), 
                entities.Select(entity => new 
                {
                    entity.BlogId, entity.Email
                }).ToArray(),
                transaction);
        }
        
        public static System.Threading.Tasks.Task InsertAsync(
            this System.Data.Common.DbConnection connection, 
            FreeAccessUser entity,
            bool idempotent = true, 
            System.Data.IDbTransaction transaction = null)
        {
            return Dapper.SqlMapper.ExecuteAsync(
                connection, 
                InsertStatement(idempotent), 
                new 
                {
                    entity.BlogId, entity.Email
                },
                transaction);
        }
        
        public static System.Threading.Tasks.Task<int> InsertAsync(
            this System.Data.Common.DbConnection connection,
            SqlGenerationParameters<FreeAccessUser, FreeAccessUser.Fields> parameters)
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
                : new Dapper.DynamicParameters(new { entity.BlogId, entity.Email });
        
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
            FreeAccessUser entity, 
            FreeAccessUser.Fields fields,
            System.Data.IDbTransaction transaction = null)
        {
            return Dapper.SqlMapper.ExecuteAsync(
                connection, 
                UpsertStatement(FreeAccessUser.Fields.Empty, fields), 
                new 
                {
                    entity.BlogId, entity.Email
                },
                transaction);
        }
        
        public static System.Threading.Tasks.Task UpsertAsync(
            this System.Data.Common.DbConnection connection, 
            FreeAccessUser entity, 
            FreeAccessUser.Fields mergeOnFields,
            FreeAccessUser.Fields updateFields,
            System.Data.IDbTransaction transaction = null)
        {
            return Dapper.SqlMapper.ExecuteAsync(
                connection, 
                UpsertStatement(mergeOnFields, updateFields), 
                new 
                {
                    entity.BlogId, entity.Email
                },
                transaction);
        }
        
        public static System.Threading.Tasks.Task UpdateAsync(
            this System.Data.Common.DbConnection connection, 
            FreeAccessUser entity, 
            FreeAccessUser.Fields fields,
            System.Data.IDbTransaction transaction = null)
        {
            return Dapper.SqlMapper.ExecuteAsync(connection, UpdateStatement(fields), OnlySpecifiedParameters(entity, fields), transaction);
        }
        
        public static System.Threading.Tasks.Task<int> UpdateAsync(
            this System.Data.Common.DbConnection connection,
            SqlGenerationParameters<FreeAccessUser, FreeAccessUser.Fields> parameters)
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
            const string insert = "INSERT INTO FreeAccessUsers(BlogId, Email) VALUES(@BlogId, @Email)";
            return idempotent ? SqlStatementTemplates.IdempotentInsert(insert) : insert;
        }
        
        public static string UpsertStatement(FreeAccessUser.Fields mergeOnFields, FreeAccessUser.Fields updateFields)
        {
            // HOLDLOCK ensures operation is concurrent by not releasing the U lock on the row after determining
            // it does not exist. See: http://weblogs.sqlteam.com/dang/archive/2009/01/31/UPSERT-Race-Condition-With-MERGE.aspx
            var sql = new System.Text.StringBuilder();
            sql.Append(
                @"MERGE FreeAccessUsers WITH (HOLDLOCK) as Target
                USING (VALUES (@BlogId, @Email)) AS Source (BlogId, Email)
                ON    (");
                
            if (mergeOnFields == FreeAccessUser.Fields.Empty)
            {
                sql.Append(@"Target.BlogId = Source.BlogId AND Target.Email = Source.Email");
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
                    INSERT  (BlogId, Email)
                    VALUES  (Source.BlogId, Source.Email);");
            return sql.ToString();
        }
        
        public static string UpdateStatement(FreeAccessUser.Fields fields)
        {
            var sql = new System.Text.StringBuilder();
            sql.Append("UPDATE FreeAccessUsers SET ");
            sql.AppendUpdateParameters(GetFieldNames(fields));
            sql.Append(" WHERE BlogId = @BlogId AND Email = @Email");
            return sql.ToString();
        }
        
        private static System.Collections.Generic.IReadOnlyList<string> GetFieldNames(FreeAccessUser.Fields fields, bool autoIncludePrimaryKeys = true)
        {
            var fieldNames = new System.Collections.Generic.List<string>();
            if (autoIncludePrimaryKeys)
            {
                fieldNames.Add("BlogId");
            }
        
            if (autoIncludePrimaryKeys)
            {
                fieldNames.Add("Email");
            }
        
            return fieldNames;
        }
        
        private static Dapper.DynamicParameters OnlySpecifiedParameters(
            FreeAccessUser entity, 
            FreeAccessUser.Fields fields,
            FreeAccessUser.Fields? excludedFields = null)
        {
            var parameters = new Dapper.DynamicParameters();
        
            // Assume we never want to exclude primary key field(s) from our input.
            parameters.Add("BlogId", entity.BlogId);
            parameters.Add("Email", entity.Email);
            return parameters;
        }
        
        private static Dapper.DynamicParameters AllExceptSpecifiedParameters(
            FreeAccessUser entity, 
            FreeAccessUser.Fields fields)
        {
            var parameters = new Dapper.DynamicParameters();
        
            // Assume we never want to exclude primary key field(s) from our input.
            parameters.Add("BlogId", entity.BlogId);
            parameters.Add("Email", entity.Email);
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
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.CodeGeneration;

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
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.CodeGeneration;

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
namespace Fifthweek.Api.Persistence.Snapshots
{
    using System;
    using System.Linq;
    using System.ComponentModel.DataAnnotations;
    using Fifthweek.CodeGeneration;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class CreatorChannelsSnapshot  : IIdentityEquatable
    {
        public const string Table = "CreatorChannelsSnapshots";
        
        public CreatorChannelsSnapshot(
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
        
            return this.IdentityEquals((CreatorChannelsSnapshot)other);
        }
        
        protected bool IdentityEquals(CreatorChannelsSnapshot other)
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
            Timestamp = 2, 
            CreatorId = 4
        }
    }

    public static partial class CreatorChannelsSnapshotExtensions
    {
        public static System.Threading.Tasks.Task InsertAsync(
            this System.Data.Common.DbConnection connection, 
            System.Collections.Generic.IEnumerable<CreatorChannelsSnapshot> entities, 
            bool idempotent = true, 
            System.Data.IDbTransaction transaction = null)
        {
            return Dapper.SqlMapper.ExecuteAsync(
                connection, 
                InsertStatement(idempotent), 
                entities.Select(entity => new 
                {
                    entity.Id, entity.Timestamp, entity.CreatorId
                }).ToArray(),
                transaction);
        }
        
        public static System.Threading.Tasks.Task InsertAsync(
            this System.Data.Common.DbConnection connection, 
            CreatorChannelsSnapshot entity,
            bool idempotent = true, 
            System.Data.IDbTransaction transaction = null)
        {
            return Dapper.SqlMapper.ExecuteAsync(
                connection, 
                InsertStatement(idempotent), 
                new 
                {
                    entity.Id, entity.Timestamp, entity.CreatorId
                },
                transaction);
        }
        
        public static System.Threading.Tasks.Task<int> InsertAsync(
            this System.Data.Common.DbConnection connection,
            SqlGenerationParameters<CreatorChannelsSnapshot, CreatorChannelsSnapshot.Fields> parameters)
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
                : new Dapper.DynamicParameters(new { entity.Id, entity.Timestamp, entity.CreatorId });
        
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
            CreatorChannelsSnapshot entity, 
            CreatorChannelsSnapshot.Fields fields,
            System.Data.IDbTransaction transaction = null)
        {
            return Dapper.SqlMapper.ExecuteAsync(
                connection, 
                UpsertStatement(CreatorChannelsSnapshot.Fields.Empty, fields), 
                new 
                {
                    entity.Id, entity.Timestamp, entity.CreatorId
                },
                transaction);
        }
        
        public static System.Threading.Tasks.Task UpsertAsync(
            this System.Data.Common.DbConnection connection, 
            CreatorChannelsSnapshot entity, 
            CreatorChannelsSnapshot.Fields mergeOnFields,
            CreatorChannelsSnapshot.Fields updateFields,
            System.Data.IDbTransaction transaction = null)
        {
            return Dapper.SqlMapper.ExecuteAsync(
                connection, 
                UpsertStatement(mergeOnFields, updateFields), 
                new 
                {
                    entity.Id, entity.Timestamp, entity.CreatorId
                },
                transaction);
        }
        
        public static System.Threading.Tasks.Task UpdateAsync(
            this System.Data.Common.DbConnection connection, 
            CreatorChannelsSnapshot entity, 
            CreatorChannelsSnapshot.Fields fields,
            System.Data.IDbTransaction transaction = null)
        {
            return Dapper.SqlMapper.ExecuteAsync(connection, UpdateStatement(fields), OnlySpecifiedParameters(entity, fields), transaction);
        }
        
        public static System.Threading.Tasks.Task<int> UpdateAsync(
            this System.Data.Common.DbConnection connection,
            SqlGenerationParameters<CreatorChannelsSnapshot, CreatorChannelsSnapshot.Fields> parameters)
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
            const string insert = "INSERT INTO CreatorChannelsSnapshots(Id, Timestamp, CreatorId) VALUES(@Id, @Timestamp, @CreatorId)";
            return idempotent ? SqlStatementTemplates.IdempotentInsert(insert) : insert;
        }
        
        public static string UpsertStatement(CreatorChannelsSnapshot.Fields mergeOnFields, CreatorChannelsSnapshot.Fields updateFields)
        {
            // HOLDLOCK ensures operation is concurrent by not releasing the U lock on the row after determining
            // it does not exist. See: http://weblogs.sqlteam.com/dang/archive/2009/01/31/UPSERT-Race-Condition-With-MERGE.aspx
            var sql = new System.Text.StringBuilder();
            sql.Append(
                @"MERGE CreatorChannelsSnapshots WITH (HOLDLOCK) as Target
                USING (VALUES (@Id, @Timestamp, @CreatorId)) AS Source (Id, Timestamp, CreatorId)
                ON    (");
                
            if (mergeOnFields == CreatorChannelsSnapshot.Fields.Empty)
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
                    INSERT  (Id, Timestamp, CreatorId)
                    VALUES  (Source.Id, Source.Timestamp, Source.CreatorId);");
            return sql.ToString();
        }
        
        public static string UpdateStatement(CreatorChannelsSnapshot.Fields fields)
        {
            var sql = new System.Text.StringBuilder();
            sql.Append("UPDATE CreatorChannelsSnapshots SET ");
            sql.AppendUpdateParameters(GetFieldNames(fields));
            sql.Append(" WHERE Id = @Id");
            return sql.ToString();
        }
        
        private static System.Collections.Generic.IReadOnlyList<string> GetFieldNames(CreatorChannelsSnapshot.Fields fields, bool autoIncludePrimaryKeys = true)
        {
            var fieldNames = new System.Collections.Generic.List<string>();
            if (autoIncludePrimaryKeys)
            {
                fieldNames.Add("Id");
            }
        
            if (fields.HasFlag(CreatorChannelsSnapshot.Fields.Timestamp))
            {
                fieldNames.Add("Timestamp");
            }
        
            if (fields.HasFlag(CreatorChannelsSnapshot.Fields.CreatorId))
            {
                fieldNames.Add("CreatorId");
            }
        
            return fieldNames;
        }
        
        private static Dapper.DynamicParameters OnlySpecifiedParameters(
            CreatorChannelsSnapshot entity, 
            CreatorChannelsSnapshot.Fields fields,
            CreatorChannelsSnapshot.Fields? excludedFields = null)
        {
            var parameters = new Dapper.DynamicParameters();
        
            // Assume we never want to exclude primary key field(s) from our input.
            parameters.Add("Id", entity.Id);
            if (fields.HasFlag(CreatorChannelsSnapshot.Fields.Timestamp) && (excludedFields == null || !excludedFields.Value.HasFlag(CreatorChannelsSnapshot.Fields.Timestamp)))
            {
                parameters.Add("Timestamp", entity.Timestamp);
            }
        
            if (fields.HasFlag(CreatorChannelsSnapshot.Fields.CreatorId) && (excludedFields == null || !excludedFields.Value.HasFlag(CreatorChannelsSnapshot.Fields.CreatorId)))
            {
                parameters.Add("CreatorId", entity.CreatorId);
            }
        
            return parameters;
        }
        
        private static Dapper.DynamicParameters AllExceptSpecifiedParameters(
            CreatorChannelsSnapshot entity, 
            CreatorChannelsSnapshot.Fields fields)
        {
            var parameters = new Dapper.DynamicParameters();
        
            // Assume we never want to exclude primary key field(s) from our input.
            parameters.Add("Id", entity.Id);
            if (!fields.HasFlag(CreatorChannelsSnapshot.Fields.Timestamp))
            {
                parameters.Add("Timestamp", entity.Timestamp);
            }
        
            if (!fields.HasFlag(CreatorChannelsSnapshot.Fields.CreatorId))
            {
                parameters.Add("CreatorId", entity.CreatorId);
            }
        
            return parameters;
        }
        
    }
}
namespace Fifthweek.Api.Persistence.Snapshots
{
    using System;
    using System.Linq;
    using System.ComponentModel.DataAnnotations;
    using Fifthweek.CodeGeneration;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class CreatorChannelsSnapshotItem  : IIdentityEquatable
    {
        public const string Table = "CreatorChannelsSnapshotItems";
        
        public CreatorChannelsSnapshotItem(
            System.Guid creatorChannelsSnapshotId,
            System.Guid channelId)
        {
            if (creatorChannelsSnapshotId == null)
            {
                throw new ArgumentNullException("creatorChannelsSnapshotId");
            }

            if (channelId == null)
            {
                throw new ArgumentNullException("channelId");
            }

            this.CreatorChannelsSnapshotId = creatorChannelsSnapshotId;
            this.ChannelId = channelId;
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
        
            return this.IdentityEquals((CreatorChannelsSnapshotItem)other);
        }
        
        protected bool IdentityEquals(CreatorChannelsSnapshotItem other)
        {
            if (!object.Equals(this.CreatorChannelsSnapshotId, other.CreatorChannelsSnapshotId))
            {
                return false;
            }
        
            if (!object.Equals(this.ChannelId, other.ChannelId))
            {
                return false;
            }
        
            return true;
        }
        
        [Flags]
        public enum Fields
        {
            Empty = 0,
            CreatorChannelsSnapshotId = 1, 
            ChannelId = 2, 
            PriceInUsCentsPerWeek = 4
        }
    }

    public static partial class CreatorChannelsSnapshotItemExtensions
    {
        public static System.Threading.Tasks.Task InsertAsync(
            this System.Data.Common.DbConnection connection, 
            System.Collections.Generic.IEnumerable<CreatorChannelsSnapshotItem> entities, 
            bool idempotent = true, 
            System.Data.IDbTransaction transaction = null)
        {
            return Dapper.SqlMapper.ExecuteAsync(
                connection, 
                InsertStatement(idempotent), 
                entities.Select(entity => new 
                {
                    entity.CreatorChannelsSnapshotId, entity.ChannelId, entity.PriceInUsCentsPerWeek
                }).ToArray(),
                transaction);
        }
        
        public static System.Threading.Tasks.Task InsertAsync(
            this System.Data.Common.DbConnection connection, 
            CreatorChannelsSnapshotItem entity,
            bool idempotent = true, 
            System.Data.IDbTransaction transaction = null)
        {
            return Dapper.SqlMapper.ExecuteAsync(
                connection, 
                InsertStatement(idempotent), 
                new 
                {
                    entity.CreatorChannelsSnapshotId, entity.ChannelId, entity.PriceInUsCentsPerWeek
                },
                transaction);
        }
        
        public static System.Threading.Tasks.Task<int> InsertAsync(
            this System.Data.Common.DbConnection connection,
            SqlGenerationParameters<CreatorChannelsSnapshotItem, CreatorChannelsSnapshotItem.Fields> parameters)
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
                : new Dapper.DynamicParameters(new { entity.CreatorChannelsSnapshotId, entity.ChannelId, entity.PriceInUsCentsPerWeek });
        
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
            CreatorChannelsSnapshotItem entity, 
            CreatorChannelsSnapshotItem.Fields fields,
            System.Data.IDbTransaction transaction = null)
        {
            return Dapper.SqlMapper.ExecuteAsync(
                connection, 
                UpsertStatement(CreatorChannelsSnapshotItem.Fields.Empty, fields), 
                new 
                {
                    entity.CreatorChannelsSnapshotId, entity.ChannelId, entity.PriceInUsCentsPerWeek
                },
                transaction);
        }
        
        public static System.Threading.Tasks.Task UpsertAsync(
            this System.Data.Common.DbConnection connection, 
            CreatorChannelsSnapshotItem entity, 
            CreatorChannelsSnapshotItem.Fields mergeOnFields,
            CreatorChannelsSnapshotItem.Fields updateFields,
            System.Data.IDbTransaction transaction = null)
        {
            return Dapper.SqlMapper.ExecuteAsync(
                connection, 
                UpsertStatement(mergeOnFields, updateFields), 
                new 
                {
                    entity.CreatorChannelsSnapshotId, entity.ChannelId, entity.PriceInUsCentsPerWeek
                },
                transaction);
        }
        
        public static System.Threading.Tasks.Task UpdateAsync(
            this System.Data.Common.DbConnection connection, 
            CreatorChannelsSnapshotItem entity, 
            CreatorChannelsSnapshotItem.Fields fields,
            System.Data.IDbTransaction transaction = null)
        {
            return Dapper.SqlMapper.ExecuteAsync(connection, UpdateStatement(fields), OnlySpecifiedParameters(entity, fields), transaction);
        }
        
        public static System.Threading.Tasks.Task<int> UpdateAsync(
            this System.Data.Common.DbConnection connection,
            SqlGenerationParameters<CreatorChannelsSnapshotItem, CreatorChannelsSnapshotItem.Fields> parameters)
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
            const string insert = "INSERT INTO CreatorChannelsSnapshotItems(CreatorChannelsSnapshotId, ChannelId, PriceInUsCentsPerWeek) VALUES(@CreatorChannelsSnapshotId, @ChannelId, @PriceInUsCentsPerWeek)";
            return idempotent ? SqlStatementTemplates.IdempotentInsert(insert) : insert;
        }
        
        public static string UpsertStatement(CreatorChannelsSnapshotItem.Fields mergeOnFields, CreatorChannelsSnapshotItem.Fields updateFields)
        {
            // HOLDLOCK ensures operation is concurrent by not releasing the U lock on the row after determining
            // it does not exist. See: http://weblogs.sqlteam.com/dang/archive/2009/01/31/UPSERT-Race-Condition-With-MERGE.aspx
            var sql = new System.Text.StringBuilder();
            sql.Append(
                @"MERGE CreatorChannelsSnapshotItems WITH (HOLDLOCK) as Target
                USING (VALUES (@CreatorChannelsSnapshotId, @ChannelId, @PriceInUsCentsPerWeek)) AS Source (CreatorChannelsSnapshotId, ChannelId, PriceInUsCentsPerWeek)
                ON    (");
                
            if (mergeOnFields == CreatorChannelsSnapshotItem.Fields.Empty)
            {
                sql.Append(@"Target.CreatorChannelsSnapshotId = Source.CreatorChannelsSnapshotId AND Target.ChannelId = Source.ChannelId");
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
                    INSERT  (CreatorChannelsSnapshotId, ChannelId, PriceInUsCentsPerWeek)
                    VALUES  (Source.CreatorChannelsSnapshotId, Source.ChannelId, Source.PriceInUsCentsPerWeek);");
            return sql.ToString();
        }
        
        public static string UpdateStatement(CreatorChannelsSnapshotItem.Fields fields)
        {
            var sql = new System.Text.StringBuilder();
            sql.Append("UPDATE CreatorChannelsSnapshotItems SET ");
            sql.AppendUpdateParameters(GetFieldNames(fields));
            sql.Append(" WHERE CreatorChannelsSnapshotId = @CreatorChannelsSnapshotId AND ChannelId = @ChannelId");
            return sql.ToString();
        }
        
        private static System.Collections.Generic.IReadOnlyList<string> GetFieldNames(CreatorChannelsSnapshotItem.Fields fields, bool autoIncludePrimaryKeys = true)
        {
            var fieldNames = new System.Collections.Generic.List<string>();
            if (autoIncludePrimaryKeys)
            {
                fieldNames.Add("CreatorChannelsSnapshotId");
            }
        
            if (autoIncludePrimaryKeys)
            {
                fieldNames.Add("ChannelId");
            }
        
            if (fields.HasFlag(CreatorChannelsSnapshotItem.Fields.PriceInUsCentsPerWeek))
            {
                fieldNames.Add("PriceInUsCentsPerWeek");
            }
        
            return fieldNames;
        }
        
        private static Dapper.DynamicParameters OnlySpecifiedParameters(
            CreatorChannelsSnapshotItem entity, 
            CreatorChannelsSnapshotItem.Fields fields,
            CreatorChannelsSnapshotItem.Fields? excludedFields = null)
        {
            var parameters = new Dapper.DynamicParameters();
        
            // Assume we never want to exclude primary key field(s) from our input.
            parameters.Add("CreatorChannelsSnapshotId", entity.CreatorChannelsSnapshotId);
            parameters.Add("ChannelId", entity.ChannelId);
            if (fields.HasFlag(CreatorChannelsSnapshotItem.Fields.PriceInUsCentsPerWeek) && (excludedFields == null || !excludedFields.Value.HasFlag(CreatorChannelsSnapshotItem.Fields.PriceInUsCentsPerWeek)))
            {
                parameters.Add("PriceInUsCentsPerWeek", entity.PriceInUsCentsPerWeek);
            }
        
            return parameters;
        }
        
        private static Dapper.DynamicParameters AllExceptSpecifiedParameters(
            CreatorChannelsSnapshotItem entity, 
            CreatorChannelsSnapshotItem.Fields fields)
        {
            var parameters = new Dapper.DynamicParameters();
        
            // Assume we never want to exclude primary key field(s) from our input.
            parameters.Add("CreatorChannelsSnapshotId", entity.CreatorChannelsSnapshotId);
            parameters.Add("ChannelId", entity.ChannelId);
            if (!fields.HasFlag(CreatorChannelsSnapshotItem.Fields.PriceInUsCentsPerWeek))
            {
                parameters.Add("PriceInUsCentsPerWeek", entity.PriceInUsCentsPerWeek);
            }
        
            return parameters;
        }
        
    }
}
namespace Fifthweek.Api.Persistence.Snapshots
{
    using System;
    using System.Linq;
    using System.ComponentModel.DataAnnotations;
    using Fifthweek.CodeGeneration;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class CreatorFreeAccessUsersSnapshot  : IIdentityEquatable
    {
        public const string Table = "CreatorFreeAccessUsersSnapshots";
        
        public CreatorFreeAccessUsersSnapshot(
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
        
            return this.IdentityEquals((CreatorFreeAccessUsersSnapshot)other);
        }
        
        protected bool IdentityEquals(CreatorFreeAccessUsersSnapshot other)
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
            Timestamp = 2, 
            CreatorId = 4
        }
    }

    public static partial class CreatorFreeAccessUsersSnapshotExtensions
    {
        public static System.Threading.Tasks.Task InsertAsync(
            this System.Data.Common.DbConnection connection, 
            System.Collections.Generic.IEnumerable<CreatorFreeAccessUsersSnapshot> entities, 
            bool idempotent = true, 
            System.Data.IDbTransaction transaction = null)
        {
            return Dapper.SqlMapper.ExecuteAsync(
                connection, 
                InsertStatement(idempotent), 
                entities.Select(entity => new 
                {
                    entity.Id, entity.Timestamp, entity.CreatorId
                }).ToArray(),
                transaction);
        }
        
        public static System.Threading.Tasks.Task InsertAsync(
            this System.Data.Common.DbConnection connection, 
            CreatorFreeAccessUsersSnapshot entity,
            bool idempotent = true, 
            System.Data.IDbTransaction transaction = null)
        {
            return Dapper.SqlMapper.ExecuteAsync(
                connection, 
                InsertStatement(idempotent), 
                new 
                {
                    entity.Id, entity.Timestamp, entity.CreatorId
                },
                transaction);
        }
        
        public static System.Threading.Tasks.Task<int> InsertAsync(
            this System.Data.Common.DbConnection connection,
            SqlGenerationParameters<CreatorFreeAccessUsersSnapshot, CreatorFreeAccessUsersSnapshot.Fields> parameters)
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
                : new Dapper.DynamicParameters(new { entity.Id, entity.Timestamp, entity.CreatorId });
        
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
            CreatorFreeAccessUsersSnapshot entity, 
            CreatorFreeAccessUsersSnapshot.Fields fields,
            System.Data.IDbTransaction transaction = null)
        {
            return Dapper.SqlMapper.ExecuteAsync(
                connection, 
                UpsertStatement(CreatorFreeAccessUsersSnapshot.Fields.Empty, fields), 
                new 
                {
                    entity.Id, entity.Timestamp, entity.CreatorId
                },
                transaction);
        }
        
        public static System.Threading.Tasks.Task UpsertAsync(
            this System.Data.Common.DbConnection connection, 
            CreatorFreeAccessUsersSnapshot entity, 
            CreatorFreeAccessUsersSnapshot.Fields mergeOnFields,
            CreatorFreeAccessUsersSnapshot.Fields updateFields,
            System.Data.IDbTransaction transaction = null)
        {
            return Dapper.SqlMapper.ExecuteAsync(
                connection, 
                UpsertStatement(mergeOnFields, updateFields), 
                new 
                {
                    entity.Id, entity.Timestamp, entity.CreatorId
                },
                transaction);
        }
        
        public static System.Threading.Tasks.Task UpdateAsync(
            this System.Data.Common.DbConnection connection, 
            CreatorFreeAccessUsersSnapshot entity, 
            CreatorFreeAccessUsersSnapshot.Fields fields,
            System.Data.IDbTransaction transaction = null)
        {
            return Dapper.SqlMapper.ExecuteAsync(connection, UpdateStatement(fields), OnlySpecifiedParameters(entity, fields), transaction);
        }
        
        public static System.Threading.Tasks.Task<int> UpdateAsync(
            this System.Data.Common.DbConnection connection,
            SqlGenerationParameters<CreatorFreeAccessUsersSnapshot, CreatorFreeAccessUsersSnapshot.Fields> parameters)
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
            const string insert = "INSERT INTO CreatorFreeAccessUsersSnapshots(Id, Timestamp, CreatorId) VALUES(@Id, @Timestamp, @CreatorId)";
            return idempotent ? SqlStatementTemplates.IdempotentInsert(insert) : insert;
        }
        
        public static string UpsertStatement(CreatorFreeAccessUsersSnapshot.Fields mergeOnFields, CreatorFreeAccessUsersSnapshot.Fields updateFields)
        {
            // HOLDLOCK ensures operation is concurrent by not releasing the U lock on the row after determining
            // it does not exist. See: http://weblogs.sqlteam.com/dang/archive/2009/01/31/UPSERT-Race-Condition-With-MERGE.aspx
            var sql = new System.Text.StringBuilder();
            sql.Append(
                @"MERGE CreatorFreeAccessUsersSnapshots WITH (HOLDLOCK) as Target
                USING (VALUES (@Id, @Timestamp, @CreatorId)) AS Source (Id, Timestamp, CreatorId)
                ON    (");
                
            if (mergeOnFields == CreatorFreeAccessUsersSnapshot.Fields.Empty)
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
                    INSERT  (Id, Timestamp, CreatorId)
                    VALUES  (Source.Id, Source.Timestamp, Source.CreatorId);");
            return sql.ToString();
        }
        
        public static string UpdateStatement(CreatorFreeAccessUsersSnapshot.Fields fields)
        {
            var sql = new System.Text.StringBuilder();
            sql.Append("UPDATE CreatorFreeAccessUsersSnapshots SET ");
            sql.AppendUpdateParameters(GetFieldNames(fields));
            sql.Append(" WHERE Id = @Id");
            return sql.ToString();
        }
        
        private static System.Collections.Generic.IReadOnlyList<string> GetFieldNames(CreatorFreeAccessUsersSnapshot.Fields fields, bool autoIncludePrimaryKeys = true)
        {
            var fieldNames = new System.Collections.Generic.List<string>();
            if (autoIncludePrimaryKeys)
            {
                fieldNames.Add("Id");
            }
        
            if (fields.HasFlag(CreatorFreeAccessUsersSnapshot.Fields.Timestamp))
            {
                fieldNames.Add("Timestamp");
            }
        
            if (fields.HasFlag(CreatorFreeAccessUsersSnapshot.Fields.CreatorId))
            {
                fieldNames.Add("CreatorId");
            }
        
            return fieldNames;
        }
        
        private static Dapper.DynamicParameters OnlySpecifiedParameters(
            CreatorFreeAccessUsersSnapshot entity, 
            CreatorFreeAccessUsersSnapshot.Fields fields,
            CreatorFreeAccessUsersSnapshot.Fields? excludedFields = null)
        {
            var parameters = new Dapper.DynamicParameters();
        
            // Assume we never want to exclude primary key field(s) from our input.
            parameters.Add("Id", entity.Id);
            if (fields.HasFlag(CreatorFreeAccessUsersSnapshot.Fields.Timestamp) && (excludedFields == null || !excludedFields.Value.HasFlag(CreatorFreeAccessUsersSnapshot.Fields.Timestamp)))
            {
                parameters.Add("Timestamp", entity.Timestamp);
            }
        
            if (fields.HasFlag(CreatorFreeAccessUsersSnapshot.Fields.CreatorId) && (excludedFields == null || !excludedFields.Value.HasFlag(CreatorFreeAccessUsersSnapshot.Fields.CreatorId)))
            {
                parameters.Add("CreatorId", entity.CreatorId);
            }
        
            return parameters;
        }
        
        private static Dapper.DynamicParameters AllExceptSpecifiedParameters(
            CreatorFreeAccessUsersSnapshot entity, 
            CreatorFreeAccessUsersSnapshot.Fields fields)
        {
            var parameters = new Dapper.DynamicParameters();
        
            // Assume we never want to exclude primary key field(s) from our input.
            parameters.Add("Id", entity.Id);
            if (!fields.HasFlag(CreatorFreeAccessUsersSnapshot.Fields.Timestamp))
            {
                parameters.Add("Timestamp", entity.Timestamp);
            }
        
            if (!fields.HasFlag(CreatorFreeAccessUsersSnapshot.Fields.CreatorId))
            {
                parameters.Add("CreatorId", entity.CreatorId);
            }
        
            return parameters;
        }
        
    }
}
namespace Fifthweek.Api.Persistence.Snapshots
{
    using System;
    using System.Linq;
    using System.ComponentModel.DataAnnotations;
    using Fifthweek.CodeGeneration;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class CreatorFreeAccessUsersSnapshotItem  : IIdentityEquatable
    {
        public const string Table = "CreatorFreeAccessUsersSnapshotItems";
        
        public CreatorFreeAccessUsersSnapshotItem(
            System.Guid creatorFreeAccessUsersSnapshotId,
            System.String email)
        {
            if (creatorFreeAccessUsersSnapshotId == null)
            {
                throw new ArgumentNullException("creatorFreeAccessUsersSnapshotId");
            }

            if (email == null)
            {
                throw new ArgumentNullException("email");
            }

            this.CreatorFreeAccessUsersSnapshotId = creatorFreeAccessUsersSnapshotId;
            this.Email = email;
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
        
            return this.IdentityEquals((CreatorFreeAccessUsersSnapshotItem)other);
        }
        
        protected bool IdentityEquals(CreatorFreeAccessUsersSnapshotItem other)
        {
            if (!object.Equals(this.CreatorFreeAccessUsersSnapshotId, other.CreatorFreeAccessUsersSnapshotId))
            {
                return false;
            }
        
            if (!object.Equals(this.Email, other.Email))
            {
                return false;
            }
        
            return true;
        }
        
        [Flags]
        public enum Fields
        {
            Empty = 0,
            CreatorFreeAccessUsersSnapshotId = 1, 
            Email = 2
        }
    }

    public static partial class CreatorFreeAccessUsersSnapshotItemExtensions
    {
        public static System.Threading.Tasks.Task InsertAsync(
            this System.Data.Common.DbConnection connection, 
            System.Collections.Generic.IEnumerable<CreatorFreeAccessUsersSnapshotItem> entities, 
            bool idempotent = true, 
            System.Data.IDbTransaction transaction = null)
        {
            return Dapper.SqlMapper.ExecuteAsync(
                connection, 
                InsertStatement(idempotent), 
                entities.Select(entity => new 
                {
                    entity.CreatorFreeAccessUsersSnapshotId, entity.Email
                }).ToArray(),
                transaction);
        }
        
        public static System.Threading.Tasks.Task InsertAsync(
            this System.Data.Common.DbConnection connection, 
            CreatorFreeAccessUsersSnapshotItem entity,
            bool idempotent = true, 
            System.Data.IDbTransaction transaction = null)
        {
            return Dapper.SqlMapper.ExecuteAsync(
                connection, 
                InsertStatement(idempotent), 
                new 
                {
                    entity.CreatorFreeAccessUsersSnapshotId, entity.Email
                },
                transaction);
        }
        
        public static System.Threading.Tasks.Task<int> InsertAsync(
            this System.Data.Common.DbConnection connection,
            SqlGenerationParameters<CreatorFreeAccessUsersSnapshotItem, CreatorFreeAccessUsersSnapshotItem.Fields> parameters)
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
                : new Dapper.DynamicParameters(new { entity.CreatorFreeAccessUsersSnapshotId, entity.Email });
        
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
            CreatorFreeAccessUsersSnapshotItem entity, 
            CreatorFreeAccessUsersSnapshotItem.Fields fields,
            System.Data.IDbTransaction transaction = null)
        {
            return Dapper.SqlMapper.ExecuteAsync(
                connection, 
                UpsertStatement(CreatorFreeAccessUsersSnapshotItem.Fields.Empty, fields), 
                new 
                {
                    entity.CreatorFreeAccessUsersSnapshotId, entity.Email
                },
                transaction);
        }
        
        public static System.Threading.Tasks.Task UpsertAsync(
            this System.Data.Common.DbConnection connection, 
            CreatorFreeAccessUsersSnapshotItem entity, 
            CreatorFreeAccessUsersSnapshotItem.Fields mergeOnFields,
            CreatorFreeAccessUsersSnapshotItem.Fields updateFields,
            System.Data.IDbTransaction transaction = null)
        {
            return Dapper.SqlMapper.ExecuteAsync(
                connection, 
                UpsertStatement(mergeOnFields, updateFields), 
                new 
                {
                    entity.CreatorFreeAccessUsersSnapshotId, entity.Email
                },
                transaction);
        }
        
        public static System.Threading.Tasks.Task UpdateAsync(
            this System.Data.Common.DbConnection connection, 
            CreatorFreeAccessUsersSnapshotItem entity, 
            CreatorFreeAccessUsersSnapshotItem.Fields fields,
            System.Data.IDbTransaction transaction = null)
        {
            return Dapper.SqlMapper.ExecuteAsync(connection, UpdateStatement(fields), OnlySpecifiedParameters(entity, fields), transaction);
        }
        
        public static System.Threading.Tasks.Task<int> UpdateAsync(
            this System.Data.Common.DbConnection connection,
            SqlGenerationParameters<CreatorFreeAccessUsersSnapshotItem, CreatorFreeAccessUsersSnapshotItem.Fields> parameters)
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
            const string insert = "INSERT INTO CreatorFreeAccessUsersSnapshotItems(CreatorFreeAccessUsersSnapshotId, Email) VALUES(@CreatorFreeAccessUsersSnapshotId, @Email)";
            return idempotent ? SqlStatementTemplates.IdempotentInsert(insert) : insert;
        }
        
        public static string UpsertStatement(CreatorFreeAccessUsersSnapshotItem.Fields mergeOnFields, CreatorFreeAccessUsersSnapshotItem.Fields updateFields)
        {
            // HOLDLOCK ensures operation is concurrent by not releasing the U lock on the row after determining
            // it does not exist. See: http://weblogs.sqlteam.com/dang/archive/2009/01/31/UPSERT-Race-Condition-With-MERGE.aspx
            var sql = new System.Text.StringBuilder();
            sql.Append(
                @"MERGE CreatorFreeAccessUsersSnapshotItems WITH (HOLDLOCK) as Target
                USING (VALUES (@CreatorFreeAccessUsersSnapshotId, @Email)) AS Source (CreatorFreeAccessUsersSnapshotId, Email)
                ON    (");
                
            if (mergeOnFields == CreatorFreeAccessUsersSnapshotItem.Fields.Empty)
            {
                sql.Append(@"Target.CreatorFreeAccessUsersSnapshotId = Source.CreatorFreeAccessUsersSnapshotId AND Target.Email = Source.Email");
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
                    INSERT  (CreatorFreeAccessUsersSnapshotId, Email)
                    VALUES  (Source.CreatorFreeAccessUsersSnapshotId, Source.Email);");
            return sql.ToString();
        }
        
        public static string UpdateStatement(CreatorFreeAccessUsersSnapshotItem.Fields fields)
        {
            var sql = new System.Text.StringBuilder();
            sql.Append("UPDATE CreatorFreeAccessUsersSnapshotItems SET ");
            sql.AppendUpdateParameters(GetFieldNames(fields));
            sql.Append(" WHERE CreatorFreeAccessUsersSnapshotId = @CreatorFreeAccessUsersSnapshotId AND Email = @Email");
            return sql.ToString();
        }
        
        private static System.Collections.Generic.IReadOnlyList<string> GetFieldNames(CreatorFreeAccessUsersSnapshotItem.Fields fields, bool autoIncludePrimaryKeys = true)
        {
            var fieldNames = new System.Collections.Generic.List<string>();
            if (autoIncludePrimaryKeys)
            {
                fieldNames.Add("CreatorFreeAccessUsersSnapshotId");
            }
        
            if (autoIncludePrimaryKeys)
            {
                fieldNames.Add("Email");
            }
        
            return fieldNames;
        }
        
        private static Dapper.DynamicParameters OnlySpecifiedParameters(
            CreatorFreeAccessUsersSnapshotItem entity, 
            CreatorFreeAccessUsersSnapshotItem.Fields fields,
            CreatorFreeAccessUsersSnapshotItem.Fields? excludedFields = null)
        {
            var parameters = new Dapper.DynamicParameters();
        
            // Assume we never want to exclude primary key field(s) from our input.
            parameters.Add("CreatorFreeAccessUsersSnapshotId", entity.CreatorFreeAccessUsersSnapshotId);
            parameters.Add("Email", entity.Email);
            return parameters;
        }
        
        private static Dapper.DynamicParameters AllExceptSpecifiedParameters(
            CreatorFreeAccessUsersSnapshotItem entity, 
            CreatorFreeAccessUsersSnapshotItem.Fields fields)
        {
            var parameters = new Dapper.DynamicParameters();
        
            // Assume we never want to exclude primary key field(s) from our input.
            parameters.Add("CreatorFreeAccessUsersSnapshotId", entity.CreatorFreeAccessUsersSnapshotId);
            parameters.Add("Email", entity.Email);
            return parameters;
        }
        
    }
}
namespace Fifthweek.Api.Persistence.Snapshots
{
    using System;
    using System.Linq;
    using System.ComponentModel.DataAnnotations;
    using Fifthweek.CodeGeneration;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class SubscriberChannelsSnapshot  : IIdentityEquatable
    {
        public const string Table = "SubscriberChannelsSnapshots";
        
        public SubscriberChannelsSnapshot(
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
        
            return this.IdentityEquals((SubscriberChannelsSnapshot)other);
        }
        
        protected bool IdentityEquals(SubscriberChannelsSnapshot other)
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
            Timestamp = 2, 
            SubscriberId = 4
        }
    }

    public static partial class SubscriberChannelsSnapshotExtensions
    {
        public static System.Threading.Tasks.Task InsertAsync(
            this System.Data.Common.DbConnection connection, 
            System.Collections.Generic.IEnumerable<SubscriberChannelsSnapshot> entities, 
            bool idempotent = true, 
            System.Data.IDbTransaction transaction = null)
        {
            return Dapper.SqlMapper.ExecuteAsync(
                connection, 
                InsertStatement(idempotent), 
                entities.Select(entity => new 
                {
                    entity.Id, entity.Timestamp, entity.SubscriberId
                }).ToArray(),
                transaction);
        }
        
        public static System.Threading.Tasks.Task InsertAsync(
            this System.Data.Common.DbConnection connection, 
            SubscriberChannelsSnapshot entity,
            bool idempotent = true, 
            System.Data.IDbTransaction transaction = null)
        {
            return Dapper.SqlMapper.ExecuteAsync(
                connection, 
                InsertStatement(idempotent), 
                new 
                {
                    entity.Id, entity.Timestamp, entity.SubscriberId
                },
                transaction);
        }
        
        public static System.Threading.Tasks.Task<int> InsertAsync(
            this System.Data.Common.DbConnection connection,
            SqlGenerationParameters<SubscriberChannelsSnapshot, SubscriberChannelsSnapshot.Fields> parameters)
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
                : new Dapper.DynamicParameters(new { entity.Id, entity.Timestamp, entity.SubscriberId });
        
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
            SubscriberChannelsSnapshot entity, 
            SubscriberChannelsSnapshot.Fields fields,
            System.Data.IDbTransaction transaction = null)
        {
            return Dapper.SqlMapper.ExecuteAsync(
                connection, 
                UpsertStatement(SubscriberChannelsSnapshot.Fields.Empty, fields), 
                new 
                {
                    entity.Id, entity.Timestamp, entity.SubscriberId
                },
                transaction);
        }
        
        public static System.Threading.Tasks.Task UpsertAsync(
            this System.Data.Common.DbConnection connection, 
            SubscriberChannelsSnapshot entity, 
            SubscriberChannelsSnapshot.Fields mergeOnFields,
            SubscriberChannelsSnapshot.Fields updateFields,
            System.Data.IDbTransaction transaction = null)
        {
            return Dapper.SqlMapper.ExecuteAsync(
                connection, 
                UpsertStatement(mergeOnFields, updateFields), 
                new 
                {
                    entity.Id, entity.Timestamp, entity.SubscriberId
                },
                transaction);
        }
        
        public static System.Threading.Tasks.Task UpdateAsync(
            this System.Data.Common.DbConnection connection, 
            SubscriberChannelsSnapshot entity, 
            SubscriberChannelsSnapshot.Fields fields,
            System.Data.IDbTransaction transaction = null)
        {
            return Dapper.SqlMapper.ExecuteAsync(connection, UpdateStatement(fields), OnlySpecifiedParameters(entity, fields), transaction);
        }
        
        public static System.Threading.Tasks.Task<int> UpdateAsync(
            this System.Data.Common.DbConnection connection,
            SqlGenerationParameters<SubscriberChannelsSnapshot, SubscriberChannelsSnapshot.Fields> parameters)
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
            const string insert = "INSERT INTO SubscriberChannelsSnapshots(Id, Timestamp, SubscriberId) VALUES(@Id, @Timestamp, @SubscriberId)";
            return idempotent ? SqlStatementTemplates.IdempotentInsert(insert) : insert;
        }
        
        public static string UpsertStatement(SubscriberChannelsSnapshot.Fields mergeOnFields, SubscriberChannelsSnapshot.Fields updateFields)
        {
            // HOLDLOCK ensures operation is concurrent by not releasing the U lock on the row after determining
            // it does not exist. See: http://weblogs.sqlteam.com/dang/archive/2009/01/31/UPSERT-Race-Condition-With-MERGE.aspx
            var sql = new System.Text.StringBuilder();
            sql.Append(
                @"MERGE SubscriberChannelsSnapshots WITH (HOLDLOCK) as Target
                USING (VALUES (@Id, @Timestamp, @SubscriberId)) AS Source (Id, Timestamp, SubscriberId)
                ON    (");
                
            if (mergeOnFields == SubscriberChannelsSnapshot.Fields.Empty)
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
                    INSERT  (Id, Timestamp, SubscriberId)
                    VALUES  (Source.Id, Source.Timestamp, Source.SubscriberId);");
            return sql.ToString();
        }
        
        public static string UpdateStatement(SubscriberChannelsSnapshot.Fields fields)
        {
            var sql = new System.Text.StringBuilder();
            sql.Append("UPDATE SubscriberChannelsSnapshots SET ");
            sql.AppendUpdateParameters(GetFieldNames(fields));
            sql.Append(" WHERE Id = @Id");
            return sql.ToString();
        }
        
        private static System.Collections.Generic.IReadOnlyList<string> GetFieldNames(SubscriberChannelsSnapshot.Fields fields, bool autoIncludePrimaryKeys = true)
        {
            var fieldNames = new System.Collections.Generic.List<string>();
            if (autoIncludePrimaryKeys)
            {
                fieldNames.Add("Id");
            }
        
            if (fields.HasFlag(SubscriberChannelsSnapshot.Fields.Timestamp))
            {
                fieldNames.Add("Timestamp");
            }
        
            if (fields.HasFlag(SubscriberChannelsSnapshot.Fields.SubscriberId))
            {
                fieldNames.Add("SubscriberId");
            }
        
            return fieldNames;
        }
        
        private static Dapper.DynamicParameters OnlySpecifiedParameters(
            SubscriberChannelsSnapshot entity, 
            SubscriberChannelsSnapshot.Fields fields,
            SubscriberChannelsSnapshot.Fields? excludedFields = null)
        {
            var parameters = new Dapper.DynamicParameters();
        
            // Assume we never want to exclude primary key field(s) from our input.
            parameters.Add("Id", entity.Id);
            if (fields.HasFlag(SubscriberChannelsSnapshot.Fields.Timestamp) && (excludedFields == null || !excludedFields.Value.HasFlag(SubscriberChannelsSnapshot.Fields.Timestamp)))
            {
                parameters.Add("Timestamp", entity.Timestamp);
            }
        
            if (fields.HasFlag(SubscriberChannelsSnapshot.Fields.SubscriberId) && (excludedFields == null || !excludedFields.Value.HasFlag(SubscriberChannelsSnapshot.Fields.SubscriberId)))
            {
                parameters.Add("SubscriberId", entity.SubscriberId);
            }
        
            return parameters;
        }
        
        private static Dapper.DynamicParameters AllExceptSpecifiedParameters(
            SubscriberChannelsSnapshot entity, 
            SubscriberChannelsSnapshot.Fields fields)
        {
            var parameters = new Dapper.DynamicParameters();
        
            // Assume we never want to exclude primary key field(s) from our input.
            parameters.Add("Id", entity.Id);
            if (!fields.HasFlag(SubscriberChannelsSnapshot.Fields.Timestamp))
            {
                parameters.Add("Timestamp", entity.Timestamp);
            }
        
            if (!fields.HasFlag(SubscriberChannelsSnapshot.Fields.SubscriberId))
            {
                parameters.Add("SubscriberId", entity.SubscriberId);
            }
        
            return parameters;
        }
        
    }
}
namespace Fifthweek.Api.Persistence.Snapshots
{
    using System;
    using System.Linq;
    using System.ComponentModel.DataAnnotations;
    using Fifthweek.CodeGeneration;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class SubscriberChannelsSnapshotItem  : IIdentityEquatable
    {
        public const string Table = "SubscriberChannelsSnapshotItems";
        
        public SubscriberChannelsSnapshotItem(
            System.Guid subscriberChannelsSnapshotId,
            System.Guid channelId)
        {
            if (subscriberChannelsSnapshotId == null)
            {
                throw new ArgumentNullException("subscriberChannelsSnapshotId");
            }

            if (channelId == null)
            {
                throw new ArgumentNullException("channelId");
            }

            this.SubscriberChannelsSnapshotId = subscriberChannelsSnapshotId;
            this.ChannelId = channelId;
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
        
            return this.IdentityEquals((SubscriberChannelsSnapshotItem)other);
        }
        
        protected bool IdentityEquals(SubscriberChannelsSnapshotItem other)
        {
            if (!object.Equals(this.SubscriberChannelsSnapshotId, other.SubscriberChannelsSnapshotId))
            {
                return false;
            }
        
            if (!object.Equals(this.ChannelId, other.ChannelId))
            {
                return false;
            }
        
            return true;
        }
        
        [Flags]
        public enum Fields
        {
            Empty = 0,
            SubscriberChannelsSnapshotId = 1, 
            ChannelId = 2, 
            AcceptedPriceInUsCentsPerWeek = 4, 
            SubscriptionStartDate = 8
        }
    }

    public static partial class SubscriberChannelsSnapshotItemExtensions
    {
        public static System.Threading.Tasks.Task InsertAsync(
            this System.Data.Common.DbConnection connection, 
            System.Collections.Generic.IEnumerable<SubscriberChannelsSnapshotItem> entities, 
            bool idempotent = true, 
            System.Data.IDbTransaction transaction = null)
        {
            return Dapper.SqlMapper.ExecuteAsync(
                connection, 
                InsertStatement(idempotent), 
                entities.Select(entity => new 
                {
                    entity.SubscriberChannelsSnapshotId, entity.ChannelId, entity.AcceptedPriceInUsCentsPerWeek, entity.SubscriptionStartDate
                }).ToArray(),
                transaction);
        }
        
        public static System.Threading.Tasks.Task InsertAsync(
            this System.Data.Common.DbConnection connection, 
            SubscriberChannelsSnapshotItem entity,
            bool idempotent = true, 
            System.Data.IDbTransaction transaction = null)
        {
            return Dapper.SqlMapper.ExecuteAsync(
                connection, 
                InsertStatement(idempotent), 
                new 
                {
                    entity.SubscriberChannelsSnapshotId, entity.ChannelId, entity.AcceptedPriceInUsCentsPerWeek, entity.SubscriptionStartDate
                },
                transaction);
        }
        
        public static System.Threading.Tasks.Task<int> InsertAsync(
            this System.Data.Common.DbConnection connection,
            SqlGenerationParameters<SubscriberChannelsSnapshotItem, SubscriberChannelsSnapshotItem.Fields> parameters)
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
                : new Dapper.DynamicParameters(new { entity.SubscriberChannelsSnapshotId, entity.ChannelId, entity.AcceptedPriceInUsCentsPerWeek, entity.SubscriptionStartDate });
        
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
            SubscriberChannelsSnapshotItem entity, 
            SubscriberChannelsSnapshotItem.Fields fields,
            System.Data.IDbTransaction transaction = null)
        {
            return Dapper.SqlMapper.ExecuteAsync(
                connection, 
                UpsertStatement(SubscriberChannelsSnapshotItem.Fields.Empty, fields), 
                new 
                {
                    entity.SubscriberChannelsSnapshotId, entity.ChannelId, entity.AcceptedPriceInUsCentsPerWeek, entity.SubscriptionStartDate
                },
                transaction);
        }
        
        public static System.Threading.Tasks.Task UpsertAsync(
            this System.Data.Common.DbConnection connection, 
            SubscriberChannelsSnapshotItem entity, 
            SubscriberChannelsSnapshotItem.Fields mergeOnFields,
            SubscriberChannelsSnapshotItem.Fields updateFields,
            System.Data.IDbTransaction transaction = null)
        {
            return Dapper.SqlMapper.ExecuteAsync(
                connection, 
                UpsertStatement(mergeOnFields, updateFields), 
                new 
                {
                    entity.SubscriberChannelsSnapshotId, entity.ChannelId, entity.AcceptedPriceInUsCentsPerWeek, entity.SubscriptionStartDate
                },
                transaction);
        }
        
        public static System.Threading.Tasks.Task UpdateAsync(
            this System.Data.Common.DbConnection connection, 
            SubscriberChannelsSnapshotItem entity, 
            SubscriberChannelsSnapshotItem.Fields fields,
            System.Data.IDbTransaction transaction = null)
        {
            return Dapper.SqlMapper.ExecuteAsync(connection, UpdateStatement(fields), OnlySpecifiedParameters(entity, fields), transaction);
        }
        
        public static System.Threading.Tasks.Task<int> UpdateAsync(
            this System.Data.Common.DbConnection connection,
            SqlGenerationParameters<SubscriberChannelsSnapshotItem, SubscriberChannelsSnapshotItem.Fields> parameters)
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
            const string insert = "INSERT INTO SubscriberChannelsSnapshotItems(SubscriberChannelsSnapshotId, ChannelId, AcceptedPriceInUsCentsPerWeek, SubscriptionStartDate) VALUES(@SubscriberChannelsSnapshotId, @ChannelId, @AcceptedPriceInUsCentsPerWeek, @SubscriptionStartDate)";
            return idempotent ? SqlStatementTemplates.IdempotentInsert(insert) : insert;
        }
        
        public static string UpsertStatement(SubscriberChannelsSnapshotItem.Fields mergeOnFields, SubscriberChannelsSnapshotItem.Fields updateFields)
        {
            // HOLDLOCK ensures operation is concurrent by not releasing the U lock on the row after determining
            // it does not exist. See: http://weblogs.sqlteam.com/dang/archive/2009/01/31/UPSERT-Race-Condition-With-MERGE.aspx
            var sql = new System.Text.StringBuilder();
            sql.Append(
                @"MERGE SubscriberChannelsSnapshotItems WITH (HOLDLOCK) as Target
                USING (VALUES (@SubscriberChannelsSnapshotId, @ChannelId, @AcceptedPriceInUsCentsPerWeek, @SubscriptionStartDate)) AS Source (SubscriberChannelsSnapshotId, ChannelId, AcceptedPriceInUsCentsPerWeek, SubscriptionStartDate)
                ON    (");
                
            if (mergeOnFields == SubscriberChannelsSnapshotItem.Fields.Empty)
            {
                sql.Append(@"Target.SubscriberChannelsSnapshotId = Source.SubscriberChannelsSnapshotId AND Target.ChannelId = Source.ChannelId");
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
                    INSERT  (SubscriberChannelsSnapshotId, ChannelId, AcceptedPriceInUsCentsPerWeek, SubscriptionStartDate)
                    VALUES  (Source.SubscriberChannelsSnapshotId, Source.ChannelId, Source.AcceptedPriceInUsCentsPerWeek, Source.SubscriptionStartDate);");
            return sql.ToString();
        }
        
        public static string UpdateStatement(SubscriberChannelsSnapshotItem.Fields fields)
        {
            var sql = new System.Text.StringBuilder();
            sql.Append("UPDATE SubscriberChannelsSnapshotItems SET ");
            sql.AppendUpdateParameters(GetFieldNames(fields));
            sql.Append(" WHERE SubscriberChannelsSnapshotId = @SubscriberChannelsSnapshotId AND ChannelId = @ChannelId");
            return sql.ToString();
        }
        
        private static System.Collections.Generic.IReadOnlyList<string> GetFieldNames(SubscriberChannelsSnapshotItem.Fields fields, bool autoIncludePrimaryKeys = true)
        {
            var fieldNames = new System.Collections.Generic.List<string>();
            if (autoIncludePrimaryKeys)
            {
                fieldNames.Add("SubscriberChannelsSnapshotId");
            }
        
            if (autoIncludePrimaryKeys)
            {
                fieldNames.Add("ChannelId");
            }
        
            if (fields.HasFlag(SubscriberChannelsSnapshotItem.Fields.AcceptedPriceInUsCentsPerWeek))
            {
                fieldNames.Add("AcceptedPriceInUsCentsPerWeek");
            }
        
            if (fields.HasFlag(SubscriberChannelsSnapshotItem.Fields.SubscriptionStartDate))
            {
                fieldNames.Add("SubscriptionStartDate");
            }
        
            return fieldNames;
        }
        
        private static Dapper.DynamicParameters OnlySpecifiedParameters(
            SubscriberChannelsSnapshotItem entity, 
            SubscriberChannelsSnapshotItem.Fields fields,
            SubscriberChannelsSnapshotItem.Fields? excludedFields = null)
        {
            var parameters = new Dapper.DynamicParameters();
        
            // Assume we never want to exclude primary key field(s) from our input.
            parameters.Add("SubscriberChannelsSnapshotId", entity.SubscriberChannelsSnapshotId);
            parameters.Add("ChannelId", entity.ChannelId);
            if (fields.HasFlag(SubscriberChannelsSnapshotItem.Fields.AcceptedPriceInUsCentsPerWeek) && (excludedFields == null || !excludedFields.Value.HasFlag(SubscriberChannelsSnapshotItem.Fields.AcceptedPriceInUsCentsPerWeek)))
            {
                parameters.Add("AcceptedPriceInUsCentsPerWeek", entity.AcceptedPriceInUsCentsPerWeek);
            }
        
            if (fields.HasFlag(SubscriberChannelsSnapshotItem.Fields.SubscriptionStartDate) && (excludedFields == null || !excludedFields.Value.HasFlag(SubscriberChannelsSnapshotItem.Fields.SubscriptionStartDate)))
            {
                parameters.Add("SubscriptionStartDate", entity.SubscriptionStartDate);
            }
        
            return parameters;
        }
        
        private static Dapper.DynamicParameters AllExceptSpecifiedParameters(
            SubscriberChannelsSnapshotItem entity, 
            SubscriberChannelsSnapshotItem.Fields fields)
        {
            var parameters = new Dapper.DynamicParameters();
        
            // Assume we never want to exclude primary key field(s) from our input.
            parameters.Add("SubscriberChannelsSnapshotId", entity.SubscriberChannelsSnapshotId);
            parameters.Add("ChannelId", entity.ChannelId);
            if (!fields.HasFlag(SubscriberChannelsSnapshotItem.Fields.AcceptedPriceInUsCentsPerWeek))
            {
                parameters.Add("AcceptedPriceInUsCentsPerWeek", entity.AcceptedPriceInUsCentsPerWeek);
            }
        
            if (!fields.HasFlag(SubscriberChannelsSnapshotItem.Fields.SubscriptionStartDate))
            {
                parameters.Add("SubscriptionStartDate", entity.SubscriptionStartDate);
            }
        
            return parameters;
        }
        
    }
}
namespace Fifthweek.Api.Persistence.Snapshots
{
    using System;
    using System.Linq;
    using System.ComponentModel.DataAnnotations;
    using Fifthweek.CodeGeneration;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class SubscriberSnapshot  : IIdentityEquatable
    {
        public const string Table = "SubscriberSnapshots";
        
        public SubscriberSnapshot(
            System.DateTime timestamp,
            System.Guid subscriberId)
        {
            if (timestamp == null)
            {
                throw new ArgumentNullException("timestamp");
            }

            if (subscriberId == null)
            {
                throw new ArgumentNullException("subscriberId");
            }

            this.Timestamp = timestamp;
            this.SubscriberId = subscriberId;
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
        
            return this.IdentityEquals((SubscriberSnapshot)other);
        }
        
        protected bool IdentityEquals(SubscriberSnapshot other)
        {
            if (!object.Equals(this.Timestamp, other.Timestamp))
            {
                return false;
            }
        
            if (!object.Equals(this.SubscriberId, other.SubscriberId))
            {
                return false;
            }
        
            return true;
        }
        
        [Flags]
        public enum Fields
        {
            Empty = 0,
            Timestamp = 1, 
            SubscriberId = 2, 
            Email = 4
        }
    }

    public static partial class SubscriberSnapshotExtensions
    {
        public static System.Threading.Tasks.Task InsertAsync(
            this System.Data.Common.DbConnection connection, 
            System.Collections.Generic.IEnumerable<SubscriberSnapshot> entities, 
            bool idempotent = true, 
            System.Data.IDbTransaction transaction = null)
        {
            return Dapper.SqlMapper.ExecuteAsync(
                connection, 
                InsertStatement(idempotent), 
                entities.Select(entity => new 
                {
                    entity.Timestamp, entity.SubscriberId, entity.Email
                }).ToArray(),
                transaction);
        }
        
        public static System.Threading.Tasks.Task InsertAsync(
            this System.Data.Common.DbConnection connection, 
            SubscriberSnapshot entity,
            bool idempotent = true, 
            System.Data.IDbTransaction transaction = null)
        {
            return Dapper.SqlMapper.ExecuteAsync(
                connection, 
                InsertStatement(idempotent), 
                new 
                {
                    entity.Timestamp, entity.SubscriberId, entity.Email
                },
                transaction);
        }
        
        public static System.Threading.Tasks.Task<int> InsertAsync(
            this System.Data.Common.DbConnection connection,
            SqlGenerationParameters<SubscriberSnapshot, SubscriberSnapshot.Fields> parameters)
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
                : new Dapper.DynamicParameters(new { entity.Timestamp, entity.SubscriberId, entity.Email });
        
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
            SubscriberSnapshot entity, 
            SubscriberSnapshot.Fields fields,
            System.Data.IDbTransaction transaction = null)
        {
            return Dapper.SqlMapper.ExecuteAsync(
                connection, 
                UpsertStatement(SubscriberSnapshot.Fields.Empty, fields), 
                new 
                {
                    entity.Timestamp, entity.SubscriberId, entity.Email
                },
                transaction);
        }
        
        public static System.Threading.Tasks.Task UpsertAsync(
            this System.Data.Common.DbConnection connection, 
            SubscriberSnapshot entity, 
            SubscriberSnapshot.Fields mergeOnFields,
            SubscriberSnapshot.Fields updateFields,
            System.Data.IDbTransaction transaction = null)
        {
            return Dapper.SqlMapper.ExecuteAsync(
                connection, 
                UpsertStatement(mergeOnFields, updateFields), 
                new 
                {
                    entity.Timestamp, entity.SubscriberId, entity.Email
                },
                transaction);
        }
        
        public static System.Threading.Tasks.Task UpdateAsync(
            this System.Data.Common.DbConnection connection, 
            SubscriberSnapshot entity, 
            SubscriberSnapshot.Fields fields,
            System.Data.IDbTransaction transaction = null)
        {
            return Dapper.SqlMapper.ExecuteAsync(connection, UpdateStatement(fields), OnlySpecifiedParameters(entity, fields), transaction);
        }
        
        public static System.Threading.Tasks.Task<int> UpdateAsync(
            this System.Data.Common.DbConnection connection,
            SqlGenerationParameters<SubscriberSnapshot, SubscriberSnapshot.Fields> parameters)
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
            const string insert = "INSERT INTO SubscriberSnapshots(Timestamp, SubscriberId, Email) VALUES(@Timestamp, @SubscriberId, @Email)";
            return idempotent ? SqlStatementTemplates.IdempotentInsert(insert) : insert;
        }
        
        public static string UpsertStatement(SubscriberSnapshot.Fields mergeOnFields, SubscriberSnapshot.Fields updateFields)
        {
            // HOLDLOCK ensures operation is concurrent by not releasing the U lock on the row after determining
            // it does not exist. See: http://weblogs.sqlteam.com/dang/archive/2009/01/31/UPSERT-Race-Condition-With-MERGE.aspx
            var sql = new System.Text.StringBuilder();
            sql.Append(
                @"MERGE SubscriberSnapshots WITH (HOLDLOCK) as Target
                USING (VALUES (@Timestamp, @SubscriberId, @Email)) AS Source (Timestamp, SubscriberId, Email)
                ON    (");
                
            if (mergeOnFields == SubscriberSnapshot.Fields.Empty)
            {
                sql.Append(@"Target.Timestamp = Source.Timestamp AND Target.SubscriberId = Source.SubscriberId");
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
                    INSERT  (Timestamp, SubscriberId, Email)
                    VALUES  (Source.Timestamp, Source.SubscriberId, Source.Email);");
            return sql.ToString();
        }
        
        public static string UpdateStatement(SubscriberSnapshot.Fields fields)
        {
            var sql = new System.Text.StringBuilder();
            sql.Append("UPDATE SubscriberSnapshots SET ");
            sql.AppendUpdateParameters(GetFieldNames(fields));
            sql.Append(" WHERE Timestamp = @Timestamp AND SubscriberId = @SubscriberId");
            return sql.ToString();
        }
        
        private static System.Collections.Generic.IReadOnlyList<string> GetFieldNames(SubscriberSnapshot.Fields fields, bool autoIncludePrimaryKeys = true)
        {
            var fieldNames = new System.Collections.Generic.List<string>();
            if (autoIncludePrimaryKeys)
            {
                fieldNames.Add("Timestamp");
            }
        
            if (autoIncludePrimaryKeys)
            {
                fieldNames.Add("SubscriberId");
            }
        
            if (fields.HasFlag(SubscriberSnapshot.Fields.Email))
            {
                fieldNames.Add("Email");
            }
        
            return fieldNames;
        }
        
        private static Dapper.DynamicParameters OnlySpecifiedParameters(
            SubscriberSnapshot entity, 
            SubscriberSnapshot.Fields fields,
            SubscriberSnapshot.Fields? excludedFields = null)
        {
            var parameters = new Dapper.DynamicParameters();
        
            // Assume we never want to exclude primary key field(s) from our input.
            parameters.Add("Timestamp", entity.Timestamp);
            parameters.Add("SubscriberId", entity.SubscriberId);
            if (fields.HasFlag(SubscriberSnapshot.Fields.Email) && (excludedFields == null || !excludedFields.Value.HasFlag(SubscriberSnapshot.Fields.Email)))
            {
                parameters.Add("Email", entity.Email);
            }
        
            return parameters;
        }
        
        private static Dapper.DynamicParameters AllExceptSpecifiedParameters(
            SubscriberSnapshot entity, 
            SubscriberSnapshot.Fields fields)
        {
            var parameters = new Dapper.DynamicParameters();
        
            // Assume we never want to exclude primary key field(s) from our input.
            parameters.Add("Timestamp", entity.Timestamp);
            parameters.Add("SubscriberId", entity.SubscriberId);
            if (!fields.HasFlag(SubscriberSnapshot.Fields.Email))
            {
                parameters.Add("Email", entity.Email);
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
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.CodeGeneration;

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
    using System.ComponentModel.DataAnnotations.Schema;

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
    using System.ComponentModel.DataAnnotations.Schema;

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
            Email = 32, 
            Name = 64, 
            Id = 128, 
            AccessFailedCount = 256, 
            EmailConfirmed = 512, 
            LockoutEnabled = 1024, 
            LockoutEndDateUtc = 2048, 
            PasswordHash = 4096, 
            PhoneNumber = 8192, 
            PhoneNumberConfirmed = 16384, 
            SecurityStamp = 32768, 
            TwoFactorEnabled = 65536, 
            UserName = 131072
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
                    entity.ExampleWork, entity.RegistrationDate, entity.LastSignInDate, entity.LastAccessTokenDate, entity.ProfileImageFileId, entity.Email, entity.Name, entity.Id, entity.AccessFailedCount, entity.EmailConfirmed, entity.LockoutEnabled, entity.LockoutEndDateUtc, entity.PasswordHash, entity.PhoneNumber, entity.PhoneNumberConfirmed, entity.SecurityStamp, entity.TwoFactorEnabled, entity.UserName
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
                    entity.ExampleWork, entity.RegistrationDate, entity.LastSignInDate, entity.LastAccessTokenDate, entity.ProfileImageFileId, entity.Email, entity.Name, entity.Id, entity.AccessFailedCount, entity.EmailConfirmed, entity.LockoutEnabled, entity.LockoutEndDateUtc, entity.PasswordHash, entity.PhoneNumber, entity.PhoneNumberConfirmed, entity.SecurityStamp, entity.TwoFactorEnabled, entity.UserName
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
                : new Dapper.DynamicParameters(new { entity.ExampleWork, entity.RegistrationDate, entity.LastSignInDate, entity.LastAccessTokenDate, entity.ProfileImageFileId, entity.Email, entity.Name, entity.Id, entity.AccessFailedCount, entity.EmailConfirmed, entity.LockoutEnabled, entity.LockoutEndDateUtc, entity.PasswordHash, entity.PhoneNumber, entity.PhoneNumberConfirmed, entity.SecurityStamp, entity.TwoFactorEnabled, entity.UserName });
        
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
                    entity.ExampleWork, entity.RegistrationDate, entity.LastSignInDate, entity.LastAccessTokenDate, entity.ProfileImageFileId, entity.Email, entity.Name, entity.Id, entity.AccessFailedCount, entity.EmailConfirmed, entity.LockoutEnabled, entity.LockoutEndDateUtc, entity.PasswordHash, entity.PhoneNumber, entity.PhoneNumberConfirmed, entity.SecurityStamp, entity.TwoFactorEnabled, entity.UserName
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
                    entity.ExampleWork, entity.RegistrationDate, entity.LastSignInDate, entity.LastAccessTokenDate, entity.ProfileImageFileId, entity.Email, entity.Name, entity.Id, entity.AccessFailedCount, entity.EmailConfirmed, entity.LockoutEnabled, entity.LockoutEndDateUtc, entity.PasswordHash, entity.PhoneNumber, entity.PhoneNumberConfirmed, entity.SecurityStamp, entity.TwoFactorEnabled, entity.UserName
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
            const string insert = "INSERT INTO AspNetUsers(ExampleWork, RegistrationDate, LastSignInDate, LastAccessTokenDate, ProfileImageFileId, Email, Name, Id, AccessFailedCount, EmailConfirmed, LockoutEnabled, LockoutEndDateUtc, PasswordHash, PhoneNumber, PhoneNumberConfirmed, SecurityStamp, TwoFactorEnabled, UserName) VALUES(@ExampleWork, @RegistrationDate, @LastSignInDate, @LastAccessTokenDate, @ProfileImageFileId, @Email, @Name, @Id, @AccessFailedCount, @EmailConfirmed, @LockoutEnabled, @LockoutEndDateUtc, @PasswordHash, @PhoneNumber, @PhoneNumberConfirmed, @SecurityStamp, @TwoFactorEnabled, @UserName)";
            return idempotent ? SqlStatementTemplates.IdempotentInsert(insert) : insert;
        }
        
        public static string UpsertStatement(FifthweekUser.Fields mergeOnFields, FifthweekUser.Fields updateFields)
        {
            // HOLDLOCK ensures operation is concurrent by not releasing the U lock on the row after determining
            // it does not exist. See: http://weblogs.sqlteam.com/dang/archive/2009/01/31/UPSERT-Race-Condition-With-MERGE.aspx
            var sql = new System.Text.StringBuilder();
            sql.Append(
                @"MERGE AspNetUsers WITH (HOLDLOCK) as Target
                USING (VALUES (@ExampleWork, @RegistrationDate, @LastSignInDate, @LastAccessTokenDate, @ProfileImageFileId, @Email, @Name, @Id, @AccessFailedCount, @EmailConfirmed, @LockoutEnabled, @LockoutEndDateUtc, @PasswordHash, @PhoneNumber, @PhoneNumberConfirmed, @SecurityStamp, @TwoFactorEnabled, @UserName)) AS Source (ExampleWork, RegistrationDate, LastSignInDate, LastAccessTokenDate, ProfileImageFileId, Email, Name, Id, AccessFailedCount, EmailConfirmed, LockoutEnabled, LockoutEndDateUtc, PasswordHash, PhoneNumber, PhoneNumberConfirmed, SecurityStamp, TwoFactorEnabled, UserName)
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
                    INSERT  (ExampleWork, RegistrationDate, LastSignInDate, LastAccessTokenDate, ProfileImageFileId, Email, Name, Id, AccessFailedCount, EmailConfirmed, LockoutEnabled, LockoutEndDateUtc, PasswordHash, PhoneNumber, PhoneNumberConfirmed, SecurityStamp, TwoFactorEnabled, UserName)
                    VALUES  (Source.ExampleWork, Source.RegistrationDate, Source.LastSignInDate, Source.LastAccessTokenDate, Source.ProfileImageFileId, Source.Email, Source.Name, Source.Id, Source.AccessFailedCount, Source.EmailConfirmed, Source.LockoutEnabled, Source.LockoutEndDateUtc, Source.PasswordHash, Source.PhoneNumber, Source.PhoneNumberConfirmed, Source.SecurityStamp, Source.TwoFactorEnabled, Source.UserName);");
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
        
            if (fields.HasFlag(FifthweekUser.Fields.Email))
            {
                fieldNames.Add("Email");
            }
        
            if (fields.HasFlag(FifthweekUser.Fields.Name))
            {
                fieldNames.Add("Name");
            }
        
            if (autoIncludePrimaryKeys)
            {
                fieldNames.Add("Id");
            }
        
            if (fields.HasFlag(FifthweekUser.Fields.AccessFailedCount))
            {
                fieldNames.Add("AccessFailedCount");
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
        
            if (fields.HasFlag(FifthweekUser.Fields.Email) && (excludedFields == null || !excludedFields.Value.HasFlag(FifthweekUser.Fields.Email)))
            {
                parameters.Add("Email", entity.Email);
            }
        
            if (fields.HasFlag(FifthweekUser.Fields.Name) && (excludedFields == null || !excludedFields.Value.HasFlag(FifthweekUser.Fields.Name)))
            {
                parameters.Add("Name", entity.Name);
            }
        
            parameters.Add("Id", entity.Id);
            if (fields.HasFlag(FifthweekUser.Fields.AccessFailedCount) && (excludedFields == null || !excludedFields.Value.HasFlag(FifthweekUser.Fields.AccessFailedCount)))
            {
                parameters.Add("AccessFailedCount", entity.AccessFailedCount);
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
        
            if (!fields.HasFlag(FifthweekUser.Fields.Email))
            {
                parameters.Add("Email", entity.Email);
            }
        
            if (!fields.HasFlag(FifthweekUser.Fields.Name))
            {
                parameters.Add("Name", entity.Name);
            }
        
            parameters.Add("Id", entity.Id);
            if (!fields.HasFlag(FifthweekUser.Fields.AccessFailedCount))
            {
                parameters.Add("AccessFailedCount", entity.AccessFailedCount);
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
    using System.ComponentModel.DataAnnotations.Schema;

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

