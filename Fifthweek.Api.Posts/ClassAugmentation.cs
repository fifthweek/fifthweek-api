using System;
using System.Linq;



namespace Fifthweek.Api.Posts
{
    using System;
    using System.Linq;
    using Fifthweek.CodeGeneration;
    using Dapper;
    using System.Threading.Tasks;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Shared;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Collections;
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

		public class DapperTypeHandler : Dapper.SqlMapper.TypeHandler<Comment>, Fifthweek.Api.Persistence.IAutoRegisteredTypeHandler<Comment>
        {
            public override void SetValue(System.Data.IDbDataParameter parameter, Comment value)
            {
                parameter.DbType = System.Data.DbType.String;
                parameter.Value = value.Value;
            }

            public override Comment Parse(object value)
            {
                return new Comment((System.String)value);
            }
        }
    }

}
namespace Fifthweek.Api.Posts
{
    using System;
    using System.Linq;
    using Fifthweek.CodeGeneration;
    using Dapper;
    using System.Threading.Tasks;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Shared;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Collections;
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

		public class DapperTypeHandler : Dapper.SqlMapper.TypeHandler<PostId>, Fifthweek.Api.Persistence.IAutoRegisteredTypeHandler<PostId>
        {
            public override void SetValue(System.Data.IDbDataParameter parameter, PostId value)
            {
                parameter.DbType = System.Data.DbType.Guid;
                parameter.Value = value.Value;
            }

            public override PostId Parse(object value)
            {
                return new PostId((System.Guid)value);
            }
        }
    }

}

