using System;
using System.Linq;



namespace Fifthweek.Api.Persistence
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Fifthweek.Api.Core;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using System.Linq;
    using Fifthweek.Api.Persistence.Identity;
    public partial class Channel 
    {
        public Channel(
            System.Guid id, 
            System.Guid subscriptionId, 
            Fifthweek.Api.Persistence.Subscription subscription, 
            System.Int32 priceInUsCentsPerWeek, 
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

            if (creationDate == null)
            {
                throw new ArgumentNullException("creationDate");
            }

            this.Id = id;
            this.SubscriptionId = subscriptionId;
            this.Subscription = subscription;
            this.PriceInUsCentsPerWeek = priceInUsCentsPerWeek;
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
    using Fifthweek.Api.Core;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using Fifthweek.Api.Persistence.Identity;
    public partial class Collection 
    {
        public Collection(
            System.Guid id, 
            System.Guid channelId, 
            Fifthweek.Api.Persistence.Channel channel, 
            System.String name)
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

            this.Id = id;
            this.ChannelId = channelId;
            this.Channel = channel;
            this.Name = name;
        }
    }

}
namespace Fifthweek.Api.Persistence
{
    using System;
    using System.Linq;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Fifthweek.Api.Core;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
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
    using System.Linq;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Fifthweek.Api.Core;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using Fifthweek.Api.Persistence.Identity;
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
            System.Nullable<System.Int32> queuePosition, 
            System.Nullable<System.DateTime> liveDate, 
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
            this.QueuePosition = queuePosition;
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
    using Fifthweek.Api.Core;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using Fifthweek.Api.Persistence.Identity;
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
    using Fifthweek.Api.Core;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using Fifthweek.Api.Persistence.Identity;
    public partial class WeeklyReleaseTime 
    {
        public WeeklyReleaseTime(
            System.Guid collectionId, 
            Fifthweek.Api.Persistence.Collection collection, 
            System.Byte dayOfWeek, 
            System.TimeSpan timeOfDay)
        {
            if (collectionId == null)
            {
                throw new ArgumentNullException("collectionId");
            }

            if (dayOfWeek == null)
            {
                throw new ArgumentNullException("dayOfWeek");
            }

            if (timeOfDay == null)
            {
                throw new ArgumentNullException("timeOfDay");
            }

            this.CollectionId = collectionId;
            this.Collection = collection;
            this.DayOfWeek = dayOfWeek;
            this.TimeOfDay = timeOfDay;
        }
    }

}

