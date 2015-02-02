using System;
using System.Linq;

//// Generated on 02/02/2015 20:59:58 (UTC)
//// Mapped solution in 3.51s


namespace Fifthweek.Api.Posts.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Persistence;
    using System.Collections.Generic;
    using System.Text;
    using Dapper;

    public partial class DeletePostCommand 
    {
        public DeletePostCommand(
            Fifthweek.Api.Posts.Shared.PostId postId,
            Fifthweek.Api.Identity.Shared.Membership.Requester requester)
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
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Persistence;
    using System.Collections.Generic;
    using System.Text;
    using Dapper;

    public partial class DeletePostCommandHandler 
    {
        public DeletePostCommandHandler(
            Fifthweek.Api.FileManagement.Shared.IScheduleGarbageCollectionStatement scheduleGarbageCollection,
            Fifthweek.Api.Posts.Shared.IPostSecurity postSecurity,
            Fifthweek.Api.Identity.Shared.Membership.IRequesterSecurity requesterSecurity,
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
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Persistence;
    using System.Collections.Generic;
    using System.Text;
    using Dapper;

    public partial class PostFileCommand 
    {
        public PostFileCommand(
            Fifthweek.Api.Identity.Shared.Membership.Requester requester,
            Fifthweek.Api.Posts.Shared.PostId newPostId,
            Fifthweek.Api.Collections.Shared.CollectionId collectionId,
            Fifthweek.Api.FileManagement.Shared.FileId fileId,
            Fifthweek.Api.Posts.Shared.ValidComment comment,
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
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Persistence;
    using System.Collections.Generic;
    using System.Text;
    using Dapper;

    public partial class PostFileCommandHandler 
    {
        public PostFileCommandHandler(
            Fifthweek.Api.Collections.Shared.ICollectionSecurity collectionSecurity,
            Fifthweek.Api.FileManagement.Shared.IFileSecurity fileSecurity,
            Fifthweek.Api.Identity.Shared.Membership.IRequesterSecurity requesterSecurity,
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
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Persistence;
    using System.Collections.Generic;
    using System.Text;
    using Dapper;

    public partial class PostImageCommand 
    {
        public PostImageCommand(
            Fifthweek.Api.Identity.Shared.Membership.Requester requester,
            Fifthweek.Api.Posts.Shared.PostId newPostId,
            Fifthweek.Api.Collections.Shared.CollectionId collectionId,
            Fifthweek.Api.FileManagement.Shared.FileId imageFileId,
            Fifthweek.Api.Posts.Shared.ValidComment comment,
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
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Persistence;
    using System.Collections.Generic;
    using System.Text;
    using Dapper;

    public partial class PostImageCommandHandler 
    {
        public PostImageCommandHandler(
            Fifthweek.Api.Collections.Shared.ICollectionSecurity collectionSecurity,
            Fifthweek.Api.FileManagement.Shared.IFileSecurity fileSecurity,
            Fifthweek.Api.Identity.Shared.Membership.IRequesterSecurity requesterSecurity,
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
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Persistence;
    using System.Collections.Generic;
    using System.Text;
    using Dapper;

    public partial class PostNoteCommand 
    {
        public PostNoteCommand(
            Fifthweek.Api.Identity.Shared.Membership.Requester requester,
            Fifthweek.Api.Posts.Shared.PostId newPostId,
            Fifthweek.Api.Channels.Shared.ChannelId channelId,
            Fifthweek.Api.Posts.Shared.ValidNote note,
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
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Persistence;
    using System.Collections.Generic;
    using System.Text;
    using Dapper;

    public partial class PostNoteCommandHandler 
    {
        public PostNoteCommandHandler(
            Fifthweek.Api.Channels.Shared.IChannelSecurity channelSecurity,
            Fifthweek.Api.Identity.Shared.Membership.IRequesterSecurity requesterSecurity,
            Fifthweek.Api.Persistence.IFifthweekDbContext databaseContext,
            Fifthweek.Api.Posts.IScheduledDateClippingFunction scheduledDateClipping)
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

            if (scheduledDateClipping == null)
            {
                throw new ArgumentNullException("scheduledDateClipping");
            }

            this.channelSecurity = channelSecurity;
            this.requesterSecurity = requesterSecurity;
            this.databaseContext = databaseContext;
            this.scheduledDateClipping = scheduledDateClipping;
        }
    }
}
namespace Fifthweek.Api.Posts.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Persistence;
    using System.Collections.Generic;
    using System.Text;
    using Dapper;

    public partial class ReorderQueueCommand 
    {
        public ReorderQueueCommand(
            Fifthweek.Api.Identity.Shared.Membership.Requester requester,
            Fifthweek.Api.Collections.Shared.CollectionId collectionId,
            System.Collections.Generic.IReadOnlyList<Fifthweek.Api.Posts.Shared.PostId> newPartialQueueOrder)
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
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Persistence;
    using System.Collections.Generic;
    using System.Text;
    using Dapper;

    public partial class ReorderQueueCommandHandler 
    {
        public ReorderQueueCommandHandler(
            Fifthweek.Api.Identity.Shared.Membership.IRequesterSecurity requesterSecurity,
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
namespace Fifthweek.Api.Posts.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Persistence;
    using System.Collections.Generic;
    using System.Text;
    using Dapper;

    public partial class ReviseNoteCommand 
    {
        public ReviseNoteCommand(
            Fifthweek.Api.Identity.Shared.Membership.Requester requester,
            Fifthweek.Api.Posts.Shared.PostId postId,
            Fifthweek.Api.Channels.Shared.ChannelId channelId,
            Fifthweek.Api.Posts.Shared.ValidNote note,
            System.Nullable<System.DateTime> scheduledPostDate)
        {
            if (requester == null)
            {
                throw new ArgumentNullException("requester");
            }

            if (postId == null)
            {
                throw new ArgumentNullException("postId");
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
            this.PostId = postId;
            this.ChannelId = channelId;
            this.Note = note;
            this.ScheduledPostDate = scheduledPostDate;
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
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Commands;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Posts.Queries;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Shared;
    using Fifthweek.Api.FileManagement.Shared;

    public partial class FilePostController 
    {
        public FilePostController(
            Fifthweek.Api.Core.ICommandHandler<Fifthweek.Api.Posts.Commands.PostFileCommand> postFile,
            Fifthweek.Api.Core.ICommandHandler<Fifthweek.Api.Posts.Commands.ReviseFileCommand> reviseFile,
            Fifthweek.Api.Identity.Shared.Membership.IRequesterContext requesterContext,
            Fifthweek.Api.Core.IGuidCreator guidCreator)
        {
            if (postFile == null)
            {
                throw new ArgumentNullException("postFile");
            }

            if (reviseFile == null)
            {
                throw new ArgumentNullException("reviseFile");
            }

            if (requesterContext == null)
            {
                throw new ArgumentNullException("requesterContext");
            }

            if (guidCreator == null)
            {
                throw new ArgumentNullException("guidCreator");
            }

            this.postFile = postFile;
            this.reviseFile = reviseFile;
            this.requesterContext = requesterContext;
            this.guidCreator = guidCreator;
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
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Commands;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Posts.Queries;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Shared;
    using Fifthweek.Api.FileManagement.Shared;

    public partial class ImagePostController 
    {
        public ImagePostController(
            Fifthweek.Api.Core.ICommandHandler<Fifthweek.Api.Posts.Commands.PostImageCommand> postImage,
            Fifthweek.Api.Core.ICommandHandler<Fifthweek.Api.Posts.Commands.ReviseImageCommand> reviseImage,
            Fifthweek.Api.Identity.Shared.Membership.IRequesterContext requesterContext,
            Fifthweek.Api.Core.IGuidCreator guidCreator)
        {
            if (postImage == null)
            {
                throw new ArgumentNullException("postImage");
            }

            if (reviseImage == null)
            {
                throw new ArgumentNullException("reviseImage");
            }

            if (requesterContext == null)
            {
                throw new ArgumentNullException("requesterContext");
            }

            if (guidCreator == null)
            {
                throw new ArgumentNullException("guidCreator");
            }

            this.postImage = postImage;
            this.reviseImage = reviseImage;
            this.requesterContext = requesterContext;
            this.guidCreator = guidCreator;
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
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Commands;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Posts.Queries;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Shared;
    using Fifthweek.Api.FileManagement.Shared;

    public partial class NotePostController 
    {
        public NotePostController(
            Fifthweek.Api.Core.ICommandHandler<Fifthweek.Api.Posts.Commands.PostNoteCommand> postNote,
            Fifthweek.Api.Core.ICommandHandler<Fifthweek.Api.Posts.Commands.ReviseNoteCommand> reviseNote,
            Fifthweek.Api.Identity.Shared.Membership.IRequesterContext requesterContext,
            Fifthweek.Api.Core.IGuidCreator guidCreator)
        {
            if (postNote == null)
            {
                throw new ArgumentNullException("postNote");
            }

            if (reviseNote == null)
            {
                throw new ArgumentNullException("reviseNote");
            }

            if (requesterContext == null)
            {
                throw new ArgumentNullException("requesterContext");
            }

            if (guidCreator == null)
            {
                throw new ArgumentNullException("guidCreator");
            }

            this.postNote = postNote;
            this.reviseNote = reviseNote;
            this.requesterContext = requesterContext;
            this.guidCreator = guidCreator;
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
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Commands;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Posts.Queries;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Shared;
    using Fifthweek.Api.FileManagement.Shared;

    public partial class PostController 
    {
        public PostController(
            Fifthweek.Api.Core.ICommandHandler<Fifthweek.Api.Posts.Commands.DeletePostCommand> deletePost,
            Fifthweek.Api.Core.ICommandHandler<Fifthweek.Api.Posts.Commands.ReorderQueueCommand> reorderQueue,
            Fifthweek.Api.Core.IQueryHandler<Fifthweek.Api.Posts.Queries.GetCreatorBacklogQuery,System.Collections.Generic.IReadOnlyList<Fifthweek.Api.Posts.Queries.BacklogPost>> getCreatorBacklog,
            Fifthweek.Api.Core.IQueryHandler<Fifthweek.Api.Posts.Queries.GetCreatorNewsfeedQuery,System.Collections.Generic.IReadOnlyList<Fifthweek.Api.Posts.Queries.NewsfeedPost>> getCreatorNewsfeed,
            Fifthweek.Api.Identity.Shared.Membership.IRequesterContext requesterContext)
        {
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

            if (requesterContext == null)
            {
                throw new ArgumentNullException("requesterContext");
            }

            this.deletePost = deletePost;
            this.reorderQueue = reorderQueue;
            this.getCreatorBacklog = getCreatorBacklog;
            this.getCreatorNewsfeed = getCreatorNewsfeed;
            this.requesterContext = requesterContext;
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
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Commands;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Posts.Queries;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Shared;
    using Fifthweek.Api.FileManagement.Shared;

    public partial class RevisedNoteData 
    {
        public RevisedNoteData(
            Fifthweek.Api.Channels.Shared.ChannelId channelId,
            System.String note,
            System.Nullable<System.DateTime> scheduledPostDate)
        {
            if (channelId == null)
            {
                throw new ArgumentNullException("channelId");
            }

            if (note == null)
            {
                throw new ArgumentNullException("note");
            }

            this.ChannelId = channelId;
            this.Note = note;
            this.ScheduledPostDate = scheduledPostDate;
        }
    }
}
namespace Fifthweek.Api.Posts
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.Api.Collections.Shared;
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
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Collections;

    public partial class PostFileTypeChecks 
    {
        public PostFileTypeChecks(
            Fifthweek.Api.FileManagement.Shared.IGetFileExtensionDbStatement getFileExtension,
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
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.Api.Collections.Shared;
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
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.Api.Collections.Shared;
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
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.Api.Collections.Shared;
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
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Collections;

    public partial class PostToCollectionDbSubStatements 
    {
        public PostToCollectionDbSubStatements(
            Fifthweek.Api.Persistence.IFifthweekDbContext databaseContext,
            Fifthweek.Api.Collections.Shared.IGetLiveDateOfNewQueuedPostDbStatement getLiveDateOfNewQueuedPost,
            Fifthweek.Api.Posts.IScheduledDateClippingFunction scheduledDateClipping)
        {
            if (databaseContext == null)
            {
                throw new ArgumentNullException("databaseContext");
            }

            if (getLiveDateOfNewQueuedPost == null)
            {
                throw new ArgumentNullException("getLiveDateOfNewQueuedPost");
            }

            if (scheduledDateClipping == null)
            {
                throw new ArgumentNullException("scheduledDateClipping");
            }

            this.databaseContext = databaseContext;
            this.getLiveDateOfNewQueuedPost = getLiveDateOfNewQueuedPost;
            this.scheduledDateClipping = scheduledDateClipping;
        }
    }
}
namespace Fifthweek.Api.Posts.Queries
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Shared;

    public partial class BacklogPost 
    {
        public BacklogPost(
            Fifthweek.Api.Posts.Shared.PostId postId,
            Fifthweek.Api.Channels.Shared.ChannelId channelId,
            Fifthweek.Api.Collections.Shared.CollectionId collectionId,
            Fifthweek.Api.Posts.Shared.Comment comment,
            Fifthweek.Api.FileManagement.Shared.FileId fileId,
            Fifthweek.Api.FileManagement.Shared.FileId imageId,
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
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Shared;

    public partial class GetCreatorBacklogQuery 
    {
        public GetCreatorBacklogQuery(
            Fifthweek.Api.Identity.Shared.Membership.Requester requester,
            Fifthweek.Api.Identity.Shared.Membership.UserId requestedUserId)
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
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Shared;

    public partial class GetCreatorBacklogQueryHandler 
    {
        public GetCreatorBacklogQueryHandler(
            Fifthweek.Api.Identity.Shared.Membership.IRequesterSecurity requesterSecurity,
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
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Shared;

    public partial class GetCreatorNewsfeedQuery 
    {
        public GetCreatorNewsfeedQuery(
            Fifthweek.Api.Identity.Shared.Membership.Requester requester,
            Fifthweek.Api.Identity.Shared.Membership.UserId requestedUserId,
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
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Shared;

    public partial class GetCreatorNewsfeedQueryHandler 
    {
        public GetCreatorNewsfeedQueryHandler(
            Fifthweek.Api.Identity.Shared.Membership.IRequesterSecurity requesterSecurity,
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
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Shared;

    public partial class NewsfeedPost 
    {
        public NewsfeedPost(
            Fifthweek.Api.Posts.Shared.PostId postId,
            Fifthweek.Api.Channels.Shared.ChannelId channelId,
            Fifthweek.Api.Collections.Shared.CollectionId collectionId,
            Fifthweek.Api.Posts.Shared.Comment comment,
            Fifthweek.Api.FileManagement.Shared.FileId fileId,
            Fifthweek.Api.FileManagement.Shared.FileId imageId,
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
namespace Fifthweek.Api.Posts.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Commands;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Posts.Queries;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Shared;
    using Fifthweek.Api.FileManagement.Shared;

    public partial class NewFileData 
    {
        public NewFileData(
            Fifthweek.Api.Collections.Shared.CollectionId collectionId,
            Fifthweek.Api.FileManagement.Shared.FileId fileId,
            System.String comment,
            System.Nullable<System.DateTime> scheduledPostDate,
            System.Boolean isQueued)
        {
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

            this.CollectionId = collectionId;
            this.FileId = fileId;
            this.Comment = comment;
            this.ScheduledPostDate = scheduledPostDate;
            this.IsQueued = isQueued;
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
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Commands;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Posts.Queries;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Shared;
    using Fifthweek.Api.FileManagement.Shared;

    public partial class NewImageData 
    {
        public NewImageData(
            Fifthweek.Api.Collections.Shared.CollectionId collectionId,
            Fifthweek.Api.FileManagement.Shared.FileId imageFileId,
            System.String comment,
            System.Nullable<System.DateTime> scheduledPostDate,
            System.Boolean isQueued)
        {
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

            this.CollectionId = collectionId;
            this.ImageFileId = imageFileId;
            this.Comment = comment;
            this.ScheduledPostDate = scheduledPostDate;
            this.IsQueued = isQueued;
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
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Commands;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Posts.Queries;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Shared;
    using Fifthweek.Api.FileManagement.Shared;

    public partial class NewNoteData 
    {
        public NewNoteData(
            Fifthweek.Api.Channels.Shared.ChannelId channelId,
            System.String note,
            System.Nullable<System.DateTime> scheduledPostDate)
        {
            if (channelId == null)
            {
                throw new ArgumentNullException("channelId");
            }

            if (note == null)
            {
                throw new ArgumentNullException("note");
            }

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
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Persistence;
    using System.Collections.Generic;
    using System.Text;
    using Dapper;

    public partial class ReviseFileCommand 
    {
        public ReviseFileCommand(
            Fifthweek.Api.Identity.Shared.Membership.Requester requester,
            Fifthweek.Api.Posts.Shared.PostId postId,
            Fifthweek.Api.Collections.Shared.CollectionId collectionId,
            Fifthweek.Api.FileManagement.Shared.FileId fileId,
            Fifthweek.Api.Posts.Shared.ValidComment comment,
            System.Nullable<System.DateTime> scheduledPostDate,
            System.Boolean isQueued)
        {
            if (requester == null)
            {
                throw new ArgumentNullException("requester");
            }

            if (postId == null)
            {
                throw new ArgumentNullException("postId");
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
            this.PostId = postId;
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
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Persistence;
    using System.Collections.Generic;
    using System.Text;
    using Dapper;

    public partial class ReviseImageCommand 
    {
        public ReviseImageCommand(
            Fifthweek.Api.Identity.Shared.Membership.Requester requester,
            Fifthweek.Api.Posts.Shared.PostId postId,
            Fifthweek.Api.Collections.Shared.CollectionId collectionId,
            Fifthweek.Api.FileManagement.Shared.FileId imageFileId,
            Fifthweek.Api.Posts.Shared.ValidComment comment,
            System.Nullable<System.DateTime> scheduledPostDate,
            System.Boolean isQueued)
        {
            if (requester == null)
            {
                throw new ArgumentNullException("requester");
            }

            if (postId == null)
            {
                throw new ArgumentNullException("postId");
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
            this.PostId = postId;
            this.CollectionId = collectionId;
            this.ImageFileId = imageFileId;
            this.Comment = comment;
            this.ScheduledPostDate = scheduledPostDate;
            this.IsQueued = isQueued;
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
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Commands;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Posts.Queries;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Shared;
    using Fifthweek.Api.FileManagement.Shared;

    public partial class RevisedFileData 
    {
        public RevisedFileData(
            Fifthweek.Api.Collections.Shared.CollectionId collectionId,
            Fifthweek.Api.FileManagement.Shared.FileId fileId,
            System.String comment,
            System.Nullable<System.DateTime> scheduledPostDate,
            System.Boolean isQueued)
        {
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

            this.CollectionId = collectionId;
            this.FileId = fileId;
            this.Comment = comment;
            this.ScheduledPostDate = scheduledPostDate;
            this.IsQueued = isQueued;
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
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Commands;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Posts.Queries;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Shared;
    using Fifthweek.Api.FileManagement.Shared;

    public partial class RevisedImageData 
    {
        public RevisedImageData(
            Fifthweek.Api.Collections.Shared.CollectionId collectionId,
            Fifthweek.Api.FileManagement.Shared.FileId imageFileId,
            System.String comment,
            System.Nullable<System.DateTime> scheduledPostDate,
            System.Boolean isQueued)
        {
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
    using System.Threading.Tasks;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;

    public partial class ReviseNoteCommandHandler 
    {
        public ReviseNoteCommandHandler(
            Fifthweek.Api.Channels.Shared.IChannelSecurity channelSecurity,
            Fifthweek.Api.Posts.Shared.IPostSecurity postSecurity,
            Fifthweek.Api.Identity.Shared.Membership.IRequesterSecurity requesterSecurity,
            Fifthweek.Api.Persistence.IFifthweekDbContext databaseContext,
            Fifthweek.Api.Posts.IScheduledDateClippingFunction scheduledDateClipping)
        {
            if (channelSecurity == null)
            {
                throw new ArgumentNullException("channelSecurity");
            }

            if (postSecurity == null)
            {
                throw new ArgumentNullException("postSecurity");
            }

            if (requesterSecurity == null)
            {
                throw new ArgumentNullException("requesterSecurity");
            }

            if (databaseContext == null)
            {
                throw new ArgumentNullException("databaseContext");
            }

            if (scheduledDateClipping == null)
            {
                throw new ArgumentNullException("scheduledDateClipping");
            }

            this.channelSecurity = channelSecurity;
            this.postSecurity = postSecurity;
            this.requesterSecurity = requesterSecurity;
            this.databaseContext = databaseContext;
            this.scheduledDateClipping = scheduledDateClipping;
        }
    }
}

namespace Fifthweek.Api.Posts.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Channels.Shared;
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
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Channels.Shared;
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
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Channels.Shared;
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
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Channels.Shared;
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
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Channels.Shared;
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
namespace Fifthweek.Api.Posts.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Persistence;
    using System.Collections.Generic;
    using System.Text;
    using Dapper;

    public partial class ReviseNoteCommand 
    {
        public override string ToString()
        {
            return string.Format("ReviseNoteCommand({0}, {1}, {2}, {3}, {4})", this.Requester == null ? "null" : this.Requester.ToString(), this.PostId == null ? "null" : this.PostId.ToString(), this.ChannelId == null ? "null" : this.ChannelId.ToString(), this.Note == null ? "null" : this.Note.ToString(), this.ScheduledPostDate == null ? "null" : this.ScheduledPostDate.ToString());
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
        
            return this.Equals((ReviseNoteCommand)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Requester != null ? this.Requester.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.PostId != null ? this.PostId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.ChannelId != null ? this.ChannelId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Note != null ? this.Note.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.ScheduledPostDate != null ? this.ScheduledPostDate.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(ReviseNoteCommand other)
        {
            if (!object.Equals(this.Requester, other.Requester))
            {
                return false;
            }
        
            if (!object.Equals(this.PostId, other.PostId))
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
namespace Fifthweek.Api.Posts.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Commands;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Posts.Queries;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Shared;
    using Fifthweek.Api.FileManagement.Shared;

    public partial class RevisedNoteData 
    {
        public override string ToString()
        {
            return string.Format("RevisedNoteData({0}, \"{1}\", {2})", this.ChannelId == null ? "null" : this.ChannelId.ToString(), this.Note == null ? "null" : this.Note.ToString(), this.ScheduledPostDate == null ? "null" : this.ScheduledPostDate.ToString());
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
        
            return this.Equals((RevisedNoteData)obj);
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
        
        protected bool Equals(RevisedNoteData other)
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
namespace Fifthweek.Api.Posts.Queries
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
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
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
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
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
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
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
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
    using System.Threading.Tasks;
    using System.Web.Http;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Commands;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Posts.Queries;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Shared;
    using Fifthweek.Api.FileManagement.Shared;

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
    using System.Threading.Tasks;
    using System.Web.Http;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Commands;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Posts.Queries;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Shared;
    using Fifthweek.Api.FileManagement.Shared;

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
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Commands;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Posts.Queries;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Shared;
    using Fifthweek.Api.FileManagement.Shared;

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
namespace Fifthweek.Api.Posts.Commands
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Persistence;
    using System.Collections.Generic;
    using System.Text;
    using Dapper;

    public partial class ReviseFileCommand 
    {
        public override string ToString()
        {
            return string.Format("ReviseFileCommand({0}, {1}, {2}, {3}, {4}, {5}, {6})", this.Requester == null ? "null" : this.Requester.ToString(), this.PostId == null ? "null" : this.PostId.ToString(), this.CollectionId == null ? "null" : this.CollectionId.ToString(), this.FileId == null ? "null" : this.FileId.ToString(), this.Comment == null ? "null" : this.Comment.ToString(), this.ScheduledPostDate == null ? "null" : this.ScheduledPostDate.ToString(), this.IsQueued == null ? "null" : this.IsQueued.ToString());
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
        
            return this.Equals((ReviseFileCommand)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Requester != null ? this.Requester.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.PostId != null ? this.PostId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.CollectionId != null ? this.CollectionId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.FileId != null ? this.FileId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Comment != null ? this.Comment.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.ScheduledPostDate != null ? this.ScheduledPostDate.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.IsQueued != null ? this.IsQueued.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(ReviseFileCommand other)
        {
            if (!object.Equals(this.Requester, other.Requester))
            {
                return false;
            }
        
            if (!object.Equals(this.PostId, other.PostId))
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
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using System.Threading.Tasks;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Persistence;
    using System.Collections.Generic;
    using System.Text;
    using Dapper;

    public partial class ReviseImageCommand 
    {
        public override string ToString()
        {
            return string.Format("ReviseImageCommand({0}, {1}, {2}, {3}, {4}, {5}, {6})", this.Requester == null ? "null" : this.Requester.ToString(), this.PostId == null ? "null" : this.PostId.ToString(), this.CollectionId == null ? "null" : this.CollectionId.ToString(), this.ImageFileId == null ? "null" : this.ImageFileId.ToString(), this.Comment == null ? "null" : this.Comment.ToString(), this.ScheduledPostDate == null ? "null" : this.ScheduledPostDate.ToString(), this.IsQueued == null ? "null" : this.IsQueued.ToString());
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
        
            return this.Equals((ReviseImageCommand)obj);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 0;
                hashCode = (hashCode * 397) ^ (this.Requester != null ? this.Requester.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.PostId != null ? this.PostId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.CollectionId != null ? this.CollectionId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.ImageFileId != null ? this.ImageFileId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Comment != null ? this.Comment.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.ScheduledPostDate != null ? this.ScheduledPostDate.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.IsQueued != null ? this.IsQueued.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        protected bool Equals(ReviseImageCommand other)
        {
            if (!object.Equals(this.Requester, other.Requester))
            {
                return false;
            }
        
            if (!object.Equals(this.PostId, other.PostId))
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
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Commands;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Posts.Queries;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Shared;
    using Fifthweek.Api.FileManagement.Shared;

    public partial class RevisedFileData 
    {
        public override string ToString()
        {
            return string.Format("RevisedFileData({0}, {1}, \"{2}\", {3}, {4})", this.CollectionId == null ? "null" : this.CollectionId.ToString(), this.FileId == null ? "null" : this.FileId.ToString(), this.Comment == null ? "null" : this.Comment.ToString(), this.ScheduledPostDate == null ? "null" : this.ScheduledPostDate.ToString(), this.IsQueued == null ? "null" : this.IsQueued.ToString());
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
        
            return this.Equals((RevisedFileData)obj);
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
        
        protected bool Equals(RevisedFileData other)
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
    using System.Threading.Tasks;
    using System.Web.Http;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Commands;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Posts.Queries;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Shared;
    using Fifthweek.Api.FileManagement.Shared;

    public partial class RevisedImageData 
    {
        public override string ToString()
        {
            return string.Format("RevisedImageData({0}, {1}, \"{2}\", {3}, {4})", this.CollectionId == null ? "null" : this.CollectionId.ToString(), this.ImageFileId == null ? "null" : this.ImageFileId.ToString(), this.Comment == null ? "null" : this.Comment.ToString(), this.ScheduledPostDate == null ? "null" : this.ScheduledPostDate.ToString(), this.IsQueued == null ? "null" : this.IsQueued.ToString());
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
        
            return this.Equals((RevisedImageData)obj);
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
        
        protected bool Equals(RevisedImageData other)
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
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Commands;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Posts.Queries;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Shared;
    using Fifthweek.Api.FileManagement.Shared;

    public partial class CreatorNewsfeedPaginationData 
    {
        public override string ToString()
        {
            return string.Format("CreatorNewsfeedPaginationData({0}, {1})", this.StartIndex == null ? "null" : this.StartIndex.ToString(), this.Count == null ? "null" : this.Count.ToString());
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
        
            return this.Equals((CreatorNewsfeedPaginationData)obj);
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
        
        protected bool Equals(CreatorNewsfeedPaginationData other)
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
namespace Fifthweek.Api.Posts.Queries
{
    using System;
    using System.Linq;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Shared;

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
            public Fifthweek.Api.Posts.Shared.PostId PostId { get; set; }
            public Fifthweek.Api.Channels.Shared.ChannelId ChannelId { get; set; }
            public Fifthweek.Api.Collections.Shared.CollectionId CollectionId { get; set; }
            public Fifthweek.Api.Posts.Shared.Comment Comment { get; set; }
            public Fifthweek.Api.FileManagement.Shared.FileId FileId { get; set; }
            public Fifthweek.Api.FileManagement.Shared.FileId ImageId { get; set; }
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
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using System.Threading.Tasks;
    using Dapper;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Shared;

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
            public Fifthweek.Api.Posts.Shared.PostId PostId { get; set; }
            public Fifthweek.Api.Channels.Shared.ChannelId ChannelId { get; set; }
            public Fifthweek.Api.Collections.Shared.CollectionId CollectionId { get; set; }
            public Fifthweek.Api.Posts.Shared.Comment Comment { get; set; }
            public Fifthweek.Api.FileManagement.Shared.FileId FileId { get; set; }
            public Fifthweek.Api.FileManagement.Shared.FileId ImageId { get; set; }
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
    using System.Threading.Tasks;
    using System.Web.Http;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Commands;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Posts.Queries;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Shared;
    using Fifthweek.Api.FileManagement.Shared;

    public partial class RevisedNoteData 
    {
        public class Parsed
        {
            public Parsed(
                Fifthweek.Api.Channels.Shared.ChannelId channelId,
                ValidNote note,
                System.Nullable<System.DateTime> scheduledPostDate)
            {
                if (channelId == null)
                {
                    throw new ArgumentNullException("channelId");
                }

                if (note == null)
                {
                    throw new ArgumentNullException("note");
                }

                this.ChannelId = channelId;
                this.Note = note;
                this.ScheduledPostDate = scheduledPostDate;
            }
        
            public Fifthweek.Api.Channels.Shared.ChannelId ChannelId { get; private set; }
        
            public ValidNote Note { get; private set; }
        
            public System.Nullable<System.DateTime> ScheduledPostDate { get; private set; }
        }
    }

    public static partial class RevisedNoteDataExtensions
    {
        public static RevisedNoteData.Parsed Parse(this RevisedNoteData target)
        {
            var modelStateDictionary = new System.Web.Http.ModelBinding.ModelStateDictionary();
        
            ValidNote parsed0 = null;
            if (!ValidNote.IsEmpty(target.Note))
            {
                System.Collections.Generic.IReadOnlyCollection<string> parsed0Errors;
                if (!ValidNote.TryParse(target.Note, out parsed0, out parsed0Errors))
                {
                    var modelState = new System.Web.Http.ModelBinding.ModelState();
                    foreach (var errorMessage in parsed0Errors)
                    {
                        modelState.Errors.Add(errorMessage);
                    }

                    modelStateDictionary.Add("Note", modelState);
                }
            }
            else
            {
                var modelState = new System.Web.Http.ModelBinding.ModelState();
                modelState.Errors.Add("Value required");
                modelStateDictionary.Add("Note", modelState);
            }

            if (!modelStateDictionary.IsValid)
            {
                throw new Fifthweek.Api.Core.ModelValidationException(modelStateDictionary);
            }
        
            return new RevisedNoteData.Parsed(
                target.ChannelId,
                parsed0,
                target.ScheduledPostDate);
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
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Commands;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Posts.Queries;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Shared;
    using Fifthweek.Api.FileManagement.Shared;

    public partial class NewFileData 
    {
        public class Parsed
        {
            public Parsed(
                Fifthweek.Api.Collections.Shared.CollectionId collectionId,
                Fifthweek.Api.FileManagement.Shared.FileId fileId,
                ValidComment comment,
                System.Nullable<System.DateTime> scheduledPostDate,
                System.Boolean isQueued)
            {
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

                this.CollectionId = collectionId;
                this.FileId = fileId;
                this.Comment = comment;
                this.ScheduledPostDate = scheduledPostDate;
                this.IsQueued = isQueued;
            }
        
            public Fifthweek.Api.Collections.Shared.CollectionId CollectionId { get; private set; }
        
            public Fifthweek.Api.FileManagement.Shared.FileId FileId { get; private set; }
        
            public ValidComment Comment { get; private set; }
        
            public System.Nullable<System.DateTime> ScheduledPostDate { get; private set; }
        
            public System.Boolean IsQueued { get; private set; }
        }
    }

    public static partial class NewFileDataExtensions
    {
        public static NewFileData.Parsed Parse(this NewFileData target)
        {
            var modelStateDictionary = new System.Web.Http.ModelBinding.ModelStateDictionary();
        
            ValidComment parsed0 = null;
            if (!ValidComment.IsEmpty(target.Comment))
            {
                System.Collections.Generic.IReadOnlyCollection<string> parsed0Errors;
                if (!ValidComment.TryParse(target.Comment, out parsed0, out parsed0Errors))
                {
                    var modelState = new System.Web.Http.ModelBinding.ModelState();
                    foreach (var errorMessage in parsed0Errors)
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
        
            return new NewFileData.Parsed(
                target.CollectionId,
                target.FileId,
                parsed0,
                target.ScheduledPostDate,
                target.IsQueued);
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
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Commands;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Posts.Queries;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Shared;
    using Fifthweek.Api.FileManagement.Shared;

    public partial class NewImageData 
    {
        public class Parsed
        {
            public Parsed(
                Fifthweek.Api.Collections.Shared.CollectionId collectionId,
                Fifthweek.Api.FileManagement.Shared.FileId imageFileId,
                ValidComment comment,
                System.Nullable<System.DateTime> scheduledPostDate,
                System.Boolean isQueued)
            {
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

                this.CollectionId = collectionId;
                this.ImageFileId = imageFileId;
                this.Comment = comment;
                this.ScheduledPostDate = scheduledPostDate;
                this.IsQueued = isQueued;
            }
        
            public Fifthweek.Api.Collections.Shared.CollectionId CollectionId { get; private set; }
        
            public Fifthweek.Api.FileManagement.Shared.FileId ImageFileId { get; private set; }
        
            public ValidComment Comment { get; private set; }
        
            public System.Nullable<System.DateTime> ScheduledPostDate { get; private set; }
        
            public System.Boolean IsQueued { get; private set; }
        }
    }

    public static partial class NewImageDataExtensions
    {
        public static NewImageData.Parsed Parse(this NewImageData target)
        {
            var modelStateDictionary = new System.Web.Http.ModelBinding.ModelStateDictionary();
        
            ValidComment parsed0 = null;
            if (!ValidComment.IsEmpty(target.Comment))
            {
                System.Collections.Generic.IReadOnlyCollection<string> parsed0Errors;
                if (!ValidComment.TryParse(target.Comment, out parsed0, out parsed0Errors))
                {
                    var modelState = new System.Web.Http.ModelBinding.ModelState();
                    foreach (var errorMessage in parsed0Errors)
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
        
            return new NewImageData.Parsed(
                target.CollectionId,
                target.ImageFileId,
                parsed0,
                target.ScheduledPostDate,
                target.IsQueued);
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
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Commands;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Posts.Queries;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Shared;
    using Fifthweek.Api.FileManagement.Shared;

    public partial class NewNoteData 
    {
        public class Parsed
        {
            public Parsed(
                Fifthweek.Api.Channels.Shared.ChannelId channelId,
                ValidNote note,
                System.Nullable<System.DateTime> scheduledPostDate)
            {
                if (channelId == null)
                {
                    throw new ArgumentNullException("channelId");
                }

                if (note == null)
                {
                    throw new ArgumentNullException("note");
                }

                this.ChannelId = channelId;
                this.Note = note;
                this.ScheduledPostDate = scheduledPostDate;
            }
        
            public Fifthweek.Api.Channels.Shared.ChannelId ChannelId { get; private set; }
        
            public ValidNote Note { get; private set; }
        
            public System.Nullable<System.DateTime> ScheduledPostDate { get; private set; }
        }
    }

    public static partial class NewNoteDataExtensions
    {
        public static NewNoteData.Parsed Parse(this NewNoteData target)
        {
            var modelStateDictionary = new System.Web.Http.ModelBinding.ModelStateDictionary();
        
            ValidNote parsed0 = null;
            if (!ValidNote.IsEmpty(target.Note))
            {
                System.Collections.Generic.IReadOnlyCollection<string> parsed0Errors;
                if (!ValidNote.TryParse(target.Note, out parsed0, out parsed0Errors))
                {
                    var modelState = new System.Web.Http.ModelBinding.ModelState();
                    foreach (var errorMessage in parsed0Errors)
                    {
                        modelState.Errors.Add(errorMessage);
                    }

                    modelStateDictionary.Add("Note", modelState);
                }
            }
            else
            {
                var modelState = new System.Web.Http.ModelBinding.ModelState();
                modelState.Errors.Add("Value required");
                modelStateDictionary.Add("Note", modelState);
            }

            if (!modelStateDictionary.IsValid)
            {
                throw new Fifthweek.Api.Core.ModelValidationException(modelStateDictionary);
            }
        
            return new NewNoteData.Parsed(
                target.ChannelId,
                parsed0,
                target.ScheduledPostDate);
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
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Commands;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Posts.Queries;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Shared;
    using Fifthweek.Api.FileManagement.Shared;

    public partial class RevisedFileData 
    {
        public class Parsed
        {
            public Parsed(
                Fifthweek.Api.Collections.Shared.CollectionId collectionId,
                Fifthweek.Api.FileManagement.Shared.FileId fileId,
                ValidComment comment,
                System.Nullable<System.DateTime> scheduledPostDate,
                System.Boolean isQueued)
            {
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

                this.CollectionId = collectionId;
                this.FileId = fileId;
                this.Comment = comment;
                this.ScheduledPostDate = scheduledPostDate;
                this.IsQueued = isQueued;
            }
        
            public Fifthweek.Api.Collections.Shared.CollectionId CollectionId { get; private set; }
        
            public Fifthweek.Api.FileManagement.Shared.FileId FileId { get; private set; }
        
            public ValidComment Comment { get; private set; }
        
            public System.Nullable<System.DateTime> ScheduledPostDate { get; private set; }
        
            public System.Boolean IsQueued { get; private set; }
        }
    }

    public static partial class RevisedFileDataExtensions
    {
        public static RevisedFileData.Parsed Parse(this RevisedFileData target)
        {
            var modelStateDictionary = new System.Web.Http.ModelBinding.ModelStateDictionary();
        
            ValidComment parsed0 = null;
            if (!ValidComment.IsEmpty(target.Comment))
            {
                System.Collections.Generic.IReadOnlyCollection<string> parsed0Errors;
                if (!ValidComment.TryParse(target.Comment, out parsed0, out parsed0Errors))
                {
                    var modelState = new System.Web.Http.ModelBinding.ModelState();
                    foreach (var errorMessage in parsed0Errors)
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
        
            return new RevisedFileData.Parsed(
                target.CollectionId,
                target.FileId,
                parsed0,
                target.ScheduledPostDate,
                target.IsQueued);
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
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Commands;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Posts.Queries;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Shared;
    using Fifthweek.Api.FileManagement.Shared;

    public partial class RevisedImageData 
    {
        public class Parsed
        {
            public Parsed(
                Fifthweek.Api.Collections.Shared.CollectionId collectionId,
                Fifthweek.Api.FileManagement.Shared.FileId imageFileId,
                ValidComment comment,
                System.Nullable<System.DateTime> scheduledPostDate,
                System.Boolean isQueued)
            {
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

                this.CollectionId = collectionId;
                this.ImageFileId = imageFileId;
                this.Comment = comment;
                this.ScheduledPostDate = scheduledPostDate;
                this.IsQueued = isQueued;
            }
        
            public Fifthweek.Api.Collections.Shared.CollectionId CollectionId { get; private set; }
        
            public Fifthweek.Api.FileManagement.Shared.FileId ImageFileId { get; private set; }
        
            public ValidComment Comment { get; private set; }
        
            public System.Nullable<System.DateTime> ScheduledPostDate { get; private set; }
        
            public System.Boolean IsQueued { get; private set; }
        }
    }

    public static partial class RevisedImageDataExtensions
    {
        public static RevisedImageData.Parsed Parse(this RevisedImageData target)
        {
            var modelStateDictionary = new System.Web.Http.ModelBinding.ModelStateDictionary();
        
            ValidComment parsed0 = null;
            if (!ValidComment.IsEmpty(target.Comment))
            {
                System.Collections.Generic.IReadOnlyCollection<string> parsed0Errors;
                if (!ValidComment.TryParse(target.Comment, out parsed0, out parsed0Errors))
                {
                    var modelState = new System.Web.Http.ModelBinding.ModelState();
                    foreach (var errorMessage in parsed0Errors)
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
        
            return new RevisedImageData.Parsed(
                target.CollectionId,
                target.ImageFileId,
                parsed0,
                target.ScheduledPostDate,
                target.IsQueued);
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
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Commands;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using System.Collections.Generic;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Posts.Queries;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Shared;
    using Fifthweek.Api.FileManagement.Shared;

    public partial class CreatorNewsfeedPaginationData 
    {
        public class Parsed
        {
            public Parsed(
                NonNegativeInt startIndex,
                PositiveInt count)
            {
                if (startIndex == null)
                {
                    throw new ArgumentNullException("startIndex");
                }

                if (count == null)
                {
                    throw new ArgumentNullException("count");
                }

                this.StartIndex = startIndex;
                this.Count = count;
            }
        
            public NonNegativeInt StartIndex { get; private set; }
        
            public PositiveInt Count { get; private set; }
        }
    }

    public static partial class CreatorNewsfeedPaginationDataExtensions
    {
        public static CreatorNewsfeedPaginationData.Parsed Parse(this CreatorNewsfeedPaginationData target)
        {
            var modelStateDictionary = new System.Web.Http.ModelBinding.ModelStateDictionary();
        
            NonNegativeInt parsed0 = null;
            System.Collections.Generic.IReadOnlyCollection<string> parsed0Errors;
            if (!NonNegativeInt.TryParse(target.StartIndex, out parsed0, out parsed0Errors))
            {
                var modelState = new System.Web.Http.ModelBinding.ModelState();
                foreach (var errorMessage in parsed0Errors)
                {
                    modelState.Errors.Add(errorMessage);
                }

                modelStateDictionary.Add("StartIndex", modelState);
            }

            PositiveInt parsed1 = null;
            System.Collections.Generic.IReadOnlyCollection<string> parsed1Errors;
            if (!PositiveInt.TryParse(target.Count, out parsed1, out parsed1Errors))
            {
                var modelState = new System.Web.Http.ModelBinding.ModelState();
                foreach (var errorMessage in parsed1Errors)
                {
                    modelState.Errors.Add(errorMessage);
                }

                modelStateDictionary.Add("Count", modelState);
            }

            if (!modelStateDictionary.IsValid)
            {
                throw new Fifthweek.Api.Core.ModelValidationException(modelStateDictionary);
            }
        
            return new CreatorNewsfeedPaginationData.Parsed(
                parsed0,
                parsed1);
        }    
    }
}


