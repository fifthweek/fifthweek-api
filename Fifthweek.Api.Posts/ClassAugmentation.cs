using System;
using System.Linq;



namespace Fifthweek.Api.Posts.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Subscriptions;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence;
    public partial class PostFileCommand 
    {
        public PostFileCommand(
            Fifthweek.Api.Identity.Membership.UserId requester, 
            Fifthweek.Api.Posts.PostId newPostId, 
            Fifthweek.Api.Subscriptions.CollectionId collectionId, 
            Fifthweek.Api.FileManagement.FileId fileId, 
            Fifthweek.Api.Posts.ValidComment comment, 
            System.Nullable<System.DateTime> scheduledPostDate, 
            System.Boolean isQueued)
        {
            if (requester == null)
            {
                throw new ArgumentNullException("requester");
            }

            if (newPostId == null)
            {
                throw new ArgumentNullException("newPostId");
            }

            if (collectionId == null)
            {
                throw new ArgumentNullException("collectionId");
            }

            if (fileId == null)
            {
                throw new ArgumentNullException("fileId");
            }

            if (isQueued == null)
            {
                throw new ArgumentNullException("isQueued");
            }

            this.Requester = requester;
            this.NewPostId = newPostId;
            this.CollectionId = collectionId;
            this.FileId = fileId;
            this.Comment = comment;
            this.ScheduledPostDate = scheduledPostDate;
            this.IsQueued = isQueued;
        }
    }

}
namespace Fifthweek.Api.Posts.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Subscriptions;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence;
    public partial class PostImageCommand 
    {
        public PostImageCommand(
            Fifthweek.Api.Identity.Membership.UserId requester, 
            Fifthweek.Api.Posts.PostId newPostId, 
            Fifthweek.Api.Subscriptions.CollectionId collectionId, 
            Fifthweek.Api.FileManagement.FileId imageFileId, 
            Fifthweek.Api.Posts.ValidComment comment, 
            System.Nullable<System.DateTime> scheduledPostDate, 
            System.Boolean isQueued)
        {
            if (requester == null)
            {
                throw new ArgumentNullException("requester");
            }

            if (newPostId == null)
            {
                throw new ArgumentNullException("newPostId");
            }

            if (collectionId == null)
            {
                throw new ArgumentNullException("collectionId");
            }

            if (imageFileId == null)
            {
                throw new ArgumentNullException("imageFileId");
            }

            if (isQueued == null)
            {
                throw new ArgumentNullException("isQueued");
            }

            this.Requester = requester;
            this.NewPostId = newPostId;
            this.CollectionId = collectionId;
            this.ImageFileId = imageFileId;
            this.Comment = comment;
            this.ScheduledPostDate = scheduledPostDate;
            this.IsQueued = isQueued;
        }
    }

}
namespace Fifthweek.Api.Posts.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Subscriptions;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence;
    public partial class PostNoteCommand 
    {
        public PostNoteCommand(
            Fifthweek.Api.Identity.Membership.UserId requester, 
            Fifthweek.Api.Posts.PostId newPostId, 
            Fifthweek.Api.Subscriptions.ChannelId channelId, 
            Fifthweek.Api.Posts.ValidNote note, 
            System.Nullable<System.DateTime> scheduledPostDate)
        {
            if (requester == null)
            {
                throw new ArgumentNullException("requester");
            }

            if (newPostId == null)
            {
                throw new ArgumentNullException("newPostId");
            }

            if (channelId == null)
            {
                throw new ArgumentNullException("channelId");
            }

            if (note == null)
            {
                throw new ArgumentNullException("note");
            }

            this.Requester = requester;
            this.NewPostId = newPostId;
            this.ChannelId = channelId;
            this.Note = note;
            this.ScheduledPostDate = scheduledPostDate;
        }
    }

}
namespace Fifthweek.Api.Posts.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Subscriptions;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence;
    public partial class PostNoteCommandHandler 
    {
        public PostNoteCommandHandler(
            Fifthweek.Api.Subscriptions.IChannelSecurity channelSecurity, 
            Fifthweek.Api.Persistence.IFifthweekDbContext databaseContext)
        {
            if (channelSecurity == null)
            {
                throw new ArgumentNullException("channelSecurity");
            }

            if (databaseContext == null)
            {
                throw new ArgumentNullException("databaseContext");
            }

            this.channelSecurity = channelSecurity;
            this.databaseContext = databaseContext;
        }
    }

}
namespace Fifthweek.Api.Posts.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.OAuth;
    using Fifthweek.Api.Posts.Commands;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.Subscriptions;
    public partial class PostController 
    {
        public PostController(
            Fifthweek.Api.Core.ICommandHandler<Fifthweek.Api.Posts.Commands.PostNoteCommand> postNote, 
            Fifthweek.Api.Core.ICommandHandler<Fifthweek.Api.Posts.Commands.PostImageCommand> postImage, 
            Fifthweek.Api.Core.ICommandHandler<Fifthweek.Api.Posts.Commands.PostFileCommand> postFile, 
            Fifthweek.Api.Identity.OAuth.IUserContext userContext, 
            Fifthweek.Api.Core.IGuidCreator guidCreator)
        {
            if (postNote == null)
            {
                throw new ArgumentNullException("postNote");
            }

            if (postImage == null)
            {
                throw new ArgumentNullException("postImage");
            }

            if (postFile == null)
            {
                throw new ArgumentNullException("postFile");
            }

            if (userContext == null)
            {
                throw new ArgumentNullException("userContext");
            }

            if (guidCreator == null)
            {
                throw new ArgumentNullException("guidCreator");
            }

            this.postNote = postNote;
            this.postImage = postImage;
            this.postFile = postFile;
            this.userContext = userContext;
            this.guidCreator = guidCreator;
        }
    }

}
namespace Fifthweek.Api.Posts
{
    using System;
    using System.Linq;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Subscriptions;
    public partial class PostId 
    {
        public PostId(
            System.Guid value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            this.Value = value;
        }
    }

}
namespace Fifthweek.Api.Posts.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Subscriptions;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence;
    public partial class PostImageCommandHandler 
    {
        public PostImageCommandHandler(
            Fifthweek.Api.Subscriptions.ICollectionSecurity collectionSecurity, 
            Fifthweek.Api.FileManagement.IFileSecurity fileSecurity, 
            Fifthweek.Api.Posts.IPostToCollectionDbStatement postToCollectionDbStatement)
        {
            if (collectionSecurity == null)
            {
                throw new ArgumentNullException("collectionSecurity");
            }

            if (fileSecurity == null)
            {
                throw new ArgumentNullException("fileSecurity");
            }

            if (postToCollectionDbStatement == null)
            {
                throw new ArgumentNullException("postToCollectionDbStatement");
            }

            this.collectionSecurity = collectionSecurity;
            this.fileSecurity = fileSecurity;
            this.postToCollectionDbStatement = postToCollectionDbStatement;
        }
    }

}
namespace Fifthweek.Api.Posts
{
    using System;
    using System.Linq;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Subscriptions;
    public partial class PostToCollectionDbStatement 
    {
        public PostToCollectionDbStatement(
            Fifthweek.Api.Posts.IPostToCollectionDbSubStatements subStatements)
        {
            if (subStatements == null)
            {
                throw new ArgumentNullException("subStatements");
            }

            this.subStatements = subStatements;
        }
    }

}
namespace Fifthweek.Api.Posts.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Subscriptions;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence;
    public partial class PostFileCommandHandler 
    {
        public PostFileCommandHandler(
            Fifthweek.Api.Subscriptions.ICollectionSecurity collectionSecurity, 
            Fifthweek.Api.FileManagement.IFileSecurity fileSecurity, 
            Fifthweek.Api.Posts.IPostToCollectionDbStatement postToCollectionDbStatement)
        {
            if (collectionSecurity == null)
            {
                throw new ArgumentNullException("collectionSecurity");
            }

            if (fileSecurity == null)
            {
                throw new ArgumentNullException("fileSecurity");
            }

            if (postToCollectionDbStatement == null)
            {
                throw new ArgumentNullException("postToCollectionDbStatement");
            }

            this.collectionSecurity = collectionSecurity;
            this.fileSecurity = fileSecurity;
            this.postToCollectionDbStatement = postToCollectionDbStatement;
        }
    }

}
namespace Fifthweek.Api.Posts
{
    using System;
    using System.Linq;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Subscriptions;
    public partial class PostToCollectionDbSubStatements 
    {
        public PostToCollectionDbSubStatements(
            Fifthweek.Api.Persistence.IFifthweekDbContext databaseContext)
        {
            if (databaseContext == null)
            {
                throw new ArgumentNullException("databaseContext");
            }

            this.databaseContext = databaseContext;
        }
    }

}