namespace Fifthweek.Api.Persistence
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Fifthweek.Api.Core;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using System.Linq;
    using Fifthweek.Api.Persistence.Identity;
    public partial class Channel 
    {
        public override string ToString()
        {
            return string.Format("Channel({0}, {1}, {2}, {3})", this.Id == null ? "null" : this.Id.ToString(), this.SubscriptionId == null ? "null" : this.SubscriptionId.ToString(), this.PriceInUsCentsPerWeek == null ? "null" : this.PriceInUsCentsPerWeek.ToString(), this.CreationDate == null ? "null" : this.CreationDate.ToString());
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
                hashCode = (hashCode * 397) ^ (this.PriceInUsCentsPerWeek != null ? this.PriceInUsCentsPerWeek.GetHashCode() : 0);
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

            if (!object.Equals(this.PriceInUsCentsPerWeek, other.PriceInUsCentsPerWeek))
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
    using Fifthweek.Api.Core;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using Fifthweek.Api.Persistence.Identity;
    public partial class Collection 
    {
        public override string ToString()
        {
            return string.Format("Collection({0}, {1}, \"{2}\")", this.Id == null ? "null" : this.Id.ToString(), this.ChannelId == null ? "null" : this.ChannelId.ToString(), this.Name == null ? "null" : this.Name.ToString());
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
    using Fifthweek.Api.Core;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using Fifthweek.Api.Persistence.Identity;
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
    using Fifthweek.Api.Core;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using Fifthweek.Api.Persistence.Identity;
    public partial class Post 
    {
        public override string ToString()
        {
            return string.Format("Post({0}, {1}, {2}, {3}, {4}, \"{5}\", {6}, {7}, {8})", this.Id == null ? "null" : this.Id.ToString(), this.ChannelId == null ? "null" : this.ChannelId.ToString(), this.CollectionId == null ? "null" : this.CollectionId.ToString(), this.FileId == null ? "null" : this.FileId.ToString(), this.ImageId == null ? "null" : this.ImageId.ToString(), this.Comment == null ? "null" : this.Comment.ToString(), this.QueuePosition == null ? "null" : this.QueuePosition.ToString(), this.LiveDate == null ? "null" : this.LiveDate.ToString(), this.CreationDate == null ? "null" : this.CreationDate.ToString());
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
                hashCode = (hashCode * 397) ^ (this.QueuePosition != null ? this.QueuePosition.GetHashCode() : 0);
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

            if (!object.Equals(this.QueuePosition, other.QueuePosition))
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
    using Fifthweek.Api.Core;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using Fifthweek.Api.Persistence.Identity;
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
    using Fifthweek.Api.Core;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using Fifthweek.Api.Persistence.Identity;
    public partial class WeeklyReleaseTime 
    {
        public override string ToString()
        {
            return string.Format("WeeklyReleaseTime({0}, {1}, {2})", this.CollectionId == null ? "null" : this.CollectionId.ToString(), this.DayOfWeek == null ? "null" : this.DayOfWeek.ToString(), this.TimeOfDay == null ? "null" : this.TimeOfDay.ToString());
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
                hashCode = (hashCode * 397) ^ (this.DayOfWeek != null ? this.DayOfWeek.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.TimeOfDay != null ? this.TimeOfDay.GetHashCode() : 0);
                return hashCode;
            }
        }

        protected bool Equals(WeeklyReleaseTime other)
        {
            if (!object.Equals(this.CollectionId, other.CollectionId))
            {
                return false;
            }

            if (!object.Equals(this.DayOfWeek, other.DayOfWeek))
            {
                return false;
            }

            if (!object.Equals(this.TimeOfDay, other.TimeOfDay))
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
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Fifthweek.CodeGeneration;
    using Microsoft.AspNet.Identity.EntityFramework;
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
namespace Fifthweek.Api.Persistence
{
    using System;
    using System.Linq;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Fifthweek.Api.Core;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using Fifthweek.Api.Persistence.Identity;
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
            copy.QueuePosition = this.QueuePosition;
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
    using Fifthweek.Api.Core;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
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
            SubscriptionId = 2, 
            PriceInUsCentsPerWeek = 4, 
            CreationDate = 8
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
                    entity.Id, entity.SubscriptionId, entity.PriceInUsCentsPerWeek, entity.CreationDate
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
                    entity.Id, entity.SubscriptionId, entity.PriceInUsCentsPerWeek, entity.CreationDate
                },
                transaction);
        }

        public static System.Threading.Tasks.Task InsertAsync(
            this System.Data.Common.DbConnection connection,
            Channel entity,
            string selectValuesForInsertStatement,
            Channel.Fields selectedFields,
            bool idempotent = true,
            System.Data.IDbTransaction transaction = null)
        {
            return Dapper.SqlMapper.ExecuteAsync(
                connection,
                selectValuesForInsertStatement + System.Environment.NewLine + InsertStatement(idempotent),
                MaskParameters(entity, selectedFields, true),
                transaction);
        }

        public static System.Threading.Tasks.Task UpsertAsync(
            this System.Data.Common.DbConnection connection, 
            Channel entity, 
            Channel.Fields fields,
            System.Data.IDbTransaction transaction = null)
        {
            return Dapper.SqlMapper.ExecuteAsync(
                connection, 
                UpsertStatement(fields), 
                new 
                {
                    entity.Id, entity.SubscriptionId, entity.PriceInUsCentsPerWeek, entity.CreationDate
                },
                transaction);
        }

        public static System.Threading.Tasks.Task UpdateAsync(
            this System.Data.Common.DbConnection connection, 
            Channel entity, 
            Channel.Fields fields,
            System.Data.IDbTransaction transaction = null)
        {
            return Dapper.SqlMapper.ExecuteAsync(connection, UpdateStatement(fields), MaskParameters(entity, fields, false), transaction);
        }

        public static string InsertStatement(bool idempotent = true)
        {
            const string insert = "INSERT INTO Channels(Id, SubscriptionId, PriceInUsCentsPerWeek, CreationDate) VALUES(@Id, @SubscriptionId, @PriceInUsCentsPerWeek, @CreationDate)";
            return idempotent ? SqlStatementTemplates.IdempotentInsert(insert) : insert;
        }

        public static string UpsertStatement(Channel.Fields fields)
        {
            var sql = new System.Text.StringBuilder();
            sql.Append(
                @"MERGE Channels as Target
                USING (VALUES (@Id, @SubscriptionId, @PriceInUsCentsPerWeek, @CreationDate)) AS Source (Id, SubscriptionId, PriceInUsCentsPerWeek, CreationDate)
                ON    (Target.Id = Source.Id)
                WHEN MATCHED THEN
                    UPDATE
                    SET        ");
            sql.AppendUpdateParameters(GetFieldNames(fields));
            sql.Append(
                @" WHEN NOT MATCHED THEN
                    INSERT  (Id, SubscriptionId, PriceInUsCentsPerWeek, CreationDate)
                    VALUES  (Source.Id, Source.SubscriptionId, Source.PriceInUsCentsPerWeek, Source.CreationDate);");
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

            if (fields.HasFlag(Channel.Fields.PriceInUsCentsPerWeek))
            {
                fieldNames.Add("PriceInUsCentsPerWeek");
            }

            if (fields.HasFlag(Channel.Fields.CreationDate))
            {
                fieldNames.Add("CreationDate");
            }

            return fieldNames;
        }

        private static object MaskParameters(Channel entity, Channel.Fields fields, bool excludeSpecified)
        {
            var parameters = new Dapper.DynamicParameters();

			// Assume we never want to exclude primary key field(s) from our input.
			parameters.Add("Id", entity.Id);
            if(excludeSpecified != fields.HasFlag(Channel.Fields.SubscriptionId))
            {
                parameters.Add("SubscriptionId", entity.SubscriptionId);
            }

            if(excludeSpecified != fields.HasFlag(Channel.Fields.PriceInUsCentsPerWeek))
            {
                parameters.Add("PriceInUsCentsPerWeek", entity.PriceInUsCentsPerWeek);
            }

            if(excludeSpecified != fields.HasFlag(Channel.Fields.CreationDate))
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
    using Fifthweek.Api.Core;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using Fifthweek.Api.Persistence.Identity;
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
            Name = 4
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
                    entity.Id, entity.ChannelId, entity.Name
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
                    entity.Id, entity.ChannelId, entity.Name
                },
                transaction);
        }

        public static System.Threading.Tasks.Task InsertAsync(
            this System.Data.Common.DbConnection connection,
            Collection entity,
            string selectValuesForInsertStatement,
            Collection.Fields selectedFields,
            bool idempotent = true,
            System.Data.IDbTransaction transaction = null)
        {
            return Dapper.SqlMapper.ExecuteAsync(
                connection,
                selectValuesForInsertStatement + System.Environment.NewLine + InsertStatement(idempotent),
                MaskParameters(entity, selectedFields, true),
                transaction);
        }

        public static System.Threading.Tasks.Task UpsertAsync(
            this System.Data.Common.DbConnection connection, 
            Collection entity, 
            Collection.Fields fields,
            System.Data.IDbTransaction transaction = null)
        {
            return Dapper.SqlMapper.ExecuteAsync(
                connection, 
                UpsertStatement(fields), 
                new 
                {
                    entity.Id, entity.ChannelId, entity.Name
                },
                transaction);
        }

        public static System.Threading.Tasks.Task UpdateAsync(
            this System.Data.Common.DbConnection connection, 
            Collection entity, 
            Collection.Fields fields,
            System.Data.IDbTransaction transaction = null)
        {
            return Dapper.SqlMapper.ExecuteAsync(connection, UpdateStatement(fields), MaskParameters(entity, fields, false), transaction);
        }

        public static string InsertStatement(bool idempotent = true)
        {
            const string insert = "INSERT INTO Collections(Id, ChannelId, Name) VALUES(@Id, @ChannelId, @Name)";
            return idempotent ? SqlStatementTemplates.IdempotentInsert(insert) : insert;
        }

        public static string UpsertStatement(Collection.Fields fields)
        {
            var sql = new System.Text.StringBuilder();
            sql.Append(
                @"MERGE Collections as Target
                USING (VALUES (@Id, @ChannelId, @Name)) AS Source (Id, ChannelId, Name)
                ON    (Target.Id = Source.Id)
                WHEN MATCHED THEN
                    UPDATE
                    SET        ");
            sql.AppendUpdateParameters(GetFieldNames(fields));
            sql.Append(
                @" WHEN NOT MATCHED THEN
                    INSERT  (Id, ChannelId, Name)
                    VALUES  (Source.Id, Source.ChannelId, Source.Name);");
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

            return fieldNames;
        }

        private static object MaskParameters(Collection entity, Collection.Fields fields, bool excludeSpecified)
        {
            var parameters = new Dapper.DynamicParameters();

			// Assume we never want to exclude primary key field(s) from our input.
			parameters.Add("Id", entity.Id);
            if(excludeSpecified != fields.HasFlag(Collection.Fields.ChannelId))
            {
                parameters.Add("ChannelId", entity.ChannelId);
            }

            if(excludeSpecified != fields.HasFlag(Collection.Fields.Name))
            {
                parameters.Add("Name", entity.Name);
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
    using Fifthweek.Api.Core;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using Fifthweek.Api.Persistence.Identity;
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

        public static System.Threading.Tasks.Task InsertAsync(
            this System.Data.Common.DbConnection connection,
            File entity,
            string selectValuesForInsertStatement,
            File.Fields selectedFields,
            bool idempotent = true,
            System.Data.IDbTransaction transaction = null)
        {
            return Dapper.SqlMapper.ExecuteAsync(
                connection,
                selectValuesForInsertStatement + System.Environment.NewLine + InsertStatement(idempotent),
                MaskParameters(entity, selectedFields, true),
                transaction);
        }

        public static System.Threading.Tasks.Task UpsertAsync(
            this System.Data.Common.DbConnection connection, 
            File entity, 
            File.Fields fields,
            System.Data.IDbTransaction transaction = null)
        {
            return Dapper.SqlMapper.ExecuteAsync(
                connection, 
                UpsertStatement(fields), 
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
            return Dapper.SqlMapper.ExecuteAsync(connection, UpdateStatement(fields), MaskParameters(entity, fields, false), transaction);
        }

        public static string InsertStatement(bool idempotent = true)
        {
            const string insert = "INSERT INTO Files(Id, UserId, State, UploadStartedDate, UploadCompletedDate, ProcessingStartedDate, ProcessingCompletedDate, FileNameWithoutExtension, FileExtension, BlobSizeBytes, Purpose) VALUES(@Id, @UserId, @State, @UploadStartedDate, @UploadCompletedDate, @ProcessingStartedDate, @ProcessingCompletedDate, @FileNameWithoutExtension, @FileExtension, @BlobSizeBytes, @Purpose)";
            return idempotent ? SqlStatementTemplates.IdempotentInsert(insert) : insert;
        }

        public static string UpsertStatement(File.Fields fields)
        {
            var sql = new System.Text.StringBuilder();
            sql.Append(
                @"MERGE Files as Target
                USING (VALUES (@Id, @UserId, @State, @UploadStartedDate, @UploadCompletedDate, @ProcessingStartedDate, @ProcessingCompletedDate, @FileNameWithoutExtension, @FileExtension, @BlobSizeBytes, @Purpose)) AS Source (Id, UserId, State, UploadStartedDate, UploadCompletedDate, ProcessingStartedDate, ProcessingCompletedDate, FileNameWithoutExtension, FileExtension, BlobSizeBytes, Purpose)
                ON    (Target.Id = Source.Id)
                WHEN MATCHED THEN
                    UPDATE
                    SET        ");
            sql.AppendUpdateParameters(GetFieldNames(fields));
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

        private static object MaskParameters(File entity, File.Fields fields, bool excludeSpecified)
        {
            var parameters = new Dapper.DynamicParameters();

			// Assume we never want to exclude primary key field(s) from our input.
			parameters.Add("Id", entity.Id);
            if(excludeSpecified != fields.HasFlag(File.Fields.UserId))
            {
                parameters.Add("UserId", entity.UserId);
            }

            if(excludeSpecified != fields.HasFlag(File.Fields.State))
            {
                parameters.Add("State", entity.State);
            }

            if(excludeSpecified != fields.HasFlag(File.Fields.UploadStartedDate))
            {
                parameters.Add("UploadStartedDate", entity.UploadStartedDate);
            }

            if(excludeSpecified != fields.HasFlag(File.Fields.UploadCompletedDate))
            {
                parameters.Add("UploadCompletedDate", entity.UploadCompletedDate);
            }

            if(excludeSpecified != fields.HasFlag(File.Fields.ProcessingStartedDate))
            {
                parameters.Add("ProcessingStartedDate", entity.ProcessingStartedDate);
            }

            if(excludeSpecified != fields.HasFlag(File.Fields.ProcessingCompletedDate))
            {
                parameters.Add("ProcessingCompletedDate", entity.ProcessingCompletedDate);
            }

            if(excludeSpecified != fields.HasFlag(File.Fields.FileNameWithoutExtension))
            {
                parameters.Add("FileNameWithoutExtension", entity.FileNameWithoutExtension);
            }

            if(excludeSpecified != fields.HasFlag(File.Fields.FileExtension))
            {
                parameters.Add("FileExtension", entity.FileExtension);
            }

            if(excludeSpecified != fields.HasFlag(File.Fields.BlobSizeBytes))
            {
                parameters.Add("BlobSizeBytes", entity.BlobSizeBytes);
            }

            if(excludeSpecified != fields.HasFlag(File.Fields.Purpose))
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
    using Fifthweek.Api.Core;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using Fifthweek.Api.Persistence.Identity;
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
            QueuePosition = 64, 
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
                    entity.Id, entity.ChannelId, entity.CollectionId, entity.FileId, entity.ImageId, entity.Comment, entity.QueuePosition, entity.LiveDate, entity.CreationDate
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
                    entity.Id, entity.ChannelId, entity.CollectionId, entity.FileId, entity.ImageId, entity.Comment, entity.QueuePosition, entity.LiveDate, entity.CreationDate
                },
                transaction);
        }

        public static System.Threading.Tasks.Task InsertAsync(
            this System.Data.Common.DbConnection connection,
            Post entity,
            string selectValuesForInsertStatement,
            Post.Fields selectedFields,
            bool idempotent = true,
            System.Data.IDbTransaction transaction = null)
        {
            return Dapper.SqlMapper.ExecuteAsync(
                connection,
                selectValuesForInsertStatement + System.Environment.NewLine + InsertStatement(idempotent),
                MaskParameters(entity, selectedFields, true),
                transaction);
        }

        public static System.Threading.Tasks.Task UpsertAsync(
            this System.Data.Common.DbConnection connection, 
            Post entity, 
            Post.Fields fields,
            System.Data.IDbTransaction transaction = null)
        {
            return Dapper.SqlMapper.ExecuteAsync(
                connection, 
                UpsertStatement(fields), 
                new 
                {
                    entity.Id, entity.ChannelId, entity.CollectionId, entity.FileId, entity.ImageId, entity.Comment, entity.QueuePosition, entity.LiveDate, entity.CreationDate
                },
                transaction);
        }

        public static System.Threading.Tasks.Task UpdateAsync(
            this System.Data.Common.DbConnection connection, 
            Post entity, 
            Post.Fields fields,
            System.Data.IDbTransaction transaction = null)
        {
            return Dapper.SqlMapper.ExecuteAsync(connection, UpdateStatement(fields), MaskParameters(entity, fields, false), transaction);
        }

        public static string InsertStatement(bool idempotent = true)
        {
            const string insert = "INSERT INTO Posts(Id, ChannelId, CollectionId, FileId, ImageId, Comment, QueuePosition, LiveDate, CreationDate) VALUES(@Id, @ChannelId, @CollectionId, @FileId, @ImageId, @Comment, @QueuePosition, @LiveDate, @CreationDate)";
            return idempotent ? SqlStatementTemplates.IdempotentInsert(insert) : insert;
        }

        public static string UpsertStatement(Post.Fields fields)
        {
            var sql = new System.Text.StringBuilder();
            sql.Append(
                @"MERGE Posts as Target
                USING (VALUES (@Id, @ChannelId, @CollectionId, @FileId, @ImageId, @Comment, @QueuePosition, @LiveDate, @CreationDate)) AS Source (Id, ChannelId, CollectionId, FileId, ImageId, Comment, QueuePosition, LiveDate, CreationDate)
                ON    (Target.Id = Source.Id)
                WHEN MATCHED THEN
                    UPDATE
                    SET        ");
            sql.AppendUpdateParameters(GetFieldNames(fields));
            sql.Append(
                @" WHEN NOT MATCHED THEN
                    INSERT  (Id, ChannelId, CollectionId, FileId, ImageId, Comment, QueuePosition, LiveDate, CreationDate)
                    VALUES  (Source.Id, Source.ChannelId, Source.CollectionId, Source.FileId, Source.ImageId, Source.Comment, Source.QueuePosition, Source.LiveDate, Source.CreationDate);");
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

            if (fields.HasFlag(Post.Fields.QueuePosition))
            {
                fieldNames.Add("QueuePosition");
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

        private static object MaskParameters(Post entity, Post.Fields fields, bool excludeSpecified)
        {
            var parameters = new Dapper.DynamicParameters();

			// Assume we never want to exclude primary key field(s) from our input.
			parameters.Add("Id", entity.Id);
            if(excludeSpecified != fields.HasFlag(Post.Fields.ChannelId))
            {
                parameters.Add("ChannelId", entity.ChannelId);
            }

            if(excludeSpecified != fields.HasFlag(Post.Fields.CollectionId))
            {
                parameters.Add("CollectionId", entity.CollectionId);
            }

            if(excludeSpecified != fields.HasFlag(Post.Fields.FileId))
            {
                parameters.Add("FileId", entity.FileId);
            }

            if(excludeSpecified != fields.HasFlag(Post.Fields.ImageId))
            {
                parameters.Add("ImageId", entity.ImageId);
            }

            if(excludeSpecified != fields.HasFlag(Post.Fields.Comment))
            {
                parameters.Add("Comment", entity.Comment);
            }

            if(excludeSpecified != fields.HasFlag(Post.Fields.QueuePosition))
            {
                parameters.Add("QueuePosition", entity.QueuePosition);
            }

            if(excludeSpecified != fields.HasFlag(Post.Fields.LiveDate))
            {
                parameters.Add("LiveDate", entity.LiveDate);
            }

            if(excludeSpecified != fields.HasFlag(Post.Fields.CreationDate))
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
    using Fifthweek.Api.Core;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using Fifthweek.Api.Persistence.Identity;
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

        public static System.Threading.Tasks.Task InsertAsync(
            this System.Data.Common.DbConnection connection,
            Subscription entity,
            string selectValuesForInsertStatement,
            Subscription.Fields selectedFields,
            bool idempotent = true,
            System.Data.IDbTransaction transaction = null)
        {
            return Dapper.SqlMapper.ExecuteAsync(
                connection,
                selectValuesForInsertStatement + System.Environment.NewLine + InsertStatement(idempotent),
                MaskParameters(entity, selectedFields, true),
                transaction);
        }

        public static System.Threading.Tasks.Task UpsertAsync(
            this System.Data.Common.DbConnection connection, 
            Subscription entity, 
            Subscription.Fields fields,
            System.Data.IDbTransaction transaction = null)
        {
            return Dapper.SqlMapper.ExecuteAsync(
                connection, 
                UpsertStatement(fields), 
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
            return Dapper.SqlMapper.ExecuteAsync(connection, UpdateStatement(fields), MaskParameters(entity, fields, false), transaction);
        }

        public static string InsertStatement(bool idempotent = true)
        {
            const string insert = "INSERT INTO Subscriptions(Id, CreatorId, Name, Tagline, Introduction, Description, ExternalVideoUrl, HeaderImageFileId, CreationDate) VALUES(@Id, @CreatorId, @Name, @Tagline, @Introduction, @Description, @ExternalVideoUrl, @HeaderImageFileId, @CreationDate)";
            return idempotent ? SqlStatementTemplates.IdempotentInsert(insert) : insert;
        }

        public static string UpsertStatement(Subscription.Fields fields)
        {
            var sql = new System.Text.StringBuilder();
            sql.Append(
                @"MERGE Subscriptions as Target
                USING (VALUES (@Id, @CreatorId, @Name, @Tagline, @Introduction, @Description, @ExternalVideoUrl, @HeaderImageFileId, @CreationDate)) AS Source (Id, CreatorId, Name, Tagline, Introduction, Description, ExternalVideoUrl, HeaderImageFileId, CreationDate)
                ON    (Target.Id = Source.Id)
                WHEN MATCHED THEN
                    UPDATE
                    SET        ");
            sql.AppendUpdateParameters(GetFieldNames(fields));
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

        private static object MaskParameters(Subscription entity, Subscription.Fields fields, bool excludeSpecified)
        {
            var parameters = new Dapper.DynamicParameters();

			// Assume we never want to exclude primary key field(s) from our input.
			parameters.Add("Id", entity.Id);
            if(excludeSpecified != fields.HasFlag(Subscription.Fields.CreatorId))
            {
                parameters.Add("CreatorId", entity.CreatorId);
            }

            if(excludeSpecified != fields.HasFlag(Subscription.Fields.Name))
            {
                parameters.Add("Name", entity.Name);
            }

            if(excludeSpecified != fields.HasFlag(Subscription.Fields.Tagline))
            {
                parameters.Add("Tagline", entity.Tagline);
            }

            if(excludeSpecified != fields.HasFlag(Subscription.Fields.Introduction))
            {
                parameters.Add("Introduction", entity.Introduction);
            }

            if(excludeSpecified != fields.HasFlag(Subscription.Fields.Description))
            {
                parameters.Add("Description", entity.Description);
            }

            if(excludeSpecified != fields.HasFlag(Subscription.Fields.ExternalVideoUrl))
            {
                parameters.Add("ExternalVideoUrl", entity.ExternalVideoUrl);
            }

            if(excludeSpecified != fields.HasFlag(Subscription.Fields.HeaderImageFileId))
            {
                parameters.Add("HeaderImageFileId", entity.HeaderImageFileId);
            }

            if(excludeSpecified != fields.HasFlag(Subscription.Fields.CreationDate))
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
    using Fifthweek.Api.Core;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using Fifthweek.Api.Persistence.Identity;
    public partial class WeeklyReleaseTime  : IIdentityEquatable
    {
		public const string Table = "WeeklyReleaseTimes";

        public WeeklyReleaseTime(
            System.Guid collectionId, 
            System.Byte dayOfWeek, 
            System.TimeSpan timeOfDay)
        {
            if (collectionId == null)
            {
                throw new ArgumentNullException("collectionId");
            }

            if (dayOfWeek == null)
            {
                throw new ArgumentNullException("dayOfWeek");
            }

            if (timeOfDay == null)
            {
                throw new ArgumentNullException("timeOfDay");
            }

            this.CollectionId = collectionId;
            this.DayOfWeek = dayOfWeek;
            this.TimeOfDay = timeOfDay;
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

            if (!object.Equals(this.DayOfWeek, other.DayOfWeek))
            {
                return false;
            }

            if (!object.Equals(this.TimeOfDay, other.TimeOfDay))
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
            DayOfWeek = 2, 
            TimeOfDay = 4
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
                    entity.CollectionId, entity.DayOfWeek, entity.TimeOfDay
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
                    entity.CollectionId, entity.DayOfWeek, entity.TimeOfDay
                },
                transaction);
        }

        public static System.Threading.Tasks.Task InsertAsync(
            this System.Data.Common.DbConnection connection,
            WeeklyReleaseTime entity,
            string selectValuesForInsertStatement,
            WeeklyReleaseTime.Fields selectedFields,
            bool idempotent = true,
            System.Data.IDbTransaction transaction = null)
        {
            return Dapper.SqlMapper.ExecuteAsync(
                connection,
                selectValuesForInsertStatement + System.Environment.NewLine + InsertStatement(idempotent),
                MaskParameters(entity, selectedFields, true),
                transaction);
        }

        public static System.Threading.Tasks.Task UpsertAsync(
            this System.Data.Common.DbConnection connection, 
            WeeklyReleaseTime entity, 
            WeeklyReleaseTime.Fields fields,
            System.Data.IDbTransaction transaction = null)
        {
            return Dapper.SqlMapper.ExecuteAsync(
                connection, 
                UpsertStatement(fields), 
                new 
                {
                    entity.CollectionId, entity.DayOfWeek, entity.TimeOfDay
                },
                transaction);
        }

        public static System.Threading.Tasks.Task UpdateAsync(
            this System.Data.Common.DbConnection connection, 
            WeeklyReleaseTime entity, 
            WeeklyReleaseTime.Fields fields,
            System.Data.IDbTransaction transaction = null)
        {
            return Dapper.SqlMapper.ExecuteAsync(connection, UpdateStatement(fields), MaskParameters(entity, fields, false), transaction);
        }

        public static string InsertStatement(bool idempotent = true)
        {
            const string insert = "INSERT INTO WeeklyReleaseTimes(CollectionId, DayOfWeek, TimeOfDay) VALUES(@CollectionId, @DayOfWeek, @TimeOfDay)";
            return idempotent ? SqlStatementTemplates.IdempotentInsert(insert) : insert;
        }

        public static string UpsertStatement(WeeklyReleaseTime.Fields fields)
        {
            var sql = new System.Text.StringBuilder();
            sql.Append(
                @"MERGE WeeklyReleaseTimes as Target
                USING (VALUES (@CollectionId, @DayOfWeek, @TimeOfDay)) AS Source (CollectionId, DayOfWeek, TimeOfDay)
                ON    (Target.CollectionId = Source.CollectionId AND Target.DayOfWeek = Source.DayOfWeek AND Target.TimeOfDay = Source.TimeOfDay)
                WHEN MATCHED THEN
                    UPDATE
                    SET        ");
            sql.AppendUpdateParameters(GetFieldNames(fields));
            sql.Append(
                @" WHEN NOT MATCHED THEN
                    INSERT  (CollectionId, DayOfWeek, TimeOfDay)
                    VALUES  (Source.CollectionId, Source.DayOfWeek, Source.TimeOfDay);");
            return sql.ToString();
        }

        public static string UpdateStatement(WeeklyReleaseTime.Fields fields)
        {
            var sql = new System.Text.StringBuilder();
            sql.Append("UPDATE WeeklyReleaseTimes SET ");
            sql.AppendUpdateParameters(GetFieldNames(fields));
            sql.Append(" WHERE CollectionId = @CollectionId AND DayOfWeek = @DayOfWeek AND TimeOfDay = @TimeOfDay");
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
				fieldNames.Add("DayOfWeek");
			}

			if (autoIncludePrimaryKeys)
			{
				fieldNames.Add("TimeOfDay");
			}

            return fieldNames;
        }

        private static object MaskParameters(WeeklyReleaseTime entity, WeeklyReleaseTime.Fields fields, bool excludeSpecified)
        {
            var parameters = new Dapper.DynamicParameters();

			// Assume we never want to exclude primary key field(s) from our input.
			parameters.Add("CollectionId", entity.CollectionId);
			parameters.Add("DayOfWeek", entity.DayOfWeek);
			parameters.Add("TimeOfDay", entity.TimeOfDay);
            return parameters;
        }
    }
}
namespace Fifthweek.Api.Persistence.Identity
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Fifthweek.CodeGeneration;
    using Microsoft.AspNet.Identity.EntityFramework;
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

        public static System.Threading.Tasks.Task InsertAsync(
            this System.Data.Common.DbConnection connection,
            FifthweekUser entity,
            string selectValuesForInsertStatement,
            FifthweekUser.Fields selectedFields,
            bool idempotent = true,
            System.Data.IDbTransaction transaction = null)
        {
            return Dapper.SqlMapper.ExecuteAsync(
                connection,
                selectValuesForInsertStatement + System.Environment.NewLine + InsertStatement(idempotent),
                MaskParameters(entity, selectedFields, true),
                transaction);
        }

        public static System.Threading.Tasks.Task UpsertAsync(
            this System.Data.Common.DbConnection connection, 
            FifthweekUser entity, 
            FifthweekUser.Fields fields,
            System.Data.IDbTransaction transaction = null)
        {
            return Dapper.SqlMapper.ExecuteAsync(
                connection, 
                UpsertStatement(fields), 
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
            return Dapper.SqlMapper.ExecuteAsync(connection, UpdateStatement(fields), MaskParameters(entity, fields, false), transaction);
        }

        public static string InsertStatement(bool idempotent = true)
        {
            const string insert = "INSERT INTO AspNetUsers(ExampleWork, RegistrationDate, LastSignInDate, LastAccessTokenDate, ProfileImageFileId, Id, AccessFailedCount, Email, EmailConfirmed, LockoutEnabled, LockoutEndDateUtc, PasswordHash, PhoneNumber, PhoneNumberConfirmed, SecurityStamp, TwoFactorEnabled, UserName) VALUES(@ExampleWork, @RegistrationDate, @LastSignInDate, @LastAccessTokenDate, @ProfileImageFileId, @Id, @AccessFailedCount, @Email, @EmailConfirmed, @LockoutEnabled, @LockoutEndDateUtc, @PasswordHash, @PhoneNumber, @PhoneNumberConfirmed, @SecurityStamp, @TwoFactorEnabled, @UserName)";
            return idempotent ? SqlStatementTemplates.IdempotentInsert(insert) : insert;
        }

        public static string UpsertStatement(FifthweekUser.Fields fields)
        {
            var sql = new System.Text.StringBuilder();
            sql.Append(
                @"MERGE AspNetUsers as Target
                USING (VALUES (@ExampleWork, @RegistrationDate, @LastSignInDate, @LastAccessTokenDate, @ProfileImageFileId, @Id, @AccessFailedCount, @Email, @EmailConfirmed, @LockoutEnabled, @LockoutEndDateUtc, @PasswordHash, @PhoneNumber, @PhoneNumberConfirmed, @SecurityStamp, @TwoFactorEnabled, @UserName)) AS Source (ExampleWork, RegistrationDate, LastSignInDate, LastAccessTokenDate, ProfileImageFileId, Id, AccessFailedCount, Email, EmailConfirmed, LockoutEnabled, LockoutEndDateUtc, PasswordHash, PhoneNumber, PhoneNumberConfirmed, SecurityStamp, TwoFactorEnabled, UserName)
                ON    (Target.Id = Source.Id)
                WHEN MATCHED THEN
                    UPDATE
                    SET        ");
            sql.AppendUpdateParameters(GetFieldNames(fields));
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

        private static object MaskParameters(FifthweekUser entity, FifthweekUser.Fields fields, bool excludeSpecified)
        {
            var parameters = new Dapper.DynamicParameters();

			// Assume we never want to exclude primary key field(s) from our input.
            if(excludeSpecified != fields.HasFlag(FifthweekUser.Fields.ExampleWork))
            {
                parameters.Add("ExampleWork", entity.ExampleWork);
            }

            if(excludeSpecified != fields.HasFlag(FifthweekUser.Fields.RegistrationDate))
            {
                parameters.Add("RegistrationDate", entity.RegistrationDate);
            }

            if(excludeSpecified != fields.HasFlag(FifthweekUser.Fields.LastSignInDate))
            {
                parameters.Add("LastSignInDate", entity.LastSignInDate);
            }

            if(excludeSpecified != fields.HasFlag(FifthweekUser.Fields.LastAccessTokenDate))
            {
                parameters.Add("LastAccessTokenDate", entity.LastAccessTokenDate);
            }

            if(excludeSpecified != fields.HasFlag(FifthweekUser.Fields.ProfileImageFileId))
            {
                parameters.Add("ProfileImageFileId", entity.ProfileImageFileId);
            }

			parameters.Add("Id", entity.Id);
            if(excludeSpecified != fields.HasFlag(FifthweekUser.Fields.AccessFailedCount))
            {
                parameters.Add("AccessFailedCount", entity.AccessFailedCount);
            }

            if(excludeSpecified != fields.HasFlag(FifthweekUser.Fields.Email))
            {
                parameters.Add("Email", entity.Email);
            }

            if(excludeSpecified != fields.HasFlag(FifthweekUser.Fields.EmailConfirmed))
            {
                parameters.Add("EmailConfirmed", entity.EmailConfirmed);
            }

            if(excludeSpecified != fields.HasFlag(FifthweekUser.Fields.LockoutEnabled))
            {
                parameters.Add("LockoutEnabled", entity.LockoutEnabled);
            }

            if(excludeSpecified != fields.HasFlag(FifthweekUser.Fields.LockoutEndDateUtc))
            {
                parameters.Add("LockoutEndDateUtc", entity.LockoutEndDateUtc);
            }

            if(excludeSpecified != fields.HasFlag(FifthweekUser.Fields.PasswordHash))
            {
                parameters.Add("PasswordHash", entity.PasswordHash);
            }

            if(excludeSpecified != fields.HasFlag(FifthweekUser.Fields.PhoneNumber))
            {
                parameters.Add("PhoneNumber", entity.PhoneNumber);
            }

            if(excludeSpecified != fields.HasFlag(FifthweekUser.Fields.PhoneNumberConfirmed))
            {
                parameters.Add("PhoneNumberConfirmed", entity.PhoneNumberConfirmed);
            }

            if(excludeSpecified != fields.HasFlag(FifthweekUser.Fields.SecurityStamp))
            {
                parameters.Add("SecurityStamp", entity.SecurityStamp);
            }

            if(excludeSpecified != fields.HasFlag(FifthweekUser.Fields.TwoFactorEnabled))
            {
                parameters.Add("TwoFactorEnabled", entity.TwoFactorEnabled);
            }

            if(excludeSpecified != fields.HasFlag(FifthweekUser.Fields.UserName))
            {
                parameters.Add("UserName", entity.UserName);
            }

            return parameters;
        }
    }
}