namespace Fifthweek.Api.Posts
{
    using System;
    using System.Linq;
    using Fifthweek.CodeGeneration;
    using Dapper;
    using System.Threading.Tasks;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Shared;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Collections;
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
namespace Fifthweek.Api.Posts
{
    using System;
    using System.Linq;
    using Fifthweek.CodeGeneration;
    using Dapper;
    using System.Threading.Tasks;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Shared;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Collections;
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
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.Azure;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.Collections;
    using Fifthweek.Api.Channels;
    using Fifthweek.Api.Persistence;
    using System.Collections.Generic;
    using System.Text;
    using Dapper;
    public partial class DeletePostCommand 
    {
        public DeletePostCommand(
            Fifthweek.Api.Posts.PostId postId, 
            Requester requester)
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
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.Azure;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.Collections;
    using Fifthweek.Api.Channels;
    using Fifthweek.Api.Persistence;
    using System.Collections.Generic;
    using System.Text;
    using Dapper;
    public partial class DeletePostCommandHandler 
    {
        public DeletePostCommandHandler(
            Fifthweek.Api.FileManagement.IScheduleGarbageCollectionStatement scheduleGarbageCollection, 
            Fifthweek.Api.Posts.IPostSecurity postSecurity, 
            IRequesterSecurity requesterSecurity, 
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
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.Azure;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.Collections;
    using Fifthweek.Api.Channels;
    using Fifthweek.Api.Persistence;
    using System.Collections.Generic;
    using System.Text;
    using Dapper;

    using CollectionId = Fifthweek.Api.Collections.Shared.CollectionId;
    using FileId = Fifthweek.Api.FileManagement.Shared.FileId;

    public partial class PostFileCommand 
    {
        public PostFileCommand(
            Requester requester, 
            Fifthweek.Api.Posts.PostId newPostId, 
            CollectionId collectionId, 
            FileId fileId, 
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

    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.Azure;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.Collections;
    using Fifthweek.Api.Channels;
    using Fifthweek.Api.Persistence;
    using System.Collections.Generic;
    using System.Text;
    using Dapper;
    public partial class PostFileCommandHandler 
    {
        public PostFileCommandHandler(
            ICollectionSecurity collectionSecurity, 
            IFileSecurity fileSecurity, 
            IRequesterSecurity requesterSecurity, 
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
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.Azure;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.Collections;
    using Fifthweek.Api.Channels;
    using Fifthweek.Api.Persistence;
    using System.Collections.Generic;
    using System.Text;
    using Dapper;

    using CollectionId = Fifthweek.Api.Collections.Shared.CollectionId;
    using FileId = Fifthweek.Api.FileManagement.Shared.FileId;

    public partial class PostImageCommand 
    {
        public PostImageCommand(
            Requester requester, 
            Fifthweek.Api.Posts.PostId newPostId, 
            CollectionId collectionId, 
            FileId imageFileId, 
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

    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.Azure;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.Collections;
    using Fifthweek.Api.Channels;
    using Fifthweek.Api.Persistence;
    using System.Collections.Generic;
    using System.Text;
    using Dapper;
    public partial class PostImageCommandHandler 
    {
        public PostImageCommandHandler(
            ICollectionSecurity collectionSecurity, 
            IFileSecurity fileSecurity, 
            IRequesterSecurity requesterSecurity, 
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
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.Azure;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.Collections;
    using Fifthweek.Api.Channels;
    using Fifthweek.Api.Persistence;
    using System.Collections.Generic;
    using System.Text;
    using Dapper;

    using ChannelId = Fifthweek.Api.Channels.Shared.ChannelId;

    public partial class PostNoteCommand 
    {
        public PostNoteCommand(
            Requester requester, 
            Fifthweek.Api.Posts.PostId newPostId, 
            ChannelId channelId, 
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

    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.Azure;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.Collections;
    using Fifthweek.Api.Channels;
    using Fifthweek.Api.Persistence;
    using System.Collections.Generic;
    using System.Text;
    using Dapper;
    public partial class PostNoteCommandHandler 
    {
        public PostNoteCommandHandler(
            IChannelSecurity channelSecurity, 
            IRequesterSecurity requesterSecurity, 
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
namespace Fifthweek.Api.Posts.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.Azure;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.Collections;
    using Fifthweek.Api.Channels;
    using Fifthweek.Api.Persistence;
    using System.Collections.Generic;
    using System.Text;
    using Dapper;

    using CollectionId = Fifthweek.Api.Collections.Shared.CollectionId;

    public partial class ReorderQueueCommand 
    {
        public ReorderQueueCommand(
            Requester requester, 
            CollectionId collectionId, 
            System.Collections.Generic.IReadOnlyList<Fifthweek.Api.Posts.PostId> newPartialQueueOrder)
        {
            if (requester == null)
            {
                throw new ArgumentNullException("requester");
            }

            if (collectionId == null)
            {
                throw new ArgumentNullException("collectionId");
            }

            if (newPartialQueueOrder == null)
            {
                throw new ArgumentNullException("newPartialQueueOrder");
            }

            this.Requester = requester;
            this.CollectionId = collectionId;
            this.NewPartialQueueOrder = newPartialQueueOrder;
        }
    }

}
namespace Fifthweek.Api.Posts.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.Azure;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.Collections;
    using Fifthweek.Api.Channels;
    using Fifthweek.Api.Persistence;
    using System.Collections.Generic;
    using System.Text;
    using Dapper;
    public partial class ReorderQueueCommandHandler 
    {
        public ReorderQueueCommandHandler(
            IRequesterSecurity requesterSecurity, 
            Fifthweek.Api.Persistence.IFifthweekDbContext databaseContext)
        {
            if (requesterSecurity == null)
            {
                throw new ArgumentNullException("requesterSecurity");
            }

            if (databaseContext == null)
            {
                throw new ArgumentNullException("databaseContext");
            }

            this.requesterSecurity = requesterSecurity;
            this.databaseContext = databaseContext;
        }
    }

}
namespace Fifthweek.Api.Posts.Controllers
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Fifthweek.Api.Collections;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Identity.OAuth;
    using Fifthweek.Api.Posts.Commands;
    using Fifthweek.Api.Posts.Queries;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using Fifthweek.Api.FileManagement;
    public partial class PostController 
    {
        public PostController(
            Fifthweek.Api.Core.ICommandHandler<Fifthweek.Api.Posts.Commands.PostNoteCommand> postNote, 
            Fifthweek.Api.Core.ICommandHandler<Fifthweek.Api.Posts.Commands.PostImageCommand> postImage, 
            Fifthweek.Api.Core.ICommandHandler<Fifthweek.Api.Posts.Commands.PostFileCommand> postFile, 
            Fifthweek.Api.Core.ICommandHandler<Fifthweek.Api.Posts.Commands.DeletePostCommand> deletePost, 
            Fifthweek.Api.Core.ICommandHandler<Fifthweek.Api.Posts.Commands.ReorderQueueCommand> reorderQueue, 
            Fifthweek.Api.Core.IQueryHandler<Fifthweek.Api.Posts.Queries.GetCreatorBacklogQuery,System.Collections.Generic.IReadOnlyList<Fifthweek.Api.Posts.Queries.BacklogPost>> getCreatorBacklog, 
            Fifthweek.Api.Core.IQueryHandler<Fifthweek.Api.Posts.Queries.GetCreatorNewsfeedQuery,System.Collections.Generic.IReadOnlyList<Fifthweek.Api.Posts.Queries.NewsfeedPost>> getCreatorNewsfeed, 
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

            if (reorderQueue == null)
            {
                throw new ArgumentNullException("reorderQueue");
            }

            if (getCreatorBacklog == null)
            {
                throw new ArgumentNullException("getCreatorBacklog");
            }

            if (getCreatorNewsfeed == null)
            {
                throw new ArgumentNullException("getCreatorNewsfeed");
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
            this.reorderQueue = reorderQueue;
            this.getCreatorBacklog = getCreatorBacklog;
            this.getCreatorNewsfeed = getCreatorNewsfeed;
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
    using Dapper;
    using System.Threading.Tasks;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Shared;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Collections;
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
    using Fifthweek.CodeGeneration;
    using Dapper;
    using System.Threading.Tasks;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Shared;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Collections;
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
    using Fifthweek.CodeGeneration;
    using Dapper;
    using System.Threading.Tasks;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Shared;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Collections;
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
    using Fifthweek.CodeGeneration;
    using Dapper;
    using System.Threading.Tasks;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Shared;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Collections;
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
    using Fifthweek.CodeGeneration;
    using Dapper;
    using System.Threading.Tasks;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Shared;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Collections;
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
    using Fifthweek.CodeGeneration;
    using Dapper;
    using System.Threading.Tasks;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Shared;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Collections;
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
    using System.Linq;
    using Fifthweek.Api.Channels;
    using Fifthweek.Api.Collections;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Shared;

    using ChannelId = Fifthweek.Api.Channels.Shared.ChannelId;
    using CollectionId = Fifthweek.Api.Collections.Shared.CollectionId;
    using FileId = Fifthweek.Api.FileManagement.Shared.FileId;

    public partial class BacklogPost 
    {
        public BacklogPost(
            Fifthweek.Api.Posts.PostId postId, 
            ChannelId channelId, 
            CollectionId collectionId, 
            Fifthweek.Api.Posts.Comment comment, 
            FileId fileId, 
            FileId imageId, 
            System.Boolean scheduledByQueue, 
            System.DateTime liveDate)
        {
            if (postId == null)
            {
                throw new ArgumentNullException("postId");
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

            this.PostId = postId;
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
    using System;
    using System.Linq;
    using Fifthweek.Api.Channels;
    using Fifthweek.Api.Collections;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Shared;

    using UserId = Fifthweek.Api.Identity.Shared.Membership.UserId;

    public partial class GetCreatorBacklogQuery 
    {
        public GetCreatorBacklogQuery(
            Requester requester, 
            UserId requestedUserId)
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
namespace Fifthweek.Api.Posts.Queries
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Channels;
    using Fifthweek.Api.Collections;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Shared;
    public partial class GetCreatorBacklogQueryHandler 
    {
        public GetCreatorBacklogQueryHandler(
            IRequesterSecurity requesterSecurity, 
            Fifthweek.Api.Persistence.IFifthweekDbContext databaseContext)
        {
            if (requesterSecurity == null)
            {
                throw new ArgumentNullException("requesterSecurity");
            }

            if (databaseContext == null)
            {
                throw new ArgumentNullException("databaseContext");
            }

            this.requesterSecurity = requesterSecurity;
            this.databaseContext = databaseContext;
        }
    }

}
namespace Fifthweek.Api.Posts.Queries
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Channels;
    using Fifthweek.Api.Collections;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Shared;

    using UserId = Fifthweek.Api.Identity.Shared.Membership.UserId;

    public partial class GetCreatorNewsfeedQuery 
    {
        public GetCreatorNewsfeedQuery(
            Requester requester, 
            UserId requestedUserId, 
            Fifthweek.Shared.NonNegativeInt startIndex, 
            Fifthweek.Shared.PositiveInt count)
        {
            if (requester == null)
            {
                throw new ArgumentNullException("requester");
            }

            if (requestedUserId == null)
            {
                throw new ArgumentNullException("requestedUserId");
            }

            if (startIndex == null)
            {
                throw new ArgumentNullException("startIndex");
            }

            if (count == null)
            {
                throw new ArgumentNullException("count");
            }

            this.Requester = requester;
            this.RequestedUserId = requestedUserId;
            this.StartIndex = startIndex;
            this.Count = count;
        }
    }

}
namespace Fifthweek.Api.Posts.Queries
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Channels;
    using Fifthweek.Api.Collections;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Shared;
    public partial class GetCreatorNewsfeedQueryHandler 
    {
        public GetCreatorNewsfeedQueryHandler(
            IRequesterSecurity requesterSecurity, 
            Fifthweek.Api.Persistence.IFifthweekDbContext databaseContext)
        {
            if (requesterSecurity == null)
            {
                throw new ArgumentNullException("requesterSecurity");
            }

            if (databaseContext == null)
            {
                throw new ArgumentNullException("databaseContext");
            }

            this.requesterSecurity = requesterSecurity;
            this.databaseContext = databaseContext;
        }
    }

}
namespace Fifthweek.Api.Posts.Queries
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Channels;
    using Fifthweek.Api.Collections;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Shared;

    using ChannelId = Fifthweek.Api.Channels.Shared.ChannelId;
    using CollectionId = Fifthweek.Api.Collections.Shared.CollectionId;
    using FileId = Fifthweek.Api.FileManagement.Shared.FileId;

    public partial class NewsfeedPost 
    {
        public NewsfeedPost(
            Fifthweek.Api.Posts.PostId postId, 
            ChannelId channelId, 
            CollectionId collectionId, 
            Fifthweek.Api.Posts.Comment comment, 
            FileId fileId, 
            FileId imageId, 
            System.DateTime liveDate)
        {
            if (postId == null)
            {
                throw new ArgumentNullException("postId");
            }

            if (channelId == null)
            {
                throw new ArgumentNullException("channelId");
            }

            if (liveDate == null)
            {
                throw new ArgumentNullException("liveDate");
            }

            this.PostId = postId;
            this.ChannelId = channelId;
            this.CollectionId = collectionId;
            this.Comment = comment;
            this.FileId = fileId;
            this.ImageId = imageId;
            this.LiveDate = liveDate;
        }
    }

}

namespace Fifthweek.Api.Posts
{
    using System;
    using System.Linq;
    using Fifthweek.CodeGeneration;
    using Dapper;
    using System.Threading.Tasks;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Shared;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Collections;
    public partial class Comment 
    {
        public override string ToString()
        {
            return string.Format("Comment(\"{0}\")", this.Value == null ? "null" : this.Value.ToString());
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

            return this.Equals((Comment)obj);
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

        protected bool Equals(Comment other)
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
    using Dapper;
    using System.Threading.Tasks;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Shared;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Collections;
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
    using Fifthweek.Api.Channels;
    using Fifthweek.Api.Persistence;
    using System.Collections.Generic;
    using System.Text;
    using Dapper;
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
    using Fifthweek.Api.Channels;
    using Fifthweek.Api.Persistence;
    using System.Collections.Generic;
    using System.Text;
    using Dapper;
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
    using Fifthweek.Api.Channels;
    using Fifthweek.Api.Persistence;
    using System.Collections.Generic;
    using System.Text;
    using Dapper;
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
    using Fifthweek.Api.Channels;
    using Fifthweek.Api.Persistence;
    using System.Collections.Generic;
    using System.Text;
    using Dapper;
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
    using Fifthweek.Api.Channels;
    using Fifthweek.Api.Persistence;
    using System.Collections.Generic;
    using System.Text;
    using Dapper;
    public partial class ReorderQueueCommand 
    {
        public override string ToString()
        {
            return string.Format("ReorderQueueCommand({0}, {1}, {2})", this.Requester == null ? "null" : this.Requester.ToString(), this.CollectionId == null ? "null" : this.CollectionId.ToString(), this.NewPartialQueueOrder == null ? "null" : this.NewPartialQueueOrder.ToString());
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

            return this.Equals((ReorderQueueCommand)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Requester != null ? this.Requester.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.CollectionId != null ? this.CollectionId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.NewPartialQueueOrder != null 
				    ? this.NewPartialQueueOrder.Aggregate(0, (previous, current) => 
				        { 
				            unchecked
				            {
				                return (previous * 397) ^ (current != null ? current.GetHashCode() : 0);
				            }
				        })
				    : 0);
                return hashCode;
            }
        }

        protected bool Equals(ReorderQueueCommand other)
        {
            if (!object.Equals(this.Requester, other.Requester))
            {
                return false;
            }

            if (!object.Equals(this.CollectionId, other.CollectionId))
            {
                return false;
            }

            if (this.NewPartialQueueOrder != null && other.NewPartialQueueOrder != null)
            {
                if (!this.NewPartialQueueOrder.SequenceEqual(other.NewPartialQueueOrder))
                {
                    return false;    
                }
            }
            else if (this.NewPartialQueueOrder != null || other.NewPartialQueueOrder != null)
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
    using System.Linq;
    using Fifthweek.Api.Channels;
    using Fifthweek.Api.Collections;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Shared;
    public partial class BacklogPost 
    {
        public override string ToString()
        {
            return string.Format("BacklogPost({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7})", this.PostId == null ? "null" : this.PostId.ToString(), this.ChannelId == null ? "null" : this.ChannelId.ToString(), this.CollectionId == null ? "null" : this.CollectionId.ToString(), this.Comment == null ? "null" : this.Comment.ToString(), this.FileId == null ? "null" : this.FileId.ToString(), this.ImageId == null ? "null" : this.ImageId.ToString(), this.ScheduledByQueue == null ? "null" : this.ScheduledByQueue.ToString(), this.LiveDate == null ? "null" : this.LiveDate.ToString());
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
                hashCode = (hashCode * 397) ^ (this.PostId != null ? this.PostId.GetHashCode() : 0);
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
            if (!object.Equals(this.PostId, other.PostId))
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
    using System;
    using System.Linq;
    using Fifthweek.Api.Channels;
    using Fifthweek.Api.Collections;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Shared;
    public partial class GetCreatorBacklogQuery 
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
namespace Fifthweek.Api.Posts.Queries
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Channels;
    using Fifthweek.Api.Collections;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Shared;
    public partial class GetCreatorNewsfeedQuery 
    {
        public override string ToString()
        {
            return string.Format("GetCreatorNewsfeedQuery({0}, {1}, {2}, {3})", this.Requester == null ? "null" : this.Requester.ToString(), this.RequestedUserId == null ? "null" : this.RequestedUserId.ToString(), this.StartIndex == null ? "null" : this.StartIndex.ToString(), this.Count == null ? "null" : this.Count.ToString());
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

            return this.Equals((GetCreatorNewsfeedQuery)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Requester != null ? this.Requester.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.RequestedUserId != null ? this.RequestedUserId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.StartIndex != null ? this.StartIndex.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Count != null ? this.Count.GetHashCode() : 0);
                return hashCode;
            }
        }

        protected bool Equals(GetCreatorNewsfeedQuery other)
        {
            if (!object.Equals(this.Requester, other.Requester))
            {
                return false;
            }

            if (!object.Equals(this.RequestedUserId, other.RequestedUserId))
            {
                return false;
            }

            if (!object.Equals(this.StartIndex, other.StartIndex))
            {
                return false;
            }

            if (!object.Equals(this.Count, other.Count))
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
    using System.Linq;
    using Fifthweek.Api.Channels;
    using Fifthweek.Api.Collections;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Shared;
    public partial class NewsfeedPost 
    {
        public override string ToString()
        {
            return string.Format("NewsfeedPost({0}, {1}, {2}, {3}, {4}, {5}, {6})", this.PostId == null ? "null" : this.PostId.ToString(), this.ChannelId == null ? "null" : this.ChannelId.ToString(), this.CollectionId == null ? "null" : this.CollectionId.ToString(), this.Comment == null ? "null" : this.Comment.ToString(), this.FileId == null ? "null" : this.FileId.ToString(), this.ImageId == null ? "null" : this.ImageId.ToString(), this.LiveDate == null ? "null" : this.LiveDate.ToString());
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

            return this.Equals((NewsfeedPost)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.PostId != null ? this.PostId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.ChannelId != null ? this.ChannelId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.CollectionId != null ? this.CollectionId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Comment != null ? this.Comment.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.FileId != null ? this.FileId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.ImageId != null ? this.ImageId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.LiveDate != null ? this.LiveDate.GetHashCode() : 0);
                return hashCode;
            }
        }

        protected bool Equals(NewsfeedPost other)
        {
            if (!object.Equals(this.PostId, other.PostId))
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

            if (!object.Equals(this.LiveDate, other.LiveDate))
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
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Fifthweek.Api.Collections;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Identity.OAuth;
    using Fifthweek.Api.Posts.Commands;
    using Fifthweek.Api.Posts.Queries;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using Fifthweek.Api.FileManagement;
    public partial class CreatorNewsfeedRequestData 
    {
        public override string ToString()
        {
            return string.Format("CreatorNewsfeedRequestData({0}, {1})", this.StartIndex == null ? "null" : this.StartIndex.ToString(), this.Count == null ? "null" : this.Count.ToString());
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

            return this.Equals((CreatorNewsfeedRequestData)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.StartIndex != null ? this.StartIndex.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Count != null ? this.Count.GetHashCode() : 0);
                return hashCode;
            }
        }

        protected bool Equals(CreatorNewsfeedRequestData other)
        {
            if (!object.Equals(this.StartIndex, other.StartIndex))
            {
                return false;
            }

            if (!object.Equals(this.Count, other.Count))
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
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Fifthweek.Api.Collections;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Identity.OAuth;
    using Fifthweek.Api.Posts.Commands;
    using Fifthweek.Api.Posts.Queries;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using Fifthweek.Api.FileManagement;
    public partial class NewFileData 
    {
        public override string ToString()
        {
            return string.Format("NewFileData({0}, {1}, \"{2}\", {3}, {4})", this.CollectionId == null ? "null" : this.CollectionId.ToString(), this.FileId == null ? "null" : this.FileId.ToString(), this.Comment == null ? "null" : this.Comment.ToString(), this.ScheduledPostDate == null ? "null" : this.ScheduledPostDate.ToString(), this.IsQueued == null ? "null" : this.IsQueued.ToString());
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
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Fifthweek.Api.Collections;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Identity.OAuth;
    using Fifthweek.Api.Posts.Commands;
    using Fifthweek.Api.Posts.Queries;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using Fifthweek.Api.FileManagement;
    public partial class NewImageData 
    {
        public override string ToString()
        {
            return string.Format("NewImageData({0}, {1}, \"{2}\", {3}, {4})", this.CollectionId == null ? "null" : this.CollectionId.ToString(), this.ImageFileId == null ? "null" : this.ImageFileId.ToString(), this.Comment == null ? "null" : this.Comment.ToString(), this.ScheduledPostDate == null ? "null" : this.ScheduledPostDate.ToString(), this.IsQueued == null ? "null" : this.IsQueued.ToString());
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
    using Fifthweek.Api.Channels;
    using Fifthweek.CodeGeneration;
    public partial class NewNoteData 
    {
        public override string ToString()
        {
            return string.Format("NewNoteData({0}, \"{1}\", {2})", this.ChannelId == null ? "null" : this.ChannelId.ToString(), this.Note == null ? "null" : this.Note.ToString(), this.ScheduledPostDate == null ? "null" : this.ScheduledPostDate.ToString());
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
                hashCode = (hashCode * 397) ^ (this.ChannelId != null ? this.ChannelId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Note != null ? this.Note.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.ScheduledPostDate != null ? this.ScheduledPostDate.GetHashCode() : 0);
                return hashCode;
            }
        }

        protected bool Equals(NewNoteData other)
        {
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
    using System.Collections.Generic;
    using System.Linq;
    using Fifthweek.CodeGeneration;
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
namespace Fifthweek.Api.Posts.Queries
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Channels;
    using Fifthweek.Api.Collections;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Shared;

    using ChannelId = Fifthweek.Api.Channels.Shared.ChannelId;
    using CollectionId = Fifthweek.Api.Collections.Shared.CollectionId;
    using FileId = Fifthweek.Api.FileManagement.Shared.FileId;

    public partial class BacklogPost 
    {
        public Builder ToBuilder()
        {
            var builder = new Builder();
            builder.PostId = this.PostId;
            builder.ChannelId = this.ChannelId;
            builder.CollectionId = this.CollectionId;
            builder.Comment = this.Comment;
            builder.FileId = this.FileId;
            builder.ImageId = this.ImageId;
            builder.ScheduledByQueue = this.ScheduledByQueue;
            builder.LiveDate = this.LiveDate;
            return builder;
        }

        public BacklogPost Copy(Action<Builder> applyDelta)
        {
            var builder = this.ToBuilder();
            applyDelta(builder);
            return builder.Build();
        }

        public partial class Builder
        {
            public Fifthweek.Api.Posts.PostId PostId { get; set; }
            public ChannelId ChannelId { get; set; }
            public CollectionId CollectionId { get; set; }
            public Fifthweek.Api.Posts.Comment Comment { get; set; }
            public FileId FileId { get; set; }
            public FileId ImageId { get; set; }
            public System.Boolean ScheduledByQueue { get; set; }
            public System.DateTime LiveDate { get; set; }

            public BacklogPost Build()
            {
                return new BacklogPost(
                    this.PostId, 
                    this.ChannelId, 
                    this.CollectionId, 
                    this.Comment, 
                    this.FileId, 
                    this.ImageId, 
                    this.ScheduledByQueue, 
                    this.LiveDate);
            }
        }
    }

}
namespace Fifthweek.Api.Posts.Queries
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Channels;
    using Fifthweek.Api.Collections;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Shared;

    using ChannelId = Fifthweek.Api.Channels.Shared.ChannelId;
    using CollectionId = Fifthweek.Api.Collections.Shared.CollectionId;
    using FileId = Fifthweek.Api.FileManagement.Shared.FileId;

    public partial class NewsfeedPost 
    {
        public Builder ToBuilder()
        {
            var builder = new Builder();
            builder.PostId = this.PostId;
            builder.ChannelId = this.ChannelId;
            builder.CollectionId = this.CollectionId;
            builder.Comment = this.Comment;
            builder.FileId = this.FileId;
            builder.ImageId = this.ImageId;
            builder.LiveDate = this.LiveDate;
            return builder;
        }

        public NewsfeedPost Copy(Action<Builder> applyDelta)
        {
            var builder = this.ToBuilder();
            applyDelta(builder);
            return builder.Build();
        }

        public partial class Builder
        {
            public Fifthweek.Api.Posts.PostId PostId { get; set; }
            public ChannelId ChannelId { get; set; }
            public CollectionId CollectionId { get; set; }
            public Fifthweek.Api.Posts.Comment Comment { get; set; }
            public FileId FileId { get; set; }
            public FileId ImageId { get; set; }
            public System.DateTime LiveDate { get; set; }

            public NewsfeedPost Build()
            {
                return new NewsfeedPost(
                    this.PostId, 
                    this.ChannelId, 
                    this.CollectionId, 
                    this.Comment, 
                    this.FileId, 
                    this.ImageId, 
                    this.LiveDate);
            }
        }
    }

}
namespace Fifthweek.Api.Posts.Controllers
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Fifthweek.Api.Collections;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Identity.OAuth;
    using Fifthweek.Api.Posts.Commands;
    using Fifthweek.Api.Posts.Queries;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using Fifthweek.Api.FileManagement;
    public partial class CreatorNewsfeedRequestData 
    {
		[Optional]
        public NonNegativeInt StartIndexObject { get; set; }
		[Optional]
        public PositiveInt CountObject { get; set; }
    }

    public static partial class CreatorNewsfeedRequestDataExtensions
    {
        public static void Parse(this CreatorNewsfeedRequestData target)
        {
            var modelStateDictionary = new System.Web.Http.ModelBinding.ModelStateDictionary();

            if (true || !NonNegativeInt.IsEmpty(target.StartIndex))
            {
                NonNegativeInt @object;
                System.Collections.Generic.IReadOnlyCollection<string> errorMessages;
                if (NonNegativeInt.TryParse(target.StartIndex, out @object, out errorMessages))
                {
                    target.StartIndexObject = @object;
                }
                else
                {
                    var modelState = new System.Web.Http.ModelBinding.ModelState();
                    foreach (var errorMessage in errorMessages)
                    {
                        modelState.Errors.Add(errorMessage);
                    }

                    modelStateDictionary.Add("StartIndex", modelState);
                }
            }

            if (true || !PositiveInt.IsEmpty(target.Count))
            {
                PositiveInt @object;
                System.Collections.Generic.IReadOnlyCollection<string> errorMessages;
                if (PositiveInt.TryParse(target.Count, out @object, out errorMessages))
                {
                    target.CountObject = @object;
                }
                else
                {
                    var modelState = new System.Web.Http.ModelBinding.ModelState();
                    foreach (var errorMessage in errorMessages)
                    {
                        modelState.Errors.Add(errorMessage);
                    }

                    modelStateDictionary.Add("Count", modelState);
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
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Fifthweek.Api.Collections;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Identity.OAuth;
    using Fifthweek.Api.Posts.Commands;
    using Fifthweek.Api.Posts.Queries;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using Fifthweek.Api.FileManagement;
    public partial class NewFileData 
    {
		[Optional]
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
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Fifthweek.Api.Collections;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Identity.OAuth;
    using Fifthweek.Api.Posts.Commands;
    using Fifthweek.Api.Posts.Queries;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using Fifthweek.Api.FileManagement;
    public partial class NewImageData 
    {
		[Optional]
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
    using Fifthweek.Api.Channels;
    using Fifthweek.CodeGeneration;
    public partial class NewNoteData 
    {
		[Optional]
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