namespace Fifthweek.Api.Posts.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Subscriptions;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence;
    public partial class PostFileCommand 
    {
        public override string ToString()
        {
            return string.Format("PostFileCommand({0}, {1}, {2}, {3}, {4}, {5}, {6})", this.Requester == null ? "null" : this.Requester.ToString(), this.NewPostId == null ? "null" : this.NewPostId.ToString(), this.CollectionId == null ? "null" : this.CollectionId.ToString(), this.FileId == null ? "null" : this.FileId.ToString(), this.Comment == null ? "null" : this.Comment.ToString(), this.ScheduledPostDate == null ? "null" : this.ScheduledPostDate.ToString(), this.IsQueued == null ? "null" : this.IsQueued.ToString());
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

            return this.Equals((PostFileCommand)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Requester != null ? this.Requester.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.NewPostId != null ? this.NewPostId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.CollectionId != null ? this.CollectionId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.FileId != null ? this.FileId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Comment != null ? this.Comment.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.ScheduledPostDate != null ? this.ScheduledPostDate.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.IsQueued != null ? this.IsQueued.GetHashCode() : 0);
                return hashCode;
            }
        }

        protected bool Equals(PostFileCommand other)
        {
            if (!object.Equals(this.Requester, other.Requester))
            {
                return false;
            }

            if (!object.Equals(this.NewPostId, other.NewPostId))
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

            if (!object.Equals(this.Comment, other.Comment))
            {
                return false;
            }

            if (!object.Equals(this.ScheduledPostDate, other.ScheduledPostDate))
            {
                return false;
            }

            if (!object.Equals(this.IsQueued, other.IsQueued))
            {
                return false;
            }

            return true;
        }
    }

}
namespace Fifthweek.Api.Posts.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Subscriptions;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence;
    public partial class PostImageCommand 
    {
        public override string ToString()
        {
            return string.Format("PostImageCommand({0}, {1}, {2}, {3}, {4}, {5}, {6})", this.Requester == null ? "null" : this.Requester.ToString(), this.NewPostId == null ? "null" : this.NewPostId.ToString(), this.CollectionId == null ? "null" : this.CollectionId.ToString(), this.ImageFileId == null ? "null" : this.ImageFileId.ToString(), this.Comment == null ? "null" : this.Comment.ToString(), this.ScheduledPostDate == null ? "null" : this.ScheduledPostDate.ToString(), this.IsQueued == null ? "null" : this.IsQueued.ToString());
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

            return this.Equals((PostImageCommand)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Requester != null ? this.Requester.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.NewPostId != null ? this.NewPostId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.CollectionId != null ? this.CollectionId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.ImageFileId != null ? this.ImageFileId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Comment != null ? this.Comment.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.ScheduledPostDate != null ? this.ScheduledPostDate.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.IsQueued != null ? this.IsQueued.GetHashCode() : 0);
                return hashCode;
            }
        }

        protected bool Equals(PostImageCommand other)
        {
            if (!object.Equals(this.Requester, other.Requester))
            {
                return false;
            }

            if (!object.Equals(this.NewPostId, other.NewPostId))
            {
                return false;
            }

            if (!object.Equals(this.CollectionId, other.CollectionId))
            {
                return false;
            }

            if (!object.Equals(this.ImageFileId, other.ImageFileId))
            {
                return false;
            }

            if (!object.Equals(this.Comment, other.Comment))
            {
                return false;
            }

            if (!object.Equals(this.ScheduledPostDate, other.ScheduledPostDate))
            {
                return false;
            }

            if (!object.Equals(this.IsQueued, other.IsQueued))
            {
                return false;
            }

            return true;
        }
    }

}
namespace Fifthweek.Api.Posts.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Subscriptions;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence;
    public partial class PostNoteCommand 
    {
        public override string ToString()
        {
            return string.Format("PostNoteCommand({0}, {1}, {2}, {3}, {4})", this.Requester == null ? "null" : this.Requester.ToString(), this.NewPostId == null ? "null" : this.NewPostId.ToString(), this.ChannelId == null ? "null" : this.ChannelId.ToString(), this.Note == null ? "null" : this.Note.ToString(), this.ScheduledPostDate == null ? "null" : this.ScheduledPostDate.ToString());
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

            return this.Equals((PostNoteCommand)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Requester != null ? this.Requester.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.NewPostId != null ? this.NewPostId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.ChannelId != null ? this.ChannelId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Note != null ? this.Note.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.ScheduledPostDate != null ? this.ScheduledPostDate.GetHashCode() : 0);
                return hashCode;
            }
        }

        protected bool Equals(PostNoteCommand other)
        {
            if (!object.Equals(this.Requester, other.Requester))
            {
                return false;
            }

            if (!object.Equals(this.NewPostId, other.NewPostId))
            {
                return false;
            }

            if (!object.Equals(this.ChannelId, other.ChannelId))
            {
                return false;
            }

            if (!object.Equals(this.Note, other.Note))
            {
                return false;
            }

            if (!object.Equals(this.ScheduledPostDate, other.ScheduledPostDate))
            {
                return false;
            }

            return true;
        }
    }

}
namespace Fifthweek.Api.Posts
{
    using System;
    using System.Linq;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Subscriptions;
    public partial class PostId 
    {
        public override string ToString()
        {
            return string.Format("PostId({0})", this.Value == null ? "null" : this.Value.ToString());
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

            return this.Equals((PostId)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Value != null ? this.Value.GetHashCode() : 0);
                return hashCode;
            }
        }

