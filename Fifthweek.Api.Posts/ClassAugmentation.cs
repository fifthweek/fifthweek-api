using System;
using System.Linq;



namespace Fifthweek.Api.Posts
{
    using Fifthweek.CodeGeneration;
    [Newtonsoft.Json.JsonConverter(typeof(JsonConverter))]
    public partial class Comment 
    {
		public class JsonConverter : Newtonsoft.Json.JsonConverter
        {
            public override void WriteJson(Newtonsoft.Json.JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
            {
                var valueType = (Comment)value;
                serializer.Serialize(writer, valueType.Value);
            }

            public override object ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
            {
                if (objectType != typeof(Comment))
                {
                    throw new ArgumentException("Expected to deserialize JSON for type " + typeof(Comment).Name, "objectType");
                }

                var value = serializer.Deserialize<System.String>(reader);
                return new Comment(value);
            }

            public override bool CanConvert(Type objectType)
            {
                return objectType == typeof(Comment);
            }
        }
    }

}
namespace Fifthweek.Api.Posts
{
    using System;
    using Fifthweek.CodeGeneration;
    [Newtonsoft.Json.JsonConverter(typeof(JsonConverter))]
    public partial class PostId 
    {
		public class JsonConverter : Newtonsoft.Json.JsonConverter
        {
            public override void WriteJson(Newtonsoft.Json.JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
            {
                var valueType = (PostId)value;
                serializer.Serialize(writer, valueType.Value);
            }

            public override object ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
            {
                if (objectType != typeof(PostId))
                {
                    throw new ArgumentException("Expected to deserialize JSON for type " + typeof(PostId).Name, "objectType");
                }

                var value = serializer.Deserialize<System.Guid>(reader);
                return new PostId(value);
            }

            public override bool CanConvert(Type objectType)
            {
                return objectType == typeof(PostId);
            }
        }
    }

}

namespace Fifthweek.Api.Posts.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.Azure;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.Collections;
    using Fifthweek.Api.Subscriptions;
    using Fifthweek.Api.Persistence;
    public partial class DeletePostCommand 
    {
        public DeletePostCommand(
            Fifthweek.Api.Posts.PostId postId, 
            Fifthweek.Api.Identity.Membership.Requester requester)
        {
            if (postId == null)
            {
                throw new ArgumentNullException("postId");
            }

            if (requester == null)
            {
                throw new ArgumentNullException("requester");
            }

            this.PostId = postId;
            this.Requester = requester;
        }
    }

}
namespace Fifthweek.Api.Posts.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.Azure;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.Collections;
    using Fifthweek.Api.Subscriptions;
    using Fifthweek.Api.Persistence;
    public partial class DeletePostCommandHandler 
    {
        public DeletePostCommandHandler(
            Fifthweek.Api.FileManagement.IScheduleGarbageCollectionStatement scheduleGarbageCollection, 
            Fifthweek.Api.Posts.IPostSecurity postSecurity, 
            Fifthweek.Api.Identity.Membership.IRequesterSecurity requesterSecurity, 
            Fifthweek.Api.Posts.IDeletePostDbStatement deletePost)
        {
            if (scheduleGarbageCollection == null)
            {
                throw new ArgumentNullException("scheduleGarbageCollection");
            }

            if (postSecurity == null)
            {
                throw new ArgumentNullException("postSecurity");
            }

            if (requesterSecurity == null)
            {
                throw new ArgumentNullException("requesterSecurity");
            }

            if (deletePost == null)
            {
                throw new ArgumentNullException("deletePost");
            }

            this.scheduleGarbageCollection = scheduleGarbageCollection;
            this.postSecurity = postSecurity;
            this.requesterSecurity = requesterSecurity;
            this.deletePost = deletePost;
        }
    }

}
namespace Fifthweek.Api.Posts.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.Azure;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.Collections;
    using Fifthweek.Api.Subscriptions;
    using Fifthweek.Api.Persistence;
    public partial class PostFileCommand 
    {
        public PostFileCommand(
            Fifthweek.Api.Identity.Membership.Requester requester, 
            Fifthweek.Api.Posts.PostId newPostId, 
            Fifthweek.Api.Collections.CollectionId collectionId, 
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
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.Azure;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.Collections;
    using Fifthweek.Api.Subscriptions;
    using Fifthweek.Api.Persistence;
    public partial class PostFileCommandHandler 
    {
        public PostFileCommandHandler(
            Fifthweek.Api.Collections.ICollectionSecurity collectionSecurity, 
            Fifthweek.Api.FileManagement.IFileSecurity fileSecurity, 
            Fifthweek.Api.Identity.Membership.IRequesterSecurity requesterSecurity, 
            Fifthweek.Api.Posts.IPostFileTypeChecks postFileTypeChecks, 
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

            if (requesterSecurity == null)
            {
                throw new ArgumentNullException("requesterSecurity");
            }

            if (postFileTypeChecks == null)
            {
                throw new ArgumentNullException("postFileTypeChecks");
            }

            if (postToCollectionDbStatement == null)
            {
                throw new ArgumentNullException("postToCollectionDbStatement");
            }

            this.collectionSecurity = collectionSecurity;
            this.fileSecurity = fileSecurity;
            this.requesterSecurity = requesterSecurity;
            this.postFileTypeChecks = postFileTypeChecks;
            this.postToCollectionDbStatement = postToCollectionDbStatement;
        }
    }

}
namespace Fifthweek.Api.Posts.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.Azure;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.Collections;
    using Fifthweek.Api.Subscriptions;
    using Fifthweek.Api.Persistence;
    public partial class PostImageCommand 
    {
        public PostImageCommand(
            Fifthweek.Api.Identity.Membership.Requester requester, 
            Fifthweek.Api.Posts.PostId newPostId, 
            Fifthweek.Api.Collections.CollectionId collectionId, 
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
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.Azure;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.Collections;
    using Fifthweek.Api.Subscriptions;
    using Fifthweek.Api.Persistence;
    public partial class PostImageCommandHandler 
    {
        public PostImageCommandHandler(
            Fifthweek.Api.Collections.ICollectionSecurity collectionSecurity, 
            Fifthweek.Api.FileManagement.IFileSecurity fileSecurity, 
            Fifthweek.Api.Identity.Membership.IRequesterSecurity requesterSecurity, 
            Fifthweek.Api.Posts.IPostFileTypeChecks postFileTypeChecks, 
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

            if (requesterSecurity == null)
            {
                throw new ArgumentNullException("requesterSecurity");
            }

            if (postFileTypeChecks == null)
            {
                throw new ArgumentNullException("postFileTypeChecks");
            }

            if (postToCollectionDbStatement == null)
            {
                throw new ArgumentNullException("postToCollectionDbStatement");
            }

            this.collectionSecurity = collectionSecurity;
            this.fileSecurity = fileSecurity;
            this.requesterSecurity = requesterSecurity;
            this.postFileTypeChecks = postFileTypeChecks;
            this.postToCollectionDbStatement = postToCollectionDbStatement;
        }
    }

}
namespace Fifthweek.Api.Posts.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.Azure;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.Collections;
    using Fifthweek.Api.Subscriptions;
    using Fifthweek.Api.Persistence;
    public partial class PostNoteCommand 
    {
        public PostNoteCommand(
            Fifthweek.Api.Identity.Membership.Requester requester, 
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
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.Azure;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.Collections;
    using Fifthweek.Api.Subscriptions;
    using Fifthweek.Api.Persistence;
    public partial class PostNoteCommandHandler 
    {
        public PostNoteCommandHandler(
            Fifthweek.Api.Subscriptions.IChannelSecurity channelSecurity, 
            Fifthweek.Api.Identity.Membership.IRequesterSecurity requesterSecurity, 
            Fifthweek.Api.Persistence.IFifthweekDbContext databaseContext)
        {
            if (channelSecurity == null)
            {
                throw new ArgumentNullException("channelSecurity");
            }

            if (requesterSecurity == null)
            {
                throw new ArgumentNullException("requesterSecurity");
            }

            if (databaseContext == null)
            {
                throw new ArgumentNullException("databaseContext");
            }

            this.channelSecurity = channelSecurity;
            this.requesterSecurity = requesterSecurity;
            this.databaseContext = databaseContext;
        }
    }

}
namespace Fifthweek.Api.Posts
{
    using Fifthweek.CodeGeneration;
    public partial class Comment 
    {
        public Comment(
            System.String value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            this.Value = value;
        }
    }

}
namespace Fifthweek.Api.Posts.Controllers
{
<<<<<<< HEAD
    using System.Collections;
    using System.Collections.Generic;
=======
    using System;
>>>>>>> Implement RequesterSecurity and update all code to use it
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Identity.OAuth;
    using Fifthweek.Api.Posts.Commands;
    using Fifthweek.Api.Posts.Queries;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Collections;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.Subscriptions;
    public partial class PostController 
    {
        public PostController(
            Fifthweek.Api.Core.ICommandHandler<Fifthweek.Api.Posts.Commands.PostNoteCommand> postNote, 
            Fifthweek.Api.Core.ICommandHandler<Fifthweek.Api.Posts.Commands.PostImageCommand> postImage, 
            Fifthweek.Api.Core.ICommandHandler<Fifthweek.Api.Posts.Commands.PostFileCommand> postFile, 
            Fifthweek.Api.Core.ICommandHandler<Fifthweek.Api.Posts.Commands.DeletePostCommand> deletePost, 
            Fifthweek.Api.Core.IQueryHandler<Fifthweek.Api.Posts.Queries.GetCreatorBacklogQuery,System.Collections.Generic.IReadOnlyList<Fifthweek.Api.Posts.Queries.BacklogPost>> getCreatorBacklog, 
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

            if (deletePost == null)
            {
                throw new ArgumentNullException("deletePost");
            }

            if (getCreatorBacklog == null)
            {
                throw new ArgumentNullException("getCreatorBacklog");
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
            this.deletePost = deletePost;
            this.getCreatorBacklog = getCreatorBacklog;
            this.userContext = userContext;
            this.guidCreator = guidCreator;
        }
    }

}
namespace Fifthweek.Api.Posts
{
    using System;
    using System.Linq;
    using Dapper;
    using System.Threading.Tasks;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Shared;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Collections;
    using System.Collections.Generic;
    public partial class DeletePostDbStatement 
    {
        public DeletePostDbStatement(
            Fifthweek.Api.Persistence.IFifthweekDbContext fifthweekDbContext)
        {
            if (fifthweekDbContext == null)
            {
                throw new ArgumentNullException("fifthweekDbContext");
            }

            this.fifthweekDbContext = fifthweekDbContext;
        }
    }

}
namespace Fifthweek.Api.Posts
{
    using System;
    using System.Linq;
    using Dapper;
    using System.Threading.Tasks;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Shared;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Collections;
    using System.Collections.Generic;
    public partial class PostFileTypeChecks 
    {
        public PostFileTypeChecks(
            Fifthweek.Api.FileManagement.IGetFileExtensionDbStatement getFileExtension, 
            Fifthweek.Shared.IMimeTypeMap mimeTypeMap)
        {
            if (getFileExtension == null)
            {
                throw new ArgumentNullException("getFileExtension");
            }

            if (mimeTypeMap == null)
            {
                throw new ArgumentNullException("mimeTypeMap");
            }

            this.getFileExtension = getFileExtension;
            this.mimeTypeMap = mimeTypeMap;
        }
    }

}
namespace Fifthweek.Api.Posts
{
    using System;
    using System.Linq;
    using Dapper;
    using System.Threading.Tasks;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Shared;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Collections;
    using System.Collections.Generic;
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
namespace Fifthweek.Api.Posts
{
    using System;
    using System.Linq;
    using Dapper;
    using System.Threading.Tasks;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Shared;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Collections;
    using System.Collections.Generic;
    public partial class PostOwnership 
    {
        public PostOwnership(
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
namespace Fifthweek.Api.Posts
{
    using System;
    using System.Linq;
    using Dapper;
    using System.Threading.Tasks;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Shared;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Collections;
    using System.Collections.Generic;
    public partial class PostSecurity 
    {
        public PostSecurity(
            Fifthweek.Api.Posts.IPostOwnership postOwnership)
        {
            if (postOwnership == null)
            {
                throw new ArgumentNullException("postOwnership");
            }

            this.postOwnership = postOwnership;
        }
    }

}
namespace Fifthweek.Api.Posts
{
    using System;
    using System.Linq;
    using Dapper;
    using System.Threading.Tasks;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Shared;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Collections;
    using System.Collections.Generic;
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
namespace Fifthweek.Api.Posts
{
    using System;
    using System.Linq;
    using Dapper;
    using System.Threading.Tasks;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Shared;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Collections;
    using System.Collections.Generic;
    public partial class PostToCollectionDbSubStatements 
    {
        public PostToCollectionDbSubStatements(
            Fifthweek.Api.Persistence.IFifthweekDbContext databaseContext, 
            Fifthweek.Api.Collections.IGetLiveDateOfNewQueuedPostDbStatement getLiveDateOfNewQueuedPost)
        {
            if (databaseContext == null)
            {
                throw new ArgumentNullException("databaseContext");
            }

            if (getLiveDateOfNewQueuedPost == null)
            {
                throw new ArgumentNullException("getLiveDateOfNewQueuedPost");
            }

            this.databaseContext = databaseContext;
            this.getLiveDateOfNewQueuedPost = getLiveDateOfNewQueuedPost;
        }
    }

}
namespace Fifthweek.Api.Posts.Queries
{
    using System;
    using Fifthweek.Api.Collections;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.Subscriptions;
    using Fifthweek.CodeGeneration;
    public partial class BacklogPost 
    {
        public BacklogPost(
            Fifthweek.Api.Subscriptions.ChannelId channelId, 
            Fifthweek.Api.Collections.CollectionId collectionId, 
            Fifthweek.Api.Posts.Comment comment, 
            Fifthweek.Api.FileManagement.FileId fileId, 
            Fifthweek.Api.FileManagement.FileId imageId, 
            System.Boolean scheduledByQueue, 
            System.DateTime liveDate)
        {
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

            this.ChannelId = channelId;
            this.CollectionId = collectionId;
            this.Comment = comment;
            this.FileId = fileId;
            this.ImageId = imageId;
            this.ScheduledByQueue = scheduledByQueue;
            this.LiveDate = liveDate;
        }
    }

}
namespace Fifthweek.Api.Posts.Queries
{
    using System.Collections.Generic;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.CodeGeneration;
    public partial class GetCreatorBacklogQuery 
    {
        public GetCreatorBacklogQuery(
            Fifthweek.Api.Identity.Membership.Requester requester, 
            Fifthweek.Api.Identity.Membership.UserId requestedUserId)
        {
            if (requester == null)
            {
                throw new ArgumentNullException("requester");
            }

            if (requestedUserId == null)
            {
                throw new ArgumentNullException("requestedUserId");
            }

            this.Requester = requester;
            this.RequestedUserId = requestedUserId;
        }
    }

}

namespace Fifthweek.Api.Posts.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.Azure;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.Collections;
    using Fifthweek.Api.Subscriptions;
    using Fifthweek.Api.Persistence;
    public partial class DeletePostCommand 
    {
        public override string ToString()
        {
            return string.Format("DeletePostCommand({0}, {1})", this.PostId == null ? "null" : this.PostId.ToString(), this.Requester == null ? "null" : this.Requester.ToString());
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

            return this.Equals((DeletePostCommand)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.PostId != null ? this.PostId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Requester != null ? this.Requester.GetHashCode() : 0);
                return hashCode;
            }
        }

        protected bool Equals(DeletePostCommand other)
        {
            if (!object.Equals(this.PostId, other.PostId))
            {
                return false;
            }

            if (!object.Equals(this.Requester, other.Requester))
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
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.Azure;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.Collections;
    using Fifthweek.Api.Subscriptions;
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
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.Azure;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.Collections;
    using Fifthweek.Api.Subscriptions;
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
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.Azure;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.Collections;
    using Fifthweek.Api.Subscriptions;
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
<<<<<<< HEAD
    using Fifthweek.CodeGeneration;
    public partial class Comment 
    {
        public override string ToString()
        {
            return string.Format("Comment(\"{0}\")", this.Value == null ? "null" : this.Value.ToString());
=======
    using System;
    using System.Linq;
    using Dapper;
    using System.Threading.Tasks;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Shared;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Collections;
    using System.Collections.Generic;
    public partial class PostId 
    {
        public override string ToString()
        {
            return string.Format("PostId({0})", this.Value == null ? "null" : this.Value.ToString());
>>>>>>> Implement RequesterSecurity and update all code to use it
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

<<<<<<< HEAD
            return this.Equals((Comment)obj);
=======
            return this.Equals((PostId)obj);
>>>>>>> Implement RequesterSecurity and update all code to use it
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

<<<<<<< HEAD
        protected bool Equals(Comment other)
=======
        protected bool Equals(PostId other)
>>>>>>> Implement RequesterSecurity and update all code to use it
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
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Identity.OAuth;
    using Fifthweek.Api.Posts.Commands;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Collections;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.Subscriptions;
    public partial class NewFileData 
    {
        public override string ToString()
        {
<<<<<<< HEAD
            return string.Format("NewFileData({0}, {1}, \"{2}\", {3}, {4})", this.CollectionId == null ? "null" : this.CollectionId.ToString(), this.FileId == null ? "null" : this.FileId.ToString(), this.Comment == null ? "null" : this.Comment.ToString(), this.ScheduledPostDate == null ? "null" : this.ScheduledPostDate.ToString(), this.IsQueued == null ? "null" : this.IsQueued.ToString());
=======
            return string.Format("NewFileData({0}, {1}, {2}, \"{3}\", \"{4}\", \"{5}\", {6}, {7})", this.CollectionIdObject == null ? "null" : this.CollectionIdObject.ToString(), this.FileIdObject == null ? "null" : this.FileIdObject.ToString(), this.CommentObject == null ? "null" : this.CommentObject.ToString(), this.CollectionId == null ? "null" : this.CollectionId.ToString(), this.FileId == null ? "null" : this.FileId.ToString(), this.Comment == null ? "null" : this.Comment.ToString(), this.ScheduledPostDate == null ? "null" : this.ScheduledPostDate.ToString(), this.IsQueued == null ? "null" : this.IsQueued.ToString());
>>>>>>> Implement RequesterSecurity and update all code to use it
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
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Identity.OAuth;
    using Fifthweek.Api.Posts.Commands;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Collections;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.Subscriptions;
    public partial class NewImageData 
    {
        public override string ToString()
        {
<<<<<<< HEAD
            return string.Format("NewImageData({0}, {1}, \"{2}\", {3}, {4})", this.CollectionId == null ? "null" : this.CollectionId.ToString(), this.ImageFileId == null ? "null" : this.ImageFileId.ToString(), this.Comment == null ? "null" : this.Comment.ToString(), this.ScheduledPostDate == null ? "null" : this.ScheduledPostDate.ToString(), this.IsQueued == null ? "null" : this.IsQueued.ToString());
=======
            return string.Format("NewImageData({0}, {1}, {2}, \"{3}\", \"{4}\", \"{5}\", {6}, {7})", this.CollectionIdObject == null ? "null" : this.CollectionIdObject.ToString(), this.ImageFileIdObject == null ? "null" : this.ImageFileIdObject.ToString(), this.CommentObject == null ? "null" : this.CommentObject.ToString(), this.CollectionId == null ? "null" : this.CollectionId.ToString(), this.ImageFileId == null ? "null" : this.ImageFileId.ToString(), this.Comment == null ? "null" : this.Comment.ToString(), this.ScheduledPostDate == null ? "null" : this.ScheduledPostDate.ToString(), this.IsQueued == null ? "null" : this.IsQueued.ToString());
>>>>>>> Implement RequesterSecurity and update all code to use it
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
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Identity.OAuth;
    using Fifthweek.Api.Posts.Commands;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Collections;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.Subscriptions;
    public partial class NewNoteData 
    {
        public override string ToString()
        {
<<<<<<< HEAD
            return string.Format("NewNoteData({0}, \"{1}\", {2})", this.ChannelId == null ? "null" : this.ChannelId.ToString(), this.Note == null ? "null" : this.Note.ToString(), this.ScheduledPostDate == null ? "null" : this.ScheduledPostDate.ToString());
=======
            return string.Format("NewNoteData({0}, {1}, \"{2}\", \"{3}\", {4})", this.ChannelIdObject == null ? "null" : this.ChannelIdObject.ToString(), this.NoteObject == null ? "null" : this.NoteObject.ToString(), this.ChannelId == null ? "null" : this.ChannelId.ToString(), this.Note == null ? "null" : this.Note.ToString(), this.ScheduledPostDate == null ? "null" : this.ScheduledPostDate.ToString());
>>>>>>> Implement RequesterSecurity and update all code to use it
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
namespace Fifthweek.Api.Posts.Queries
{
    using System;
<<<<<<< HEAD
    using Fifthweek.Api.Collections;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.Subscriptions;
    using Fifthweek.CodeGeneration;
    public partial class BacklogPost 
    {
        public override string ToString()
        {
            return string.Format("BacklogPost({0}, {1}, {2}, {3}, {4}, {5}, {6})", this.ChannelId == null ? "null" : this.ChannelId.ToString(), this.CollectionId == null ? "null" : this.CollectionId.ToString(), this.Comment == null ? "null" : this.Comment.ToString(), this.FileId == null ? "null" : this.FileId.ToString(), this.ImageId == null ? "null" : this.ImageId.ToString(), this.ScheduledByQueue == null ? "null" : this.ScheduledByQueue.ToString(), this.LiveDate == null ? "null" : this.LiveDate.ToString());
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

            return this.Equals((BacklogPost)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.ChannelId != null ? this.ChannelId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.CollectionId != null ? this.CollectionId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Comment != null ? this.Comment.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.FileId != null ? this.FileId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.ImageId != null ? this.ImageId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.ScheduledByQueue != null ? this.ScheduledByQueue.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.LiveDate != null ? this.LiveDate.GetHashCode() : 0);
                return hashCode;
            }
        }

        protected bool Equals(BacklogPost other)
        {
            if (!object.Equals(this.ChannelId, other.ChannelId))
            {
                return false;
            }

            if (!object.Equals(this.CollectionId, other.CollectionId))
            {
                return false;
            }

            if (!object.Equals(this.Comment, other.Comment))
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

            if (!object.Equals(this.ScheduledByQueue, other.ScheduledByQueue))
            {
                return false;
            }

            if (!object.Equals(this.LiveDate, other.LiveDate))
            {
                return false;
            }

            return true;
        }
    }

}
namespace Fifthweek.Api.Posts.Queries
{
    using System.Collections.Generic;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.CodeGeneration;
    public partial class GetCreatorBacklogQuery 
=======
    using System.Linq;
    using Dapper;
    using System.Threading.Tasks;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Shared;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Collections;
    using System.Collections.Generic;
    public partial class ValidComment 
>>>>>>> Implement RequesterSecurity and update all code to use it
    {
        public override string ToString()
        {
            return string.Format("GetCreatorBacklogQuery({0}, {1})", this.Requester == null ? "null" : this.Requester.ToString(), this.RequestedUserId == null ? "null" : this.RequestedUserId.ToString());
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

            return this.Equals((GetCreatorBacklogQuery)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Requester != null ? this.Requester.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.RequestedUserId != null ? this.RequestedUserId.GetHashCode() : 0);
                return hashCode;
            }
        }

        protected bool Equals(GetCreatorBacklogQuery other)
        {
            if (!object.Equals(this.Requester, other.Requester))
            {
                return false;
            }

            if (!object.Equals(this.RequestedUserId, other.RequestedUserId))
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
    using Dapper;
    using System.Threading.Tasks;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Shared;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Collections;
    using System.Collections.Generic;
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
namespace Fifthweek.Api.Posts.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Identity.OAuth;
    using Fifthweek.Api.Posts.Commands;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Collections;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.Subscriptions;
    public partial class NewFileData 
    {
        public ValidComment CommentObject { get; set; }
    }

    public static partial class NewFileDataExtensions
    {
        public static void Parse(this NewFileData target)
        {
            var modelStateDictionary = new System.Web.Http.ModelBinding.ModelStateDictionary();

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
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Identity.OAuth;
    using Fifthweek.Api.Posts.Commands;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Collections;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.Subscriptions;
    public partial class NewImageData 
    {
        public ValidComment CommentObject { get; set; }
    }

    public static partial class NewImageDataExtensions
    {
        public static void Parse(this NewImageData target)
        {
            var modelStateDictionary = new System.Web.Http.ModelBinding.ModelStateDictionary();

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
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Identity.OAuth;
    using Fifthweek.Api.Posts.Commands;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Collections;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.Subscriptions;
    public partial class NewNoteData 
    {
        public ValidNote NoteObject { get; set; }
    }

    public static partial class NewNoteDataExtensions
    {
        public static void Parse(this NewNoteData target)
        {
            var modelStateDictionary = new System.Web.Http.ModelBinding.ModelStateDictionary();

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