        protected bool Equals(PostId other)
        {
            if (!object.Equals(this.Value, other.Value))
            {
                return false;
            }

            return true;
        }
    }

}
namespace Fifthweek.Api.Posts.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.OAuth;
    using Fifthweek.Api.Posts.Commands;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.Subscriptions;
    public partial class NewFileData 
    {
        public override string ToString()
        {
            return string.Format("NewFileData({0}, {1}, {2}, \"{3}\", \"{4}\", \"{5}\", {6}, {7})", this.CollectionIdObject == null ? "null" : this.CollectionIdObject.ToString(), this.FileIdObject == null ? "null" : this.FileIdObject.ToString(), this.CommentObject == null ? "null" : this.CommentObject.ToString(), this.CollectionId == null ? "null" : this.CollectionId.ToString(), this.FileId == null ? "null" : this.FileId.ToString(), this.Comment == null ? "null" : this.Comment.ToString(), this.ScheduledPostDate == null ? "null" : this.ScheduledPostDate.ToString(), this.IsQueued == null ? "null" : this.IsQueued.ToString());
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

            return this.Equals((NewFileData)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.CollectionIdObject != null ? this.CollectionIdObject.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.FileIdObject != null ? this.FileIdObject.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.CommentObject != null ? this.CommentObject.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.CollectionId != null ? this.CollectionId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.FileId != null ? this.FileId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Comment != null ? this.Comment.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.ScheduledPostDate != null ? this.ScheduledPostDate.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.IsQueued != null ? this.IsQueued.GetHashCode() : 0);
                return hashCode;
            }
        }

        protected bool Equals(NewFileData other)
        {
            if (!object.Equals(this.CollectionIdObject, other.CollectionIdObject))
            {
                return false;
            }

            if (!object.Equals(this.FileIdObject, other.FileIdObject))
            {
                return false;
            }

            if (!object.Equals(this.CommentObject, other.CommentObject))
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

            if (!object.Equals(this.Comment, other.Comment))
            {
                return false;
            }

            if (!object.Equals(this.ScheduledPostDate, other.ScheduledPostDate))
            {
                return false;
            }

            if (!object.Equals(this.IsQueued, other.IsQueued))
            {
                return false;
            }

            return true;
        }
    }

}
namespace Fifthweek.Api.Posts.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.OAuth;
    using Fifthweek.Api.Posts.Commands;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.Subscriptions;
    public partial class NewImageData 
    {
        public override string ToString()
        {
            return string.Format("NewImageData({0}, {1}, {2}, \"{3}\", \"{4}\", \"{5}\", {6}, {7})", this.CollectionIdObject == null ? "null" : this.CollectionIdObject.ToString(), this.ImageFileIdObject == null ? "null" : this.ImageFileIdObject.ToString(), this.CommentObject == null ? "null" : this.CommentObject.ToString(), this.CollectionId == null ? "null" : this.CollectionId.ToString(), this.ImageFileId == null ? "null" : this.ImageFileId.ToString(), this.Comment == null ? "null" : this.Comment.ToString(), this.ScheduledPostDate == null ? "null" : this.ScheduledPostDate.ToString(), this.IsQueued == null ? "null" : this.IsQueued.ToString());
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

            return this.Equals((NewImageData)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.CollectionIdObject != null ? this.CollectionIdObject.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.ImageFileIdObject != null ? this.ImageFileIdObject.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.CommentObject != null ? this.CommentObject.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.CollectionId != null ? this.CollectionId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.ImageFileId != null ? this.ImageFileId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Comment != null ? this.Comment.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.ScheduledPostDate != null ? this.ScheduledPostDate.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.IsQueued != null ? this.IsQueued.GetHashCode() : 0);
                return hashCode;
            }
        }

        protected bool Equals(NewImageData other)
        {
            if (!object.Equals(this.CollectionIdObject, other.CollectionIdObject))
            {
                return false;
            }

            if (!object.Equals(this.ImageFileIdObject, other.ImageFileIdObject))
            {
                return false;
            }

            if (!object.Equals(this.CommentObject, other.CommentObject))
            {
                return false;
            }

            if (!object.Equals(this.CollectionId, other.CollectionId))
            {
                return false;
            }

            if (!object.Equals(this.ImageFileId, other.ImageFileId))
            {
                return false;
            }

            if (!object.Equals(this.Comment, other.Comment))
            {
                return false;
            }

            if (!object.Equals(this.ScheduledPostDate, other.ScheduledPostDate))
            {
                return false;
            }

            if (!object.Equals(this.IsQueued, other.IsQueued))
            {
                return false;
            }

            return true;
        }
    }

}
namespace Fifthweek.Api.Posts.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.OAuth;
    using Fifthweek.Api.Posts.Commands;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.Subscriptions;
    public partial class NewNoteData 
    {
        public override string ToString()
        {
            return string.Format("NewNoteData({0}, {1}, \"{2}\", \"{3}\", {4})", this.ChannelIdObject == null ? "null" : this.ChannelIdObject.ToString(), this.NoteObject == null ? "null" : this.NoteObject.ToString(), this.ChannelId == null ? "null" : this.ChannelId.ToString(), this.Note == null ? "null" : this.Note.ToString(), this.ScheduledPostDate == null ? "null" : this.ScheduledPostDate.ToString());
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

            return this.Equals((NewNoteData)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.ChannelIdObject != null ? this.ChannelIdObject.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.NoteObject != null ? this.NoteObject.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.ChannelId != null ? this.ChannelId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Note != null ? this.Note.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.ScheduledPostDate != null ? this.ScheduledPostDate.GetHashCode() : 0);
                return hashCode;
            }
        }

        protected bool Equals(NewNoteData other)
        {
            if (!object.Equals(this.ChannelIdObject, other.ChannelIdObject))
            {
                return false;
            }

            if (!object.Equals(this.NoteObject, other.NoteObject))
            {
                return false;
            }

            if (!object.Equals(this.ChannelId, other.ChannelId))
            {
                return false;
            }

            if (!object.Equals(this.Note, other.Note))
            {
                return false;
            }

            if (!object.Equals(this.ScheduledPostDate, other.ScheduledPostDate))
            {
                return false;
            }

            return true;
        }
    }

}
namespace Fifthweek.Api.Posts
{
    using System;
    using System.Linq;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Subscriptions;
    public partial class ValidNote 
    {
        public override string ToString()
        {
            return string.Format("ValidNote(\"{0}\")", this.Value == null ? "null" : this.Value.ToString());
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

            return this.Equals((ValidNote)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Value != null ? this.Value.GetHashCode() : 0);
                return hashCode;
            }
        }

        protected bool Equals(ValidNote other)
        {
            if (!object.Equals(this.Value, other.Value))
            {
                return false;
            }

            return true;
        }
    }

}
namespace Fifthweek.Api.Posts
{
    using System;
    using System.Linq;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Subscriptions;
    public partial class ValidComment 
    {
        public override string ToString()
        {
            return string.Format("ValidComment(\"{0}\")", this.Value == null ? "null" : this.Value.ToString());
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

            return this.Equals((ValidComment)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Value != null ? this.Value.GetHashCode() : 0);
                return hashCode;
            }
        }

        protected bool Equals(ValidComment other)
        {
            if (!object.Equals(this.Value, other.Value))
            {
                return false;
            }

            return true;
        }
    }

}

namespace Fifthweek.Api.Posts.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.OAuth;
    using Fifthweek.Api.Posts.Commands;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.Subscriptions;
    public partial class NewFileData 
    {
        public CollectionId CollectionIdObject { get; set; }
        public FileId FileIdObject { get; set; }
        public ValidComment CommentObject { get; set; }
    }

    public static partial class NewFileDataExtensions
    {
        public static void Parse(this NewFileData target)
        {
            var modelStateDictionary = new System.Web.Http.ModelBinding.ModelStateDictionary();

            if (target.CollectionId != null)
            {
                target.CollectionIdObject = new CollectionId(Fifthweek.Api.Core.Extensions.DecodeGuid(target.CollectionId));
            }
            else if (true)
            {
                var modelState = new System.Web.Http.ModelBinding.ModelState();
                modelState.Errors.Add("Value required");
                modelStateDictionary.Add("CollectionId", modelState);
            }

            if (target.FileId != null)
            {
                target.FileIdObject = new FileId(Fifthweek.Api.Core.Extensions.DecodeGuid(target.FileId));
            }
            else if (true)
            {
                var modelState = new System.Web.Http.ModelBinding.ModelState();
                modelState.Errors.Add("Value required");
                modelStateDictionary.Add("FileId", modelState);
            }

            if (false || !ValidComment.IsEmpty(target.Comment))
            {
                ValidComment @object;
                System.Collections.Generic.IReadOnlyCollection<string> errorMessages;
                if (ValidComment.TryParse(target.Comment, out @object, out errorMessages))
                {
                    target.CommentObject = @object;
                }
                else
                {
                    var modelState = new System.Web.Http.ModelBinding.ModelState();
                    foreach (var errorMessage in errorMessages)
                    {
                        modelState.Errors.Add(errorMessage);
                    }

                    modelStateDictionary.Add("Comment", modelState);
                }
            }

            if (!modelStateDictionary.IsValid)
            {
                throw new Fifthweek.Api.Core.ModelValidationException(modelStateDictionary);
            }
        }    
    }
}
namespace Fifthweek.Api.Posts.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.OAuth;
    using Fifthweek.Api.Posts.Commands;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.Subscriptions;
    public partial class NewImageData 
    {
        public CollectionId CollectionIdObject { get; set; }
        public FileId ImageFileIdObject { get; set; }
        public ValidComment CommentObject { get; set; }
    }

    public static partial class NewImageDataExtensions
    {
        public static void Parse(this NewImageData target)
        {
            var modelStateDictionary = new System.Web.Http.ModelBinding.ModelStateDictionary();

            if (target.CollectionId != null)
            {
                target.CollectionIdObject = new CollectionId(Fifthweek.Api.Core.Extensions.DecodeGuid(target.CollectionId));
            }
            else if (true)
            {
                var modelState = new System.Web.Http.ModelBinding.ModelState();
                modelState.Errors.Add("Value required");
                modelStateDictionary.Add("CollectionId", modelState);
            }

            if (target.ImageFileId != null)
            {
                target.ImageFileIdObject = new FileId(Fifthweek.Api.Core.Extensions.DecodeGuid(target.ImageFileId));
            }
            else if (true)
            {
                var modelState = new System.Web.Http.ModelBinding.ModelState();
                modelState.Errors.Add("Value required");
                modelStateDictionary.Add("ImageFileId", modelState);
            }

            if (false || !ValidComment.IsEmpty(target.Comment))
            {
                ValidComment @object;
                System.Collections.Generic.IReadOnlyCollection<string> errorMessages;
                if (ValidComment.TryParse(target.Comment, out @object, out errorMessages))
                {
                    target.CommentObject = @object;
                }
                else
                {
                    var modelState = new System.Web.Http.ModelBinding.ModelState();
                    foreach (var errorMessage in errorMessages)
                    {
                        modelState.Errors.Add(errorMessage);
                    }

                    modelStateDictionary.Add("Comment", modelState);
                }
            }

            if (!modelStateDictionary.IsValid)
            {
                throw new Fifthweek.Api.Core.ModelValidationException(modelStateDictionary);
            }
        }    
    }
}
namespace Fifthweek.Api.Posts.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.OAuth;
    using Fifthweek.Api.Posts.Commands;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.Subscriptions;
    public partial class NewNoteData 
    {
        public ChannelId ChannelIdObject { get; set; }
        public ValidNote NoteObject { get; set; }
    }

    public static partial class NewNoteDataExtensions
    {
        public static void Parse(this NewNoteData target)
        {
            var modelStateDictionary = new System.Web.Http.ModelBinding.ModelStateDictionary();

            if (target.ChannelId != null)
            {
                target.ChannelIdObject = new ChannelId(Fifthweek.Api.Core.Extensions.DecodeGuid(target.ChannelId));
            }
            else if (true)
            {
                var modelState = new System.Web.Http.ModelBinding.ModelState();
                modelState.Errors.Add("Value required");
                modelStateDictionary.Add("ChannelId", modelState);
            }

            if (true || !ValidNote.IsEmpty(target.Note))
            {
                ValidNote @object;
                System.Collections.Generic.IReadOnlyCollection<string> errorMessages;
                if (ValidNote.TryParse(target.Note, out @object, out errorMessages))
                {
                    target.NoteObject = @object;
                }
                else
                {
                    var modelState = new System.Web.Http.ModelBinding.ModelState();
                    foreach (var errorMessage in errorMessages)
                    {
                        modelState.Errors.Add(errorMessage);
                    }

                    modelStateDictionary.Add("Note", modelState);
                }
            }

            if (!modelStateDictionary.IsValid)
            {
                throw new Fifthweek.Api.Core.ModelValidationException(modelStateDictionary);
            }
        }    
    }
}

